using System;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;
using BackendlessAPI;
using BackendlessAPI.Async;
using BackendlessAPI.Exception;
using Requests;
using System.Collections;
using UnityEngine.Networking;
using System.Threading.Tasks;

public class LoginPanel : Panel {
    public InputFieldScript phone, password;
    public Image backButton;
    private Person person;
    private bool isFromFooter;

    public void Login() {
        if (Validate()) {
            Person person = new Person(phone.text.text, password.GetComponent<InputField>().text);
            Request<Person> request = new Login(person);
            request.Send(Response);
            
        }
    }

    private void Response(Person p, HttpStatusCode code, string message) {
        if (!code.Equals(HttpStatusCode.OK)) {
            OpenDialog("Incorrect email or password", false);
        } else {
            if (p.IsDriver == true)
                Program.User = (Driver)p;
            else
                Program.User = p;
            Program.SetPhone(phone.text.text);
            Program.SetPassword(password.GetComponent<InputField>().text);
            OpenDialog("Welcome back to PickApp", true);
            Program.IsLoggedIn = true;
            Program.UserToken = p.Token;
            Program.CountryInformations = p.CountryInformations;
            if (!isFromFooter)
                back();
            else {
                FooterMenu.dFooterMenu.OpenProfilePanel();
            }
        }
    }

    public void Init(bool isFromFooter) {
        Clear();
        this.isFromFooter = isFromFooter;
        if (!isFromFooter) {
            backButton.gameObject.SetActive(true);
        }
        if (!string.IsNullOrEmpty(Program.GetPhone())) {
            phone.SetText(Program.GetPhone());
        }
        if (!string.IsNullOrEmpty(Program.GetPassword())) {
            password.SetText(Program.GetPassword());
        }
    }

    private bool Validate() {
        bool valid = true;
        if (phone.text.text.Equals("")) {
            phone.Error();
            Panel p = PanelsFactory.CreateDialogBox("Please enter phone", false);
            OpenDialog(p);
            valid = false;
        }
        if (Regex.Matches(phone.text.text, @"[a-zA-Z]").Count > 0) {
            phone.Error();
            Panel p = PanelsFactory.CreateDialogBox("Invalid phone", false);
            OpenDialog(p);
            valid = false;
        }
        if (phone.text.text.Length > 15) {
            phone.Error();
            Panel p = PanelsFactory.CreateDialogBox("Your phone number is invalid", false);
            OpenDialog(p);
            valid = false;
        }
        if (password.text.text.Equals("")) {
            password.Error();
            Panel p = PanelsFactory.CreateDialogBox("Enter password", false);
            OpenDialog(p);
            valid = false;
        }
        if (password.text.text.Length < 7) {
            password.Error();
            Panel p = PanelsFactory.CreateDialogBox("Invalid password", false);
            OpenDialog(p);
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
        Panel p = PanelsFactory.createPhonePanel();
        openCreated(p);
    }
    public void CreateAccount() {
        Panel p = PanelsFactory.createRegister();
        openCreated(p);
    }
    internal override void Clear() {
        backButton.gameObject.SetActive(false);
        phone.Reset();
        password.Reset();
    }
}
