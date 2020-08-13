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
    public Dropdown nbPersons, luggages, region;
    private Alert alert;

    public void Init(SearchInfo searchInfo) {
        Clear();
        this.from.SetText(searchInfo.From.ToString());
        this.to.SetText(searchInfo.To.ToString());
        this.minDate.text = searchInfo.MinDate.ToString();
        this.maxDate.text = searchInfo.MaxDate.ToString();
        this.nbPersons.value = searchInfo.PassengersNumber;
    }
    public void submit() {
        if (Validate()) {
            alert = null;
            alert = new Alert(Program.User, fromL, toL, price.text.text, DateTime.Parse(minDate.text), DateTime.Parse(maxDate.text), int.Parse(nbPersons.options[nbPersons.value].text), int.Parse(luggages.options[luggages.value].text), new CountryInformations("l.l"), comment.text.text);
            Request<Alert> request = new BroadCastAlert(alert);
            request.sendRequest.AddListener(OpenSpinner);
            request.receiveResponse.AddListener(CloseSpinner);
            request.Send(response);
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
    private void response(Alert result, int code, string message) {
        if (!code.Equals(HttpStatusCode.OK))
            OpenDialog("Something went wrong!", false);
        else {
            MissionCompleted(SearchPanel.PANELNAME, "Your alert has been sent!");
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
            if (DateTime.Parse(minDate.text) < DateTime.Now) {
                OpenDialog("Minimum date range can't be empty!", false);
                valid = false;
            }
            if (DateTime.Parse(minDate.text) > Program.MaxAlertDate) {
                OpenDialog("The max period of alert is six months", false);
                valid = false;
            }
        }
        if (maxDate.text.Equals("")) {
            OpenDialog("Maximum date range can't be empty!", false);
            valid = false;
        } else {
            if (DateTime.Parse(maxDate.text) < DateTime.Now) {
                OpenDialog("Invalid maximum date range", false);
                valid = false;
            }
            if (DateTime.Parse(maxDate.text) > Program.MaxAlertDate) {
                OpenDialog("The max period of alert is six months", false);
                valid = false;
            }
        }
        if (DateTime.Parse(maxDate.text) < DateTime.Parse(minDate.text)) {
            OpenDialog("The maximum date range couldn't be less tham the minimum.", false);
            valid = false;
        }
        if (region.value == 0) {
            valid = false;
            OpenDialog("Please select a region", false);
        }
        if (to.text.text.Equals("")) {
            to.Error();
            OpenDialog("Going-To can't be empty!", false);
            valid = false;
        }
        if (price.text.text.Equals("")) {
            price.Error();
            OpenDialog("CountryInfo can't be empty!", false);
            valid = false;
        }
        return valid;
    }

    internal override void Clear() {
        from.Reset();
        to.Reset();
        price.Reset();
        nbPersons.value = 0;
        luggages.value = 0;
        region.value = 0;
        comment.Reset();
        minDate.text = Program.DateToString(DateTime.Now);
        maxDate.text = Program.DateToString(DateTime.Now);
    }

}
