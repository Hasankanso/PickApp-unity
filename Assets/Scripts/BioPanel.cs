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
        bio.SetText(Program.Person.Bio);
    }
    public void submit() {
        if (Validate()) {
            Person oldPerson = Program.Person;
            User oldUser = Program.User;
            Person editedPerson = new Person(oldPerson.id, oldPerson.FirstName, oldPerson.LastName, oldPerson.Chattiness, oldPerson.Phone,
            oldPerson.CountryInformations, bio.text.text, oldPerson.RateAverage, oldPerson.Gender, oldPerson.Birthday,
            DateTime.Now, oldPerson.profilePictureUrl);
            User editedUser = new User(editedPerson, oldUser.Driver, oldUser.phone, oldUser.password, oldUser.Email, oldUser.Id, oldUser.Token);
            Request<User> request = new EditAccount(editedUser);
            request.AddSendListener(OpenSpinner);
            request.AddReceiveListener(CloseSpinner);
            request.Send(Response);
        }
    }
    private void Response(User result, int code, string message) {
        if (!code.Equals(HttpStatusCode.OK)) {
            OpenDialog("There was an error adding bio", false);
        } else {
            OpenDialog("Bio has been added", true);
            MissionCompleted(ProfilePanel.PANELNAME, "Bio has been added");
        }
    }

    private bool Validate() {
        bool valid = true;
        if (bio.text.text.Equals("")) {
            bio.Error();
            OpenDialog("There was an error adding bio", false);
            valid = false;
        }
        if (bio.text.text.Length < 20) {
            bio.Error();
            OpenDialog("Bio is too short", false);
            valid = false;
        }
        if (bio.text.text.Length > 190) {
            bio.Error();
            OpenDialog("Bio is too long", false);
            valid = false;
        }
        return valid;
    }
    internal override void Clear() {
        //clear content of all inptfield.
        bio.Reset();
    }
}
