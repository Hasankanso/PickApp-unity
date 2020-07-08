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

public class ProfilePanel : Panel
{
  public Text fullName, ratings;
  public Scrollbar scroll;
  public ListView listCarView;
  public GameObject mainContent, carContainer, scheduleContainer, scrollView, scheduleLabel, becomeDriverLabel, regionsLabel;
  public ListView listSchduleView;
  public Image profilePicture;
  //  private Person person = null;

  public void Init()
  {
    Clear();

    Person person = Program.Person;
    Driver driver = Program.Driver;

    if (Program.Person.ProfilePicture == null)
      StartCoroutine(Program.RequestImage(person.ProfilePictureUrl, Succed, Error));
    Status = StatusE.VIEW;
    fullName.text = person.FirstName + " " + person.LastName;
    ratings.text = person.RateAverage.ToString() + "/5 ";
    if (driver != null)
    {
      scheduleLabel.SetActive(true);
      regionsLabel.SetActive(true);
      becomeDriverLabel.SetActive(false);
      ImplementCarList(driver.Cars);
      ImplementScheduleList(driver.Schedules);
    }
    else
      becomeDriverLabel.SetActive(true);
  }
  private void Succed(Texture2D t)
  {
    profilePicture.sprite = Program.GetImage(t);
    Program.Person.ProfilePicture = t;
  }
  private void Error(string error)
  {
    OpenDialog(error, false);
  }

  public void ImplementCarList(List<Car> cars)
  {
    if ((cars != null) && cars.Count > 0)
    {
      mainContent.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 1345.8f);
      carContainer.SetActive(true);
      listCarView.Clear();
      foreach (Car o in cars)
      {
        var item = ItemsFactory.CreateCarItem(listCarView.scrollContainer, o, this);
        listCarView.Add(item.gameObject);
      }
    }
  }
  public void ImplementScheduleList(List<ScheduleRide> schedules)
  {
    Person person = Program.Person;
    if ((schedules != null) && schedules.Count > 0 && Program.Driver.Cars.Count > 0)
    {
      scheduleLabel.SetActive(false);
      scheduleContainer.SetActive(true);
      mainContent.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 1856.4f);
      listSchduleView.Clear();
      foreach (ScheduleRide o in schedules)
      {
        var item = ItemsFactory.CreateScheduleItem(listSchduleView.scrollContainer, o, this);
        listSchduleView.Add(item.gameObject);
      }
    }
  }
  public void EditAccount()
  {
    Panel panel = PanelsFactory.CreateAccountPanel();
    openCreated(panel);
  }
  public void BecomeDriver()
  {
    Panel panel = PanelsFactory.CreateBecomeDriver();
    openCreated(panel);
  }
  public void EditRegions()
  {
    Panel panel = PanelsFactory.CreateBecomeDriver();
    openCreated(panel);
  }
  public void EditBio()
  {
    Panel panel = PanelsFactory.CreateBioPanel();
    openCreated(panel);
  }
  public void EditChattiness()
  {
    Panel panel = PanelsFactory.CreateChattinessPanel();
    openCreated(panel);
  }
  public void AddSchedule()
  {
    Driver driver = Program.Driver;
    if (driver.Cars.Count > 0)
    {
      if (driver.Schedules.Count < Program.MaxSchedulesPerUser)
      {
        SchedulePanel panel = PanelsFactory.CreateAddSchedule();
        panel.Init();
        openCreated(panel);
      }
      else
        OpenDialog("You have added the maximum number of schedule rides", false);
    }
    else
    {
      OpenDialog("Please add a car first.", false);
    }
  }
  public void AddCar()
  {
    Driver driver = Program.Driver;
    if (driver.Cars.Count < 4)
    {
      Panel panel = PanelsFactory.createAddCar();
      openCreated(panel);
    }
    else
      OpenDialog("You have added the maximum number of cars", false);

  }
  public void OpenSettings()
  {
    Panel panel = PanelsFactory.createSettings();
    openCreated(panel);
  }
  public void OpenUserRatings()
  {
    Panel panel = PanelsFactory.CreateUserRatings(Program.User);
    openCreated(panel);
  }
  public void OpenMyRidesHistory()
  {
    Panel panel = PanelsFactory.CreateMyRidesHistory();
    openCreated(panel);
  }
  internal override void Clear()
  {
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
