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
    public InputFieldScript phone, code;
    public Image backButton;
    public GameObject phoneNumberView;

    private bool isFromProfilePanel;

    //verifyCode
    public GameObject verifyEmailView;
    public Text VerifyEmailText;
    public InputFieldScript verificationCode;

    private int viewId = 0;
    private bool showFirstBack;
    public void SendAccountVerification() {
        if (ValidatePhone()) {
            Request<string> request = new VerifyAccount("+" + code.text.text + phone.text.text);
            request.AddSendListener(OpenSpinner);
            request.AddReceiveListener(CloseSpinner);
            request.Send(RespondAccountVerification);
        }
    }

    private string HideEmail(string email) {

        string hiddenEmail = "";
        for (int i = 0; i < email.Length; i++) {
            if (i < 2) {
                hiddenEmail += email[i];
            } else {
                if (email[i].Equals('@')) {
                    hiddenEmail += "@";
                } else {
                    hiddenEmail += "*";
                }
            }
        }
        return hiddenEmail;
    }

    private void RespondAccountVerification(string email, int status, string message) {
        if (!status.Equals((int)HttpStatusCode.OK)) {
            OpenDialog(message, false);
        } else {
            string hiddenEmail = HideEmail(email);
            VerifyEmailText.text = "Verification Code Sent to " + hiddenEmail;
            OpenVerifyEmail();
        }
    }

    public void LoginCode() {
        if (Validate()) {
            User user = new User("+" + code.text.text + phone.text.text, verificationCode.text.text);
            Request<User> request = new Login(user);
            request.AddSendListener(OpenSpinner);
            request.AddReceiveListener(CloseSpinner);
            request.Send(Response);
        }
    }

    private void Response(User u, int code, string message) {
        if (!code.Equals((int)HttpStatusCode.OK)) {
            OpenDialog(message, false);
        } else {
            Program.User = u;
            Cache.User(u);
            Program.IsLoggedIn = true;
            if (!isFromProfilePanel) {
                OpenDialog("Welcome back to PickApp " + Program.User.Person.FirstName, true);
                BackClose();
            } else {
                MissionCompleted(ProfilePanel.PANELNAME, "Welcome back to PickApp " + Program.User.Person.FirstName + " !");
            }
        }
    }

    public void Init(bool isFromProfilePanel) {
        Clear();
        this.isFromProfilePanel = isFromProfilePanel;
        if (!isFromProfilePanel) {
            backButton.gameObject.SetActive(true);
            showFirstBack = true;
        }
    }

    private bool Validate() {
        if (verificationCode.text.text.Length < 4) {
            verificationCode.Error();
            OpenDialog("Code is too small", false);
            return false;
        }
        return true;
    }

    private bool ValidatePhone() {
        if (code.text.text.Equals("")) {
            code.Error();
            OpenDialog("Please enter code", false);
            return false;
        }
        if (phone.text.text.Equals("")) {
            phone.Error();
            OpenDialog("Please enter phone", false);
            return false;
        }
        if (Regex.Matches(phone.text.text, @"[a-zA-Z]").Count > 0) {
            phone.Error();
            OpenDialog("Invalid phone", false);
            return false;
        }
        if (phone.text.text.Length > 15) {
            phone.Error();
            OpenDialog("Your phone number is invalid", false);
            return false;
        }
        if (phone.text.text.Length < 0) {
            phone.Error();
            OpenDialog("Your phone number is invalid", false);
            return false;
        }

        return true;
    }

    public void BackView() {
        if (viewId == 0) {
            Back();
        } else if (viewId == 1) {
            viewId = 0;
            phoneNumberView.SetActive(true);
            verifyEmailView.SetActive(false);
            if (showFirstBack) {
                backButton.gameObject.SetActive(true);
            } else {
                backButton.gameObject.SetActive(false);
            }
        }
    }

    public void OpenVerifyEmail() {
        phoneNumberView.SetActive(false);
        verifyEmailView.SetActive(true);

        viewId = 1;
        backButton.gameObject.SetActive(true);
    }
    public void openView(int index) {
        if (index == 0) {
            phoneNumberView.SetActive(true);
            verifyEmailView.SetActive(false);
        } else if (index == 1 && ValidatePhone()) {
            phoneNumberView.SetActive(false);
            verifyEmailView.SetActive(true);
        }

    }

    internal override void Clear() {
        backButton.gameObject.SetActive(false);
        phone.Reset();
        code.Reset();
        verificationCode.Reset();
        verifyEmailView.SetActive(false);
        phoneNumberView.SetActive(true);
    }
}
