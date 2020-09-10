using Requests;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.UI;
public class RatePanel : Panel {

    public Image firstStar, secondStar, thirdStar, fourthStar, fifthStar;
    public InputFieldScript comment;

    public Ride ride = null;
    public Person reviewer = null, target = null;
    public Dropdown reasonDropdown;

    private int grade;

    public int Grade { get { return grade; } set { grade = value; } }

    public void submit() {
        if (vadilate()) {
            var reason = reasonDropdown.options[reasonDropdown.value].text;
            Rate rate = new Rate(grade, comment.text.text,reason,DateTime.Parse(DateTime.Now.ToString("dd/MM/yyyy H:mm")), reviewer, ride, target);
            Request<Rate> request = new AddRate(rate,Program.User);
            request.AddSendListener(OpenSpinner);
            request.AddReceiveListener(CloseSpinner);
            request.Send(response);
        }
    }
    private void response(Rate result, int code, string message) {
        if (!code.Equals(HttpStatusCode.OK))
            OpenDialog("There was an error rating this user", false);
        else {
            MissionCompleted(SearchPanel.PANELNAME, "Thank you for your rating!");
        }
    }
    public void Init(Ride ride, Person reviewer, Person target) {
        Clear();
        this.ride = ride;
        this.reviewer = reviewer;
        this.target = target;
    }
    public void starOnOff(int starPosition) {
        secondStar.color = new Color(186f / 255f, 186f / 255f, 186f / 255f);
        thirdStar.color = new Color(186f / 255f, 186f / 255f, 186f / 255f);
        fourthStar.color = new Color(186f / 255f, 186f / 255f, 186f / 255f);
        fifthStar.color = new Color(186f / 255f, 186f / 255f, 186f / 255f);
        if (starPosition == 0) {
            firstStar.color = new Color(255f / 255f, 188f / 255f, 66f / 255f);
        } else if (starPosition == 1) {
            firstStar.color = new Color(255f / 255f, 188f / 255f, 66f / 255f);
            secondStar.color = new Color(255f / 255f, 188f / 255f, 66f / 255f);
        } else if (starPosition == 2) {
            firstStar.color = new Color(255f / 255f, 188f / 255f, 66f / 255f);
            secondStar.color = new Color(255f / 255f, 188f / 255f, 66f / 255f);
            thirdStar.color = new Color(255f / 255f, 188f / 255f, 66f / 255f);
        } else if (starPosition == 3) {
            firstStar.color = new Color(255f / 255f, 188f / 255f, 66f / 255f);
            secondStar.color = new Color(255f / 255f, 188f / 255f, 66f / 255f);
            thirdStar.color = new Color(255f / 255f, 188f / 255f, 66f / 255f);
            fourthStar.color = new Color(255f / 255f, 188f / 255f, 66f / 255f);
        } else if (starPosition == 4) {
            firstStar.color = new Color(255f / 255f, 188f / 255f, 66f / 255f);
            secondStar.color = new Color(255f / 255f, 188f / 255f, 66f / 255f);
            thirdStar.color = new Color(255f / 255f, 188f / 255f, 66f / 255f);
            fourthStar.color = new Color(255f / 255f, 188f / 255f, 66f / 255f);
            fifthStar.color = new Color(255f / 255f, 188f / 255f, 66f / 255f);
        }
        grade = starPosition + 1;
    }
    private bool vadilate() {
        bool valid = true;
        if (comment.text.text.Equals("") && (grade == 1 || grade == 2)) {
            comment.Error();

            OpenDialog("Please leave a comment", false);
            valid = false;
        }
        return valid;
    }
    internal override void Clear() {
        comment.Reset();
        starOnOff(2);
    }
}