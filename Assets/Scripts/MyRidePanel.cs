using Requests;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class MyRidePanel : Panel
{
  public ListView listMyRidesView;

  public void Init()
  {
        Request<List<Ride>> request = new GetMyUpcomingRides(Program.User);
        request.Send(GetUpcomingRides);
        StatusProperty = Status.VIEW;
  }

    private void GetUpcomingRides(List<Ride> arg1, HttpStatusCode arg2, string arg3)
    {
        Program.Person.UpcomingRides = arg1;
        ImplementYourRidesList(Program.Person.UpcomingRides);

    }

    public void ImplementYourRidesList(List<Ride> rides)
    {
        if ((rides != null))
        {     
        listMyRidesView.Clear();
      foreach (Ride o in rides)
      {
        var item = ItemsFactory.CreateMyRideItem(listMyRidesView.scrollContainer, o, Program.Person, this);
        listMyRidesView.Add(item.gameObject);
      }
    }
  }
  internal override void Clear()
  {
  }
}
