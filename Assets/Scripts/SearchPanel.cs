﻿using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Requests;
using System.Net;
using Newtonsoft.Json.Linq;
using ArabicSupport;

public class SearchPanel : Panel
{
  public InputFieldScript from, to;
  public Text minDate, maxDate, nbOfNotifications;
  private Location fromL, toL;
  private SearchInfo info = null;
  public UpDownPicker persons;
  private bool maxDateChanged = false;

  public static readonly string PANELNAME = "SEARCHPANEL";

  public override void Init()
  {
    Clear();
    Status = StatusE.VIEW;
    var minDT = Program.StringToDate(minDate.text);
    var maxDT = Program.StringToDate(maxDate.text);
  }

  public void Search()
  {
    if (Vadilate())
    {
      info = new SearchInfo(fromL, toL, Program.StringToDate(minDate.text), Program.StringToDate(maxDate.text), persons.Value);
      Request<List<Ride>> request = new SearchForRides(info);
      request.AddSendListener(OpenSpinner);
      request.AddReceiveListener(CloseSpinner);
      request.Send(SearchResults);
    }
  }

  public void OpenMinDateTimePicker()
  {
    OpenDateTimePicker(OnMinDatePicked);
  }

  private void OnMinDatePicked(DateTime dt){
    minDate.text = Program.DateToString(dt);
    if (!maxDateChanged)
    {
      maxDate.text = Program.DateToString(dt.AddDays(1));
    } else if(Program.StringToDate(maxDate.text) <= dt){
      maxDate.text = Program.DateToString(dt.AddDays(1));
    }
  }

  public void OpenMaxDateTimePicker()
  {
    OpenDateTimePicker((dt) => maxDate.text = Program.DateToString(dt));
    maxDateChanged = true;
  }

  public void OpenNotificationPanel()
  {
    NotificationScript p = PanelsFactory.CreateNotificationScript();
    p.AddNotificationItem("hello", "me");
    Open(p);
  }

  private void SearchResults(List<Ride> results, int code, string message)
  {
    if (!code.Equals((int)HttpStatusCode.OK))
      OpenDialog(message, false);
    else
    {
      RideResultsPanel p = PanelsFactory.CreateRideResults();
      Open(p, () => { p.Init(results, info); });
    }
  }

  private bool Vadilate()
  {
    if (from.text.text.Equals(""))
    {
      from.Error();
      OpenDialog("From can't be empty!", false);
      return false;
    }
    if (to.text.text.Equals(""))
    {
      to.Error();
      OpenDialog("Going-To can't be empty!", false);
      return false;
    }
    if (minDate.text.Equals(""))
    {
      OpenDialog("Minimum date range can't be empty!", false);
      return false;
    }
    else
    {
      if (Program.StringToDate(minDate.text) < DateTime.Now)
      {
        OpenDialog("Minimum date can't be in the past", false);
        return false;
      }
      if (Program.StringToDate(minDate.text) > Program.MaxAlertDate)
      {
        OpenDialog("The max period of alert is six months", false);
        return false;
      }
    }
    if (maxDate.text.Equals(""))
    {
      OpenDialog("Maximum date range can't be empty!", false);
      return false;
    }
    else
    {
      if (Program.StringToDate(maxDate.text) < DateTime.Now)
      {
        OpenDialog("Invalid maximum date range", false);
        return false;
      }
      if (Program.StringToDate(maxDate.text) > Program.MaxAlertDate)
      {
        OpenDialog("The max period of alert is six months", false);
        return false;
      }
    }
    if (Program.StringToDate(maxDate.text) < Program.StringToDate(minDate.text))
    {
      OpenDialog("The maximum date range couldn't be less tham the minimum.", false);
      return false;
    }
    return true;
  }

  public void OpenFromLocationPicker()
  {
    OpenLocationFinder(from.text.text, OnFromLocationPicked);
  }

  public void OpenToLocationPicker()
  {
    OpenLocationFinder(to.text.text, OnToLocationPicked);
  }

  public void OnFromLocationPicked(Location loc)
  {
    if (Program.language.Arabic == true)
    {
      fromL = loc;
      string fromLoca = fromL.Name;
      fromLoca = ArabicFixer.Fix(fromLoca, true, true);
      from.GetComponent<InputField>().text = fromLoca;
      from.PlaceHolder();
    }
    fromL = loc;
    from.GetComponent<InputField>().text = fromL.Name;
    from.PlaceHolder();
  }

  public void OnToLocationPicked(Location loc)
  {
    if (Program.language.Arabic == true)
    {
      toL = loc;
      string toLoca = ArabicFixer.Fix(toL.Name, true, true);
      to.GetComponent<InputField>().text = toLoca;
      to.PlaceHolder();
    }
    toL = loc;
    to.GetComponent<InputField>().text = toL.Name;
    to.PlaceHolder();
  }

  internal override void Clear()
  {
    from.Reset();
    to.Reset();
    minDate.text = Program.DateToString(DateTime.Now.AddMinutes(10));
    maxDate.text = Program.DateToString(DateTime.Now.AddDays(1));
    persons.Clear();
    maxDateChanged = false;
  }
}