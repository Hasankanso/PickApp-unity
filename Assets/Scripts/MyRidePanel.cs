using Requests;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class MyRidePanel : Panel {
    public ListView listMyRidesView;
    public static readonly string PANELNAME = "MyRides";


    public override void Init() {
        Request<List<Ride>> request = new GetMyUpcomingRides(Program.User);
        request.AddSendListener(OpenSpinner);
        request.AddReceiveListener(CloseSpinner);
        request.Send(GetUpcomingRides);
        Status = StatusE.VIEW;
    }

    private void GetUpcomingRides(List<Ride> arg1, int arg2, string arg3) {
        Program.Person.UpcomingRides = arg1;
        ImplementYourRidesList(Program.Person.UpcomingRides);

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
