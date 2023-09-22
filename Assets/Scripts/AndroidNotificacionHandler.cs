using System;
using System.Collections;
using System.Collections.Generic;
#if UNITY_ANDROID
using Unity.Notifications.Android;
#endif
using UnityEngine;

public class AndroidNotificacionHandler : MonoBehaviour
{
#if UNITY_ANDROID
    private const string ChannelId = "notification_channel";
    public void ScheduleNotification(DateTime dateTime)
    {
        AndroidNotificationChannel notiChannel = new AndroidNotificationChannel {

            Id = ChannelId,
            Name = "Notification Channel",
            Description = "Random description",
            Importance = Importance.Default
            
        
        };

        AndroidNotificationCenter.RegisterNotificationChannel(notiChannel);

        AndroidNotification notification = new AndroidNotification { 
        
            Title = "Energy Recharged!",
            Text = "Your energy has recharged, comeback!!",
            SmallIcon = "default",
            LargeIcon = "default",
            FireTime= dateTime

        
        };

        AndroidNotificationCenter.SendNotification(notification, ChannelId);
    }
#endif
}
