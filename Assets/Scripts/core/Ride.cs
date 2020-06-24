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

public class Ride
{
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
  private int reservedSeats;
  private int availableLuggages;
  private int reservedLuggages;
  private int stopTime;
  private string comment;
  private Texture2D map;
  private string mapBase64;
  private float price;
  private CountryInformations countryInformations;
  private Driver driver;
  private List<Passenger> passengers;
  private Car car;
  private DateTime updated;

  public Ride(string id, Location from, Location to, DateTime date, bool musicAllowed, bool acAllowed, bool smokingAllowed, bool petsAllowed, bool kidSeat, int availableSeats, int availableLuggages, int stopTime, string comment, string price, CountryInformations countryInformations, int reservedLuggages, int reservedSeats, Texture2D map)
  {
    Id = id;
    From = from;
    To = to;
    LeavingDate = date;
    MusicAllowed = musicAllowed;
    AcAllowed = acAllowed;
    SmokingAllowed = smokingAllowed;
    PetsAllowed = petsAllowed;
    KidSeat = kidSeat;
    AvailableSeats = availableSeats;
    AvailableLuggages = availableLuggages;
    StopTime = stopTime;
    Comment = comment;
    Map = map;
    Price = float.Parse(price);
    CountryInformations = countryInformations;
    ReservedLuggages = reservedLuggages;
    ReservedSeats = reservedSeats;
  }

  public Ride(List<Passenger> passengers)
  {
    Passengers = passengers;
  }

  public Ride(string id)
  {
    Id = id;
  }

  //constructor to add full ride
  public Ride(Driver driver, Car car, Location from, Location to, string comment, string price, CountryInformations countryInformations, DateTime date, bool musicAllowed, bool acAllowed, bool smokingAllowed, bool petsAllowed, bool kidSeat, int availableSeats, int availableLuggages, int stopTime, Texture2D map)
  {
    Driver = driver;
    Car = car;
    From = from;
    To = to;
    LeavingDate = date;
    MusicAllowed = musicAllowed;
    AcAllowed = acAllowed;
    SmokingAllowed = smokingAllowed;
    PetsAllowed = petsAllowed;
    KidSeat = kidSeat;
    AvailableSeats = availableSeats;
    AvailableLuggages = availableLuggages;
    StopTime = stopTime;
    Comment = comment;
    CountryInformations = countryInformations;
    Price = float.Parse(price);
    Map = map;
    ReservedLuggages = reservedLuggages;
    ReservedSeats = reservedSeats;
  }

  public Ride(Driver driver, Location from, Location to, CountryInformations countryInformations, DateTime date)
  {
    Driver = driver;
    From = from;
    To = to;
    LeavingDate = date;
    CountryInformations = countryInformations;
  }

  public JObject ToJson()
  {
    JObject rideJ = new JObject();

    rideJ[nameof(this.kidSeat)] = this.KidSeat;
    rideJ[nameof(this.acAllowed)] = this.AcAllowed;
    rideJ[nameof(this.musicAllowed)] = this.MusicAllowed;
    rideJ[nameof(this.smokingAllowed)] = this.SmokingAllowed;
    rideJ[nameof(this.petsAllowed)] = this.PetsAllowed;

    rideJ[nameof(this.availableLuggages)] = this.AvailableLuggages;
    rideJ[nameof(this.availableSeats)] = this.AvailableSeats;

    rideJ[nameof(this.stopTime)] = this.StopTime;
    rideJ[nameof(this.leavingDate)] = this.LeavingDate;

    rideJ[nameof(this.car)] = this.Car.Id;
    rideJ[nameof(this.comment)] = this.Comment;
    rideJ[nameof(this.driver)] = this.Driver.Did;

    rideJ[nameof(this.reservedLuggages)] = this.ReservedLuggages;

    rideJ[nameof(this.price)] = this.Price;

    rideJ[nameof(this.to)] = to.ToJson();
    rideJ[nameof(this.from)] = from.ToJson();
    rideJ[nameof(this.countryInformations)] = countryInformations.ToJson();


    rideJ[nameof(this.map)] = this.mapBase64;
    return rideJ;
  }

  public static Ride ToObject(JObject json)
  {
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

    int availableSeats = -1;
    var aS = json[nameof(Ride.availableSeats)];
    if (aS != null)
      int.TryParse(aS.ToString(), out availableSeats);

    int stopTime = -1;
    var sT = json[nameof(Ride.stopTime)];
    if (sT != null)
      int.TryParse(sT.ToString(), out stopTime);


    string comment = "";
    var c = json[nameof(Ride.comment)];
    if (c != null)
      comment = c.ToString();

    JObject carJ = (JObject) json[nameof(Ride.car)];
    Car car = Car.ToObject(carJ);

    double leavingDateDouble = -1;
    var ld = json[nameof(Ride.leavingDate)];
    if (ld != null)
    {
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
    CountryInformations countryInformations = CountryInformations.ToObject((JObject)json[nameof(Ride.countryInformations)]);
    //Driver d = Driver.ToObject((JObject)json[nameof(Ride.driver)]);
    string price = "";
    var p = json[nameof(price)];
    if (p != null)
      price = p.ToString();

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
    return new Ride(id, from, to, leavingDate, musicAllowed, acAllowed, smokingAllowed, petsAllowed,
    kidSeat, availableSeats, availableLuggages, stopTime, comment, price, countryInformations, reservedLuggages, reservedSeats, null);
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
  public int StopTime { get => stopTime; set => stopTime = value; }
  public string Comment { get => comment; set => comment = value; }
  public CountryInformations CountryInformations { get => countryInformations; set => countryInformations = value; }
  public Texture2D Map { get => map; set { map = value; if (value != null) { mapBase64 = Convert.ToBase64String(value.EncodeToPNG()); } } }
  public int ReservedLuggages { get => reservedLuggages; set => reservedLuggages = value; }
  public int ReservedSeats { get => reservedSeats; set => reservedSeats = value; }
  public Driver Driver { get => driver; set => driver = value; }
  public List<Passenger> Passengers { get => passengers; set => passengers = value; }
  public Car Car { get => car; set => car = value; } //has texture2d
  public string MapBase64 { get => mapBase64; }
  public DateTime Updated { get => updated; set => updated = value; }
  public float Price { get => price; set => price = value; }
}
