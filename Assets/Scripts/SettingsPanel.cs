using Requests;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SettingsPanel : Panel {

    public Image englishCheck;
    public Image arabicCheck;
    public User user = null;

    public override void Init() {
        this.user = Program.User;
        string lang = Cache.GetLanguage();
        if (lang == "English"){
            englishCheck.enabled = true;
            arabicCheck.enabled = false;
        } else if (lang == "Arabic"){
            arabicCheck.enabled = true;
            englishCheck.enabled = false;
        }
        Clear();
    }

    public void ChangeLanguages(int index) {
        Clear();
        //0 is English
        if (index == 0) {
            englishCheck.enabled = true;
            Program.language.Arabic = false;
            Cache.SetLanguage("English");
        } else if (index == 1) {
            arabicCheck.enabled = true;
            Program.language.Arabic = true;
            Cache.SetLanguage("Arabic");
        }
        SceneManager.LoadScene("MainScene");
    }
    public void openHowItWorks() {
        Panel panel = PanelsFactory.CreateHowItWorks();
        Open(panel);
    }
    public void openTermsConditions() {
        Panel panel = PanelsFactory.CreateTermsConditions();
        Open(panel);
    }
    public void openPrivacyPolicy() {
        Panel panel = PanelsFactory.CreatePrivacyPolicy();
        Open(panel);
    }
    public void openLicenses() {
        Panel panel = PanelsFactory.CreateLicenses();
        Open(panel);
    }
    public void openContactUs() {
        Panel panel = PanelsFactory.CreateContactUs();
        Open(panel);
    }
    internal override void Clear() {
    }
    public void openNotifications() {
        Panel panel = PanelsFactory.CreateNotificationPanel();
        Open(panel);
    }
    public void Logout() {
        Program.User = null;
        Cache.SetUserId("");
        Program.IsLoggedIn = false;
        MissionCompleted(SearchPanel.PANELNAME, "Waiting for you to come back!");
    }
}
