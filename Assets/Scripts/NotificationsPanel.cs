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
        Program.SetNewsCheckbox(news.isOn);
        Program.SetDisableAllCheckbox(disableAll.isOn);
        closeBack();
    }
    public void Init() {
        news.isOn = Program.GetNewsCheckbox();
        disableAll.isOn = Program.GetDisableAllCheckbox();
    }
    internal override void Clear() {
        throw new NotImplementedException();
    }
}