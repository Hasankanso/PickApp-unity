using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Requests;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using UnityEngine;

public class Ride {
    public string id;
    private Location from;
    private Location to;
    private DateTime leavingDate;
    private bool musicAllowed;
    private bool acAllowed;
    private bool smokingAllowed;
    private bool petsAllowed;
    private bool kidSeat;
    private int availableSeats;
    private int maxSeats;
    private int maxLuggages;
    private int reservedSeats;
    private int availableLuggages;
    private int reservedLuggages;
    private int stopTime;
    private string comment;
    private Texture2D map;
    private string mapUrl;
    private string mapBase64;
    private bool reserved;
    private float price;
    private User user;
    private List<Passenger> passengers;
    private Car car;
    private DateTime updated;

    public Ride(string id, Location from, Location to, DateTime date, bool musicAllowed, bool acAllowed, bool smokingAllowed, bool petsAllowed, bool kidSeat, int maxSeats, int maxLuggages, int stopTime, string comment, string price, int reservedLuggages, int reservedSeats, Texture2D map, List<Passenger> passengers) {
        Id = id;
        From = from;
        To = to;
        LeavingDate = date;
        MusicAllowed = musicAllowed;
        AcAllowed = acAllowed;
        SmokingAllowed = smokingAllowed;
        PetsAllowed = petsAllowed;
        KidSeat = kidSeat;
        MaxSeats = maxSeats;
        AvailableSeats = maxSeats - reservedSeats;
        MaxLuggages = maxLuggages;
        AvailableLuggages = maxLuggages - reservedLuggages;
        StopTime = stopTime;
        Comment = comment;
        Map = map;
        Price = float.Parse(price);
        ReservedLuggages = reservedLuggages;
        ReservedSeats = reservedSeats;
        this.passengers = passengers;
    }

    public Ride(List<Passenger> passengers) {
        Passengers = passengers;
    }
    public Ride() { 
    }
    public static string Valid(Ride ride) {
        string validateUser = User.ValidateLogin(Program.User);
        if (!string.IsNullOrEmpty(validateUser)) {
            return validateUser;
        }

        string fromValidation = Location.Validate(ride.From);
        if (!string.IsNullOrEmpty(fromValidation)) {
            return fromValidation;
        }
        string toValidation = Location.Validate(ride.To);
        if (!string.IsNullOrEmpty(toValidation)) {
            return toValidation;
        }


        //   if (ride.From.Equals(ride.To))
        //   {
        //       return "From and To are too close (1 km)";
        //   }
        if (ride.From.Latitude == ride.To.Latitude && ride.From.Longitude == ride.To.Longitude) {
            return "From and To must be different";
        }
        if (DateTime.Compare(ride.LeavingDate, DateTime.Now.AddMinutes(30)) < 0) {
            return "Your ride must be after one hour or more from now";
        }
        if (ride.AvailableSeats <= 0 || ride.AvailableSeats > ride.Car.MaxSeats) {
            return "Invalid number of seats";
        }
        if (ride.AvailableLuggages < 0 || ride.AvailableLuggages > ride.Car.MaxLuggage) {
            return "Invalid number of luggage";
        }
        if (ride.StopTime != 0 && (ride.StopTime < 5 || ride.StopTime > 30)) {
            return "Your stop time must be between 5 and 30 minutes";
        }
        if (string.IsNullOrEmpty(ride.Comment) || ride.Comment.Length < 25 || ride.Comment.Length > 400) {
            return "Please add a comment between 25 and 400 characters";
        }
        if (string.IsNullOrEmpty(ride.MapBase64)) {
            return "Please choose your ride's road";
        }
        if (string.IsNullOrEmpty(ride.Car.id)) {
            return "Please choose a car";
        }
        if (ride.Price <= 0) {
            return "Please set a price";
        }
        if (string.IsNullOrEmpty(ride.CountryInformations.Id)) {
            return "Please choose a country info";
        }
        if (string.IsNullOrEmpty(ride.Driver.Id)) {
            return "You're not a driver";
        }
        var rideDate = ride.LeavingDate.AddMinutes(-20);
        foreach (var item in Program.Person.UpcomingRides) {
            if (DateTime.Compare(rideDate, item.LeavingDate) >= 0) {
                return "you have an upcoming ride in this ride time";
            }
        }
        return string.Empty;
    }
    //constructor for ride to object
    public Ride(string id, User driver, Car car, Location from, Location to, string comment, string price, DateTime date, int maxSeats, int maxLuggage, bool musicAllowed, bool acAllowed, bool smokingAllowed, bool petsAllowed, bool kidSeat, int availableSeats, int availableLuggages, int stopTime, string mapUrl, List<Passenger> passenger)
    : this(id, driver, car, from, to, comment, price, date, musicAllowed, acAllowed, smokingAllowed, petsAllowed, kidSeat, availableSeats, availableLuggages, stopTime, null, mapUrl, passenger) {
        Id = id;
    }

