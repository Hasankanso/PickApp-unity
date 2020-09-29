using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotificationItem : Panel
{
    private Panel inboxPanel;
    public Text Title;
    public Text Message;
    public Person person = null;

    public void Init(Person person)
    {
        Clear();
        this.person = person;

    }

    internal override void Clear()
    {

    }
}