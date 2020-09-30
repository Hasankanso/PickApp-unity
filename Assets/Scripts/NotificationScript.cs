using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class NotificationScript : Panel
{
    public ListView listView;
    public InputField search;
    public Image backImage;
    public Text title;
    public Person person;
    private List<NotificationItem> NotificationItems = new List<NotificationItem>();

    public static readonly string PANELNAME = "NotificationScript";

    internal override void Clear()
    {
        backImage.gameObject.SetActive(false);
        listView.Clear();
        title.transform.position = new Vector3(56f, title.transform.position.y, title.transform.position.z);
    }
    public void Init(List<NotificationItem> notificationItem)
    {
        Clear();
        Status = StatusE.VIEW;
        foreach (NotificationItem NI in notificationItem)
        {
            var item = ItemsFactory.CreateNotificationItem(listView.scrollContainer, OpenAddRide);
            listView.Add(item.gameObject);
            NotificationItems.Add(item);
        }
    }
    public void AddNotificationItem(string title,string message)
    {
        var item = ItemsFactory.CreateNotificationItem(listView.scrollContainer,OpenAlert);
        listView.Add(item.gameObject);
        item.title.text = title;
        item.message.text = message;
        NotificationItems.Add(item);
    }
    public void OpenAlert(NotificationScript n) 
    {
         Debug.Log("sexzxz");
    }
    public void OpenAddRide(NotificationScript v )
    {

    }


}
