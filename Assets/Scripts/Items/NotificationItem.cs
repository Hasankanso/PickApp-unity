using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotificationItem : Panel
{
    public Text title;
    public Text message;
    Action<NotificationScript> OnItemClick;

    public void Init(Action<NotificationScript> OnItemClick)
    {
        Clear();

        this.OnItemClick = OnItemClick;
    }

    internal override void Clear()
    {
        this.title.text = "";
        this.message.text = "";
    }

}