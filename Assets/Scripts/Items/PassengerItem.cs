using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PassengerItem : Panel {
    private Panel rideDetail;
    public Text fullName, ratings, bio,persons;
    public Image profilePicture;
    private AddRidePanel addRidePanel;
    public Passenger passenger = null;
    public void Init(Passenger passenger, Panel rideDetail) {
        Clear();
        this.passenger = passenger;
        this.rideDetail = rideDetail;
        fullName.text = passenger.Person.FirstName + " "+ passenger.Person.LastName;
        ratings.text = passenger.Person.RateAverage + "/5 - " + passenger.Person.Rates.Count + " ratings";
        bio.text = passenger.Person.Bio;
        persons.text = passenger.NumberOfPersons.ToString();
    }
    public void OpenUserDetails() {
        Panel panel = PanelsFactory.CreateUserDetails(passenger.Person, true);
        rideDetail.openCreated(panel);
    }
    internal override void Clear() {
        fullName.text = "";
        ratings.text = "";
        bio.text = "";
    }
}
