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
            Request<Car> request = new DeleteCar(car, Program.Driver);
            request.AddSendListener(OpenSpinner);
            request.AddReceiveListener(CloseSpinner);
            request.Send(response);
        
    }
    private void response(Car result, int code, string message)
    {
        if (!code.Equals((int)HttpStatusCode.OK))
        {
        }
        if (code == 1)
        {
            Destroy(gameObject);
            FooterMenu.dFooterMenu.OpenProfilePanel();
            OpenDialog(message, false);
            // MissionCompleted(ProfilePanel.PANELNAME, message);

        }

        else 
        {
            FooterMenu.dFooterMenu.OpenProfilePanel();
            OpenDialog(message, true);
            DestroyForwardBackward();

        }
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
