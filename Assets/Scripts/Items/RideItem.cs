using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RideItem : MonoBehaviour
{
  public Text time, price, origin, target, driverName, rating;
  public Image profilePicture;
  public Ride ride;
  Action<Ride> OnItemClicked;

  public void Init(Ride r, Action<Ride> OnItemClicked)
  {
    ride = r;
    this.time.text = Program.DateToString(ride.LeavingDate);
    this.price.text = ride.CountryInformations.ToString();
    this.origin.text = ride.From.ToString();
    this.target.text = ride.To.ToString();
    Person driver = ride.Driver;
   // this.driverName.text = driver.FirstName + " " + driver.LastName;
   // this.rating.text = driver.RateAverage + "";
    //this.profilePicture.sprite = Program.GetImage(driver.ProfilePicture);
    this.OnItemClicked = OnItemClicked;
  }

  public void OnClick()
  {
    OnItemClicked(ride);
  }

}
