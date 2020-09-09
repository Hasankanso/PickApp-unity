using Newtonsoft.Json.Linq;
using Quobject.SocketIoClientDotNet.Client;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class User {
    public string phone;
    public string verificationCode;
    private string email;
    public string id;
    public string userStatus;


    private Person person;
    private Driver driver;

    public JObject ToJson() {
        JObject userJ = new JObject();
        userJ[nameof(this.phone)] = this.phone;
        userJ[nameof(this.email)] = this.email;
        userJ[nameof(this.verificationCode)] = this.verificationCode;
        userJ[nameof(this.person)] = person.ToJson();
        return userJ;
    }

    public static User ToObject(JObject json) {
        string userId = "";
        var uId = json["objectId"];
        if (uId != null)
            userId = uId.ToString();
        string phone = "";
        var ph = json[nameof(phone)];
        if (ph != null)
            phone = ph.ToString();
        string email = "";
        var em = json[nameof(email)];
        if (em != null)
            email = em.ToString();
        string userStatus = "";
        var status = json[nameof(userStatus)];
        if (status != null)
            userStatus = status.ToString();
        JObject driverJ = (JObject)json["driver"];
        JObject personJ = (JObject)json["person"];

        Person person = Person.ToObject(personJ);
        Driver driver = null;
        if (driverJ != null) {
            driver = Driver.ToObject(driverJ);
        }

        return new User(person, driver, phone, email, userId, userStatus);
    }

    public User(Person person, Driver driver) {
        this.person = person;
        this.driver = driver;
    }
    public User() {
    }
    public User(Person person, Driver driver, string phone, string email, string userId, string userStatus) {
        this.person = person;
        this.driver = driver; 
        this.phone = phone;    
        this.email = email;
        this.id = userId;
        this.userStatus = userStatus;
    }

    public User(string phone, string verificationCode) {
        this.phone = phone;
        this.verificationCode = verificationCode;
    }

    public User(Person person, string email) {
        this.person = person;
        this.email = email;
    }

    public override bool Equals(object u) {
        return this.person.id == ((User)u).person.id;
    }
    public static string ValidateLogin(User user) {
        if (string.IsNullOrEmpty(user.id)) {
            return "Please login";
        }
        return string.Empty;
    }
    public string Phone { get => phone; set => phone = value; }
    public string Email { get => email; set => email = value; }
    public string UserStatus { get => userStatus; set => userStatus = value; }
    public string Id { get => id; set => id = value; }
    public Person Person { get => person; set => person = value; }
    public Driver Driver { get => driver; set => driver = value; }
}
