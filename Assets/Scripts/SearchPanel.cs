using System;
using System.Collections.Generic;
using UnityEngine.UI;
using Requests;
using System.Net;
using Newtonsoft.Json.Linq;

public class SearchPanel : Panel
{
  public InputFieldScript from, to;
  public Text minDate, maxDate;
  public Dropdown numberOfPersons;

  private Location fromL, toL;
  private SearchInfo info = null;
  private void Start()
  {
  //  string json = "{    \"___jsonclass\": \"ride\",    \"petsAllowed\": false,    \"reservedLuggages\": 1, \"reservedSeats\": 4,    \"ownerId\": null,    \"acAllowed\": false,    \"smokingAllowed\": false,    \"car\": {        \"___jsonclass\": \"car\",        \"image\": null,        \"color\": \"Red\",        \"year\": 2020,        \"created\": 1588979019000,        \"maxSeats\": 4,        \"ownerId\": \"660F3CD4-998B-B3D7-FF74-1C043ADCF000\",        \"maxLuggage\": 4,        \"Picture\": null,        \"maxLuggages\": null,        \"name\": \"YarisExterior\",        \"___class\": \"car\",        \"updated\": 1591715942000,        \"brand\": \"Toyota\",        \"objectId\": \"5D841996-3173-DC51-FFBC-B0CFE4B2CA00\"    },    \"price\": 2222,    \"___class\": \"ride\",    \"stopTime\": 10,    \"currency\": [        {            \"___jsonclass\": \"currency\",            \"unit\": \"LL\",            \"created\": 1588891447000,            \"___class\": \"currency\",            \"ownerId\": null,            \"updated\": null,            \"objectId\": \"F85258BF-63A7-F939-FF31-C78BB1837300\"        }    ],    \"map\": null,    \"objectId\": \"006EBFFC-CA58-DA39-FF8A-A672D3AE5E00\",    \"leavingDate\": null,    \"toPosition\": {        \"___jsonclass\": \"location\",        \"created\": 1592142673000,        \"name\": \"Nabatieh, Lebanon\",        \"___class\": \"location\",        \"position\": {            \"coordinates\": [                35.4835902,                33.3771898            ],            \"type\": \"Point\"        },        \"ownerId\": null,        \"updated\": null,        \"objectId\": \"7560A9CC-3D0D-53BB-FF77-4700C6501D00\"    },    \"availableSeats\": 1,    \"created\": 1592142673000,    \"availableLuggages\": 1,    \"maxSeats\": 3,    \"maxLuggage\": 5,    \"musicAllowed\": false,    \"driver\": {        \"___jsonclass\": \"driver\",        \"created\": 1591716233000,        \"___class\": \"driver\",        \"region\": 1,        \"ownerId\": \"660F3CD4-998B-B3D7-FF74-1C043ADCF000\",        \"updated\": 1591716243000,        \"objectId\": \"C8C7CCE2-4F4F-E67F-FF55-57B5CF89D300\"    },    \"fromPosition\": {        \"___jsonclass\": \"location\",        \"created\": 1592142673000,        \"name\": \"Beirut, Lebanon\",        \"___class\": \"location\",        \"position\": {            \"coordinates\": [                35.5017767,                33.8937913            ],            \"type\": \"Point\"        },        \"ownerId\": null,        \"updated\": null,        \"objectId\": \"AE15A814-5B88-C176-FF94-1452B50B6700\"    },    \"comment\": \"I am Serhan and may stop to eat\",    \"kidSeat\": true,    \"updated\": null}";
   // JObject jo = JObject.Parse(json);
  //  Ride r = Ride.ToObject(jo);
   // print(r.From.Longitude);

    //this should be in init
    Clear();
  }
  public void Init()
  {
    StatusProperty = StatusE.VIEW;
    var minDT = Program.StringToDate(minDate.text);
    var maxDT = Program.StringToDate(maxDate.text);
  }
  public void Search()
  {
    if (Vadilate())
    {
      info = new SearchInfo(fromL, toL, Program.StringToDate(minDate.text), Program.StringToDate(maxDate.text), int.Parse(numberOfPersons.options[numberOfPersons.value].text));
      Request<List<Ride>> request = new SearchForRides(info);
      request.Send(SearchResults);
    }
  }

  public void OpenDateTimePicker(Text dateLabel)
  {
    OpenDateTimePicker((dt) => OnDatePicked(dateLabel, dt));
  }

  private void SearchResults(List<Ride> results, int code, string message)
  {
    if (!code.Equals((int) HttpStatusCode.OK))
      OpenDialog("Something went wrong!", false);
    else
    {
      Panel p = PanelsFactory.CreateRideResults(results, info);
      openCreated(p);
    }
  }

  private bool Vadilate()
  {
    bool valid = true;
    if (from.text.text.Equals(""))
    {
      from.Error();

      OpenDialog("From can't be empty!", false);
      valid = false;
    }
    if (to.text.text.Equals(""))
    {
      to.Error();

      OpenDialog("Going-To can't be empty!", false);
      valid = false;
    }
    if (minDate.text.Equals(""))
    {
      OpenDialog("Minimum date range can't be empty!", false);
      valid = false;
    }
    else
    {
      if (Program.StringToDate(minDate.text) < DateTime.Now)
      {
        OpenDialog("Minimum date can't be in the past", false);
        valid = false;
      }
      if (Program.StringToDate(minDate.text) > Program.MaxAlertDate)
      {
        OpenDialog("The max period of alert is six months", false);
        valid = false;
      }
    }
    if (maxDate.text.Equals(""))
    {
      OpenDialog("Maximum date range can't be empty!", false);
      valid = false;
    }
    else
    {
      if (Program.StringToDate(maxDate.text) < DateTime.Now)
      {
        OpenDialog("Invalid maximum date range", false);
        valid = false;
      }
      if (Program.StringToDate(maxDate.text) > Program.MaxAlertDate)
      {
        OpenDialog("The max period of alert is six months", false);
        valid = false;
      }
    }
    if (Program.StringToDate(maxDate.text) < Program.StringToDate(minDate.text))
    {
      OpenDialog("The maximum date range couldn't be less tham the minimum.", false);
      valid = false;
    }
    return valid;
  }

  public void OnDatePicked(Text dateLabel, DateTime d)
  {
    dateLabel.text = Program.DateToString(d);
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
    fromL = loc;
    from.GetComponent<InputField>().text = fromL.Name;
    from.PlaceHolder();
  }

  public void OnToLocationPicked(Location loc)
  {
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
    numberOfPersons.value = 0;
  }
}