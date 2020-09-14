using Requests;
using System;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.UI;

public class RegisterPanel : Panel {
    public InputFieldScript firstName, email, lastName;
    public Dropdown genderDP, countryDP;
    public Toggle acceptCheckBox;
    public Text birthday;
    public Image profilePicture;
    private bool haveProfile = false;
    public User user = null;
    public void Register() {
        if (Validate()) {
            Person person = new Person();
            person.FirstName = firstName.text.text;
            person.LastName = lastName.text.text;
            person.Birthday = Program.StringToBirthday(birthday.text);
            if (haveProfile) {
                person.ProfilePicture = profilePicture.sprite.texture;
            }
            CountryInformations ci = new CountryInformations();
            ci.Name = countryDP.options[countryDP.value].text;
            var country = Program.CountriesInformations[ci.Name];
            person.CountryInformations = country;
            person.Gender = genderDP.value == 0;
            user = new User(person, email.text.text);
            PhonePanel panel = PanelsFactory.CreatePhonePanel();
            Open(panel, () => panel.Init(user));
        }
    }
    internal int CalculateAge() {
        DateTime birthdate = Program.StringToBirthday(birthday.text);
        int years = DateTime.Now.Year - birthdate.Year;
        if (DateTime.Now.Month < birthdate.Month || (DateTime.Now.Month == birthdate.Month && DateTime.Now.Day < birthdate.Day))
            years--;
        return years;
    }
    public void ViewChoosenImage() {
        Panel panel = PanelsFactory.CreateImageViewer(profilePicture.sprite.texture);
        OpenDialog(panel);
    }
    private void OnDatePicked(DateTime d) {
        birthday.text = Program.BirthdayToString(d);
    }
    public void OpenDatePicker() {
        DateTime now = DateTime.Now;
        MobileDateTimePicker.CreateDate(now.Year, now.Month, now.Day, null, delegate (DateTime dt) { OnDatePicked(dt); });
    }
    public void PickImage() {
        NativeGallery.Permission permission = NativeGallery.GetImageFromGallery((path) => {
            if (path != null) {
                // Create Texture from selected image
                Texture2D texture = NativeGallery.LoadImageAtPath(path, -1);
                if (texture == null) {
                    return;
                } else {
                    profilePicture.sprite = Program.GetImage(texture);
                    haveProfile = true;
                }
            }
        }, "Select a PNG image", "image/png");
        Debug.Log("Permission result: " + permission);
    }
    private bool Validate() {
        if (firstName.text.text.Equals("")) {
            firstName.Error();
            Panel p = PanelsFactory.CreateDialogBox("Please insert your name", false);
            OpenDialog(p);
            return false;
        }
        if (lastName.text.text.Equals("")) {
            lastName.Error();
            Panel p = PanelsFactory.CreateDialogBox("Please insert your last name", false);
            OpenDialog(p);
            return false;
        }
        if (CalculateAge() < 14) {
            Panel p = PanelsFactory.CreateDialogBox("You are under the legal age", false);
            OpenDialog(p);
            return false;
        }
        Debug.Log(CalculateAge());
        if (CalculateAge() > 100) {
            Panel p = PanelsFactory.CreateDialogBox("Invalid birthday", false);
            OpenDialog(p);
            return false;
        }
        if (email.text.text.Equals("")) {
            email.Error();
            Panel p = PanelsFactory.CreateDialogBox("Enter your email", false);
            OpenDialog(p);
        }
        if (!IsValidEmail(email.text.text)) {
            email.Error();
            Panel p = PanelsFactory.CreateDialogBox("Invalid email", false);
            OpenDialog(p);
        }
        if (!acceptCheckBox.isOn) {
            OpenDialog("Accept terms & privacy", false);
            return false;
        }
        return true;
    }
    bool IsValidEmail(string email) {
        try {
            var address = new System.Net.Mail.MailAddress(email);
            return address.Address == email;
        } catch {
            return false;
        }
    }
    public override void Init() { //ToDo
        Clear();
        Request<Dictionary<string, CountryInformations>> request = new GetCountries();
        request.AddSendListener(OpenSpinner);
        request.AddReceiveListener(CloseSpinner);
        request.Send(GetCountriesResponse);
    }

    private void GetCountriesResponse(Dictionary<string, CountryInformations> result, int code, string message) {
        if (!code.Equals((int)HttpStatusCode.OK))
            OpenDialog("Error", false);
        else {
            Program.CountriesInformations = result;
            countryDP.AddOptions(Program.CountriesInformationsNames);
        }
    }
    public void Login() {
        LoginPanel panel = PanelsFactory.CreateLogin();
        Open(panel, () => { panel.Init(false); });
    }
    internal override void Clear() {
        firstName.Reset();
        lastName.Reset();
        birthday.text = Program.BirthdayToString(DateTime.Now.AddYears(-14));
        genderDP.value = 0;
        countryDP.value = 0;
        countryDP.ClearOptions();
    }
}
