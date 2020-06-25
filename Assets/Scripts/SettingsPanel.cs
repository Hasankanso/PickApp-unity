using Requests;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class SettingsPanel : Panel {

    public Image englishCheck;
    public Image arabicCheck;
    public Person person = null;
    public void init(Person person) {
        this.person = person;
        Clear();
    }
    public void ChangeLanguages(int index) {
        Clear();
        //0 is English
        if (index == 0) {
            englishCheck.enabled = true;
        } else if (index == 1) {
            arabicCheck.enabled = true;
        }
    }
    public void openHowItWorks() {
        Panel panel = PanelsFactory.createHowItWorks();
        openCreated(panel);
    }
    public void openTermsConditions() {
        Panel panel = PanelsFactory.createTermsConditions();
        openCreated(panel);
    }
    public void openPrivacyPolicy() {
        Panel panel = PanelsFactory.CreatePrivacyPolicy();
        openCreated(panel);
    }
    public void openLicenses() {
        Panel panel = PanelsFactory.createLicenses();
        openCreated(panel);
    }
    public void openContactUs() {
        Panel panel = PanelsFactory.createContactUs();
        openCreated(panel);
    }
    internal override void Clear() {
        englishCheck.enabled = false;
        arabicCheck.enabled = false;
    }
    public void Logout() {
        Request<string> request = new Logout();
        request.Send(response);
    }
    private void response(string result, HttpStatusCode code, string message) {
        if (!code.Equals(HttpStatusCode.OK)) {
        } else {
            OpenDialog("Waiting for your to come back!", true);
            Program.User = null;
            Program.IsLoggedIn = false;
            Program.UserToken = null;
            Program.CountryInformations = null;
            FooterMenu.dFooterMenu.OpenSearchPanel();
            this.destroy();
        }
    }
    public void openNotifications() {
        Panel panel = PanelsFactory.createNotificationPanel();
        openCreated(panel);
    }
    public void changePassword() {
        Panel panel = PanelsFactory.ChangePassword(person);
        openCreated(panel);
    }

}