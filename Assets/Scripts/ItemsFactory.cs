using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemsFactory : MonoBehaviour
{
  private static ItemsFactory defaultItemsFactory;
  public RideItem rideItem;
  public RoadItem roadItem;
  public MyRidesHistoryItem myRidesHistoryItem;
  public CarItem carItem;
  public MyRideItem myRideItem;
  public RatingItem ratingItem;
  public ScheduleItem scheduleItem;
  public PassengerItem passengerItem;
  public InboxItem inboxItem;
  public RegionItem regionItem;
  public MessageItem messageItem;

  private void Start()
  {
    defaultItemsFactory = this;
  }

  public static MessageItem CreateMessageItem(GameObject parent, Message m)
  {
    MessageItem mItem = Instantiate(defaultItemsFactory.messageItem);
    mItem.transform.SetParent(parent.transform, false);
    mItem.Init(m);
    return mItem;
  }

  public static CarItem CreateCarItem(GameObject parent, Car car, Action<Car, Item> OnClickFunction)
  {
    CarItem caritem = Instantiate(defaultItemsFactory.carItem);
    caritem.transform.SetParent(parent.transform, false);
    caritem.Init(car, OnClickFunction);
    return caritem;
  }

  public static RegionItem CreateRegionItem(GameObject parent,BecomeDriver becomeDriver)
  {
    RegionItem obj = Instantiate(defaultItemsFactory.regionItem);
    obj.transform.SetParent(parent.transform, false);
    obj.Init(becomeDriver);
    return obj;
  }
  public static RegionItem CreateRegionItem(GameObject parent, string regions,BecomeDriver becomeDriver)
  {
    RegionItem obj = Instantiate(defaultItemsFactory.regionItem);
    obj.transform.SetParent(parent.transform, false);
    obj.Init(regions,becomeDriver);
    return obj;
  }
  public static RatingItem CreateRatingItem(GameObject parent, Rate o)
  {
    RatingItem ratingItem = Instantiate(defaultItemsFactory.ratingItem);
    ratingItem.transform.SetParent(parent.transform, false);
    ratingItem.init(o.Date, o.Grade, o.Comment, o.Reviewer.FirstName + " " + o.Reviewer.LastName, o.Reviewer.ProfilePicture);
    return ratingItem;
  }

  public static ScheduleItem CreateScheduleItem(GameObject parent, ScheduleRide scheduleRide, Panel profilePanel)
  {
    ScheduleItem scheduleItem = Instantiate(defaultItemsFactory.scheduleItem);
    scheduleItem.transform.SetParent(parent.transform, false);
    scheduleItem.init(scheduleRide, profilePanel);
    return scheduleItem;
  }
  public static MyRideItem CreateMyRideItem(GameObject parent, Ride ride, Person person, MyRidePanel yourRidesPanel)
  {
        Debug.Log("CreateMyRideItem");
    MyRideItem myRideItem = Instantiate(defaultItemsFactory.myRideItem);
    myRideItem.transform.SetParent(parent.transform, false);
    myRideItem.Init(ride, person, yourRidesPanel);
    return myRideItem;
  }
  public static MyRidesHistoryItem CreateMyRidesHistoryItem(GameObject parent, Ride o)
  {
    MyRidesHistoryItem myRidesHistoryItem = Instantiate(defaultItemsFactory.myRidesHistoryItem);
    myRidesHistoryItem.transform.SetParent(parent.transform, false);
    myRidesHistoryItem.init(o);
    return myRidesHistoryItem;
  }
  public static RideItem CreateRideItem(GameObject parent, Ride ride, Action<Ride> OnItemClick)
  {
    RideItem rideItem = Instantiate(defaultItemsFactory.rideItem);
    rideItem.transform.SetParent(parent.transform, false);
    rideItem.Init(ride, OnItemClick);
    return rideItem;
  }
  public static InboxItem CreateInboxItem(GameObject parent, Chat chat, Person person, Panel inboxPanel)
  {
    InboxItem inboxItem = Instantiate(defaultItemsFactory.inboxItem);
    inboxItem.transform.SetParent(parent.transform, false);
    inboxItem.Init(chat, person, inboxPanel);
    return inboxItem;
  }
  public static RoadItem CreateRoadItem(GameObject parent, DirectionsFinderPanel panel, string details, string points)
  {
    RoadItem roadItem = Instantiate(defaultItemsFactory.roadItem);
    roadItem.transform.SetParent(parent.transform, false);
    roadItem.Init(panel, details, points);
    return roadItem;
  }

  internal static PassengerItem CreatPassengerItem(Passenger o, Panel rideDetails)
  {
    PassengerItem obj = Instantiate(defaultItemsFactory.passengerItem);
    obj.Init(o, rideDetails);
    return obj;
  }
}