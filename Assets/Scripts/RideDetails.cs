using Requests;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class RideDetails : Panel {
    public ListView listView;
    public Text from, to, rideReason, reserveReason, price, date, time, comment, seats, luggages, driverFullName, driverBio, driverRatings, carName, carBrand, carColor, carYear, header;
    public Image carImage, profileImage, rideMapImage, smokingImage, musicImage, acImage, kidsSeatImage, petsImage;
    public Sprite smokingOnSpirite, musicOnSpirite, acOnSpirite, kidsSeatOnSpirite, petsOnSpirite;
    public Sprite smokingOffSpirite, musicOffSpirite, acOffSpirite, kidsSeatOffSpirite, petsOffSpirite;
    public GameObject dayOfWeek, passengersContainer, contentScrollView, personsDialog, removeRideDialog, cancelReservationDialog, cancelRideReason, cancelReservedReason;
    public GameObject addScheduleButton, editScheduleButton, removeScheduleButton; //schedule
    public GameObject addRideButton, updateRideButton, editRideButton, removeRideButton; //Ride
    public GameObject reserveSeatsButton, updateReserveSeats, addReserveSeats, editReserveSeats, cancelReservedSeats; //reserve

    public Text monday, tuesday, wednesday, thursday, friday, saturday, sunday;
    public UpDownPicker personsPicker, luggagesPicker;
    private Car car = null;

    //if this panel opened to view a Ride details
    private Ride ride = null;

    //if this panel opened to view Schedule details
    private ScheduleRide schedule;

    private void SetPermissions(bool isSmokingAllowed, bool isACAllowed, bool isPetsAllowed, bool isMusicAllowed, bool isKidsSeat) {
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
    public void UserDetails() {
        UserDetails panel = PanelsFactory.CreateUserDetails();
        Open(panel, () => { panel.Init(ride.User); });
    }

    public void GoWithHim() {
        personsDialog.gameObject.SetActive(true);
    }
    public void OpenRideDialog() {
        removeRideDialog.gameObject.SetActive(true);
        TimeSpan diff = ride.LeavingDate - DateTime.Now;
        if (diff.TotalHours < 48)
            cancelRideReason.gameObject.SetActive(true);
        else
            cancelRideReason.gameObject.SetActive(false);
    }

    public void CloseRideDialog() {
        removeRideDialog.gameObject.SetActive(false);
    }
    public void OpenCancelReserveDialog() {
        cancelReservationDialog.gameObject.SetActive(true);
        TimeSpan diff = ride.LeavingDate - DateTime.Now;
        if (diff.TotalHours < 48)
            cancelReservedReason.gameObject.SetActive(true);
        else
            cancelReservedReason.gameObject.SetActive(false);
    }
    public void CloseCancelReserveDialog() {
        cancelReservationDialog.gameObject.SetActive(false);
    }
    public void ClosePersonDialog() {
        personsDialog.gameObject.SetActive(false);
    }
    public void OpenPersonDialog() {
        //add person and luggage drop down for reserve
        var availableSeats = ride.AvailableSeats;
        var availableLuggage = ride.AvailableLuggages;
        if (ride.Passengers != null && ride.Passengers[0].Seats > 0) {
            availableSeats += ride.Passengers[0].Seats;
        }
        if (ride.Passengers != null && ride.Passengers[0].Luggages > 0) {
            availableLuggage += ride.Passengers[0].Luggages;
        }
        personsPicker.Init("Seats", 1, availableSeats);
        luggagesPicker.Init("Luggage", 0, availableLuggage);
        personsDialog.gameObject.SetActive(true);
    }
    public void EditRide() {
        Status = StatusE.UPDATE;
        FooterMenu.dFooterMenu.OpenAddRidePanel(this, ride);
    }

    public void EditSchedule() {
        SchedulePanel p = PanelsFactory.CreateAddSchedule();
        Open(p, () => { p.Init(schedule); });
    }

    public void Init(ScheduleRide schedule) {
        var ride = schedule.Ride;
        this.schedule = schedule;
        this.ride = ride;
        this.car = ride.Car;
        Person person = ride.User.Person;
        date.text = Program.DateToString(ride.LeavingDate);
        from.text = ride.From.Name;
        to.text = ride.To.Name;
        price.text = ride.Price.ToString() + " " + ride.CountryInformations.Unit;
        comment.text = ride.Comment;
        seats.text = ride.AvailableSeats.ToString();
        luggages.text = ride.AvailableLuggages.ToString();
        driverFullName.text = person.FirstName + " " + person.LastName;
        driverBio.text = person.Bio;
        driverRatings.text = ride.Person.RateAverage.ToString("0.0") + "/5 - " + ride.Person.RateCount.ToString() + " ratings";
        carName.text = car.Name;
        carBrand.text = car.Brand;
        carYear.text = car.Year.ToString();
        SetColor(car.Color);
        SetPermissions(ride.SmokingAllowed, ride.AcAllowed, ride.PetsAllowed, ride.MusicAllowed, ride.KidSeat);
        if (car.Picture == null) {
            StartCoroutine(Program.RequestImage(car.CarPictureUrl, SucceedCarImage, Error));
        } else {
            carImage.sprite = Program.GetImage(car.Picture);
        }
        if (person.ProfilePicture == null) {
            StartCoroutine(Program.RequestImage(person.ProfilePictureUrl, SucceedPersonImage, Error));
        } else {
            profileImage.sprite = Program.GetImage(person.ProfilePicture);
        }
        if (ride.Map == null) {
            StartCoroutine(Program.RequestImage(ride.MapUrl, Succeed, Error));
        } else {
            rideMapImage.sprite = Program.GetImage(ride.Map);
        }
        if (Status == StatusE.ADD) {
            addScheduleButton.SetActive(true);
            editScheduleButton.SetActive(false);
            removeScheduleButton.SetActive(false);
        } else if (Status == StatusE.UPDATE) {
            editScheduleButton.SetActive(true);
        } else if (Status == StatusE.VIEW) {
            editScheduleButton.SetActive(true);
            removeScheduleButton.SetActive(true);
        }

        date.text = Program.DateToString(schedule.StartDate) + " - " + Program.DateToString(schedule.EndDate);
        SetWeekDays(schedule.IsMonday, schedule.IsTuesday, schedule.IsWednesday, schedule.IsThursday, schedule.IsFriday, schedule.IsSaturday, schedule.IsSunday);
        dayOfWeek.SetActive(true);
    }

    //Init SearchResult
    //Init MyRidesPanel
    public void Init(Ride ride) {
        Clear();
        cancelRideReason.gameObject.SetActive(false);
        cancelReservedReason.gameObject.SetActive(false);
        bool owner = false;
        if (Program.Person != null) {
            owner = Program.User.Equals(ride.User);
        }

        this.ride = ride;
        this.car = ride.Car;
        Person person = ride.User.Person;
        date.text = Program.DateToString(ride.LeavingDate);
        from.text = ride.From.Name;
        to.text = ride.To.Name;
        price.text = ride.Price.ToString() + " " + ride.CountryInformations.Unit;
        comment.text = ride.Comment;
        seats.text = ride.AvailableSeats.ToString();
        luggages.text = ride.AvailableLuggages.ToString();
        driverFullName.text = person.FirstName + " " + person.LastName;
        driverBio.text = person.Bio;
        driverRatings.text = ride.Person.RateAverage.ToString("0.0") + "/5 - " + ride.Person.RateCount.ToString() + " ratings";
        carName.text = car.Name;
        carBrand.text = car.Brand;
        carYear.text = car.Year.ToString();
        SetColor(car.Color);
        SetPermissions(ride.SmokingAllowed, ride.AcAllowed, ride.PetsAllowed, ride.MusicAllowed, ride.KidSeat);
        if (car.Picture == null) {
            StartCoroutine(Program.RequestImage(car.CarPictureUrl, SucceedCarImage, Error));
        } else {
            carImage.sprite = Program.GetImage(car.Picture);
        }
        if (person.ProfilePicture == null) {
            StartCoroutine(Program.RequestImage(person.ProfilePictureUrl, SucceedPersonImage, Error));
        } else {
            profileImage.sprite = Program.GetImage(person.ProfilePicture);
        }
        if (ride.Map == null) {
            StartCoroutine(Program.RequestImage(ride.MapUrl, Succeed, Error));
        } else {
            rideMapImage.sprite = Program.GetImage(ride.Map);
        }


        if (Status == StatusE.ADD) {
            addRideButton.SetActive(true);
        } else if (Status == StatusE.UPDATE) {
            updateRideButton.SetActive(true);
        } else if (Status == StatusE.VIEW) {
            if (owner) {
                ImplementPassengersList(ride.Passengers);
                editRideButton.SetActive(true);
                removeRideButton.SetActive(true);
            } else if (ride.Reserved) {
                editReserveSeats.SetActive(true);
                cancelReservedSeats.SetActive(true); //cancel
                updateReserveSeats.SetActive(true);
            } else {
                reserveSeatsButton.SetActive(true);
                addReserveSeats.SetActive(true);
            }
        }
    }
    private void SucceedCarImage(Texture2D t) {
        carImage.sprite = Program.GetImage(t);
        car.Picture = t;
    }
    private void SucceedPersonImage(Texture2D t) {
        profileImage.sprite = Program.GetImage(t);
        ride.User.Person.ProfilePicture = t;
    }
    private void Error(string error) {
        OpenDialog(error, false);
    }

    private void Succeed(Texture2D map) {
        rideMapImage.sprite = Program.GetImage(map);
        ride.Map = map;
    }

    private void ImplementPassengersList(List<Passenger> passengers) {
        if (passengers != null) {
            listView.Clear();
            contentScrollView.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 2399.4f);
            passengersContainer.SetActive(true);
            foreach (Passenger p in passengers) {
                var obj = ItemsFactory.CreatPassengerItem(p, this);
                listView.Add(obj.gameObject);
            }
        }
    }

    public void SetWeekDays(bool isMonday, bool isTuesday, bool isWednesday, bool isThursday, bool isFriday, bool isSaturday, bool isSunday) {
        if (isMonday)
            monday.color = new Color(255f / 255f, 188f / 255f, 66f / 255f);
        if (isTuesday) {
            tuesday.color = new Color(255f / 255f, 188f / 255f, 66f / 255f);
        }
        if (isWednesday) {
            wednesday.color = new Color(255f / 255f, 188f / 255f, 66f / 255f);
        }
        if (isThursday) {
            thursday.color = new Color(255f / 255f, 188f / 255f, 66f / 255f);
        }
        if (isFriday) {
            friday.color = new Color(255f / 255f, 188f / 255f, 66f / 255f);
        }
        if (isSaturday) {
            saturday.color = new Color(255f / 255f, 188f / 255f, 66f / 255f);
        }
        if (isSunday) {
            sunday.color = new Color(255f / 255f, 188f / 255f, 66f / 255f);
        }
    }
    private void SetColor(string color) {
        if (color.Equals("#000000")) {
            carColor.text = "Black";
        } else if (color.Equals("#ffffff")) {
            carColor.text = "White";
        } else if (color.Equals("#9F9F9F")) {
            carColor.text = "Grey";
        } else if (color.Equals("#767676")) {
            carColor.text = "Dark Grey";
        } else if (color.Equals("#FF0000")) {
            carColor.text = "Red";
        } else if (color.Equals("#004BFF")) {
            carColor.text = "Blue";
        } else if (color.Equals("#001196")) {
            carColor.text = "Dark Blue";
        } else if (color.Equals("#FFFC00")) {
            carColor.text = "Yellow";
        } else if (color.Equals("#FFA5EA")) {
            carColor.text = "Pink";
        } else if (color.Equals("#6A0DAD")) {
            carColor.text = "Purple";
        } else if (color.Equals("#964B00")) {
            carColor.text = "Brown";
        } else if (color.Equals("#FFA500")) {
            carColor.text = "Orange";
        } else if (color.Equals("#00DB00")) {
            carColor.text = "Green";
        }
    }

    internal override void Clear() {
        addRideButton.SetActive(false);
        editRideButton.SetActive(false);
        removeRideButton.SetActive(false);
        updateRideButton.SetActive(false);

        reserveSeatsButton.SetActive(false);
        cancelReservedSeats.SetActive(false);
        editReserveSeats.SetActive(false);
        editScheduleButton.SetActive(false);
        addScheduleButton.SetActive(false);
        updateReserveSeats.SetActive(false);
        addReserveSeats.SetActive(false);
        ClosePersonDialog();
        CloseRideDialog();
        CloseCancelReserveDialog();

        personsPicker.Clear();
        luggagesPicker.Clear();

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
    public void AddSchedule() {
        Request<ScheduleRide> request = new AddScheduleRide(schedule);
        request.Send(AddScheduleResponse);
    }
    public void UpdateSchedule() {

    }

    public void RemoveSchedule() {

    }
    public void AddScheduleResponse(ScheduleRide result, int code, string message) {
        //check if schedule Ride add in server success
    }

    public void EditScheduleResponse(ScheduleRide result, int code, string message) {
        //check if schedule Ride add in server success
    }

    public void RemoveScheduleResponse(ScheduleRide result, int code, string message) {
        //check if schedule Ride remove in server success
    }

    //RIDEE
    public void AddRide() {
        Request<Ride> request = new AddRide(ride);
        request.AddSendListener(OpenSpinner);
        request.AddReceiveListener(CloseSpinner);
        request.Send(AddRideResponse);
    }
    public void AddRideResponse(Ride result, int code, string message) {
        if (!code.Equals((int)HttpStatusCode.OK)) {
            OpenDialog(message, false);
            Debug.Log(code);
        } else {
            Program.Person.UpcomingRides.Add(result);
            MissionCompleted(MyRidePanel.PANELNAME, "Ride's in public!");
        }
    }
    public void UpdateRide() {
        Request<Ride> request = new EditRide(Program.Person, ride);
        request.AddSendListener(OpenSpinner);
        request.AddReceiveListener(CloseSpinner);
        request.Send(EditRideResponse);
    }
    public void EditRideResponse(Ride result, int code, string message) {
        if (!code.Equals((int)HttpStatusCode.OK)) {
            OpenDialog(message, false);
            Debug.Log(code);
        } else {
            Program.Person.UpcomingRides.Remove(ride);
            Program.Person.UpcomingRides.Add(result);
            MissionCompleted(MyRidePanel.PANELNAME, "Ride has been updated");
        }
    }
    public void RemoveRide() {
        Request<bool> request = new CancelRide(ride, rideReason.text.ToString());
        request.AddSendListener(OpenSpinner);
        request.AddReceiveListener(CloseSpinner);
        request.Send(RemoveRideResponse);
    }
    private void RemoveRideResponse(bool result, int code, string message) {
        if (!code.Equals((int)HttpStatusCode.OK)) {
            OpenDialog(message, false);
            Debug.Log(code);
        } else {
            if (result == true) {
                FooterMenu.dFooterMenu.OpenYourRidesPanel();
                DestroyImediateForwardBackward();
                Program.Person.UpcomingRides.Remove(ride);
                MissionCompleted(MyRidePanel.PANELNAME, "Ride has been removed!");
            }
        }
    }
    //RESERVATION
    public void ReserveSeat() {
        if (!Program.IsLoggedIn) {
            LoginPanel login = PanelsFactory.CreateLogin();
            Open(login, () => { login.Init(false); });
        } else {
            Request<Ride> request = new ReserveSeat(ride, Program.User, personsPicker.Value, luggagesPicker.Value);
            request.AddSendListener(OpenSpinner);
            request.AddReceiveListener(CloseSpinner);
            request.Send(ReserveSeatsResponse);
        }
    }
    private void ReserveSeatsResponse(Ride result, int code, string message) {
        if (!code.Equals((int)HttpStatusCode.OK)) {
            OpenDialog(message, false);
            Debug.Log(code);
        } else {
            Program.Person.UpcomingRides.Add(result);
            FooterMenu.dFooterMenu.OpenSearchPanel();
            OpenDialog("You reserved " + personsPicker.Value + " seat(s) and " + luggagesPicker.Value + " luggage(s).", true);
            DestroyImediateForwardBackward();
        }
    }
    public void EditReservation() {
        if (!Program.IsLoggedIn) {
            LoginPanel login = PanelsFactory.CreateLogin();
            Open(login, () => { login.Init(false); });
        } else {
            Request<Ride> request = new EditReservation(ride, personsPicker.Value, luggagesPicker.Value);
            request.AddSendListener(OpenSpinner);
            request.AddReceiveListener(CloseSpinner);
            request.Send(EditReservationResponse);
        }
    }
    private void EditReservationResponse(Ride result, int code, string message) {
        if (!code.Equals((int)HttpStatusCode.OK)) {
            OpenDialog(message, false);
            Debug.Log(code);
        } else {
            Program.Person.UpcomingRides.Remove(ride);
            Program.Person.UpcomingRides.Add(result);
            FooterMenu.dFooterMenu.OpenSearchPanel();
            OpenDialog("You reserved " + personsPicker.Value + " seat(s) and " + luggagesPicker.Value + " luggage(s).", true);
            DestroyImediateForwardBackward();
        }
    }
    public void CancelReservedSeats() {
        if (!Program.IsLoggedIn) {
            LoginPanel login = PanelsFactory.CreateLogin();
            Open(login, () => { login.Init(false); });
        } else {
            Request<Ride> request = new CancelReservedSeats(ride, reserveReason.text.ToString());
            request.AddSendListener(OpenSpinner);
            request.AddReceiveListener(CloseSpinner);
            request.Send(CancelReservedSeatsResponse);
        }
    }
    private void CancelReservedSeatsResponse(Ride result, int code, string message) {
        if (!code.Equals((int)HttpStatusCode.OK)) {
            OpenDialog(message, false);
            Debug.Log(code);
        } else {
            Program.Person.UpcomingRides.Remove(result);
            MissionCompleted(SearchPanel.PANELNAME, "You have cancelled the reservation");
        }
    }
}