    //constructor to add full ride
    public Ride(string id, User user, Car car, Location from, Location to, string comment, string price,
    DateTime date, bool musicAllowed, bool acAllowed, bool smokingAllowed, bool petsAllowed, bool kidSeat, int maxSeats, int maxLuggages,
    int stopTime, Texture2D map, string mapUrl, List<Passenger> passengers) {
        this.id = id;
        this.user = user;
        Car = car;
        From = from;
        To = to;
        LeavingDate = date;
        MusicAllowed = musicAllowed;
        AcAllowed = acAllowed;
        SmokingAllowed = smokingAllowed;
        PetsAllowed = petsAllowed;
        KidSeat = kidSeat;
        MaxSeats = maxSeats;
        AvailableSeats = maxSeats - reservedSeats;
        MaxLuggages = maxLuggages;
        AvailableLuggages = maxLuggages - reservedLuggages;
        StopTime = stopTime;
        Comment = comment;
        Price = float.Parse(price);
        Map = map;
        this.mapUrl = mapUrl;
        ReservedLuggages = reservedLuggages;
        ReservedSeats = reservedSeats;
        this.passengers = passengers;
    }

    public Ride(User user, Location from, Location to, DateTime date) {
        this.user = user;
        From = from;
        To = to;
        LeavingDate = date;
    }

