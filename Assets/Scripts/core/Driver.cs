using Newtonsoft.Json.Linq;
using Requests;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Driver : Person {
    List<string> regions = new List<string>();
    List<Car> cars;
    string dId;
    private List<ScheduleRide> schedules = new List<ScheduleRide>();

    private DateTime updated;
    public Driver(string dId,Person person, List<Car> cars, List<ScheduleRide> schedules) : base(person.Id, person.UserId, person.Token, person.FirstName, person.LastName, person.Chattiness, person.Phone, person.Email, person.CountryInformations, person.Bio,person.RateAverage, person.Gender, person.Birthday, person.Updated,person.ProfilePictureUrl) {
        this.cars = cars;
        this.schedules = schedules;
        IsDriver = true;
        this.dId = dId;
    }
    public Driver(string dId, Person person, List<Car> cars) : base(person.Id, person.UserId, person.Token ,person.FirstName, person.LastName, person.Chattiness, person.Phone, person.Email, person.CountryInformations, person.Bio, person.RateAverage, person.Gender, person.Birthday, person.Updated, person.ProfilePictureUrl) {
        this.cars = cars;
        IsDriver = true;
        this.dId = dId;
    }
    public Driver(Person person, List<string> regions) : base(person.FirstName, person.LastName, person.Phone, person.Birthday, person.ProfilePicture, person.CountryInformations, person.Gender) {
        this.Regions = regions;
        IsDriver = true;
    }
    public Driver(string id, string firstName, string lastName, DateTime birthday, string email, string phone, string password, string region, Texture2D profilePicture, bool gender, List<Rate> rates, float rateAverage, List<Car> cars) : base(id, firstName, lastName, birthday, email, phone, password, profilePicture, gender, rates, rateAverage) {
        dId = id;
        this.cars = cars;
        IsDriver = true;
    }
    public JObject ToJson(JObject json) {

        JObject driverJ = new JObject();
        JArray regionsArray = new JArray();
        JArray carsArray = new JArray();
        JArray schedulesArray = new JArray();

        foreach (string r in regions) {
            //JObject regionJ = new JObject();
            //regionJ["Region"] = r;
            regionsArray.Add(r);
        }
        driverJ[nameof(regions)] = regionsArray;
        foreach (Car c in cars) {
            carsArray.Add(c.ToJson());
        }
        driverJ[nameof(cars)] = carsArray;
        foreach (ScheduleRide s in schedules) {
            schedulesArray.Add(s.ToJson());
        }
        driverJ[nameof(schedules)] = schedulesArray;
        driverJ[nameof(Person)] = new JObject { [nameof(Person.Id)] = this.Id };
        return driverJ;
    }

    public static Driver ToObject(JObject json) {
        Person person = Person.ToObject(json);
        JObject pr = (JObject)json["person"];
        JObject driver = (JObject)pr["driver"];
        string did = "";
        var dId = driver["objectId"];
        if (dId != null)
             did = dId.ToString();
        JArray carsJ = (JArray)driver.GetValue("cars");
        List<Car> cars = new List<Car>();
        foreach (var car in carsJ) {
            cars.Add(Car.ToObject((JObject)car));
        }
        if (driver.GetValue("schedules") != null && !string.IsNullOrEmpty(driver.GetValue("schedules").ToString())&& !driver.GetValue("schedules").ToString().Equals("[]")) {
            JArray schedulesJ = (JArray)json.GetValue("schedules");
            List<ScheduleRide> schedules = new List<ScheduleRide>();
            foreach (var schedule in schedulesJ) {
                schedules.Add(ScheduleRide.ToObject((JObject)schedule));
            }
            return new Driver(did,person, cars,schedules);
        } else
            return new Driver(did,person, cars);
    }
    public override List<Car> Cars { get => cars; }
    public List<string> Regions { get => regions; set => regions = value; }
    public string Did { get => dId; set => dId = value; }
    public DateTime Updated { get => updated; set => updated = value; }
    public List<ScheduleRide> Schedules1 { get => schedules; set => schedules = value; }
}
