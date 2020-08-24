using Requests;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
public class CarDetails : Panel
{
  public Text carName, carBrand, carYear, carSeats, carColor, carLuggages;
  public Image carImage;
  public Car car=null;
  public AddCarPanel addCarPanel;
  
  public void Init(Car car)
  {
    this.car = car;
    carName.text = car.Name;
    carBrand.text = car.Brand;
    carYear.text = car.Year.ToString();
    carSeats.text = car.MaxSeats.ToString();
    carLuggages.text = car.MaxLuggage.ToString();
    carColor.text = car.Color.ToString();
    carImage.sprite = Program.GetImage(car.Picture);

  }
  public void ViewModel()
  {
    Panel panel = PanelsFactory.CreateImageViewer(car.Picture);
    OpenDialog(panel);
  }
    public void UpdateCar()
    {
        AddCarPanel panel = PanelsFactory.CreateAddCar();
        Open(panel, () => { panel.Init(car); });
    }
    public void Delete()
    {
        Debug.Log("Deleting car1");
        addCarPanel.DeleteCar(car);
        Debug.Log("Deleting car2");
        Destroy(gameObject);
    }

  internal override void Clear()
  {
    carName.text = "";
    carBrand.text = "";
    carYear.text = "";
    carSeats.text = "";
    carColor.text = "";
    carLuggages.text = "";
  }

}
