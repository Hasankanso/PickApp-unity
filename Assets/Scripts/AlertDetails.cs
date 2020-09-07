using Requests;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class AlertDetails : Panel
{   //todo

    public Text rideOrigin, rideTarget, ClientsNb, luggage, ridePrice, rideComment;
    public Person person;
    public Text minDate, maxDate;
    public GameObject confirmModel;
    private Alert alert;

    public void Init(Alert alert) {
        this.rideOrigin.text= alert.From.ToString();
        this.rideTarget.text = alert.To.ToString();
        this.ClientsNb.text = alert.NumberOfPersons.ToString();
        this.luggage.text = alert.NumberOfLuggage.ToString();
        this.ridePrice.text = alert.Price.ToString()+ Program.Person.CountryInformations.Unit.ToString();
        this.rideComment.text = alert.Comment;
        this.person = alert.User.Person;
        this.minDate.text = alert.MinDate.ToString();
        this.maxDate.text = alert.MaxDate.ToString();
        this.alert = alert;
    }
    public void ConfirmRideRequest()
    {
        FooterMenu.dFooterMenu.OpenAddRidePanel(this, alert);
    }
    public void openModel()
    {
        confirmModel.SetActive(true);
    }
    public void closeModel()
    {
        confirmModel.SetActive(false);
    }
    internal override void Clear()
    {
        closeModel();
    }
}
