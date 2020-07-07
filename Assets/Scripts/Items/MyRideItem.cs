using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyRideItem : Panel
{
  private MyRidePanel myRidesPanel;
  public Text from, to, price, date;
  public Image smokingImage, musicImage, acImage, kidsSeatImage, petsImage;
  public Sprite smokingOnSpirite, musicOnSpirite, acOnSpirite, kidsSeatOnSpirite, petsOnSpirite;
  public Sprite smokingOffSpirite, musicOffSpirite, acOffSpirite, kidsSeatOffSpirite, petsOffSpirite;
  private Ride ride = null;
  private Person person = null;

  public void Init(Ride ride, Person person, MyRidePanel myRidesPanel)
  {
    Clear();
    this.person = person;
    this.myRidesPanel = myRidesPanel;
    this.ride = ride;
    from.text = ride.From.Name;
    to.text = ride.To.Name;
    price.text = ride.Price + " " + ride.CountryInformations.Unit;
    date.text = Program.DateToString(ride.LeavingDate);
    SetPermissions(ride.SmokingAllowed, ride.AcAllowed, ride.PetsAllowed, ride.MusicAllowed, ride.KidSeat);
    }

  public void OpenRideDetails()
  {   
        if (ride.Driver != null)
        {
            bool owner = Program.Driver.Equals(ride.Driver);
            Panel panel = PanelsFactory.CreateRideDetails(true, ride, owner, StatusE.VIEW);


            myRidesPanel.openCreated(panel);
        }
      //  Debug.Log("ride.Passengers 1 " + ride.Passengers);
       /* foreach(Passenger p in ride.Passengers)
        {
          if (p.isReserved(p)) return;
        }*/
       // Debug.Log("ride.Passengers 2 " + ride.Passengers);
    }

  private void SetPermissions(bool isSmokingAllowed, bool isACAllowed, bool isPetsAllowed, bool isMusicAllowed, bool isKidsSeat)
  {
    smokingImage.sprite = smokingOffSpirite;
    acImage.sprite = acOffSpirite;
    petsImage.sprite = petsOffSpirite;
    musicImage.sprite = musicOffSpirite;
    kidsSeatImage.sprite = kidsSeatOffSpirite;
    if (isSmokingAllowed)
      smokingImage.sprite = smokingOnSpirite;
    if (isACAllowed)
      acImage.sprite = acOnSpirite;
    if (isPetsAllowed)
      petsImage.sprite = petsOnSpirite;
    if (isMusicAllowed)
      musicImage.sprite = musicOnSpirite;
    if (isKidsSeat)
      kidsSeatImage.sprite = kidsSeatOnSpirite;
  }
  internal override void Clear()
  {
    from.text = "";
    to.text = "";
    price.text = "";
    date.text = "";
    SetPermissions(false, false, false, false, false);
  }
}
