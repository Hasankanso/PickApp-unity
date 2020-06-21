using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyRidePanel : Panel
{
  public ListView listMyRidesView;
  public Person person;

  public void Init(Person person)
  {
    Clear();
    StatusProperty = Status.VIEW;
    this.person = person;
    ImplementYourRidesList(person.UpcomingRides);
  }

  public void ImplementYourRidesList(List<Ride> rides)
  {
    if ((rides != null))
    {
      listMyRidesView.Clear();
      foreach (Ride o in rides)
      {
        var item = ItemsFactory.CreateMyRideItem(listMyRidesView.scrollContainer, o, person, this);
        listMyRidesView.Add(item.gameObject);
      }
    }
  }
  internal override void Clear()
  {
    person = null;
  }
}
