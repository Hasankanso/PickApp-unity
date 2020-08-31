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
    public void Login()
    {
        Panel p = PanelsFactory.CreateLogin();
        Open(p);
    }
    public void Register()
    {
        Panel p = PanelsFactory.CreateRegister();
        Open(p);
    }
    public void NeedHelp()
    {
        //Panel p = PanelsFactory.CreateHowItWorks();  need help panel
        //Open(p);
    }

    internal override void Clear()
    {
        throw new NotImplementedException();
    }
}
