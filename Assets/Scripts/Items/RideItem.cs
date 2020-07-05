using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.VFX;

public class RideItem : MonoBehaviour
{
  public Text time, price, origin, target, driverName, rating;
  public Image star;
  public Image profilePicture;
  public Ride ride;
  Action<Ride> OnItemClicked;

  public void Init(Ride ride, Action<Ride> OnItemClicked)
  {
    this.ride = ride;
    Person driver = ride.User.Person;

    if (this.ride.LeavingDate.Year == DateTime.Now.Year)
    {
      time.text = Program.DateToString(this.ride.LeavingDate, true);
    }

    price.text = this.ride.Price + " " + this.ride.CountryInformations.Unit;
    this.origin.text = this.ride.From.ToString();
    this.target.text = this.ride.To.ToString();
    this.driverName.text = driver.FirstName + " " + driver.LastName;
    this.rating.text = driver.RateAverage + "";

    float r = 1f;
    float g = 1f;
    float rate = driver.RateAverage / 5;

    if (rate > 0.5f)
    {
      rate = (rate - 0.5f) * 2;
      r = (int)(255 - 155 * rate)/255f;
    } else if (rate < 0.5f)
    {
      rate *= 2;
      g = (int)(255 - 155 * rate)/255f;
    }

    star.color = new Color(r, g, 0f, 1f);

    this.OnItemClicked = OnItemClicked;
  }

  public void SetPPicture(Texture2D img)
  {
    ride.User.Person.profilePicture = img;
    this.profilePicture.sprite = Program.GetImage(ride.User.Person.profilePicture);
  }

  public void OnClick()
  {
    OnItemClicked(ride);
  }

}
