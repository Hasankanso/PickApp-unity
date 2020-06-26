using Requests;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
public class RideDetails : Panel
{
  public ListView listView;
  public Text from, to, price, date, time, comment, seats, luggages, driverFullName, driverBio, driverRatings, carName, carBrand, carColor, carYear;
  public Image carImage, profileImage, rideMapImage, smokingImage, musicImage, acImage, kidsSeatImage, petsImage;
  public Sprite smokingOnSpirite, musicOnSpirite, acOnSpirite, kidsSeatOnSpirite, petsOnSpirite;
  public Sprite smokingOffSpirite, musicOffSpirite, acOffSpirite, kidsSeatOffSpirite, petsOffSpirite;
  public GameObject dayOfWeek, passengersContainer, contentScrollView, personsDialog;

  public GameObject addScheduleButton, updateScheduleButton, editScheduleButton, removeScheduleButton; //schedule
  public GameObject addRideButton, updateRideButton, editRideButton, removeRideButton; //Ride
  public GameObject reserveSeatsButton; //reserve

  public Text monday, tuesday, wednesday, thursday, friday, saturday, sunday;
  public Dropdown personsDropdown;
  private Car car = null;

  //if this panel opened to view a Ride details
  private Ride ride = null;

  //if this panel opened to view Schedule details
  private ScheduleRide schedule;


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

  public void UserDetails()
  {
    Panel panel = PanelsFactory.CreateUserDetails(ride.User);
    openCreated(panel);
  }

  public void GoWithHim()
  {
    personsDialog.gameObject.SetActive(true);
  }

  public void ClosePersonDialog()
  {
    personsDialog.gameObject.SetActive(false);
  }

  public void EditRide()
  {
    FooterMenu.dFooterMenu.OpenAddRidePanel(this, ride);
  }

  public void EditSchedule()
  {
    Panel p = PanelsFactory.CreateEditSchedule(schedule);
    openCreated(p);
  }


  public void Init(ScheduleRide schedule, Status prevStatus)
  {
    Init(schedule.Ride);
    StatusProperty = prevStatus;
    if (StatusProperty == Status.ADD)
    {
      addScheduleButton.SetActive(true);
    }
    else if (StatusProperty == Status.UPDATE)
    {
      updateScheduleButton.SetActive(true);
    }
    else if (StatusProperty == Status.VIEW)
    {
      editScheduleButton.SetActive(true);
      removeScheduleButton.SetActive(true);
    }

    this.schedule = schedule;
    date.text = Program.DateToString(schedule.StartDate)+" - "+ Program.DateToString(schedule.EndDate);
    SetWeekDays(schedule.IsMonday, schedule.IsTuesday, schedule.IsWednesday, schedule.IsThursday, schedule.IsFriday, schedule.IsSaturday, schedule.IsSunday);
    dayOfWeek.SetActive(true);
  }

  //Init SearchResult
  //Init MyRidesPanel
  public void Init(Ride ride, bool owner, Status prevStatus)
  {
    Init(ride);
    StatusProperty = prevStatus;
    if (StatusProperty == Status.ADD)
    {
      addRideButton.SetActive(true);
    }
    else if (StatusProperty == Status.UPDATE)
    {
      updateRideButton.SetActive(true);
    }
    else if (StatusProperty == Status.VIEW)
    {
      if (owner)
      {
        ImplementPassengersList(ride.Passengers);
        editRideButton.SetActive(true);
        removeRideButton.SetActive(true);
      }
      else
      {
        reserveSeatsButton.SetActive(true);
      }
    }
  }


  //Basic Init
  private void Init(Ride ride)
  {
    Clear();
    
    this.ride = ride;
    this.car = ride.Car;
    Person person = ride.User.Person;
    date.text = Program.DateToString(ride.LeavingDate);
    from.text = ride.From.Name;
    to.text = ride.To.Name;
    price.text = ride.Price + " " + ride.CountryInformations.Unit;
    comment.text = ride.Comment;
    seats.text = ride.AvailableSeats.ToString();
    luggages.text = ride.AvailableLuggages.ToString();
    driverFullName.text = person.FirstName + " " + person.LastName;
    driverBio.text = person.Bio;
    //driverRatings.text = ride.Driver.RateAverage.ToString() + "/5 - " + ride.Driver.Rates.Count.ToString() + " ratings";
    carName.text = car.Name;
    carBrand.text = car.Brand;
    carYear.text = car.Year.ToString();
    SetColor(car.Color);
    SetPermissions(ride.SmokingAllowed, ride.AcAllowed, ride.PetsAllowed, ride.MusicAllowed, ride.KidSeat);
    if (car.Picture!=null) {
        carImage.sprite = Program.GetImage(car.Picture);
    }
    if (person.ProfilePicture!=null) {
        profileImage.sprite = Program.GetImage(person.ProfilePicture);
    }

    if(ride.Map ==null){
        StartCoroutine(Panel.RequestImage(ride.MapUrl, Succeed, Error));
    } else {
      rideMapImage.sprite = Program.GetImage(ride.Map);
    }
  }

  private void Error(string obj)
  {
    OpenDialog("man nzlet l soura", false);
  }

