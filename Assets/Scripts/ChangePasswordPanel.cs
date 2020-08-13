using BackendlessAPI;
using BackendlessAPI.Async;
using Requests;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class ChangePasswordPanel : Panel {
    public InputFieldScript currentPassword, newPassword, confirmPassword;
    private User user = null;
    private bool isChangePassword;
    public Button changePassword, register;
    private bool isForgetPassword = false;

    public void submit() {
        if (validate()) {
            Request<Person> request;
            if (isForgetPassword) {
                request = new ForgetPassword(user, newPassword.GetComponent<InputField>().text);
            } else {
                request = new ChangePassword(user, currentPassword.GetComponent<InputField>().text, newPassword.GetComponent<InputField>().text);
            }
            request.AddSendListener(OpenSpinner);
            request.AddReceiveListener(CloseSpinner);
            request.Send(response);
        }
    }
    private void response(Person result, int code, string message) {
        if (!code.Equals(HttpStatusCode.OK)) {
            Panel p = PanelsFactory.CreateDialogBox("There was an error changing password", false);
            OpenDialog(p);
        } else {
            Panel p = PanelsFactory.CreateDialogBox("Password has been changed", true);
            OpenDialog(p);
            Panel panel = PanelsFactory.CreateSettings();
            Open(panel);
        }
    }
    public void Register() {
        if (validate()) {
            user.Password = newPassword.GetComponent<InputField>().text;
            PhonePanel panel = PanelsFactory.CreatePhonePanel();
            Open(panel, () => panel.Init(user));
        }
    }

    public void Init(User user) {
        Clear();
        this.user = user;
        isChangePassword = true;
        changePassword.gameObject.SetActive(true);
    }
    public void Init(bool isForgetPassword, User user) {
        Clear();
        isChangePassword = false;
        this.isForgetPassword = isForgetPassword;
        currentPassword.gameObject.SetActive(false);
        this.user = user;
        if (!isForgetPassword)
            register.gameObject.SetActive(true);
        else
            changePassword.gameObject.SetActive(true);
    }
    private bool validate() {
        bool valid = true;
        if (isChangePassword) {
            if (currentPassword.text.text.Equals("")) {
                currentPassword.Error();
                OpenDialog("Please enter current password", false);
                valid = false;
            }
        }
        if (newPassword.text.text.Equals("")) {
            newPassword.Error();

            OpenDialog("Please enter new password", false);

            valid = false;
        } else if (newPassword.text.text.Length < 8) {
            newPassword.Error();

            OpenDialog("Your Password is too short", false);

            valid = false;
        }//Add special characters validation 
          else if (!newPassword.GetComponent<InputField>().text.Any(char.IsDigit)) {
            newPassword.Error();

            OpenDialog("Your Password must contain numbers", false);
            valid = false;
        }
        if (confirmPassword.text.text.Equals("")) {
            confirmPassword.Error();

            OpenDialog("Please confirm your password", false);
            valid = false;
        } else if (!newPassword.text.text.Equals(confirmPassword.text.text)) {
            confirmPassword.Error();

            OpenDialog("Password doesn't match", false);
            valid = false;
        }
        return valid;
    }
    internal override void Clear() {
        currentPassword.gameObject.SetActive(true);
        register.gameObject.SetActive(false);
        changePassword.gameObject.SetActive(false);
        //clear content of all inptfield.
        currentPassword.Reset();
        newPassword.Reset();
        confirmPassword.Reset();
    }
}