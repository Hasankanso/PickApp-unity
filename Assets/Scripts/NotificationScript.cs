﻿using System;
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
    public GameObject settingPanel;
    public GameObject firstView;
    private List<NotificationItem> NotificationItems = new List<NotificationItem>();

    public static readonly string PANELNAME = "NotificationScript";

    internal override void Clear()
    {
        firstView.gameObject.SetActive(true);
        settingPanel.gameObject.SetActive(false);
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
    }
    public void OpenAddRide(NotificationScript v )
    {

    }
    public void ClearNotifications()
    {
        this.listView.Clear();
        this.NotificationItems.Clear();
        settingPanel.gameObject.SetActive(false);
        firstView.gameObject.SetActive(true);
        OpenDialog("Notifications Cleared", true);
    }
    public void OpenNotificationsSetting()
    {
        settingPanel.gameObject.SetActive(true);
        firstView.gameObject.SetActive(false);

    }
}
