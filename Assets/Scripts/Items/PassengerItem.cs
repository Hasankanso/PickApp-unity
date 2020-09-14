using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PassengerItem : Panel
{
  private Panel rideDetail;
  public Text fullName, ratings, bio, persons;
  public Image profilePicture;
  private AddRidePanel addRidePanel;
  public Passenger passenger = null;
  public void Init(Passenger passenger, Panel rideDetail)
  {
    Clear();
    Person person = passenger.User.Person;
    this.passenger = passenger;
    this.rideDetail = rideDetail;
    fullName.text = person.FirstName + " " + person.LastName;
    ratings.text = person.RateAverage + "/5 - " + person.RateCount + " ratings";
    bio.text = person.Bio;
    persons.text = passenger.NumberOfPersons.ToString();
  }
  public void OpenUserDetails()
  {
    UserDetails panel = PanelsFactory.CreateUserDetails();
    rideDetail.Open(panel, () => { panel.Init(passenger.User); });
  }
  internal override void Clear()
  {
    fullName.text = "";
    ratings.text = "";
    bio.text = "";
  }
}
