using Requests;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
public class UserDetails : Panel {
    public Text fullName, bio, chattiness, ratings, acomplishedRides, canceledRides;
    public Image profileImage, message;
    private Person person = null;

    public void Init(User user) {
        this.person = user.Person;
        bio.text = person.Bio;
        fullName.text = person.FirstName + " " + person.LastName;
        ratings.text = person.RateAverage.ToString("0.0") + "/5 - " + person.RateCount + " ratings";
        chattiness.text = person.Chattiness;
        acomplishedRides.text = "Acomplished rides: "+person.AcomplishedRides.ToString();
        canceledRides.text = "Canceled rides: " + person.CanceledRides.ToString();
        if (person.ProfilePicture != null) {
            profileImage.sprite = Program.GetImage(person.ProfilePicture);
        }
        if (user.Driver != null) {
            message.gameObject.SetActive(true);
        }
    }
    public void ReportUser() {
        ReportUserPanel panel = PanelsFactory.CreateReportUser();
        Open(panel, () => { panel.Init(person); });
    }
    public void ContactUser() {
        FooterMenu.dFooterMenu.OpenInboxPanel(person, this);
    }
    internal override void Clear() {
        fullName.text = "";
        ratings.text = "";
        chattiness.text = "";
        message.gameObject.SetActive(false);
    }

    public void Init(Person person)
    {
        this.person =person;
        bio.text = person.Bio;
        fullName.text = person.FirstName + " " + person.LastName;
        Debug.Log(person.Rates.Count.ToString());
        ratings.text = person.RateAverage.ToString("0.0") + "/5 - " + person.Rates.Count.ToString() + " ratings";
        chattiness.text = person.Chattiness;
        profileImage.sprite = Program.GetImage(person.ProfilePicture);
        /* if (Person.Driver != null)
         {
             message.gameObject.SetActive(true);
         } ToDo */
    }
    
}
