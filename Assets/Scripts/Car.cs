using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Car : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float speedOverTime;
    [SerializeField] float turnSpeed = 150f;
    int steerValue;

    int lives = 3;
    // Update is called once per frame
    void Update()
    {
        speed += speedOverTime * Time.deltaTime;
        transform.Translate(Vector3.forward * speed *Time.deltaTime);
        transform.Rotate(0, steerValue * turnSpeed * Time.deltaTime, 0);
    }

    public void Steer(int value)
    {
        steerValue = value;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Obstacle"))
        {
            if(lives == 0)
            {
                SceneManager.LoadScene(0);
            }
            lives--;
            speed = 0.1f;
        }
     
    }
}
