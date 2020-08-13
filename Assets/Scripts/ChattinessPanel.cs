using Requests;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class ChattinessPanel : Panel {
    public Dropdown chatiness;


    public override void Init() {
        SetChosenChattiness();
    }

    public void submit() {
        if (Vadilate()) {
            var chattiness = chatiness.options[chatiness.value].text;
            Person oldPerson = Program.Person;
            User oldUser = Program.User;
            Person editedPerson = new Person(oldPerson.id, oldPerson.FirstName, oldPerson.LastName, chattiness, oldPerson.Phone,
            oldPerson.CountryInformations, oldPerson.Bio, oldPerson.RateAverage, oldPerson.Gender, oldPerson.Birthday,
            DateTime.Now, oldPerson.profilePictureUrl);
            User editedUser = new User(editedPerson, oldUser.Driver, oldUser.phone, oldUser.password, oldUser.Email, oldUser.Id, oldUser.Token);
            Request<User> request = new EditAccount(editedUser);
            request.AddSendListener(OpenSpinner);
            request.AddReceiveListener(CloseSpinner);
            request.Send(response);
        }
    }
    private void response(User result, int code, string message) {
        if (!code.Equals(HttpStatusCode.OK)) {
            OpenDialog("There was an error adding chattiness", false);
        } else {
            MissionCompleted(ProfilePanel.PANELNAME, "Chattiness has been added");
        }
    }

    private void SetChosenChattiness() {
        Person person = Program.Person;
        if (person.Chattiness.Equals("I'm a quiet person"))
            chatiness.value = 0;
        else if (person.Chattiness.Equals("I love to chat!"))
            chatiness.value = 2;
        else
            chatiness.value = 1;
    }
    private bool Vadilate() {
        if (chatiness.options[chatiness.value].text.Equals("") || chatiness.options[chatiness.value].text == null) {
            OpenDialog("Please choose chattiness", false);
        }
        return true;
    }
    internal override void Clear() {
        throw new NotImplementedException();
    }
}