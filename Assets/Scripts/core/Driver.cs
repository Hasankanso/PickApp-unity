using Newtonsoft.Json.Linq;
using Requests;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Driver
{
    public List<Location> regions = new List<Location>();
    List<Car> cars;
    string id;
    //private List<ScheduleRide> schedules = new List<ScheduleRide>();

    private DateTime updated;
    public Driver(string dId, List<Car> cars/*, List<ScheduleRide> schedules*/, List<Location> regions)
    {
        this.cars = cars;
       // this.schedules = schedules;
        this.id = dId;
        this.regions = regions;

    }
    public Driver(string dId, List<Car> cars)
    {
        this.cars = cars;
        this.id = dId;
    }
    public Driver(List<Location> regions)
    {
        this.Regions = regions;
    }
    public override bool Equals(object d)
    {
        return id == ((Driver)d).id;
    }
    public JObject ToJson()
    {

        JObject driverJ = new JObject();
        JArray regionsArray = new JArray();
        JArray carsArray = new JArray();
        JArray schedulesArray = new JArray();

        foreach (Location r in regions)
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
        /*foreach (ScheduleRide s in schedules)
        {
            schedulesArray.Add(s.ToJson());
        }
        driverJ[nameof(schedules)] = schedulesArray;*/
        return driverJ;
    }

    public static Driver ToObject(JObject driver)
    {
        string did = "";
        var dId = driver["objectId"];
        if (dId != null) did = dId.ToString();
        JArray carsJ = (JArray)driver.GetValue("cars");
        List<Car> cars = null;

        //List<ScheduleRide> schedules = new List<ScheduleRide>();
        List<Location> regions = new List<Location>(3);

        if (carsJ != null)
        {
            cars = new List<Car>();
            foreach (var car in carsJ)
            {
                cars.Add(Car.ToObject((JObject)car));
            }
        }

        var reg1 = driver["region1"];
        Location regL1 = null;
        if (reg1 != null && reg1.HasValues)
        {
            regL1 = Location.ToObject((JObject)reg1);
            regions.Add(regL1);
        }

        var reg2 = driver["region2"];
        Location regL2 = null;
        if (reg2 != null && reg2.HasValues)
        {
            regL2 = Location.ToObject((JObject)reg2);
            regions.Add(regL2);
        }

        var reg3 = driver["region3"];
        Location regL3 = null;
        if (reg3 != null && reg3.HasValues)
        {
            regL3 = Location.ToObject((JObject)reg3);
            regions.Add(regL3);
        }

       /* if (driver.GetValue("schedules") != null && !string.IsNullOrEmpty(driver.GetValue("schedules").ToString()) && !driver.GetValue("schedules").ToString().Equals("[]"))
        {
            JArray schedulesJ = (JArray)driver.GetValue("schedules");

            foreach (var schedule in schedulesJ)
            {
                schedules.Add(ScheduleRide.ToObject((JObject)schedule));
            }
        }*/

        return new Driver(did, cars,/* schedules,*/ regions);
    }
    public List<Car> Cars
    {
        get => cars;
        set => cars = value;
    }
    public List<Location> Regions
    {
        get => regions;
        set => regions = value;
    }
    public string Did
    {
        get => id;
        set => id = value;
    }
    public DateTime Updated
    {
        get => updated;
        set => updated = value;
    }
   /* public List<ScheduleRide> Schedules
    {
        get => schedules;
        set => schedules = value;
    }*/
    public string Id
    {
        get => id;
        set => id = value;
    }
}