using Requests;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class MyRidePanel : Panel {
    public ListView listMyRidesView;
    public GameObject scrollContainer;
    public static readonly string PANELNAME = "MyRides";
    float distance;


    public override void Init() {
        ImplementYourRidesList(Program.Person.UpcomingRides);
        Status = StatusE.VIEW;
        distance = Vector3.Distance(listMyRidesView.transform.position, scrollContainer.transform.position);
    }
    public void GetMyUpcomingsRidesOnPull() {
        float newDistance= Vector3.Distance(listMyRidesView.transform.position, scrollContainer.transform.position);
        if (newDistance+150<distance) {
            Debug.Log("get my upcoming rides");
            Request<List<Ride>> request = new GetMyUpcomingRides(Program.User);
            request.AddSendListener(OpenSpinner);
            request.AddReceiveListener(CloseSpinner);
            request.Send(GetUpcomingRides);
        }
    }
   
    private void GetUpcomingRides(List<Ride> rides, int code, string message) {
        if (!code.Equals((int)HttpStatusCode.OK)) {
                OpenDialog(message, false);
                Debug.Log(code);
        } else {
            Program.Person.UpcomingRides = rides;
            ImplementYourRidesList(Program.Person.UpcomingRides);
        }
    }

    public void ImplementYourRidesList(List<Ride> rides) {
        listMyRidesView.Clear();
        if (rides == null) return;
        foreach (Ride r in rides) {
            r.Reserved = true;
            var item = ItemsFactory.CreateMyRideItem(listMyRidesView.scrollContainer, r, Program.Person, this);
            listMyRidesView.Add(item.gameObject);
        }
    }
    internal override void Clear() {
    }
}
