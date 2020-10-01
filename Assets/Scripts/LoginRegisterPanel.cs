using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using UnityEngine.UI;
using Requests;
using Newtonsoft.Json.Linq;
using UnityEngine;
using BackendlessAPI;
using BackendlessAPI.Engine;
using System.Collections.Generic;
using BackendlessAPI.Utils;
using System;

public class LoginRegisterPanel : Panel
{
    public Image backButton;
    private bool isFromProfilePanel;
    public void Init(bool isFromProfilePanel)
    {
        Clear();
        this.isFromProfilePanel = isFromProfilePanel;
        if (!isFromProfilePanel)
        {
            backButton.gameObject.SetActive(true);
        }
    }    
    public void Login()
    {
        LoginPanel p = PanelsFactory.CreateLogin();
        Open(p, () => { p.Init(isFromProfilePanel); });
    }
    public void Register()
    {
        RegisterPanel p = PanelsFactory.CreateRegister();
        Open(p, () => { p.Init(); });
    }
    public void NeedHelp()
    {
      //need help panel
    }

    internal override void Clear()
    {
        backButton.gameObject.SetActive(false);
    }
}
