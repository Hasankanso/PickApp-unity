using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;
public class ScheduleItem : Panel
{
  private Panel profilePanel;
  public Text from, to, price, startDate, endDate, date, seats, luggages;
  public Image smokingImage, musicImage, acImage, kidsSeatImage, petsImage;
  public Sprite smokingOnSpirite, musicOnSpirite, acOnSpirite, kidsSeatOnSpirite, petsOnSpirite;
  public Sprite smokingOffSpirite, musicOffSpirite, acOffSpirite, kidsSeatOffSpirite, petsOffSpirite;
  public Text monday, tuesday, wednesday, thursday, friday, saturday, sunday;
  public ScheduleRide scheduleRide = null;

  public void init(ScheduleRide scheduleRide, Panel profilePanel)
  {
    Clear();
    this.scheduleRide = scheduleRide;
    from.text = scheduleRide.Ride.From.Name;
    to.text = scheduleRide.Ride.To.Name;
    price.text = scheduleRide.Ride.Price + scheduleRide.Ride.CountryInformations.Unit;
    endDate.text = Program.BirthdayToString(scheduleRide.EndDate);
    startDate.text = Program.BirthdayToString(scheduleRide.StartDate);
    date.text = scheduleRide.Ride.LeavingDate.ToString("hh:mm tt", CultureInfo.InvariantCulture);
    seats.text = scheduleRide.Ride.AvailableSeats.ToString() + " Seats";
    luggages.text = scheduleRide.Ride.AvailableLuggages.ToString() + " Luggages";
    this.profilePanel = profilePanel;
    GetPermissions(scheduleRide.Ride.SmokingAllowed, scheduleRide.Ride.AcAllowed, scheduleRide.Ride.PetsAllowed, scheduleRide.Ride.MusicAllowed, scheduleRide.Ride.KidSeat);
    GetWeekDays(scheduleRide.IsMonday, scheduleRide.IsTuesday, scheduleRide.IsWednesday, scheduleRide.IsThursday, scheduleRide.IsFriday, scheduleRide.IsSaturday, scheduleRide.IsSunday);
  }
  public void GetWeekDays(bool isMonday, bool isTuesday, bool isWednesday, bool isThursday, bool isFriday, bool isSaturday, bool isSunday)
  {
    if (isMonday)
      monday.color = new Color(255f / 255f, 188f / 255f, 66f / 255f);
    if (isTuesday)
    {
      tuesday.color = new Color(255f / 255f, 188f / 255f, 66f / 255f);
    }
    if (isWednesday)
    {
      wednesday.color = new Color(255f / 255f, 188f / 255f, 66f / 255f);
    }
    if (isThursday)
    {
      thursday.color = new Color(255f / 255f, 188f / 255f, 66f / 255f);
    }
    if (isFriday)
    {
      friday.color = new Color(255f / 255f, 188f / 255f, 66f / 255f);
    }
    if (isSaturday)
    {
      saturday.color = new Color(255f / 255f, 188f / 255f, 66f / 255f);
    }
    if (isSunday)
    {
      sunday.color = new Color(255f / 255f, 188f / 255f, 66f / 255f);
    }
  }
  private void GetPermissions(bool isSmokingAllowed, bool isACAllowed, bool isPetsAllowed, bool isMusicAllowed, bool isKidsSeat)
  {
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
  //hide o click outside
  /*  private void HideIfClickedOutside() {
        if (Input.GetMouseButton(0) && OptionsContainer.activeSelf &&
            !RectTransformUtility.RectangleContainsScreenPoint(OptionsContainer.GetComponent<RectTransform>(), Input.mousePosition)) {
            OptionsContainer.SetActive(false);
        }
    }*/
  public void EditSchedule()
  {
    SchedulePanel panel = PanelsFactory.CreateAddSchedule();
    Open(panel, () => { panel.Init(scheduleRide); });
  }
  public void OnClick()
  {
    RideDetails rideDetailsPanel = PanelsFactory.CreateRideDetails();
    profilePanel.Open(rideDetailsPanel, () => { rideDetailsPanel.Init(scheduleRide, StatusE.VIEW); });
  }
  internal override void Clear()
  {
    from.text = "";
    to.text = "";
    price.text = "";
    startDate.text = "";
    endDate.text = "";
    date.text = "";
    seats.text = "";
    luggages.text = "";
    GetWeekDays(false, false, false, false, false, false, false);
    GetPermissions(false, false, false, false, false);
  }
}
