using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Requests;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Person {
    public string id;
    public string phone, password;
    private string firstName, lastName, email, bio, chattiness;
    private DateTime birthday;
    private List<Rate> rates;
    private List<Ride> upcomingRides = new List<Ride>();
    private bool gender, isDriver;
    public Texture2D profilePicture;
    public string image, profilePictureUrl;
    private float rateAverage;
    private DateTime updated;
    private CountryInformations countryInformations;

    public Person(string phone, string password) {
        this.phone = phone;
        this.password = password;
    }
    public Person() {
    }
    //Constructor for register
    public Person(string id,string firstName, string lastName, string chattiness, string email, CountryInformations countryInformations, string bio, bool gender,DateTime birthday, string profilePictureUrl) {
        this.Id = id;
        this.firstName = firstName;
        this.bio = bio;
        this.lastName = lastName;
        this.chattiness = chattiness;
        this.ProfilePictureUrl = profilePictureUrl;
        this.email = email;
        this.gender = gender;
        this.countryInformations = countryInformations;
        this.birthday = birthday;
    }

    //Constructor for Edit
    public Person(string firstName, string lastName, string phone, DateTime birthday, Texture2D profilePicture, CountryInformations countryInformations, bool gender) {
        this.firstName = firstName;
        this.lastName = lastName;
        this.phone = phone;
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
    public Person(string id, string firstName, string lastName, DateTime birthday, string email, string phone, string password, Texture2D profilePicture, bool gender, List<Rate> rates, float rateAverage) {
        this.id = id;
        this.FirstName = firstName;
        this.LastName = lastName;
        this.Birthday = birthday;
        this.Email = email;
        this.Phone = phone;
        this.Password = password;
        this.ProfilePicture = profilePicture;
        this.rates = rates;
        this.gender = gender;
        this.rateAverage = rateAverage;
    }
    public JObject ToJson() {
        JObject personJ = new JObject();
        personJ[nameof(this.firstName)] = this.firstName;
        personJ[nameof(this.lastName)] = this.lastName;
        personJ[nameof(this.email)] = this.email;
        personJ[nameof(this.bio)] = this.bio;
        personJ[nameof(this.image)] = this.Image;
        personJ[nameof(this.chattiness)] = this.chattiness;
        personJ[nameof(this.password)] = this.password;
        personJ[nameof(this.countryInformations)] = this.countryInformations.ToJson();
        personJ[nameof(this.birthday)] = this.birthday;
        personJ[nameof(this.gender)] = this.gender;
        return personJ;
    }
    //Constructor for adding bio

    public static Person ToObject(JObject json) {
        string id = "";
        var oId = json["objectId"];
        if (oId != null)
            id = oId.ToString();
        string firstName = "";
        var fn = json["firstName"];
        if (fn != null)
            firstName = fn.ToString();
        string lastName = "";
        var ln = json["lastName"];
        if (ln != null)
            lastName = ln.ToString();
        string chattiness = "";
        var cn = json["chattiness"];
        if (cn != null)
            chattiness = cn.ToString();
        string email = "";
        var em = json["email"];
        if (em != null)
            email = em.ToString();
        string bio = "";
        var bi = json["bio"];
        if (bi != null)
            bio = bi.ToString();
        DateTime birthday=new DateTime();
        var br = json["birthday"];
        //if (br != null)
            //birthday =Program.StringToBirthday(br.ToString());

        CountryInformations countryInformations = global::CountryInformations.ToObject((JObject)json[nameof(Person.countryInformations)]);
        bool gender = false;
        var gn = json[nameof(Person.gender)];
        if (gn != null)
            bool.TryParse(gn.ToString(), out gender);

        string image = json["image"].ToString();
        Person p = new Person(id,firstName, lastName, chattiness, email, countryInformations, bio, gender,birthday, image);
        if (json.GetValue("driver") != null && !string.IsNullOrEmpty(json.GetValue("driver").ToString())) {
            return Driver.ToObject((JObject)json["driver"]); 
        }
        return p;
    }
    public Person(string email) {
        this.email = email;
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
    public string Email { get { return email; } set { email = value; } }
    public string Phone { get { return phone; } set { phone = value; } }
    public DateTime Birthday { get { return birthday; } set { birthday = value; } }
    public string Password { get { return password; } set { password = value; } }
    public CountryInformations CountryInformations { get { return countryInformations; } set { countryInformations = value; } }
    public Texture2D ProfilePicture { get { return profilePicture; } set { profilePicture = value; if (value != null) { Image = Convert.ToBase64String(value.EncodeToPNG()); } } }
    public string Bio { get { return bio; } set { bio = value; } }
    public string Chattiness { get { return chattiness; } set { chattiness = value; } }
    public bool Gender { get { return gender; } set { gender = value; } }
    public List<Ride> UpcomingRides { get { return upcomingRides; } set { upcomingRides = value; } }
    public List<Rate> Rates { get { return rates; } set { rates = value; } }
    public float RateAverage { get => rateAverage; set => rateAverage = value; }
    public bool IsDriver { get => isDriver; set => isDriver = value; }
    virtual public List<Car> Cars { get => null; }
    virtual public List<ScheduleRide> Schedules { get => null; }
    public string Image { get => image; set => image = value; }
    public DateTime Updated { get => updated; set => updated = value; }
    public string ProfilePictureUrl { get => profilePictureUrl; set => profilePictureUrl = value; }
}
