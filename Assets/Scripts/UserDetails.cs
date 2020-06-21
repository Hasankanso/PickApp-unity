using Requests;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
public class UserDetails : Panel {
    public Text fullName, bio, chattiness, ratings;
    public Image profileImage,message;
    private Person person = null;

    public void Init(Person person,bool isDriver) {
        this.person = person;
        bio.text = person.Bio;
        fullName.text = person.FirstName + " " + person.LastName;
        ratings.text = person.RateAverage.ToString() + "/5 - " + person.Rates.Count + " ratings";
        chattiness.text = person.Chattiness;
        profileImage.sprite = Program.GetImage(person.ProfilePicture);
        if (isDriver) {
            message.gameObject.SetActive(true);
        }
    }
    public void ReportUser() {
        Panel panel = PanelsFactory.createReportUser(person);
        openCreated(panel);
    }
    public void ContactUser() {
        FooterMenu.dFooterMenu.OpenInboxPanel(person,this);
    }
    internal override void Clear() {
        fullName.text = "";
        ratings.text = "";
        chattiness.text = "";
        message.gameObject.SetActive(false);
    }
}
