using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Requests;
using System.Net;
using Newtonsoft.Json.Linq;

public class SearchPanel : Panel {
    public InputFieldScript from, to;
    public Text minDate, maxDate;
    public Dropdown numberOfPersons;
    private Location fromL, toL;
    private SearchInfo info = null;

    public static readonly string PANELNAME = "SEARCHPANEL";

    private void Start() {
        //this should be in init
        Clear();
    }
    public override void Init() {
        
        Status = StatusE.VIEW;
        var minDT = Program.StringToDate(minDate.text);
        var maxDT = Program.StringToDate(maxDate.text);
    }
    public void Search() {
        if (Vadilate()) {
            info = new SearchInfo(fromL, toL, Program.StringToDate(minDate.text), Program.StringToDate(maxDate.text), int.Parse(numberOfPersons.options[numberOfPersons.value].text));
            Request<List<Ride>> request = new SearchForRides(info);
            request.AddSendListener(OpenSpinner);
            request.AddReceiveListener(CloseSpinner);
            request.Send(SearchResults);
        }
    }

    public void OpenDateTimePicker(Text dateLabel) {
        OpenDateTimePicker((dt) => OnDatePicked(dateLabel, dt));
    }

    private void SearchResults(List<Ride> results, int code, string message) {
        if (!code.Equals((int)HttpStatusCode.OK))
            OpenDialog("Something went wrong!", false);
        else {
            RideResultsPanel p = PanelsFactory.CreateRideResults();
            Open(p, () => { p.Init(results, info); });
        }
    }

    private bool Vadilate() {
        bool valid = true;
        if (from.text.text.Equals("")) {
            from.Error();

            OpenDialog("From can't be empty!", false);
            valid = false;
        }
        if (to.text.text.Equals("")) {
            to.Error();

            OpenDialog("Going-To can't be empty!", false);
            valid = false;
        }
        if (minDate.text.Equals("")) {
            OpenDialog("Minimum date range can't be empty!", false);
            valid = false;
        } else {
            if (Program.StringToDate(minDate.text) < DateTime.Now) {
                OpenDialog("Minimum date can't be in the past", false);
                valid = false;
            }
            if (Program.StringToDate(minDate.text) > Program.MaxAlertDate) {
                OpenDialog("The max period of alert is six months", false);
                valid = false;
            }
        }
        if (maxDate.text.Equals("")) {
            OpenDialog("Maximum date range can't be empty!", false);
            valid = false;
        } else {
            if (Program.StringToDate(maxDate.text) < DateTime.Now) {
                OpenDialog("Invalid maximum date range", false);
                valid = false;
            }
            if (Program.StringToDate(maxDate.text) > Program.MaxAlertDate) {
                OpenDialog("The max period of alert is six months", false);
                valid = false;
            }
        }
        if (Program.StringToDate(maxDate.text) < Program.StringToDate(minDate.text)) {
            OpenDialog("The maximum date range couldn't be less tham the minimum.", false);
            valid = false;
        }
        return valid;
    }

    public void OnDatePicked(Text dateLabel, DateTime d) {
        dateLabel.text = Program.DateToString(d);
    }

    public void OpenFromLocationPicker() {
        OpenLocationFinder(from.text.text, OnFromLocationPicked);
    }

    public void OpenToLocationPicker() {
        OpenLocationFinder(to.text.text, OnToLocationPicked);
    }

    public void OnFromLocationPicked(Location loc) {
        fromL = loc;
        from.GetComponent<InputField>().text = fromL.Name;
        from.PlaceHolder();
    }

    public void OnToLocationPicked(Location loc) {
        toL = loc;
        to.GetComponent<InputField>().text = toL.Name;
        to.PlaceHolder();
    }

    internal override void Clear() {
        from.Reset();
        to.Reset();
        minDate.text = Program.DateToString(DateTime.Now.AddMinutes(10));
        maxDate.text = Program.DateToString(DateTime.Now.AddDays(1));
        numberOfPersons.value = 0;
    }
}