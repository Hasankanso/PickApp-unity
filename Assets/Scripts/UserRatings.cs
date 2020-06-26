using Requests;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class UserRatings : Panel
{
  public ListView listView;
  private List<Rate> rates = null;
  User user;

  public void ImplementList(List<Rate> rates)
  {
    if (rates != null)
    {
      listView.Clear();
      foreach (Rate o in rates)
      {
        var item = ItemsFactory.CreateRatingItem(listView.scrollContainer, o);
        listView.Add(item.gameObject);
      }
    }
  }
  public void Init(User user)
  {
    Clear();
    this.user = user;
    GetMyRatings();
  }
  public void GetMyRatings()
  {
    Request<List<Rate>> request = new GetUserReviews(Program.User);
    Task.Run(() => request.Send(Response));
  }
  private void Response(List<Rate> result, HttpStatusCode code, string message)
  {
    if (!code.Equals(HttpStatusCode.OK))
    {
      Panel p = PanelsFactory.CreateDialogBox("Something went wrong", false);
      OpenDialog(p);
    }
    else
    {
      this.rates = result;
      ImplementList(user.Person.Rates);
    }
  }
  internal override void Clear()
  {

  }

}
