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
            var item = ItemsFactory.CreateNotificationItem(listView.scrollContainer);
            listView.Add(item.gameObject);
            NotificationItems.Add(item);
        }
    }
    public void Search()
    {
        Debug.Log("search");

        bool isNoResult = true;
        string searchText = search.text;
        for (int i = 0; i < NotificationItems.Count; i++)
        {
            if (NotificationItems[i].Title.text.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0 || NotificationItems[i].Message.text.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0)
            {
                NotificationItems[i].gameObject.SetActive(true);
                isNoResult = false;
            }
            else
            {
                NotificationItems[i].gameObject.SetActive(false);
            }
        }
        if (isNoResult)
        {
            OpenDialog("No results found", false);
        }
    }

}
