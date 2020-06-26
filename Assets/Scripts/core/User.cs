using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class User
{
  public string phone;
  public string password;
  private string email;
  public string id;
  private string token;


  private Person person;
  private Driver driver;
  private string text;

  public JObject ToJson()
  {
    JObject userJ = new JObject();
    userJ[nameof(this.email)] = this.email;
    userJ[nameof(this.password)] = this.password;
    return userJ;
  }

  public static User ToObject(JObject json)
  {
    string token = "";
    var tk = json["user-token"];
    if (tk != null)
      token = tk.ToString();
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

    JObject driverJ = (JObject)json["driver"];
    JObject personJ = (JObject)json["person"];

    Person person = Person.ToObject(personJ);
    Driver driver = null;
    if (driverJ != null)
    {
      driver = Driver.ToObject(driverJ);
    }

    return new User(person, driver, phone, "", email, userId, token);
  }

  public User(Person person, Driver driver)
  {
    this.person = person;
    this.driver = driver;
  }

  public User(Person person, Driver driver, string phone, string password, string email, string userId, string token)
  {
    this.person = person;
    this.driver = driver;
    this.phone = phone;
    this.password = password;
    this.email = email;
    this.id = userId;
    this.token = token;
  }

  public User(string phone, string password)
  {
    this.phone = phone;
    this.password = password;
  }

  public User(Person person, string text)
  {
    this.person = person;
    this.text = text;
  }

  public string Phone { get => phone; set => phone = value; }
  public string Password { get => password; set => password = value; }
  public string Email { get => email; set => email = value; }
  public string Id { get => id; set => id = value; }
  public Person Person { get => person; set => person = value; }
  public Driver Driver { get => driver; set => driver = value; }
  public string Token { get => token; set => token = value; }
}
