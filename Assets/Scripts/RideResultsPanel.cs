using Requests;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class RideResultsPanel : Panel
{

  private List<Ride> allRides;
  private List<Ride> filteredRides;
  public Text from, to, rideNumber;
  public GameObject filterView;
  public SearchInfo searchInfo;
  public ListView resultsList;
  private List<RideItem> rideItems = new List<RideItem>();
  public Text clearFilterButton;
  public Transform noResultsView;
  public Dropdown sortByDd;

  //Filter UI
  public Toggle onlyFemales;
  public Toggle onlyEmptyCars;
  public Toggle atLeast3Stars;
  public Text onlyFemalesText;
  public InputField minPrice, maxPrice;

  public void Init(List<Ride> rides, SearchInfo searchInfo)
  {
    Clear();
    this.allRides = rides;
    this.from.text = searchInfo.From.ToString();
    this.to.text = searchInfo.To.ToString();
    this.rideNumber.text = rides.Count.ToString();
    this.searchInfo = searchInfo;

    if (rides.Count == 0)
    {
      noResultsView.gameObject.SetActive(true);
      return;
    }

    //there's results
    SortBy(); //sort and add rides.
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
    RideDetails rd = PanelsFactory.CreateRideDetails();
    Open(rd, () => { rd.Init(r); });
  }

  public void ApplyFilter()
  {
    if (Validate())
    {

      filteredRides = allRides.FindAll(r => true);
      int minPriceInt = 0;
      int.TryParse(minPrice.text, out minPriceInt);

      int maxPriceInt = int.MaxValue;
      int.TryParse(maxPrice.text, out maxPriceInt);

      if (!string.IsNullOrEmpty(minPrice.text))
      {
        filteredRides = filteredRides.FindAll(r => r.Price >= minPriceInt);
      }

      if(!string.IsNullOrEmpty(maxPrice.text))
      {
        filteredRides = filteredRides.FindAll(r => r.Price <= maxPriceInt);
      }

      if (onlyFemales.isOn)
      {
        filteredRides = filteredRides.FindAll(r => r.Person.Gender == false);
      }

      /*
      if (onlyEmptyCars.isOn)
      {
        filteredRides = filteredRides.FindAll(r => r.ReservedSeats == 0);
      }

      if(atLeast3Stars.isOn){
        filteredRides = filteredRides.FindAll(r => r.Person.RateAverage >= 3);
      }
      */

      resultsList.Clear();
      foreach (Ride ri in filteredRides)
      {
        AddItemToList(ri);
      }

      if (filteredRides.Count == 0)
      {
        noResultsView.gameObject.SetActive(true);
      }
      else
      {
        noResultsView.gameObject.SetActive(false);
      }

      CloseFilter();
      clearFilterButton.gameObject.SetActive(true);
    }
  }

  public void ClearFilter()
  {
    resultsList.Clear();
    clearFilterButton.gameObject.SetActive(false);
    onlyFemales.isOn = false;
    onlyEmptyCars.isOn = false;
    atLeast3Stars.isOn = false;
    minPrice.text = "";
    maxPrice.text = "";
    if (allRides != null)
    {
      foreach (Ride r in allRides)
      {
        AddItemToList(r);
      }
      noResultsView.gameObject.SetActive(false);
    }

  }
  private bool Validate()
  {
    bool valid = true;
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

  internal override void Clear()
  {
    allRides = null;
    from.text = "";
    to.text = "";
    rideNumber.text = "";
    CloseFilter();
    ClearFilter();
    searchInfo = null;
    noResultsView.gameObject.SetActive(false);
  }

  public void SortBy()
  {
    if (sortByDd.value == 0)
    {
      allRides.Sort(new PriceComparer(true));
    }
    else if (sortByDd.value == 1)
    {
      allRides.Sort(new RateComparer(false));
    }
    else if (sortByDd.value == 2)
    {
      allRides.Sort(new DepartureComparer(true));
    }
    else if (sortByDd.value == 3)
    {
      allRides.Sort(new DepartureComparer(false));
    }
    resultsList.Clear();

    foreach (Ride ri in allRides)
    {
      AddItemToList(ri);
    }
  }

  private class DepartureComparer : IComparer<Ride>
  {
    private bool ascending = true;
    public DepartureComparer(bool ascending)
    {
      this.ascending = ascending;
    }
    public int Compare(Ride x, Ride y)
    {
      // Ride x = ri1.ride;
      // Ride y = ri2.ride;
      if (ascending)
        return x.LeavingDate > y.LeavingDate ? 1 : x.LeavingDate < y.LeavingDate ? -1 : 0;
      else
        return x.LeavingDate > y.LeavingDate ? -1 : x.LeavingDate < y.LeavingDate ? 1 : 0;
    }
  }

  /*  private class ArrivingComparer : IComparer<Ride>
    {
      public int Compare(Ride x, Ride y)
      {
        return x.arrive > y.LeavingDate ? 1 : x.LeavingDate == y.LeavingDate ? 0 : -1;
      }
    }*/

  private class PriceComparer : IComparer<Ride>
  {
    private bool ascending = true;
    public PriceComparer(bool ascending)
    {
      this.ascending = ascending;
    }

    public int Compare(Ride x, Ride y)
    {
      if (ascending)
        return x.Price > y.Price ? 1 : x.Price < y.Price ? -1 : 0;
      else
        return x.Price > y.Price ? -1 : x.Price < y.Price ? 1 : 0;
    }
  }

  private class RateComparer : IComparer<Ride>
  {
    private bool ascending = true;
    public RateComparer(bool ascending)
    {
      this.ascending = ascending;
    }

    public int Compare(Ride x, Ride y)
    {
      //     Ride x = ri1.ride;
      //   Ride y = ri2.ride;
      if (ascending)
        return x.Person.RateAverage > y.Person.RateAverage ? 1 : x.Person.RateAverage < y.Person.RateAverage ? -1 : 0;
      else
        return x.Person.RateAverage > y.Person.RateAverage ? -1 : x.Person.RateAverage < y.Person.RateAverage ? 1 : 0;
    }
  }

  public void OnAlertClicked()
  {
    AlertPanel alertPanel = PanelsFactory.CreateAlert();
    Open(alertPanel, () => { alertPanel.Init(searchInfo); });
  }
}
