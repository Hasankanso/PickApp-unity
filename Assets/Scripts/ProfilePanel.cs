using Requests;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ProfilePanel : Panel {
    public Text fullName, ratings;
    public Scrollbar scroll;
    public ListView listCarView;
    public List<CarItem> carItems = new List<CarItem>(5);
    public GameObject mainContent, carContainer, scheduleContainer, scrollView, scheduleLabel, becomeDriverLabel, regionsLabel;
    public ListView listSchduleView;
    public Image profilePicture;

    public static readonly string PANELNAME = "PROFILEPANEL";

    public override void Init() {
        Clear();

        Person person = Program.Person;
        Driver driver = Program.Driver;

        if (Program.Person.ProfilePicture == null) {
            StartCoroutine(Program.RequestImage(person.ProfilePictureUrl, Succed, Error));
        } else {
            profilePicture.sprite = Program.GetImage(person.ProfilePicture);
        }
        Status = StatusE.VIEW;
        fullName.text = person.FirstName + " " + person.LastName;
        ratings.text = person.RateAverage.ToString() + "/5 ";
        if (driver != null) {
            scheduleLabel.SetActive(true);
            regionsLabel.SetActive(true);
            becomeDriverLabel.SetActive(false);
            AddCarItemsInList(driver.Cars);
            AddScheduleItemsInList(driver.Schedules);
            DownloadAndAddCarsImages();
        } else {
            becomeDriverLabel.SetActive(true);
        }
    }
    public void SendCode() {

    }
    private async void DownloadAndAddCarsImages() {
        for (int i = 0; i < carItems.Count; i++)
            if (carItems[i].car.Picture == null) {
                Texture2D downloadedImg = await Request<object>.DownloadImage(carItems[i].car.CarPictureUrl);
                carItems[i].SetPicture(downloadedImg);
                Program.Driver.Cars[i].Picture = downloadedImg;
            }
    }

    private void Succed(Texture2D t) {
        profilePicture.sprite = Program.GetImage(t);
        Program.Person.ProfilePicture = t;
    }
    private void Error(string error) {
        OpenDialog(error, false);
    }

    public void AddCarItemsInList(List<Car> cars) {
        if ((cars != null) && cars.Count > 0) {
            mainContent.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 1345.8f);
            carContainer.SetActive(true);
            listCarView.Clear();
            carItems.Clear();

            foreach (Car c in cars) {
                CarItem item = ItemsFactory.CreateCarItem(listCarView.scrollContainer, c, OpenCarDetails);
                carItems.Add(item);
                listCarView.Add(item);
            }
        }
    }

    private void OpenCarDetails(Car car, Item arg2) {
        CarDetails carDetails = PanelsFactory.CreateCarDetails();
        Open(carDetails, () => { carDetails.Init(car); });
    }

    public void AddScheduleItemsInList(List<ScheduleRide> schedules) {
        Person person = Program.Person;
        if ((schedules != null) && schedules.Count > 0 && Program.Driver.Cars.Count > 0) {
            scheduleLabel.SetActive(false);
            scheduleContainer.SetActive(true);
            mainContent.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 1856.4f);
            listSchduleView.Clear();
            foreach (ScheduleRide o in schedules) {
                var item = ItemsFactory.CreateScheduleItem(listSchduleView.scrollContainer, o, this);
                listSchduleView.Add(item.gameObject);
            }
        }
    }
    public void EditAccount() {
        Panel panel = PanelsFactory.CreateAccountPanel();
        Open(panel, () => { panel.Init(); });
    }
    public void BecomeDriver() {
        Panel panel = PanelsFactory.CreateBecomeDriver();
        Status = StatusE.ADD;
        Open(panel, () => { panel.Init(); });
    }

    public void EditRegions() {
        Panel panel = PanelsFactory.CreateBecomeDriver();
        Status = StatusE.VIEW;
        Open(panel, () => { panel.Init(); });
    }
    public void EditBio() {
        Panel panel = PanelsFactory.CreateBioPanel();
        Open(panel);
    }
    public void EditChattiness() {
        Panel panel = PanelsFactory.CreateChattinessPanel();
        Open(panel);
    }
    public void AddSchedule() {
        Driver driver = Program.Driver;
        if (driver.Cars.Count > 0) {
            if (driver.Schedules.Count < Program.MaxSchedulesPerUser) {
                SchedulePanel panel = PanelsFactory.CreateAddSchedule();
                Status = StatusE.ADD;
                Open(panel);
            } else
                OpenDialog("You have added the maximum number of schedule rides", false);
        } else {
            OpenDialog("Please add a car first.", false);
        }
    }

    public void AddCar() {
        Driver driver = Program.Driver;
        if (driver.Cars.Count < 3) {
            Panel panel = PanelsFactory.CreateAddCar();
            Open(panel);
        } else
            OpenDialog("You have added the maximum number of cars", false);
    }
    public void OpenSettings() {
        Panel panel = PanelsFactory.CreateSettings();
        Open(panel);
    }
    public void OpenUserRatings() {
        Panel panel = PanelsFactory.CreateUserRatings();
        Open(panel);
    }
    public void OpenMyRidesHistory() {
        Panel panel = PanelsFactory.CreateMyRidesHistory();
        Open(panel, () => { panel.Init(); });
    }
    public bool ValidateDelete() {
        bool valid = true;
        if (carItems.Count < 1) {
            valid = false;
            OpenDialog("You can't delete the last car", false);
        }
        return valid;
    }




    internal override void Clear() {
        scroll.value = 1;
        carItems.Clear();
        ratings.text = "";
        fullName.text = "";
        mainContent.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, scrollView.GetComponent<RectTransform>().rect.height);
        carContainer.SetActive(false);
        scheduleContainer.SetActive(false);
        becomeDriverLabel.SetActive(true);
        scheduleLabel.SetActive(false);
        regionsLabel.SetActive(false);
    }
}
