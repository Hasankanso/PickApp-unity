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

public class LoginPanel : Panel {
    public InputFieldScript phone, code, password;
    public Image backButton;
    private Person person;
    private bool isFromFooter;

    public void Login() {
        if (Validate()) {
            User user = new User("+" + code.text.text + phone.text.text, password.GetComponent<InputField>().text);
            Request<User> request = new Login(user);
            request.sendRequest.AddListener(OpenSpinner);
            request.receiveResponse.AddListener(CloseSpinner);
            request.Send(Response);
        }
    }
    private void Response(User u, int code, string message) {
        if (!code.Equals((int)HttpStatusCode.OK)) {
            OpenDialog(message, false);
        } else {
            Program.User = u;
            Cache.User(u);
            Cache.SetPassword(password.GetComponent<InputField>().text);
            Program.IsLoggedIn = true;
            if (!isFromFooter) {
                Back();
                OpenDialog("Welcome back to PickApp", true);
            } else {
                MissionCompleted(ProfilePanel.PANELNAME, "Welcome back to PickApp");
            }
        }
    }

    public void Init(bool isFromFooter) {
        Clear();
        this.isFromFooter = isFromFooter;
        if (!isFromFooter) {
            backButton.gameObject.SetActive(true);
        }
        if (!string.IsNullOrEmpty(Cache.GetPhone())) {
            phone.SetText(Cache.GetPhone());
        }
        if (!string.IsNullOrEmpty(Cache.GetPhoneCode())) {
            this.code.SetText(Cache.GetPhoneCode());
        }
        if (!string.IsNullOrEmpty(Cache.GetPassword())) {
            password.SetText(Cache.GetPassword());
        }
    }

    private bool Validate() {
        bool valid = true;
        if (code.text.text.Equals("")) {
            code.Error();
            OpenDialog("Please enter code", false);
            valid = false;
        }
        if (phone.text.text.Equals("")) {
            phone.Error();
            OpenDialog("Please enter phone", false);
            valid = false;
        }
        if (Regex.Matches(phone.text.text, @"[a-zA-Z]").Count > 0) {
            phone.Error();
            OpenDialog("Invalid phone", false);
            valid = false;
        }
        if (phone.text.text.Length > 15) {
            phone.Error();
            OpenDialog("Your phone number is invalid", false);
            valid = false;
        }
        if (phone.text.text.Length < 0) {
            phone.Error();
            OpenDialog("Your phone number is invalid", false);
            valid = false;
        }
        if (password.text.text.Equals("")) {
            password.Error();
            OpenDialog("Enter password", false);
            valid = false;
        }
        if (password.text.text.Length < 7) {
            password.Error();
            OpenDialog("Invalid password", false);
            valid = false;
        }
        if (password.text.text.Any(char.IsDigit)) {
            password.Error();
            OpenDialog("Invalid password", false);
            valid = false;
        }
        return valid;
    }
    public void ForgetPassword() {
        Panel p = PanelsFactory.CreatePhonePanel();
        Open(p);
    }
    public void CreateAccount() {
        Panel p = PanelsFactory.CreateRegister();
        Open(p);
    }
    internal override void Clear() {
        backButton.gameObject.SetActive(false);
        phone.Reset();
        password.Reset();
        code.Reset();
    }
}
