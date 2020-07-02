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
  public void Start()
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
    Panel panel = PanelsFactory.createHowItWorks();
    openCreated(panel);
  }
  public void openTermsConditions()
  {
    Panel panel = PanelsFactory.createTermsConditions();
    openCreated(panel);
  }
  public void openPrivacyPolicy()
  {
    Panel panel = PanelsFactory.CreatePrivacyPolicy();
    openCreated(panel);
  }
  public void openLicenses()
  {
    Panel panel = PanelsFactory.createLicenses();
    openCreated(panel);
  }
  public void openContactUs()
  {
    Panel panel = PanelsFactory.createContactUs();
    openCreated(panel);
  }
  internal override void Clear()
  {
    englishCheck.enabled = false;
    arabicCheck.enabled = false;
  }
  public void Logout()
  {
    Request<string> request = new Logout();
    request.Send(response);
  }
  private void response(string result, int code, string message)
  {
    if (!code.Equals((int) HttpStatusCode.OK))
    {
    }
    else
    {
      OpenDialog("Waiting for your come back!", true);
      Program.User = null;
      Program.IsLoggedIn = false;
      FooterMenu.dFooterMenu.OpenSearchPanel();
      this.destroy();
    }
  }
  public void openNotifications()
  {
    Panel panel = PanelsFactory.createNotificationPanel();
    openCreated(panel);
  }
  public void changePassword()
  {
    Panel panel = PanelsFactory.ChangePassword(user);
    openCreated(panel);
  }

}