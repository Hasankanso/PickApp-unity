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
    private Person person = null;

    public void Start() {
        Clear();
        GetChoosenBio();
    }
    public void submit() {
        if (validate()) {
            person.Bio = bio.text.text;
            Request<Person> request = new EditAccount(person);
            Task.Run(() => request.Send(response));
        }
    }
    private void response(Person result, HttpStatusCode code, string message) {
        if (!code.Equals(HttpStatusCode.OK)) {
            Panel p = PanelsFactory.CreateDialogBox("There was an error adding bio", false);
            OpenDialog(p);
        } else {
            Panel p = PanelsFactory.CreateDialogBox("Bio has been added", true);
            OpenDialog(p);
            Panel panel = PanelsFactory.createProfile();
            openCreated(panel);
        }
    }
    public void init(Person person) {
        this.person = person;
    }
    private void GetChoosenBio() {
        if (person.Bio.Length > 0)
            bio.SetText(person.Bio);
    }
    private bool validate() {
        bool valid = true;
        if (bio.text.text.Equals("")) {
            bio.Error();
            Panel p = PanelsFactory.CreateDialogBox("There was an error adding bio", false);
            OpenDialog(p);
            valid = false;
        }
        if (bio.text.text.Length < 20) {
            bio.Error();
            Panel p = PanelsFactory.CreateDialogBox("Bio is too short", false);
            OpenDialog(p);
            valid = false;
        }
        if (bio.text.text.Length > 190) {
            bio.Error();
            Panel p = PanelsFactory.CreateDialogBox("Bio is too long", false);
            OpenDialog(p);
            valid = false;
        }
        return valid;
    }
    internal override void Clear() {
        //clear content of all inptfield.
        bio.Reset();
    }
}
