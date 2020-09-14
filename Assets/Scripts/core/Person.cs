using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Requests;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Person {
    public string id;
    private string firstName, lastName, bio, chattiness;
    private DateTime birthday;
    private List<Rate> rates;
    private List<Ride> upcomingRides = new List<Ride>();
    private bool gender;
    public Texture2D profilePicture;
    public string image, profilePictureUrl;
    private float rateAverage;
    private int acomplishedRides, canceledRides, rateCount;
    private DateTime updated;
    private CountryInformations countryInformations;

    //user
    public string phone;

    public Person() {
    }
    //Constructor for login
    public Person(string id, string firstName, string lastName,int rateCount, int acomplishedRides, int canceledRides, string chattiness, string phone, CountryInformations countryInformations, string bio, float rateAverage, bool gender, DateTime birthday, string profilePictureUrl) {
        this.id = id;
        this.firstName = firstName;
        this.acomplishedRides = acomplishedRides;
        this.canceledRides = canceledRides;
        this.bio = bio;
        this.rateCount = rateCount;
        this.phone = phone;
        this.lastName = lastName;
        this.rateAverage = rateAverage;
        this.chattiness = chattiness;
        this.ProfilePictureUrl = profilePictureUrl;
        this.gender = gender;
        this.countryInformations = countryInformations;
        this.birthday = birthday;
    }
    public Person(string id, string firstName, string lastName, string chattiness, string phone, CountryInformations countryInformations, string bio, float rateAverage, bool gender, DateTime birthday, string profilePictureUrl) {
        this.id = id;
        this.firstName = firstName;
        this.bio = bio;
        this.phone = phone;
        this.lastName = lastName;
        this.rateAverage = rateAverage;
        this.chattiness = chattiness;
        this.ProfilePictureUrl = profilePictureUrl;
        this.gender = gender;
        this.countryInformations = countryInformations;
        this.birthday = birthday;
    }
    //Constructor for Edit
    public Person(string firstName, string lastName, DateTime birthday, Texture2D profilePicture, CountryInformations countryInformations, bool gender) {
        this.firstName = firstName;
        this.lastName = lastName;
        this.birthday = birthday;
        this.ProfilePicture = profilePicture;
        this.gender = gender;
        this.countryInformations = countryInformations;
    }
    //Constructor for Edit
    public Person(string firstName, string lastName, Texture2D profilePicture) {
        this.firstName = firstName;
        this.lastName = lastName;
        this.ProfilePicture = profilePicture;
    }
    //Constructor for response
    public Person(string id, string firstName, string lastName, DateTime birthday, string phone, Texture2D profilePicture, bool gender, List<Rate> rates, float rateAverage) {
        this.id = id;
        this.FirstName = firstName;
        this.LastName = lastName;
        this.Birthday = birthday;
        this.Phone = phone;
        this.ProfilePicture = profilePicture;
        this.rates = rates;
        this.gender = gender;
        this.rateAverage = rateAverage;
    }
    public JObject ToJson() {
        JObject personJ = new JObject();
        personJ[nameof(this.firstName)] = this.firstName;
        personJ[nameof(this.lastName)] = this.lastName;
        personJ[nameof(this.bio)] = this.bio;
        personJ[nameof(this.image)] = this.Image;
        personJ[nameof(this.chattiness)] = this.chattiness;
        personJ[nameof(this.countryInformations)] = this.countryInformations.ToJson();
        personJ[nameof(this.birthday)] = this.birthday;
        personJ[nameof(this.gender)] = this.gender;
        return personJ;
    }
    //Constructor for adding bio

    public static Person ToObject(JObject json) {
        string phone = "";
        var ph = json[nameof(phone)];
        if (ph != null)
            phone = ph.ToString();

        string id = "";
        var oId = json["objectId"];
        if (oId != null)
            id = oId.ToString();
        string firstName = "";
        var fn = json[nameof(firstName)];
        if (fn != null)
            firstName = fn.ToString();
        string lastName = "";
        var ln = json[nameof(lastName)];
        if (ln != null)
            lastName = ln.ToString();
        string chattiness = "";
        var cn = json[nameof(chattiness)];
        if (cn != null)
            chattiness = cn.ToString();
        string bio = "";
        var bi = json[nameof(bio)];
        if (bi != null)
            bio = bi.ToString();

        double birthdayDouble = -1;
        var br = json[nameof(Person.birthday)];
        if (br != null) {
            double.TryParse(br.ToString(), out birthdayDouble);
        }
        DateTime birthday = Program.UnixToUtc(birthdayDouble);

        double updatedDouble = -1;
        var up = json[nameof(Person.updated)];
        if (up != null) {
            double.TryParse(up.ToString(), out updatedDouble);
        }
        DateTime updated = Program.UnixToUtc(updatedDouble);

        float rateAverage = -1;
        var ra = json[nameof(Person.rateAverage)];
        if (ra != null)
            float.TryParse(ra.ToString(), out rateAverage);
        int acomplishedRides = -1;
        var ar = json[nameof(Person.acomplishedRides)];
        if (ar != null)
            int.TryParse(ar.ToString(), out acomplishedRides);
        int canceledRides = -1;
        var cr = json[nameof(Person.canceledRides)];
        if (cr != null)
            int.TryParse(cr.ToString(), out canceledRides);
        int rateCount = -1;
        var rc = json[nameof(Person.rateCount)];
        if (rc != null)
            int.TryParse(rc.ToString(), out rateCount);
        
        CountryInformations countryInformations = CountryInformations.ToObject((JObject)json[nameof(Person.countryInformations)]);
        bool gender = false;
        var gn = json[nameof(Person.gender)];
        if (gn != null)
            bool.TryParse(gn.ToString(), out gender);

        string image = json[nameof(image)].ToString();
        Person p = new Person(id, firstName, lastName, rateCount, acomplishedRides, canceledRides, chattiness, phone, countryInformations, bio, rateAverage, gender, birthday, image);

        JArray upcomingRidesArray = (JArray)json.GetValue("upcomingRides");
        List<Ride> rides = new List<Ride>();
        if (upcomingRidesArray != null) {
            foreach (var ride in upcomingRidesArray) {
                if (ride.HasValues == true)
                    rides.Add(Ride.ToObject((JObject)ride));
            }
            p.UpcomingRides = rides;
        }
        return p;
    }


    public override string ToString() {
        return base.ToString();
    }

    public static string DisplayPerson(Person p1) {
        return p1.ToString();
    }

    public string Id { get { return id; } set { id = value; } }
    public string FirstName { get { return firstName; } set { firstName = value; } }
    public string LastName { get { return lastName; } set { lastName = value; } }
    public string Phone { get { return phone; } set { phone = value; } }
    public DateTime Birthday { get { return birthday; } set { birthday = value; } }
    public CountryInformations CountryInformations { get { return countryInformations; } set { countryInformations = value; } }
    public Texture2D ProfilePicture { get { return profilePicture; } set { profilePicture = value; if (value != null) { Image = Convert.ToBase64String(value.EncodeToPNG()); } } }
    public string Bio { get { return bio; } set { bio = value; } }
    public string Chattiness { get { return chattiness; } set { chattiness = value; } }
    public bool Gender { get { return gender; } set { gender = value; } }
    public List<Ride> UpcomingRides { get { return upcomingRides; } set { upcomingRides = value; } }
    public List<Rate> Rates { get { return rates; } set { rates = value; } }
    public float RateAverage { get => rateAverage; set => rateAverage = value; }
    public string Image { get => image; set => image = value; }
    public DateTime Updated { get => updated; set => updated = value; }
    public string ProfilePictureUrl { get => profilePictureUrl; set => profilePictureUrl = value; }
    public int AcomplishedRides { get => acomplishedRides; set => acomplishedRides = value; }
    public int CanceledRides { get => canceledRides; set => canceledRides = value; }
    public int RateCount { get => rateCount; set => rateCount = value; }
}