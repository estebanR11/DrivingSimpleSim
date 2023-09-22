using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI EnergyText;
    [SerializeField] AndroidNotificacionHandler androidNotificationHandler;
    [SerializeField] int maxEnergy;
    [SerializeField] int eneryRechargeMinutes;
    [SerializeField] Button playBut;
    int actualEnergy;

    private const string EnergyKey = "Energy";
    private const string EnergyReadyKey = "EnergyReady";

    private void Start()
    {
        OnApplicationFocus(true);
    }
    private void OnApplicationFocus(bool hasFocus)
    {
        if (!hasFocus) { return; }

        CancelInvoke();
        scoreText.text = "Highscore: " + PlayerPrefs.GetInt("HighScore").ToString();
        actualEnergy = PlayerPrefs.GetInt(EnergyKey, maxEnergy);

        if(actualEnergy==0)
        {
            string energyReadyStr = PlayerPrefs.GetString(EnergyReadyKey, string.Empty);
            if(energyReadyStr == string.Empty)
                 return;


           DateTime energyReady =  DateTime.Parse(energyReadyStr);

            if(DateTime.Now > energyReady)
            {
                actualEnergy = maxEnergy;
                PlayerPrefs.SetInt(EnergyKey, actualEnergy);
#if UNITY_ANDROID
                androidNotificationHandler.ScheduleNotification(energyReady);
#endif
            }
            else
            {
                playBut.interactable = false;
                Invoke(nameof(EnergyRecharged), (energyReady - DateTime.Now).Seconds);
            }
        }
        EnergyText.text = $"Play ({actualEnergy})";
    }
    public void Play()
    {
        if (actualEnergy < 1) return;


        actualEnergy--;

        PlayerPrefs.SetInt(EnergyKey, actualEnergy);

        if(actualEnergy==0)
        {
            DateTime energyReadyDate = DateTime.Now.AddMinutes(eneryRechargeMinutes);
            PlayerPrefs.SetString(EnergyReadyKey, energyReadyDate.ToString());
        }
        SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {
        Application.Quit();
    }


    void EnergyRecharged()
    { 
        playBut.interactable = true;

        actualEnergy = maxEnergy;
        PlayerPrefs.SetInt(EnergyKey, actualEnergy);
        EnergyText.text = $"Play ({actualEnergy})";
    }
}
