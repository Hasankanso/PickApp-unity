using Requests;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class NotificationsPanel : Panel {
    public Toggle news, disableAll;

    public void Submit() {
        Cache.SetNewsCheckbox(news.isOn);
        Cache.SetDisableAllCheckbox(disableAll.isOn);
        closeBack();
    }
    public void Init() {
        news.isOn = Cache.GetNewsCheckbox();
        disableAll.isOn = Cache.GetDisableAllCheckbox();
    }
    internal override void Clear() {
        throw new NotImplementedException();
    }
}