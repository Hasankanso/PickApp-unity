using Requests;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class SettingsPanel : Panel
{

  public Image englishCheck;
  public Image arabicCheck;
  public User user = null;

  public override void Init()
  {
    AdMob.InitializeBannerView();
    this.user = Program.User;
    string lang = Cache.GetLanguage();
    Clear();
    if (lang == "English")
    {
      englishCheck.enabled = true;
      arabicCheck.enabled = false;
    }
    else if (lang == "Arabic")
    {
      arabicCheck.enabled = true;
      englishCheck.enabled = false;
    }

  }

  public void ChangeLanguages(int index)
  {
    string newLanguage = "";
    //0 is English
    if (index == 0)
    {
      englishCheck.enabled = true;
      arabicCheck.enabled = false;
      newLanguage = "English";
    }
    else if (index == 1)
    {
      englishCheck.enabled = false;
      arabicCheck.enabled = true;
      newLanguage = "Arabic";
    }


    if (!Language.LanguageExists(newLanguage))
    {
      StartCoroutine(Language.DownloadXml(newLanguage, OnDownloadLangComplete));
    }
    else
    {
      Cache.SetLanguage(newLanguage);
      AdMob.DestroyBanner();
      SceneManager.LoadScene("MainScene");
    }

  }

  private void OnDownloadLangComplete(bool done, string language)
  {
    if (done) { 
    Cache.SetLanguage(language);
    AdMob.DestroyBanner();
    SceneManager.LoadScene("MainScene");
    }
    else { 
    OpenDialog("something went wrong...", false);
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
  }
  public void openNotifications()
  {
    Panel panel = PanelsFactory.CreateNotificationPanel();
    Open(panel);
  }
  public void Logout()
  {
    Program.User = null;
    Cache.SetUserId("");
    Program.IsLoggedIn = false;
    MissionCompleted(SearchPanel.PANELNAME, "Waiting for you to come back!");
  }
}
