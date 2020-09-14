using Requests;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class BioPanel : Panel {
    public InputFieldScript bio;

    public override void Init() {
        Clear();
        if (!string.IsNullOrEmpty(Program.Person.Bio)) {
            bio.SetText(Program.Person.Bio);
        }
    }
    public void submit() {
        if (Validate()) {
            Person oldPerson = Program.Person;
            Person editedPerson = new Person(oldPerson.id, oldPerson.FirstName, oldPerson.LastName, oldPerson.Chattiness, oldPerson.Phone,
            oldPerson.CountryInformations, bio.text.text, oldPerson.RateAverage, oldPerson.Gender, oldPerson.Birthday,
             oldPerson.profilePictureUrl);
            Request<Person> request = new EditAccount(editedPerson);
            request.AddSendListener(OpenSpinner);
            request.AddReceiveListener(CloseSpinner);
            request.Send(Response);
        }
    }
    private void Response(Person result, int code, string message) {
        if (!code.Equals((int)HttpStatusCode.OK)) {
            OpenDialog(message, false);
            Debug.Log(code);
        } else {
            List<Ride> upcomingRides = Program.Person.UpcomingRides;
            List<Rate> rates = Program.Person.Rates;
            Program.User.Person = result;
            Program.Person.UpcomingRides = upcomingRides;
            Program.Person.Rates = rates;
            MissionCompleted(ProfilePanel.PANELNAME, "Your bio has been edited!");
        }
    }

    private bool Validate() {
        bool valid = true;
        if (bio.text.text.Equals("")) {
            bio.Error();
            OpenDialog("There was an error adding bio", false);
            valid = false;
        } else
        if (bio.text.text.Length < 20) {
            bio.Error();
            OpenDialog("Bio is too short", false);
            valid = false;
        } else
        if (bio.text.text.Length > 190) {
            bio.Error();
            OpenDialog("Bio is too long", false);
            valid = false;
        }
        return valid;
    }
    internal override void Clear() {
        bio.Reset();
    }
}
