using Requests;
using System;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AccountPanel : Panel
{
  public InputFieldScript firstName, lastName, email;
  public Dropdown genderDP, countryDP;
  public Text birthday;
  public Image profilePicture;
  public User user = null;

  public void submit()
  {
    if (vadilate())
    {
      CountryInformations cI = Program.CountriesInformations[countryDP.options[countryDP.value].text];


      Person editedPerson = new Person(firstName.text.text, lastName.text.text, user.phone, Program.StringToBirthday(birthday.text),
      profilePicture.sprite.texture, cI, genderDP.value == 0);

      User editedUser = new User(editedPerson, null, user.phone, user.password, email.text.text, user.Id, user.Token);

      Request<User> request = new EditAccount(editedUser);
      Task.Run(() => request.Send(response));
    }
  }
  public void ViewChoosenImage()
  {
    Panel panel = PanelsFactory.CreateImageViewer(Program.Person.ProfilePicture);
    OpenDialog(panel);
  }
  private void response(User result, HttpStatusCode code, string message)
  {
    if (!code.Equals(HttpStatusCode.OK))
    {
      Panel p = PanelsFactory.CreateDialogBox("There was an error editing your account", false);
      OpenDialog(p);
    }
    else
    {
      Panel p = PanelsFactory.CreateDialogBox("Account has been edited", true);
      OpenDialog(p);
      Panel panel = PanelsFactory.createProfile();
      openCreated(panel);
    }
  }
  private void OnDatePicked(DateTime d)
  {
    birthday.text = Program.BirthdayToString(d);
  }
  public void OpenDatePicker()
  {
    DateTime now = DateTime.Now;
    MobileDateTimePicker.CreateDate(now.Year, now.Month, now.Day, null, delegate (DateTime dt) { OnDatePicked(dt); });
  }

  public void PickImage()
  {
    NativeGallery.Permission permission = NativeGallery.GetImageFromGallery((path) =>
    {
      if (path != null)
      {
        // Create Texture from selected image
        Texture2D texture = NativeGallery.LoadImageAtPath(path, -1);
        if (texture == null)
        {
          return;
        }
        else
        {
          profilePicture.sprite = Program.GetImage(texture);
        }
      }
    }, "Select a PNG image", "image/png");
    Debug.Log("Permission result: " + permission);
  }
  private void CheckGender(bool gender)
  {
    if (gender)
      genderDP.value = 0;
    else
      genderDP.value = 1;
  }
  private void CheckCountry(string country)
  {
    if (country.Equals("Lebanon"))
      countryDP.value = 0;
  }
  public void Init()
  {
    Clear();
    Person person = Program.Person;
    firstName.SetText(person.FirstName);
    lastName.SetText(person.LastName);
    email.SetText(Program.User.Email);
    birthday.text = Program.BirthdayToString(person.Birthday);
    CheckGender(person.Gender);
    CheckCountry(person.CountryInformations.Name);
    profilePicture.sprite = Program.GetImage(person.ProfilePicture);
  }
  internal int CalculateAge()
  {
    DateTime birthday = DateTime.Parse(this.birthday.text);
    // get the difference in years
    int years = DateTime.Now.Year - birthday.Year;
    // subtract another year if we're before the
    // birth day in the current year
    if (DateTime.Now.Month < birthday.Month || (DateTime.Now.Month == birthday.Month && DateTime.Now.Day < birthday.Day))
      years--;
    return years;
  }
  private bool vadilate()
  {
    bool valid = true;
    if (firstName.text.text.Equals(""))
    {
      firstName.Error();
      Panel p = PanelsFactory.CreateDialogBox("Please insert your name", false);
      OpenDialog(p);
      valid = false;
    }
    if (lastName.text.text.Equals(""))
    {
      lastName.Error();
      Panel p = PanelsFactory.CreateDialogBox("Please insert your last name", false);
      OpenDialog(p);
      valid = false;
    }
    if (birthday.text.Equals(""))
    {
      Panel p = PanelsFactory.CreateDialogBox("The birthday field can't be empty", false);
      OpenDialog(p);
      valid = false;
    }
    else
    {
      if (CalculateAge() < 13)
      {
        Panel p = PanelsFactory.CreateDialogBox("You are under the legal age", false);
        OpenDialog(p);
        valid = false;
      }
      if (CalculateAge() > 100)
      {
        Panel p = PanelsFactory.CreateDialogBox("Invalid birthday", false);
        OpenDialog(p);
        valid = false;
      }
    }
    if (!IsValidEmail(email.text.text))
    {
      email.Error();

      Panel p = PanelsFactory.CreateDialogBox("Invalid email", false);
      OpenDialog(p);
    }
    return valid;
  }
  bool IsValidEmail(string email)
  {
    try
    {
      var address = new System.Net.Mail.MailAddress(email);
      return address.Address == email;
    }
    catch
    {
      return false;
    }
  }

  internal override void Clear()
  {
    firstName.Reset();
    lastName.Reset();
    birthday.text = Program.BirthdayToString(DateTime.Now.AddYears(-13));
  }
}