    public JObject ToJson() {
        JObject rideJ = new JObject();

        rideJ[nameof(this.kidSeat)] = this.KidSeat;
        rideJ[nameof(this.acAllowed)] = this.AcAllowed;
        rideJ[nameof(this.musicAllowed)] = this.MusicAllowed;
        rideJ[nameof(this.smokingAllowed)] = this.SmokingAllowed;
        rideJ[nameof(this.petsAllowed)] = this.PetsAllowed;

        rideJ[nameof(this.availableLuggages)] = this.AvailableLuggages;
        rideJ[nameof(this.availableSeats)] = this.AvailableSeats;
        rideJ[nameof(this.maxSeats)] = this.MaxSeats;
        rideJ[nameof(this.maxLuggages)] = this.MaxLuggages;
        rideJ[nameof(this.stopTime)] = this.StopTime;
        rideJ[nameof(this.leavingDate)] = this.LeavingDate;

        rideJ[nameof(this.car)] = this.Car.Id;
        rideJ[nameof(this.comment)] = this.Comment;
        rideJ[nameof(this.user)] = this.user.Id;
        rideJ[nameof(this.price)] = this.Price;

        rideJ[nameof(this.to)] = to.ToJson();
        rideJ[nameof(this.from)] = from.ToJson();


        rideJ[nameof(this.map)] = this.mapBase64;
        return rideJ;
    }
    public JObject removeToJson() {
        JObject userJ = new JObject();
        userJ[nameof(this.user)] = Program.User.Id;
        userJ[nameof(this.id)] = this.Id;
        return userJ;
    }
    public static Ride ToObject(JObject json) {
        bool kidSeat = false;
        var ks = json[nameof(Ride.kidSeat)];

        if (ks != null)
            bool.TryParse(ks.ToString(), out kidSeat);

        string id = "";
        var oId = json["objectId"];
        if (oId != null)
            id = oId.ToString();

        bool acAllowed = false;
        var aAllowed = json[nameof(Ride.acAllowed)];
        if (aAllowed != null)
            bool.TryParse(aAllowed.ToString(), out acAllowed);

        bool musicAllowed = false;
        var mAllowed = json[nameof(Ride.musicAllowed)];
        if (mAllowed != null)
            bool.TryParse(mAllowed.ToString(), out musicAllowed);

        bool smokingAllowed = false;
        var sAllowed = json[nameof(Ride.smokingAllowed)];
        if (sAllowed != null)
            bool.TryParse(sAllowed.ToString(), out smokingAllowed);

        bool petsAllowed = false;
        var pAllowed = json[nameof(Ride.petsAllowed)];
        if (pAllowed != null)
            bool.TryParse(pAllowed.ToString(), out petsAllowed);

        int availableLuggages = -1;
        var aL = json[nameof(Ride.availableLuggages)];
        if (aL != null)
            int.TryParse(aL.ToString(), out availableLuggages);

        int maxLuggages = -1;
        var mL = json[nameof(Ride.maxLuggages)];
        if (mL != null)
            int.TryParse(mL.ToString(), out maxLuggages);

        int availableSeats = -1;
        var aS = json[nameof(Ride.availableSeats)];
        if (aS != null)
            int.TryParse(aS.ToString(), out availableSeats);

        int maxSeats = -1;
        var mS = json[nameof(Ride.maxSeats)];
        if (mS != null)
            int.TryParse(mS.ToString(), out maxSeats);

        int stopTime = -1;
        var sT = json[nameof(Ride.stopTime)];
        if (sT != null)
            int.TryParse(sT.ToString(), out stopTime);


        string comment = "";
        var c = json[nameof(Ride.comment)];
        if (c != null)
            comment = c.ToString();

        JObject carJ = (JObject)json[nameof(Ride.car)];
        Car car = null;
        if (carJ != null) {
            car = Car.ToObject(carJ);
        }

        JArray passengersJ = (JArray)json.GetValue("passengers");
        List<Passenger> passengers = null;

        if (passengersJ != null)
        {
            passengers = new List<Passenger>();
            foreach (var passenger in passengersJ)
            {
                passengers.Add(Passenger.ToObject((JObject)passenger));
            }
        }

        double leavingDateDouble = -1;
        var ld = json[nameof(Ride.leavingDate)];
        if (ld != null) {
            double.TryParse(ld.ToString(), out leavingDateDouble);
        }

        DateTime leavingDate = Program.UnixToUtc(leavingDateDouble);

        int reservedLuggages = -1;
        var rL = json[nameof(Ride.reservedLuggages)];
        if (rL != null)
            int.TryParse(rL.ToString(), out reservedLuggages);

        int reservedSeats = -1;
        var rs = json[nameof(Ride.reservedSeats)];
        if (rs != null)
            int.TryParse(rs.ToString(), out reservedSeats);

        Location from = Location.ToObject((JObject)json[nameof(Ride.from)]);
        Location to = Location.ToObject((JObject)json[nameof(Ride.to)]);



        JObject driverJ = (JObject)json["driver"];
        JObject personJ = (JObject)driverJ["person"];
        Person person = Person.ToObject(personJ);
        Driver driver = Driver.ToObject(driverJ);
        User user = new User(person, driver);
        string price = "";
        var p = json[nameof(price)];
        if (p != null)
            price = p.ToString();

        string mapUrl = json["map"].ToString();
        //Texture2D map = json[nameof(map)].ToString();
        /*
        rideJ[nameof(this.Date)] = this.Date;

        rideJ[nameof(this.Car)] = this.Car.Id;
        rideJ[nameof(this.Comment)] = this.Comment;
        rideJ[nameof(this.Driver.Id)] = this.Driver.Id;

        rideJ[nameof(this.ReservedLuggages)] = this.ReservedLuggages;


        rideJ[nameof(this.To)] = To.ToJson();
        rideJ[nameof(this.From)] = from.ToJson();
        rideJ[nameof(this.CountryInfo)] = CountryInfo.ToJson();


        rideJ[nameof(this.Map)] = this.MapBase64;
        */
        return new Ride(id, user, car, from, to, comment, price, leavingDate, maxSeats, maxLuggages, musicAllowed, acAllowed, smokingAllowed, petsAllowed,
        kidSeat, availableSeats, availableLuggages, stopTime, mapUrl, passengers);
    }
    public string Id { get => id; set => id = value; }
    public Location From { get => from; set => from = value; }
    public Location To { get => to; set => to = value; }
    public DateTime LeavingDate { get => leavingDate; set => leavingDate = value; }
    public bool MusicAllowed { get => musicAllowed; set => musicAllowed = value; }
    public bool AcAllowed { get => acAllowed; set => acAllowed = value; }
    public bool SmokingAllowed { get => smokingAllowed; set => smokingAllowed = value; }
    public bool PetsAllowed { get => petsAllowed; set => petsAllowed = value; }
    public bool KidSeat { get => kidSeat; set => kidSeat = value; }
    public int AvailableSeats { get => availableSeats; set => availableSeats = value; }
    public int AvailableLuggages { get => availableLuggages; set => availableLuggages = value; }
    public int MaxSeats { get => maxSeats; set => maxSeats = value; }
    public int MaxLuggages { get => maxLuggages; set => maxLuggages = value; }
    public int StopTime { get => stopTime; set => stopTime = value; }
    public string Comment { get => comment; set => comment = value; }
    public CountryInformations CountryInformations { get => Person.CountryInformations; }
    public Texture2D Map { get => map; set { map = value; if (value != null) { mapBase64 = Convert.ToBase64String(value.EncodeToPNG()); } } }
    public int ReservedLuggages { get => reservedLuggages; set => reservedLuggages = value; }
    public int ReservedSeats { get => reservedSeats; set => reservedSeats = value; }
    public User User { get => user; set => user = value; }

    public string MapUrl { get => mapUrl; set => mapUrl = value; }
    public Driver Driver { get => user.Driver; }
    public Person Person { get => user.Person; }
    public List<Passenger> Passengers { get => passengers; set => passengers = value; }
    public Car Car { get => car; set => car = value; } //has texture2d
    public string MapBase64 { get => mapBase64; }
    public DateTime Updated { get => updated; set => updated = value; }
    public float Price { get => price; set => price = value; }
    public bool Reserved { get => reserved; set => reserved = value; }
}
