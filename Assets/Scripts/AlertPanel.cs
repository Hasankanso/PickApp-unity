using Requests;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AlertPanel : Panel {
    //to do
    //validate date time
    public InputFieldScript from, to, price, comment;
    private Location fromL, toL;
    public Text minDate, maxDate;
    public UpDownPicker numberOfPersons, numberOfLuggage;
    private Alert alert;

    public void Init(SearchInfo searchInfo) {
        Clear();
        numberOfPersons.Init("Persons", 1, 8, searchInfo.PassengersNumber);
        numberOfLuggage.Init("Luggage", 0, 8);
        this.fromL = searchInfo.From;
        this.toL = searchInfo.To;
        this.from.SetText(searchInfo.From.ToString());
        this.to.SetText(searchInfo.To.ToString());
        this.minDate.text = Program.DateToString(searchInfo.MinDate);
        this.maxDate.text = Program.DateToString(searchInfo.MaxDate);
    }
    public void submit() {
        if (Validate()) {
            AdMob.ShowRewardedAd(() => {
                alert = new Alert(Program.User, fromL, toL, price.text.text, Program.StringToDate(minDate.text), Program.StringToDate(maxDate.text), numberOfPersons.Value, numberOfLuggage.Value, comment.text.text);
                Request<string> request = new BroadCastAlert(alert);
                request.AddSendListener(OpenSpinner);
                request.AddReceiveListener(CloseSpinner);
                request.Send(response);
            });
        }
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

    public void OpenDateTimePicker(Text dateLabel) {
        OpenDateTimePicker((dt) => OnDatePicked(dateLabel, dt));
    }
    private void OnDatePicked(Text dateLabel, DateTime d) {
        dateLabel.text = Program.DateToString(d);
    }
    private void response(string result, int code, string message) {
        if (!code.Equals((int)HttpStatusCode.OK))
            OpenDialog(message, false);
        else {
            MissionCompleted(SearchPanel.PANELNAME, result);
        }
    }
    private bool Validate() {
        bool valid = true;
        if (from.text.text.Equals("")) {
            from.Error();
            OpenDialog("From can't be empty!", false);
            valid = false;
        }
        if (minDate.text.Equals("")) {
            OpenDialog("Minimum date range can't be empty!", false);
            valid = false;
        } else {
            if (Program.StringToDate(minDate.text) < DateTime.Now) {
                OpenDialog("Minimum date range can't be empty!", false);
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

        if (to.text.text.Equals("")) {
            to.Error();
            OpenDialog("Going-To can't be empty!", false);
            valid = false;
        }
        if (price.text.text.Equals("")) {
            price.Error();
            OpenDialog("Price can't be empty!", false);
            valid = false;
        }
        return valid;
    }

    internal override void Clear() {
        from.Reset();
        to.Reset();
        price.Reset();
        numberOfPersons.Clear();
        numberOfLuggage.Clear();
        comment.Reset();
        minDate.text = Program.DateToString(DateTime.Now.AddMinutes(10));
        maxDate.text = Program.DateToString(DateTime.Now.AddDays(1));
    }

}
