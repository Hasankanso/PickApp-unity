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
    public GameObject removeCarDialog;
    public Text carName,
    carBrand,
    carYear,
    carSeats,
    carColor,
    carLuggages;
    public Image carImage;
    public Car car = null;
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
    public void OpenCarDialog()
    {
        removeCarDialog.gameObject.SetActive(true);
    }

    public void CloseCarDialog()
    {
        removeCarDialog.gameObject.SetActive(false);
    }
    public void ViewModel()
    {
        Panel panel = PanelsFactory.CreateImageViewer(car.Picture);
        OpenDialog(panel);
    }
    public void UpdateCar()
    {
        AddCarPanel panel = PanelsFactory.CreateAddCar();
        Open(panel, () => {
            panel.Init(car);
        });
    }
    public void Delete()
    {
        if (ValidateDelete())
        {
            Request<List<Car>> request = new DeleteCar(car, Program.User);
            request.AddSendListener(OpenSpinner);
            request.AddReceiveListener(CloseSpinner);
            request.Send(response);
        }

    }
    private void response(List<Car> result, int code, string message)
    {
        if (!code.Equals((int)HttpStatusCode.OK))
        {
            OpenDialog(message, false);
        }
        else
        {
            Program.Driver.Cars = result;
            MissionCompleted(ProfilePanel.PANELNAME, "Car has been Deleted");
        }
    }
    internal override void Clear()
    {
        CloseCarDialog();
        carName.text = "";
        carBrand.text = "";
        carYear.text = "";
        carSeats.text = "";
        carColor.text = "";
        carLuggages.text = "";
    }
    public bool ValidateDelete()
    {
        bool valid = true;
        if (Program.Driver.Cars.Count < 1)
        {
            valid = false;
        }
        return valid;
    }

}