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
    public GameObject mainContent, carContainer, scheduleContainer, scrollView, scheduleLabel, becomeDriverLabel, regionsLabel;
    public ListView listSchduleView;
    public Image profilePicture;
    private Person person = null;
    private AddRidePanel addRidePanel;

    public void init(Person p,AddRidePanel addRidePanel) {
        Clear();
        this.person = p;
        if (Program.User.ProfilePicture == null)
            StartCoroutine(Panel.RequestImage(p.ProfilePictureUrl, Succed, Error));
        StatusProperty = Status.VIEW;
        this.addRidePanel = addRidePanel;
        fullName.text = p.FirstName + " " + p.LastName;
        ratings.text = p.RateAverage.ToString() + "/5 ";
        if (p.IsDriver) {
            scheduleLabel.SetActive(true);
            regionsLabel.SetActive(true);
            becomeDriverLabel.SetActive(false);
            ImplementCarList(p.Cars);
            ImplementScheduleList(p.Schedules);
        } else
            becomeDriverLabel.SetActive(true);
    }
    private void Succed(Texture2D t) {
        profilePicture.sprite = Program.GetImage(t);
        Program.User.ProfilePicture = t;
    }
    private void Error(string error) {
        OpenDialog(error,false);
    }

    public void ImplementCarList(List<Car> cars) {
        if ((cars != null) && cars.Count > 0) {
            mainContent.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 1345.8f);
            carContainer.SetActive(true);
            listCarView.Clear();
            foreach (Car o in cars) {
                var item = ItemsFactory.CreateCarItem(listCarView.scrollContainer, o, this);
                listCarView.Add(item.gameObject);
            }
        }
    }
    public void ImplementScheduleList(List<ScheduleRide> schedules) {
        if ((schedules != null) && schedules.Count > 0 && person.Cars.Count>0) {
            scheduleLabel.SetActive(false);
            scheduleContainer.SetActive(true);
            mainContent.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 1856.4f);
            listSchduleView.Clear();
            foreach (ScheduleRide o in schedules) {
                var item = ItemsFactory.CreateScheduleItem(listSchduleView.scrollContainer, o, this, addRidePanel);
                listSchduleView.Add(item.gameObject);
            }
        }
    }
    public void EditAccount() {
        Panel panel = PanelsFactory.CreateAccountPanel(person);
        openCreated(panel);
    }
    public void BecomeDriver() {
        Panel panel = PanelsFactory.CreateBecomeDriver(person);
        openCreated(panel);
    }
    public void EditRegions() {
        Panel panel = PanelsFactory.CreateBecomeDriver((Driver)person);
        openCreated(panel);
    }
    public void EditBio() {
        Panel panel = PanelsFactory.CreateBioPanel(person);
        openCreated(panel);
    }
    public void EditChattiness() {
        Panel panel = PanelsFactory.createChattinessPanel(person);
        openCreated(panel);
    }
    public void AddSchedule() {
        if (person.Cars.Count > 0) {
            if (person.Schedules.Count < Program.MaxSchedulesPerUser) {
                SchedulePanel panel = PanelsFactory.CreateAddSchedule();
                panel.Init();
                openCreated(panel);
            } else
                OpenDialog("You have added the maximum number of schedule rides", false);
        } else {
            OpenDialog("Please add a car first.", false);
        }
    }
    public void AddCar() {
        if (person.Cars.Count < 4) {
            Panel panel = PanelsFactory.createAddCar();
            openCreated(panel);
        } else
            OpenDialog("You have added the maximum number of cars", false);

    }
    public void OpenSettings() {
        Panel panel = PanelsFactory.createSettings(person);
        openCreated(panel);
    }
    public void OpenUserRatings() {
        Panel panel = PanelsFactory.createUserRatings(person);
        openCreated(panel);
    }
    public void OpenBookingHistory() {
        Panel panel = PanelsFactory.createBookingHistory(person);
        openCreated(panel);
    }
    internal override void Clear() {
        scroll.value = 1;
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
