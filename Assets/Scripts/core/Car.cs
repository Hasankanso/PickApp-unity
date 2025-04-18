﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Requests;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class Car
{
  public string id;
  private int year,
  maxLuggage,
  maxSeats;
  private string name,
  color,
  brand,
  carPictureUrl;
  private Texture2D picture;
  private string pictureBase64;
  private DateTime updated;
  public Car(string id)
  {
    this.Id = id;
  }
  public Car(string name, int year, int maxLuggage, int maxSeats, string brand, string color, Texture2D picture)
  {

    this.Year = year;
    this.MaxLuggage = maxLuggage;
    this.MaxSeats = maxSeats;
    this.Brand = brand;
    this.Name = name;
    this.Color = color;
    this.Picture = picture;
  }
  public Car(string id, string name, int year, int maxLuggage, int maxSeats, string brand, string color, string carPictureUrl)
  {
    this.Id = id;
    this.Year = year;
    this.MaxLuggage = maxLuggage;
    this.MaxSeats = maxSeats;
    this.Brand = brand;
    this.Name = name;
    this.Color = color;
    this.CarPictureUrl = carPictureUrl;
  }
  public Car(string id, string name, int year, int maxLuggage, int maxSeats, string brand, string color, Texture2D picture)
  {
    this.id = id;
    this.Year = year;
    this.MaxLuggage = maxLuggage;
    this.MaxSeats = maxSeats;
    this.Brand = brand;
    this.Name = name;
    this.Color = color;
    this.Picture = picture;
  }
  public JObject ToJson()
  {
    JObject carJ = new JObject();
    carJ[nameof(this.id)] = this.id;
    carJ[nameof(this.year)] = this.year;
    carJ[nameof(this.maxLuggage)] = this.maxLuggage;
    carJ[nameof(this.maxSeats)] = this.maxSeats;
    carJ[nameof(this.brand)] = this.brand;
    carJ[nameof(this.name)] = this.name;
    carJ[nameof(this.color)] = this.color;
    carJ[nameof(this.picture)] = this.pictureBase64;
    return carJ;
  }

  public static Car ToObject(JObject json)
  {
    string id = "";
    var oId = json["objectId"];
    if (oId != null) id = oId.ToString();
    string name = "";
    var oName = json["name"];
    if (oName != null) name = oName.ToString();
    int year = -1;
    var oYear = json[nameof(Car.year)];
    if (oYear != null) int.TryParse(oYear.ToString(), out year);
    int maxLuggage = -1;
    var mL = json[nameof(Car.maxLuggage)];
    if (mL != null) int.TryParse(mL.ToString(), out maxLuggage);
    int maxSeats = -1;
    var mS = json[nameof(Car.maxSeats)];
    if (mS != null) int.TryParse(mS.ToString(), out maxSeats);
    string brand = "";
    var oBrand = json["brand"];
    if (oBrand != null) brand = oBrand.ToString();
    string color = "";
    var oColor = json["color"];
    if (oColor != null) color = oColor.ToString();
    string carPictureUrl = json["picture"].ToString();
    return new Car(id, name, year, maxLuggage, maxSeats, brand, color, carPictureUrl);
  }

  public static string Validate(Car car)
  {

    if (car.Year > DateTime.Now.Year || car.Year < 1900) return "Please enter a valid car year";
    if (car.MaxLuggage < 0 || car.MaxLuggage > 10)
    {
      return "Max luggage must be less or equal 10";
    }
    if (car.MaxSeats < 0 || car.MaxSeats > 50)
    {
      return "Max seats must be less or equal 50";
    }
    if (string.IsNullOrEmpty(car.Name) || car.Name.Length < 2)
    {
      return "Invalid car name";
    }

    if (string.IsNullOrEmpty(car.Brand))
    {
      return "Invalid car brand";
    }

    var regexItem = new Regex("/^#[0-9A-F]{6}$/i.test('#AABBCC')");
    if (regexItem.IsMatch(car.Color))
    {
      return "Invalid color";
    }
    if (string.IsNullOrEmpty(car.PictureBase64))
    {
      return "Please enter your car picture";
    }
    return string.Empty;
  }

  public string Id
  {
    get => id;
    set => id = value;
  }
  public int Year
  {
    get => year;
    set => year = value;
  }
  public int MaxLuggage
  {
    get => maxLuggage;
    set => maxLuggage = value;
  }
  public int MaxSeats
  {
    get => maxSeats;
    set => maxSeats = value;
  }
  public string Brand
  {
    get => brand;
    set => brand = value;
  }
  public string Name
  {
    get => name;
    set => name = value;
  }
  public string Color
  {
    get => color;
    set => color = value;
  }
  public Texture2D Picture
  {
    get => picture;
    set
    {
      picture = value;
      if (value != null)
      {
        pictureBase64 = Convert.ToBase64String(value.EncodeToPNG());
      }
    }
  }
  public string PictureBase64
  {
    get => pictureBase64;
  }
  public DateTime Updated
  {
    get => updated;
    set => updated = value;
  }
  public string CarPictureUrl
  {
    get => carPictureUrl;
    set => carPictureUrl = value;
  }

  public override bool Equals(object obj)
  {
    var car = obj as Car;
    return car != null && id == car.id && year == car.year && maxLuggage == car.maxLuggage && maxSeats == car.maxSeats && name == car.name && color == car.color && brand == car.brand;
  }

  public override string ToString()
  {
    return "nice one";
  }

}