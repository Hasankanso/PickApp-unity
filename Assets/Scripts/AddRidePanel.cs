using Requests;
using System;
using System.Net;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class AddRidePanel : Panel
{
  public InputFieldScript from, to, comment, price, stopTimeField;
  public Text date, titleFirstView, titleSecView;
  public GameObject firstView, secondView;

  private DirectionsFinderPanel directionsPanel;
  private CarsListPanel carPickerPanel;

  public ToggleUi isMusicAllowed, isPetsAllowed, isAcAllowed, isSmokingAllowed;
  public Toggle kidSeats;

  public Toggle stopTimeCheckBox;
  public GameObject stopTimeTextToShow;

  public Dropdown numberOfSeats;
  public Dropdown numberOfLuggages;
  private ScheduleRide schedule;

  public GameObject backButton;

  private Ride ride;
  private bool isAlertDetail = false;
  private Alert alert;

  Texture2D map;
  Car car;

  private Location fromL, toL;

  //FirstView <--> secondView <--> MapPicker <--> CarPicker <--> RideDetails
  public void Init()
  {
    Clear();
    Status = StatusE.ADD;
  }

  public void Init(Ride ride)
  {
    Clear();
    titleFirstView.text = "Edit Ride";
    titleSecView.text = titleFirstView.text;
    fromL = ride.From;
    toL = ride.To;
    this.ride = ride;
    Status = StatusE.UPDATE;
    if (ride.StopTime != 0 && !ride.StopTime.ToString().Equals(""))
    {
      Debug.Log(ride.StopTime);
      stopTimeCheckBox.isOn = true;
      stopTimeTextToShow.SetActive(true);
      stopTimeField.SetText(ride.StopTime.ToString());
    }
    Debug.Log(ride.id);
    from.SetText(ride.From.Name.ToString());
    to.SetText(ride.To.Name);
    comment.SetText(ride.Comment);
    Debug.Log(ride.From.Name.ToString());
    price.SetText(ride.Price.ToString());
    date.text = Program.DateToString(ride.LeavingDate);
    isMusicAllowed.isOn = ride.MusicAllowed;
    isPetsAllowed.isOn = ride.PetsAllowed;
    isAcAllowed.isOn = ride.AcAllowed;
    isSmokingAllowed.isOn = ride.SmokingAllowed;
    kidSeats.isOn = ride.KidSeat;
    numberOfLuggages.value = ride.AvailableLuggages - 1;
    numberOfSeats.value = ride.AvailableSeats - 1;
    map = ride.Map;
    car = ride.Car;
  }

  public void Init(Alert alert)
  {
    Clear();
    Status = StatusE.ADD;
    from.SetText(alert.From.Name);
    to.SetText(alert.To.Name);
    price.SetText(alert.Price.ToString());
    numberOfLuggages.value = alert.NumberOfLuggages - 1;
    numberOfSeats.value = alert.NumberOfPersons - 1;
    from.GetComponent<InputField>().enabled = false;
    from.GetComponent<InputField>().readOnly = true;
    to.GetComponent<InputField>().enabled = false;
    price.GetComponent<InputField>().enabled = false;
    numberOfLuggages.enabled = false;
    numberOfSeats.enabled = false;
    isAlertDetail = true;
    this.alert = alert;
  }

  public void Init(ScheduleRide schedule)
  {
    Clear();
    this.schedule = schedule;
    if (schedule != null)
    {
      backButton.SetActive(true);
    }
  }

  public void OpenMyCarsPicker()
  {

    if (ValidateSecondView())
    {
      if (!Program.IsLoggedIn)
      {
        Panel login = PanelsFactory.createLogin(false);
        openCreated(login);
        return; //end this function
      }

      DateTime wholeDate = Program.StringToDate(date.text);
      int stopTime = 0;
      if (stopTimeCheckBox.isOn)
      {
        stopTime = int.Parse(stopTimeField.text.text);
      }
      if (Status == StatusE.UPDATE)
        ride = new Ride(ride.id, Program.User, null, fromL, toL, comment.text.text, price.text.text,
  wholeDate, isMusicAllowed.isOn, isAcAllowed.isOn, isSmokingAllowed.isOn, isPetsAllowed.isOn, kidSeats.isOn,
  int.Parse(numberOfSeats.options[numberOfSeats.value].text), int.Parse(numberOfLuggages.options[numberOfLuggages.value].text),
  stopTime, null, null);
      else
        ride = new Ride(null, Program.User, null, fromL, toL, comment.text.text, price.text.text,
  wholeDate, isMusicAllowed.isOn, isAcAllowed.isOn, isSmokingAllowed.isOn, isPetsAllowed.isOn, kidSeats.isOn,
  int.Parse(numberOfSeats.options[numberOfSeats.value].text), int.Parse(numberOfLuggages.options[numberOfLuggages.value].text),
  stopTime, null, null);

      if (carPickerPanel == null)
      {
        carPickerPanel = PanelsFactory.CreateCarsListPanel(OnCarPicked, car);
        openCreated(carPickerPanel);
      }
      else
      {
        carPickerPanel.Init(OnCarPicked, car);
        openExisted(carPickerPanel);
      }
    }
  }

  private void OpenDirectionsPanel()
  {
    if (directionsPanel != null)
    {
      directionsPanel.Init(map, from.text.text, to.text.text, true, OnMapPicked);
      carPickerPanel.openExisted(directionsPanel);
    }
    else
    {
      directionsPanel = PanelsFactory.CreateDirectionFinderPanel(map, from.text.text, to.text.text, true, OnMapPicked);
      carPickerPanel.openCreated(directionsPanel);
    }
  }

  private void OnMapPicked(Texture2D map)
  {
    ride.Map = map;
    this.map = map;
    OpenRideDetails();
  }

  public void OpenRideDetails()
  {
    if (schedule == null)
    {
      RideDetails rideDetailsPanel = PanelsFactory.CreateRideDetails();
      Open(rideDetailsPanel, () => { rideDetailsPanel.Init(ride); });
    }
    else
    {
      schedule.Ride = ride;
      RideDetails rideDetailsPanel = PanelsFactory.CreateScheduleDetails(schedule, directionsPanel.Status);
      directionsPanel.openCreated(rideDetailsPanel);
    }
  }


  public void OnCarPicked(Car car)
  {
    ride.Car = car;
    this.car = car;
    OpenDirectionsPanel();
  }

  public void OpenNotLoginPanel()
  {
    Panel p = PanelsFactory.createLogin(false);
    openCreated(p);
  }

  public void OpenDateTimePicker()
  {
    OpenDateTimePicker((dt) => OnDatePicked(dt));
  }
  private void OnDatePicked(DateTime d)
  {
    date.text = Program.DateToString(d);
  }

  public void openView(int index)
  {
    if (index == 0)
    {
      firstView.SetActive(true);
      secondView.SetActive(false);
    }
    else if (index == 1 && ValidateFirstView())
    {
      firstView.SetActive(false);
      secondView.SetActive(true);
    }

  }

  public void closeView(int index)
  {
    if (index == 0)
    {
      firstView.SetActive(true);
      secondView.SetActive(false);
    }
    else if (index == 1)
    {
      firstView.SetActive(false);
      secondView.SetActive(true);
    }
  }

  public void FirstToggle()
  {
    if (stopTimeCheckBox.isOn)
    {
      stopTimeTextToShow.SetActive(true);
    }
    else
    {
      stopTimeTextToShow.SetActive(false);
    }
  }

  public void OpenFromLocationPicker()
  {
    OpenLocationFinder(from.text.text, OnFromLocationPicked);
  }

  public void OpenToLocationPicker()
  {
    OpenLocationFinder(to.text.text, OnToLocationPicked);
  }

  public void OnFromLocationPicked(Location loc)
  {
    fromL = loc;
    from.GetComponent<InputField>().text = fromL.Name;
    from.PlaceHolder();
  }

  public void OnToLocationPicked(Location loc)
  {
    toL = loc;
    to.GetComponent<InputField>().text = toL.Name;
    to.PlaceHolder();
  }

  private bool ValidateFirstView()
  {
    bool valid = true;
    if (from.text.text.Equals(""))
    {
      from.Error();
      valid = false;
      OpenDialog("Please fill \"From\" Field.", false);
    }
    if (to.text.text.Equals(""))
    {
      to.Error();
      valid = false;
      OpenDialog("Please fill \"To\" Field.", false);
    }
    return valid;
  }

  private bool ValidateSecondView()
  {
    bool valid = true;
    if (price.text.text.Equals(""))
    {
      price.Error();
      valid = false;
      OpenDialog("Please fill \"CountryInfo\" Field.", false);
    }


    if (!isAlertDetail)
    {
      DateTime wholeDate = Program.StringToDate(date.text);
      if (wholeDate <= DateTime.Now)
      {
        OpenDialog("Please choose a date in the future", false);
        return false;
      }
    }
    else
    {
      DateTime wholeDate = Program.StringToDate(date.text);
      if (wholeDate < alert.MinDate || wholeDate > alert.MaxDate)
      {
        OpenDialog("The date is not compatable with Alert", false);
        return false;
      }

    }

    if (stopTimeCheckBox.isOn)
    {
      if (stopTimeField.text.text.Equals(""))
      {
        stopTimeField.Error();
        valid = false;
        OpenDialog("Please fill \"Stop Time\" Field.", false);
      }
      else if (int.Parse(stopTimeField.text.text) < 0)
      {
        stopTimeField.Error();
        valid = false;
        OpenDialog("The stop time can't be negative", false);
      }
      else if (int.Parse(stopTimeField.text.text) == 0)
      {
        stopTimeField.Error();
        valid = false;
        OpenDialog("Invalid stop time", false);
      }
      else if (int.Parse(stopTimeField.text.text) > 120)
      {
        stopTimeField.Error();
        valid = false;
        OpenDialog("The stop time can't be more than 120 minutes", false);
      }
    }
    return valid;
  }
  internal override void Clear()
  {
    //activate  view so we can clear input field content
    firstView.SetActive(true);
    secondView.SetActive(true);

    //clear content of all inputfields.
    titleFirstView.text = "Add Ride";
    titleSecView.text = titleFirstView.text;
    from.Reset();
    to.Reset();
    comment.Reset();
    price.Reset();
    stopTimeField.Reset();
    date.text = Program.DateToString(DateTime.Now.AddDays(1));
    backButton.SetActive(false);

    //reset checkbox to unchecked
    stopTimeCheckBox.GetComponent<Toggle>().isOn = false;
    openView(0);
    from.GetComponent<InputField>().enabled = true;
    to.GetComponent<InputField>().enabled = true;
    numberOfLuggages.enabled = true;
    numberOfSeats.enabled = true;
    price.GetComponent<InputField>().enabled = true;
  }
}