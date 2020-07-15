using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CarItem : Item
{
  public Text carName, brand, color, year, maxSeats, maxLuggages;
  public Image carImage;
  public Car car = null;
  public Action<Car, CarItem> OnItemClicked;

  internal void Init(Car c, Action<Car, Item> OnItemClicked)
  {
    Clear();
    this.OnItemClicked = OnItemClicked;
    this.car = c;
    this.carName.text = car.Name;
    this.brand.text = car.Brand;
    this.year.text = car.Year.ToString();
    GetColor(car.Color);
    if (car.Picture != null)
    {
      this.carImage.sprite = Program.GetImage(car.Picture);
    }
    this.maxSeats.text = car.MaxSeats.ToString() + " Seats";
    this.maxLuggages.text = car.MaxLuggage.ToString() + " Luggages";
  }

  internal void SetPicture(Texture2D img)
  {
    car.Picture = img;
    this.carImage.sprite = Program.GetImage(car.Picture);
  }


  internal override void Select()
  {
    background.color = Program.SelectedItemColor();
    maskImage.color = Program.SelectedItemColor();
  }
  internal override void UnSelect()
  {
    background.color = Program.UnSelectedItemColor();
    maskImage.color = Program.UnSelectedItemColor();
  }
  private void GetColor(string carColor)
  {
    if (carColor.Equals("#000000"))
    {
      color.text = "Black";
    }
    else if (carColor.Equals("#ffffff"))
    {
      color.text = "White";
    }
    else if (carColor.Equals("#9F9F9F"))
    {
      color.text = "Grey";
    }
    else if (carColor.Equals("#767676"))
    {
      color.text = "Dark Grey";
    }
    else if (carColor.Equals("#FF0000"))
    {
      color.text = "Red";
    }
    else if (carColor.Equals("#004BFF"))
    {
      color.text = "Blue";
    }
    else if (carColor.Equals("#001196"))
    {
      color.text = "Dark Blue";
    }
    else if (carColor.Equals("#FFFC00"))
    {
      color.text = "Yellow";
    }
    else if (carColor.Equals("#FFA5EA"))
    {
      color.text = "Pink";
    }
    else if (carColor.Equals("#6A0DAD"))
    {
      color.text = "Purple";
    }
    else if (carColor.Equals("#964B00"))
    {
      color.text = "Brown";
    }
    else if (carColor.Equals("#FFA500"))
    {
      color.text = "Orange";
    }
    else if (carColor.Equals("#00DB00"))
    {
      color.text = "Green";
    }
  }
  internal override void Clear()
  {
    this.carName.text = "";
    this.brand.text = "";
    this.year.text = "";
    this.color.text = "";
    this.maxSeats.text = "";
    this.maxLuggages.text = "";
    UnSelect();
  }
  public void OnClick()
  {
    if (OnItemClicked != null)
      OnItemClicked(car, this);
  }


}
