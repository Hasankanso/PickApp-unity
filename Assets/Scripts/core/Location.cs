﻿using Newtonsoft.Json.Linq;
using Requests;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Location
{
  private string name, id, placeId;
  private double latitude, longitude;
  private DateTime updated;
  public Location(string placeId, string name)
  {
    this.placeId = placeId;
    this.name = name;
    latitude = 0;
    longitude = 0;
  }

  public Location(string placeId, string name, double lati, double longi)
  {
    this.placeId = placeId;
    this.name = name;
    latitude = lati;
    longitude = longi;
  }

  public JObject ToJson()
  {
    JObject locJ = new JObject();
    locJ[nameof(placeId)] = placeId;
    locJ[nameof(name)] = Name;
    locJ[nameof(latitude)] = Latitude;
    locJ[nameof(longitude)] = Longitude;


    return locJ;
  }

  public override string ToString()
  {
    return name;
  }

  public static Location ToObject(JObject json)
  {
    //string id = json[nameof(id)].ToString();
    string name = json[nameof(name)].ToString();
    string placeId = json[nameof(placeId)].ToString();
    double longitude = 0, latitude = 0;

    var position = json["position"];

    if (position.HasValues)
    {
      JToken coor = position["coordinates"];
      if (coor != null)
      {
        Double.TryParse(((JArray)coor)[0].ToString(), out longitude);
        Double.TryParse(((JArray)coor)[1].ToString(), out latitude);
      }
    }
    return new Location(placeId, name, latitude, longitude);
  }

  public bool Equals(Location other)
  {
    return Math.Sqrt((longitude - other.longitude)*(longitude - other.longitude) + (latitude - other.latitude)*(latitude - other.latitude)) < 1;
  }

  public static string Validate(Location location)
  {
    if (string.IsNullOrEmpty(location.PlaceId))
    {
      return "Location placeId should not be empty";
    }
    if (string.IsNullOrEmpty(location.name))
    {
      return "Location name should not be empty";
    }
    return string.Empty;
  }
  public string PlaceId { get => placeId; }
  public string Name { get => name; }
  public double Latitude { get => latitude; }
  public double Longitude { get => longitude; }
  public DateTime Updated { get => updated; set => updated = value; }
}
