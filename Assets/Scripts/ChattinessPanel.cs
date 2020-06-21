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
    private Person person = null;
    private void Start() {
        GetChoosenChattiness();
    }
    public void submit() {
        if (vadilate()) {
            person.Chattiness = chatiness.options[chatiness.value].text;
            Request<Person> request = new EditAccount(person);
            request.Send(response);
        }
    }
    private void response(Person result, HttpStatusCode code, string message) {
        if (!code.Equals(HttpStatusCode.OK)) {
            OpenDialog("There was an error adding chattiness", false);
        } else {
            OpenDialog("Chattiness has been added", false);
            Panel panel = PanelsFactory.createProfile();
            openCreated(panel);
        }
    }
    public void init(Person person) {
        this.person = person;
    }
    private void GetChoosenChattiness() {
        if (person.Chattiness.Equals("I'm the quiet person"))
            chatiness.value = 0;
        else if (person.Chattiness.Equals("I love to chat!"))
            chatiness.value = 2;
        else
            chatiness.value = 1;
    }
    private bool vadilate() {
        if (chatiness.options[chatiness.value].text.Equals("") || chatiness.options[chatiness.value].text == null) {
            OpenDialog("Please choose chattiness", false);
        }
        return true;
    }
    internal override void Clear() {
        throw new NotImplementedException();
    }
}