using Requests;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RideResultsPanel : Panel
{

  private List<Ride> rides;
  public Dropdown gender;
  public InputFieldScript rate;
  public Text from, to, rideNumber;
  public GameObject filterView;
  public SearchInfo searchInfo;
  public ListView resultsList;
  private List<RideItem> rideItems = new List<RideItem>();
  public Text filterButton, clearFilterButton;
  public Transform noResultsPanel;
  public void Init(List<Ride> rides, SearchInfo searchInfo)
  {
    Clear();
    this.rides = rides;
    this.from.text = searchInfo.From.ToString();
    this.to.text = searchInfo.To.ToString();
    this.rideNumber.text = rides.Count + " rides available";
    this.searchInfo = searchInfo;

    if (rides.Count == 0)
    {
      noResultsPanel.gameObject.SetActive(true);
      return;
    }

    //there's results
    foreach (Ride r in rides)
    {
      AddItemToList(r);
    }
    DownloadAndAddImages();
  }

  private async void DownloadAndAddImages()
  {
    foreach (RideItem ri in rideItems)
    {
      ri.SetPPicture(await Request<object>.DownloadImage(ri.ride.User.Person.ProfilePictureUrl));
    }
  }
  private void AddItemToList(Ride r)
  {
    var item = ItemsFactory.CreateRideItem(resultsList.scrollContainer, r, OnRidePick);
    resultsList.Add(item.gameObject);
    rideItems.Add(item);
  }

  public void OnRidePick(Ride r)
  {

    bool owner = false;
    if (Program.User != null && Program.Driver != null)
      owner = Program.User.Equals(r.User);
    RideDetails rd = PanelsFactory.CreateRideDetails(false, r, owner, Status.VIEW);
    openCreated(rd);
  }

  public void ApplyFilter()
  {
    if (Validate())
    {
      resultsList.Clear();
      bool isNoResult = true;
      if (gender.value != 0)
      {
        for (int i = 0; i < rideItems.Count; i++)
        {
          if ((gender.value == 1) == rideItems[i].ride.User.Person.Gender)
          {
            rideItems[i].gameObject.SetActive(true);
            isNoResult = false;
          }
          else
          {
            rideItems[i].gameObject.SetActive(false);
          }
        }
      }
      if (!rate.text.text.Equals(""))
      {
        for (int i = 0; i < rideItems.Count; i++)
        {
          if (float.Parse(rate.text.text) == rideItems[i].ride.User.Person.RateAverage)
          {
            rideItems[i].gameObject.SetActive(true);
            isNoResult = false;
          }
          else
          {
            rideItems[i].gameObject.SetActive(false);
          }
        }
      }
      if (isNoResult)
      {
        OpenDialog("No results found", false);
      }
      rate.Reset();
      gender.value = 0;
      CloseFilter();
      filterButton.gameObject.SetActive(false);
      clearFilterButton.gameObject.SetActive(true);
    }
  }

  public void ClearFilter()
  {
    resultsList.Clear();
    clearFilterButton.gameObject.SetActive(false);
    filterButton.gameObject.SetActive(true);
    foreach (Ride r in rides)
    {
      AddItemToList(r);
    }

  }
  private bool Validate()
  {
    bool valid = true;
    if (gender.value == 0 && rate.text.text.Equals(""))
    {
      OpenDialog("Please choose filter", false);
      valid = false;
    }
    if (!rate.text.text.Equals("") && (float.Parse(rate.text.text) < 0 || float.Parse(rate.text.text) > 5))
    {
      rate.Error();
      OpenDialog("Invalid rate", false);
      valid = false;
    }
    return valid;
  }
  public void OpenFilter()
  {
    filterView.SetActive(true);
  }
  public void CloseFilter()
  {
    filterView.SetActive(false);
  }
  public void OnRideBook(bool confirmed)
  {
    if (confirmed)
    {
      //book;
    }

  }
  internal override void Clear()
  {
    rides = null;
    from.text = "";
    to.text = "";
    rideNumber.text = "";
    CloseFilter();
    searchInfo = null;
    rate.Reset();
    gender.value = 0;
    filterButton.gameObject.SetActive(true);
    clearFilterButton.gameObject.SetActive(false);
    noResultsPanel.gameObject.SetActive(false);
  }

  public void OnAlertClicked()
  {
    AlertPanel alertPanel = PanelsFactory.createAlert(searchInfo);
    openCreated(alertPanel);
  }
}
