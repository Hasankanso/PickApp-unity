using Requests;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using System.Net.Mail;

public class ContactUsPanel : Panel {
    public InputFieldScript description, subject;

    private Person user;
    public void sendMessage(Person user) {
        if (validate()) {
            Request<string> request = new SendContactUs(user, subject.text.text, description.text.text);
            request.sendRequest.AddListener(OpenSpinner);
            request.receiveResponse.AddListener(CloseSpinner);
            request.Send(response);
        }
    }
    private void response(string result, int code, string message) {
        if (!code.Equals(HttpStatusCode.OK)) {
            Panel p = PanelsFactory.CreateDialogBox("Some thing went wrong", false);
            OpenDialog(p);
        } else {
            Panel p = PanelsFactory.CreateDialogBox("Message sent successfully", true);
            OpenDialog(p);
            Panel panel = PanelsFactory.CreateSettings();
            Open(panel);
        }
    }
    public void Init(Person user) {
        this.user = user;
    }
    public void openPrivacyPolicy() {
        Panel panel = PanelsFactory.CreatePrivacyPolicy();
        Open(panel);
    }
    internal override void Clear() {
        description.Reset();
        subject.Reset();
    }
    private bool validate() {
        bool valid = true;
        if (description.text.text.Length < 70) {
            description.Error();

            Panel p = PanelsFactory.CreateDialogBox("Description is too short.", false);
            OpenDialog(p);
            valid = false;
        }
        if (description.text.text.Equals("")) {
            description.Error();

            Panel p = PanelsFactory.CreateDialogBox("Insert description.", false);
            OpenDialog(p);
            valid = false;
        }
        if (subject.text.text.Equals("")) {
            subject.Error();

            Panel p = PanelsFactory.CreateDialogBox("Insert a subject.", false);
            OpenDialog(p);
            valid = false;
        }
        if (subject.text.text.Length < 10) {
            subject.Error();

            Panel p = PanelsFactory.CreateDialogBox("Subject is too short.", false);
            OpenDialog(p);
            valid = false;
        }
        return valid;
    }


}
/*try {
    MailMessage mail = new MailMessage();
SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
mail.From = new MailAddress(user.Email);
mail.To.Add("contact.maaluma@gmail.com");
    mail.Subject = subject.text.text;
    mail.Body = description.text.text;

    SmtpServer.Port = 587;
    SmtpServer.EnableSsl = true;

    SmtpServer.Send(mail);

} catch (Exception ex) {
    Console.WriteLine("Message not sent!");
}*/
