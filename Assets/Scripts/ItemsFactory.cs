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
  public BookingHistoryItem bookingHistoryItem;
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

  public static CarItem CreateCarItem(GameObject parent, Car car, Action<Car, CarItem> OnClickFunction)
  {
    CarItem caritem = Instantiate(defaultItemsFactory.carItem);
    caritem.transform.SetParent(parent.transform, false);
    caritem.Init(car, OnClickFunction);
    return caritem;
  }

  public static RegionItem CreateRegionItem(GameObject parent)
  {
    RegionItem obj = Instantiate(defaultItemsFactory.regionItem);
    obj.transform.SetParent(parent.transform, false);
    obj.Init();
    return obj;
  }
  public static RegionItem CreateRegionItem(GameObject parent, string regions)
  {
    RegionItem obj = Instantiate(defaultItemsFactory.regionItem);
    obj.transform.SetParent(parent.transform, false);
    obj.Init(regions);
    return obj;
  }
  public static RatingItem CreateRatingItem(GameObject parent, Rate o)
  {
    RatingItem ratingItem = Instantiate(defaultItemsFactory.ratingItem);
    ratingItem.transform.SetParent(parent.transform, false);
    ratingItem.init(o.Date, o.Grade, o.Comment, o.Reviewer.FirstName + " " + o.Reviewer.LastName, o.Reviewer.ProfilePicture);
    return ratingItem;
  }
  public static CarItem CreateCarItem(GameObject parent, Car car, Panel profilePanel)
  {
    CarItem caritem = Instantiate(defaultItemsFactory.carItem);
    caritem.transform.SetParent(parent.transform, false);
    caritem.init(car, profilePanel);
    return caritem;
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
    MyRideItem myRideItem = Instantiate(defaultItemsFactory.myRideItem);
    myRideItem.transform.SetParent(parent.transform, false);
    myRideItem.Init(ride, person, yourRidesPanel);
    return myRideItem;
  }
  public static BookingHistoryItem CreateBookingHistoryItem(GameObject parent, Ride o)
  {
    BookingHistoryItem bookingHistoryItem = Instantiate(defaultItemsFactory.bookingHistoryItem);
    bookingHistoryItem.transform.SetParent(parent.transform, false);
    bookingHistoryItem.init( Program.DateToString(o.LeavingDate), o.Price + o.CountryInformations.Unit, o.From.Name, o.To.Name, o.User.Person.FirstName, o.User.Person.ProfilePicture);
    return bookingHistoryItem;
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