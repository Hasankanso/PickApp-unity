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

    public override void Init() {
        Request<List<Ride>> request = new GetMyRidesHistory(Program.User);
        request.AddSendListener(OpenSpinner);
        request.AddReceiveListener(CloseSpinner);
        request.Send(Response);
    }

    private void Response(List<Ride> result, int code, string message) {
        if (!code.Equals((int)HttpStatusCode.OK)) {
            if (code == 302) {
                Program.User = null;
                Cache.SetToken("");
                Program.IsLoggedIn = false;
                OpenDialog("Please login", false);
                LoginPanel login = PanelsFactory.CreateLogin();
                Open(login, () => { login.Init(false); });
            } else {
                OpenDialog(message, false);
                Debug.Log(code);
            }
        } else {
            this.rides = result;
            ImplementYourRidesList(rides);
            DownloadAndAddImages();

        }
    }
    private async void DownloadAndAddImages() {
        foreach (MyRidesHistoryItem ri in myRidesHistoryItems) {
            ri.SetPicture(await Request<object>.DownloadImage(ri.ride.User.Person.ProfilePictureUrl));
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
        
    }
}
