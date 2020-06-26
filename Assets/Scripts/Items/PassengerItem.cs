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
    Person person = passenger.User.Person;
        this.passenger = passenger;
        this.rideDetail = rideDetail;
        fullName.text = person.FirstName + " "+ person.LastName;
        ratings.text = person.RateAverage + "/5 - " + person.Rates.Count + " ratings";
        bio.text = person.Bio;
        persons.text = passenger.NumberOfPersons.ToString();
    }
    public void OpenUserDetails() {
        Panel panel = PanelsFactory.CreateUserDetails(passenger.User);
        rideDetail.openCreated(panel);
    }
    internal override void Clear() {
        fullName.text = "";
        ratings.text = "";
        bio.text = "";
    }
}
