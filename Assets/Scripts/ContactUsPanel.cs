using Requests;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

public class ContactUsPanel : Panel {
    public InputFieldScript message, subject;

    public void SendMail() {
        if (validate()) {
            Request<string> request = new SendContactUs(subject.text.text, message.text.text);
            request.AddSendListener(OpenSpinner);
            request.AddReceiveListener(CloseSpinner);
            request.Send(response);
        }
    }
    private void response(string result, int code, string message) {
        if (!code.Equals((int)HttpStatusCode.OK)) {
            OpenDialog("Something went wrong", false);
        } else {
            OpenDialog(result, true);
            Back();
        }
    }
    public override void Init() {
        AdMob.InitializeBannerView();
    }
    private bool validate() {
        bool valid = true;
        if (message.text.text.Length < 70) {
            message.Error();
            OpenDialog("Message is too short.", false);
            valid = false;
        }
        if (message.text.text.Equals("")) {
            message.Error();
            OpenDialog("Insert Message.", false);
            valid = false;
        }
        if (subject.text.text.Equals("")) {
            subject.Error();

            OpenDialog("Insert a subject.", false);
            valid = false;
        }
        if (subject.text.text.Length < 10) {
            subject.Error();
            OpenDialog("Subject is too short.", false);
            valid = false;
        }
        return valid;
    }
    internal override void Clear() {
        message.Reset();
        subject.Reset();
    }
}