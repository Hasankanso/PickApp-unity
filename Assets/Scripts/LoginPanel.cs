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

public class LoginPanel : Panel
{
  public InputFieldScript phone, code, password;
  public Image backButton;
  private Person person;
  private bool isFromFooter;

  public void Login()
  {
    if (Validate())
    {
      User user = new User("+" + code.text.text + phone.text.text, password.GetComponent<InputField>().text);
      Request<User> request = new Login(user);
      request.Send(Response);

    }
  }

  private void Response(User u, HttpStatusCode code, string message)
  {
    if (!code.Equals(HttpStatusCode.OK))
    {
      OpenDialog("Incorrect email or password", false);
    }
    else
    {
      Program.User = u;
      OpenDialog("Welcome back to PickApp", true);
      Program.IsLoggedIn = true;
      if (!isFromFooter)
        back();
      else
        FooterMenu.dFooterMenu.OpenProfilePanel();
    }
  }

  public void Init(bool isFromFooter)
  {
    Clear();
    this.isFromFooter = isFromFooter;
    if (!isFromFooter)
    {
      backButton.gameObject.SetActive(true);
    }
    if (!string.IsNullOrEmpty(Program.GetPhone()))
    {
      phone.SetText(Program.GetPhone());
    }
    if (!string.IsNullOrEmpty(Program.GetPhoneCode()))
    {
      code.SetText(Program.GetPhoneCode());
    }
    if (!string.IsNullOrEmpty(Program.GetPassword()))
    {
      password.SetText(Program.GetPassword());
    }
  }

  private bool Validate()
  {
    bool valid = true;
    if (code.text.text.Equals(""))
    {
      code.Error();
      OpenDialog("Please enter code", false);
      valid = false;
    }
    if (phone.text.text.Equals(""))
    {
      phone.Error();
      OpenDialog("Please enter phone", false);
      valid = false;
    }
    if (Regex.Matches(phone.text.text, @"[a-zA-Z]").Count > 0)
    {
      phone.Error();
      OpenDialog("Invalid phone", false);
      valid = false;
    }
    if (phone.text.text.Length > 15)
    {
      phone.Error();
      OpenDialog("Your phone number is invalid", false);
      valid = false;
    }
    if (phone.text.text.Length < 0)
    {
      phone.Error();
      OpenDialog("Your phone number is invalid", false);
      valid = false;
    }
    if (password.text.text.Equals(""))
    {
      password.Error();
      OpenDialog("Enter password", false);
      valid = false;
    }
    if (password.text.text.Length < 7)
    {
      password.Error();
      OpenDialog("Invalid password", false);
      valid = false;
    }
    if (password.text.text.Any(char.IsDigit))
    {
      password.Error();
      OpenDialog("Invalid password", false);
      valid = false;
    }
    return valid;
  }
  public void ForgetPassword()
  {
    Panel p = PanelsFactory.createPhonePanel();
    openCreated(p);
  }
  public void CreateAccount()
  {
    Panel p = PanelsFactory.createRegister();
    openCreated(p);
  }
  internal override void Clear()
  {
    backButton.gameObject.SetActive(false);
    phone.Reset();
    password.Reset();
    code.Reset();
  }
}
