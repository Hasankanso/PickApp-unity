using Requests;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class MyRidesHistoryPanel : Panel {
    public ListView listView;
    public InputField search;
    private List<Ride> rides = null;
    private List<MyRidesHistoryItem> myRidesHistoryItems = new List<MyRidesHistoryItem>();

    public void Init() {
        Request<List<Ride>> request = new GetMyRidesHistory(Program.User);
        request.Send(Response);
    }

    private void Response(List<Ride> result, int code, string message) {
        if (!code.Equals((int)HttpStatusCode.OK)) {
            Panel p = PanelsFactory.CreateDialogBox("Something went wrong", false);
            OpenDialog(p);
        } else {
            this.rides = result;
            ImplementYourRidesList(rides);
        }
    }
    public void ImplementYourRidesList(List<Ride> rides) {
        if ((rides != null)) {
            listView.Clear();
            foreach (Ride r in rides) {
                var item = ItemsFactory.CreateMyRidesHistoryItem(listView.scrollContainer, r);
                listView.Add(item.gameObject);
                myRidesHistoryItems.Add(item);
            }
        }
    }
    public void Search() {
        bool result = true;
        String searchText = search.text;
        for (int i = 0; i < myRidesHistoryItems.Count; i++) {
            if (myRidesHistoryItems[i].driverName.text.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0 ||
                myRidesHistoryItems[i].origin.text.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0 ||
                myRidesHistoryItems[i].target.text.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0) {
                myRidesHistoryItems[i].gameObject.SetActive(true);
                result = false;
            } else {
                myRidesHistoryItems[i].gameObject.SetActive(false);
            }
        }
        if (result) {
            OpenDialog("No results found", false);
        }


    }
    internal override void Clear() {
        throw new System.NotImplementedException();
    }
}
