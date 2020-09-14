using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PassengerItem : Panel
{
  private Panel rideDetail;
  public Text fullName, ratings, bio, persons,luggages;
  public Image profilePicture;
  private AddRidePanel addRidePanel;
  public Passenger passenger = null;
  public void Init(Passenger passenger, Panel rideDetail)
  {
    Clear();
    Person person = passenger.Person;
    this.passenger = passenger;
    this.rideDetail = rideDetail;
    fullName.text = person.FirstName + " " + person.LastName;
    ratings.text = person.RateAverage + "/5 - " + person.RateCount + " ratings";
    bio.text = person.Bio;
    persons.text = passenger.Seats.ToString();
        luggages.text = passenger.Luggages.ToString();
  }
  public void OpenUserDetails()
  {
    UserDetails panel = PanelsFactory.CreateUserDetails();
    rideDetail.Open(panel, () => { panel.Init(passenger.Person); });
  }
  internal override void Clear()
  {
    fullName.text = "";
    ratings.text = "";
    bio.text = "";
  }
}
