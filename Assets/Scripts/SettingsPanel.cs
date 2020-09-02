using Requests;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class SettingsPanel : Panel
{

  public Image englishCheck;
  public Image arabicCheck;
  public User user = null;

  public override void Init()
  {
    this.user = Program.User;
    Clear();
  }

  public void ChangeLanguages(int index)
  {
    Clear();
    //0 is English
    if (index == 0)
    {
      englishCheck.enabled = true;
    }
    else if (index == 1)
    {
      arabicCheck.enabled = true;
    }
  }
  public void openHowItWorks()
  {
    Panel panel = PanelsFactory.CreateHowItWorks();
    Open(panel);
  }
  public void openTermsConditions()
  {
    Panel panel = PanelsFactory.CreateTermsConditions();
    Open(panel);
  }
  public void openPrivacyPolicy()
  {
    Panel panel = PanelsFactory.CreatePrivacyPolicy();
    Open(panel);
  }
  public void openLicenses()
  {
    Panel panel = PanelsFactory.CreateLicenses();
    Open(panel);
  }
  public void openContactUs()
  {
    Panel panel = PanelsFactory.CreateContactUs();
    Open(panel);
  }
  internal override void Clear()
  {
    englishCheck.enabled = false;
    arabicCheck.enabled = false;
  }
  public void openNotifications()
  {
    Panel panel = PanelsFactory.CreateNotificationPanel();
    Open(panel);
  }
    /*
  public void changePassword()
  {
    ChangePasswordPanel panel = PanelsFactory.ChangePassword();
    Open(panel, () => panel.Init(user));
  }*/
/*    public void Logout() {
      string cacheToken = Cache.GetToken();
      if (!string.IsNullOrEmpty(cacheToken)) {
          User user = new User();
          user.Token = cacheToken;
          Program.User = user;
          Request<string> request = new Logout();
          request.AddSendListener(OpenSpinner);
          request.AddReceiveListener(CloseSpinner);
          request.Send(response);
      } else {
          LogoutDefault();
      }
  }
  private void response(string result, int code, string message) {
      if (!code.Equals((int)HttpStatusCode.OK)) {
          LogoutDefault();
      } else {
          LogoutDefault();
      }
  }
  private void LogoutDefault() {
      Program.User = null;
      Cache.SetToken("");
      Program.IsLoggedIn = false;
      MissionCompleted(SearchPanel.PANELNAME, "Waiting for you to come back!");
  }*/

}
 