  private void Succeed(Texture2D map)
  {
    rideMapImage.sprite = Program.GetImage(map);
  }

  private void ImplementPassengersList(List<Passenger> passengers)
  {
    if (passengers != null)
    {
      listView.Clear();
      contentScrollView.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 2399.4f);
      passengersContainer.SetActive(true);
      foreach (Passenger o in passengers)
      {
        var obj = ItemsFactory.CreatPassengerItem(o, this);
        listView.Add(obj.gameObject);
      }
    }
  }

  public void SetWeekDays(bool isMonday, bool isTuesday, bool isWednesday, bool isThursday, bool isFriday, bool isSaturday, bool isSunday)
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
  private void SetColor(string color)
  {
    if (color.Equals("#000000"))
    {
      carColor.text = "Black";
    }
    else if (color.Equals("#ffffff"))
    {
      carColor.text = "White";
    }
    else if (color.Equals("#9F9F9F"))
    {
      carColor.text = "Grey";
    }
    else if (color.Equals("#767676"))
    {
      carColor.text = "Dark Grey";
    }
    else if (color.Equals("#FF0000"))
    {
      carColor.text = "Red";
    }
    else if (color.Equals("#004BFF"))
    {
      carColor.text = "Blue";
    }
    else if (color.Equals("#001196"))
    {
      carColor.text = "Dark Blue";
    }
    else if (color.Equals("#FFFC00"))
    {
      carColor.text = "Yellow";
    }
    else if (color.Equals("#FFA5EA"))
    {
      carColor.text = "Pink";
    }
    else if (color.Equals("#6A0DAD"))
    {
      carColor.text = "Purple";
    }
    else if (color.Equals("#964B00"))
    {
      carColor.text = "Brown";
    }
    else if (color.Equals("#FFA500"))
    {
      carColor.text = "Orange";
    }
    else if (color.Equals("#00DB00"))
    {
      carColor.text = "Green";
    }
  }

  internal override void Clear()
  {
    addRideButton.SetActive(false);
    editRideButton.SetActive(false);
    removeRideButton.SetActive(false);
    updateRideButton.SetActive(false);

    reserveSeatsButton.SetActive(false);

    editScheduleButton.SetActive(false);
    addScheduleButton.SetActive(false);

    from.text = "";
    to.text = "";
    price.text = "";
    date.text = "";
    time.text = "";
    comment.text = "";
    seats.text = "";
    luggages.text = "";
    driverFullName.text = "";
    driverBio.text = "";
    driverRatings.text = "";
    carName.text = "";
    carBrand.text = "";
    carColor.text = "";
    carYear.text = "";
    dayOfWeek.SetActive(false);
    SetPermissions(false, false, false, false, false);
    SetWeekDays(false, false, false, false, false, false, false);
    contentScrollView.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 1781.1f);
    passengersContainer.SetActive(false);
  }


  // TODO Implement send and receive functions
  public void AddSchedule()
  {
    Request<ScheduleRide> request = new AddScheduleRide(schedule);
    request.Send(AddScheduleResponse);
  }


  public void UpdateSchedule()
  {

  }

  public void RemoveSchedule(){

  }


  public void AddRide()
  {
    Request<Ride> request = new AddRide(ride);
    request.Send(AddRideResponse);
  }

  public void UpdateRide()
  {
    Request<Ride> request = new ReserveSeat(ride, Program.User, personsDropdown.value + 1);
    request.Send(ReserveSeatsResponse);
  }

  public void ReserveSeat()
  {
    Request<Ride> request = new ReserveSeat(ride, Program.User, personsDropdown.value + 1);
    request.Send(ReserveSeatsResponse);
  }

  public void RemoveRide(){
   //Request
  }

  public void AddScheduleResponse(ScheduleRide result, HttpStatusCode code, string message)
  {
    //check if schedule Ride add in server success
  }

  public void EditScheduleResponse(ScheduleRide result, HttpStatusCode code, string message)
  {
    //check if schedule Ride add in server success
  }

  public void RemoveScheduleResponse(ScheduleRide result, HttpStatusCode code, string message)
  {
    //check if schedule Ride remove in server success
  }

  private void AddRideResponse(Ride result, HttpStatusCode code, string message)
  {
    if (!code.Equals(HttpStatusCode.OK)) Debug.Log("no results");
        Program.Person.UpcomingRides.Add(result);
        FooterMenu.dFooterMenu.OpenYourRidesPanel();
        Debug.Log("Got Response from servaa");
        this.destroy();
  }

  public void EditRideResponse(ScheduleRide result, HttpStatusCode code, string message)
  {
    //check if schedule Ride add in server success
  }

  public void RemoveRideResponse(ScheduleRide result, HttpStatusCode code, string message)
  {
    //check if schedule Ride add in server success
  }

  private void ReserveSeatsResponse(Ride result, HttpStatusCode code, string message)
  {
    if (!code.Equals(HttpStatusCode.OK))
    {
      OpenDialog("Something went wrong", false);
    }
    else
    {
      OpenDialog("You reserved a seat(s).", true);
      Panel panel = PanelsFactory.createSearch();
      openExisted(panel);
    }
  }

  // end send and receive functions


}
