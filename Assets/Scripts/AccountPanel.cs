using Requests;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AccountPanel : Panel {
    public InputFieldScript firstName, lastName, email;
    public Dropdown genderDP, countryDP;
    public Text birthday;
    public Image profilePicture;
    public User user = null;
    private bool haveProfile = false;

    public void submit() {
        if (vadilate()) {
            Debug.Log(countryDP.options[countryDP.value].text);
            CountryInformations cI = Program.CountriesInformations[countryDP.options[countryDP.value].text];
            Person editedPerson = new Person(firstName.text.text, lastName.text.text,
            Program.StringToBirthday(birthday.text),
            null, cI, genderDP.value == 0);
            editedPerson.Bio = Program.Person.Bio;
            editedPerson.Chattiness = Program.Person.Chattiness;
            if (haveProfile) {
                editedPerson.ProfilePicture = profilePicture.sprite.texture;
            }
            Request<Person> request = new EditAccount(editedPerson, email.text.text);
            request.AddSendListener(OpenSpinner);
            request.AddReceiveListener(CloseSpinner);
            request.Send(response);
        }
    }
    public void ViewChoosenImage() {
        Panel panel = PanelsFactory.CreateImageViewer(Program.Person.ProfilePicture);
        OpenDialog(panel);
    }
    private void response(Person result, int code, string message) {
        if (!code.Equals((int)HttpStatusCode.OK)) {
                OpenDialog(message, false);
                Debug.Log(code);
        } else {
            List<Ride> upcomingRides = Program.Person.UpcomingRides;
            List<Rate> rates = Program.Person.Rates;
            Program.User.Person = result;
            Program.Person.UpcomingRides = upcomingRides;
            Program.Person.Rates = rates;
            MissionCompleted(ProfilePanel.PANELNAME, "Your account has been edited!");
        }
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
    private void CheckGender(bool gender) {
        if (gender)
            genderDP.value = 0;
        else
            genderDP.value = 1;
    }
    public override void Init() {
        Clear();
        user = Program.User;
        Person person = Program.Person;
        firstName.SetText(person.FirstName);
        lastName.SetText(person.LastName);
        email.SetText(Program.User.Email);
        birthday.text = Program.BirthdayToString(person.Birthday);
        CheckGender(person.Gender);
        if (person.ProfilePicture != null) {
            profilePicture.sprite = Program.GetImage(person.ProfilePicture);
        }
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
            countryDP.value = countryDP.options.FindIndex((i) => { return i.text.Equals(Program.Person.CountryInformations.Name); });
        }
    }
    private int CalculateAge() {
        DateTime birthday = Program.StringToBirthday(this.birthday.text);
        // get the difference in years
        int years = DateTime.Now.Year - birthday.Year;
        // subtract another year if we're before the
        // birth day in the current year
        if (DateTime.Now.Month < birthday.Month || (DateTime.Now.Month == birthday.Month && DateTime.Now.Day < birthday.Day))
            years--;
        return years;
    }
    private bool vadilate() {
        bool valid = true;
        if (firstName.text.text.Equals("")) {
            firstName.Error();
            Panel p = PanelsFactory.CreateDialogBox("Please insert your name", false);
            OpenDialog(p);
            valid = false;
        }
        if (lastName.text.text.Equals("")) {
            lastName.Error();
            Panel p = PanelsFactory.CreateDialogBox("Please insert your last name", false);
            OpenDialog(p);
            valid = false;
        }
        if (birthday.text.Equals("")) {
            Panel p = PanelsFactory.CreateDialogBox("The birthday field can't be empty", false);
            OpenDialog(p);
            valid = false;
        } else {
            int age = CalculateAge();
            if (age < 14) {
                Panel p = PanelsFactory.CreateDialogBox("You are under the legal age", false);
                OpenDialog(p);
                valid = false;
            }
            if (age > 100) {
                Panel p = PanelsFactory.CreateDialogBox("Invalid birthday", false);
                OpenDialog(p);
                valid = false;
            }
        }
        if (!IsValidEmail(email.text.text)) {
            email.Error();
            Panel p = PanelsFactory.CreateDialogBox("Invalid email", false);
            OpenDialog(p);
            valid = false;
        }
        return valid;
    }
    bool IsValidEmail(string email) {
        try {
            var address = new System.Net.Mail.MailAddress(email);
            return address.Address == email;
        } catch {
            return false;
        }
    }

    internal override void Clear() {
        firstName.Reset();
        lastName.Reset();
        birthday.text = Program.BirthdayToString(DateTime.Now.AddYears(-14));
    }
}