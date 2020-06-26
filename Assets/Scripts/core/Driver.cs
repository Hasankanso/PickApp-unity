using Newtonsoft.Json.Linq;
using Requests;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Driver
{
  List<string> regions = new List<string>();
  List<Car> cars;
  string id;
  private List<ScheduleRide> schedules = new List<ScheduleRide>();

  private DateTime updated;
  public Driver(string dId, List<Car> cars, List<ScheduleRide> schedules)
  {
    this.cars = cars;
    this.schedules = schedules;
    this.id = dId;
  }
  public Driver(string dId, List<Car> cars)
  {
    this.cars = cars;
    this.id = dId;
  }
  public Driver(List<string> regions)
  {
    this.Regions = regions;
  }
  public Driver(string id, string firstName, string lastName, DateTime birthday, string email, string phone, string password, string region, Texture2D profilePicture, bool gender, List<Rate> rates, float rateAverage, List<Car> cars)
  {
    this.id = id;
    this.cars = cars;
  }
  public JObject ToJson()
  {

    JObject driverJ = new JObject();
    JArray regionsArray = new JArray();
    JArray carsArray = new JArray();
    JArray schedulesArray = new JArray();

    foreach (string r in regions)
    {
      //JObject regionJ = new JObject();
      //regionJ["Region"] = r;
      regionsArray.Add(r);
    }
    driverJ[nameof(regions)] = regionsArray;
    foreach (Car c in cars)
    {
      carsArray.Add(c.ToJson());
    }
    driverJ[nameof(cars)] = carsArray;
    foreach (ScheduleRide s in schedules)
    {
      schedulesArray.Add(s.ToJson());
    }
    driverJ[nameof(schedules)] = schedulesArray;
    return driverJ;
  }

  public static Driver ToObject(JObject driver)
  {
    string did = "";
    var dId = driver["objectId"];
    if (dId != null)
      did = dId.ToString();
    JArray carsJ = (JArray)driver.GetValue("cars");
    List<Car> cars = null;
    if (carsJ != null)
    {
      cars = new List<Car>();
      foreach (var car in carsJ)
      {
        cars.Add(Car.ToObject((JObject)car));
      }
    }

    if (driver.GetValue("schedules") != null && !string.IsNullOrEmpty(driver.GetValue("schedules").ToString()) && !driver.GetValue("schedules").ToString().Equals("[]"))
    {
      JArray schedulesJ = (JArray)driver.GetValue("schedules");
      List<ScheduleRide> schedules = new List<ScheduleRide>();
      foreach (var schedule in schedulesJ)
      {
        schedules.Add(ScheduleRide.ToObject((JObject)schedule));
      }
      return new Driver(did, cars, schedules);
    }
    else
      return new Driver(did, cars);
  }
  public List<Car> Cars { get => cars; }
  public List<string> Regions { get => regions; set => regions = value; }
  public string Did { get => id; set => id = value; }
  public DateTime Updated { get => updated; set => updated = value; }
  public List<ScheduleRide> Schedules { get => schedules; set => schedules = value; }
  public string Id { get => id; set => id = value; }
}
