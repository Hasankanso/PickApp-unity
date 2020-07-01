using Requests;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class BookingHistoryPanel : Panel
{
    public ListView listView;
    public InputField search;
    private List<Ride> rides = null;
    private List<BookingHistoryItem> bookingHistoryItems = new List<BookingHistoryItem>();

    public void Init()
    {
        Debug.Log("hopz");
        Request<List<Ride>> request = new GetMyRidesHistory(Program.User);
        Task.Run(() => request.Send(Response));
    }

    private void Response(List<Ride> result, int code, string message) {
        Debug.Log("hopz");

        if (!code.Equals(HttpStatusCode.OK)) {
            Debug.Log("hopz");
            Panel p = PanelsFactory.CreateDialogBox("Something went wrong", false);
            OpenDialog(p);
        }
        else
        {
            this.rides = result;
            Debug.Log("hii");
            Debug.Log(Program.DateToString(rides[0].LeavingDate)+"  "+
                rides[0].Price + rides[0].CountryInformations.Unit+"  "+ rides[0].From.Name+"  "+
                rides[0].To.Name+"  "+ rides[0].User.Person.FirstName+"  "+
                rides[0].User.Person.ProfilePicture);
            ImplementYourRidesList(rides);
        }
    }
    public void ImplementYourRidesList(List<Ride> rides) {
        if ((rides != null)) {
            listView.Clear();
            foreach (Ride r in rides) {
                var item = ItemsFactory.CreateBookingHistoryItem(listView.scrollContainer, r);
                listView.Add(item.gameObject);
                bookingHistoryItems.Add(item);
            }
        }
    }
    public void Search()
    {
        bool result = true;
        String searchText = search.text;
        for (int i = 0; i < bookingHistoryItems.Count; i++)
        {
            if (bookingHistoryItems[i].driverName.text.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0 ||
                bookingHistoryItems[i].origin.text.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0 ||
                bookingHistoryItems[i].target.text.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0)
            {
                bookingHistoryItems[i].gameObject.SetActive(true);
                result = false;
            }
            else
            {
                bookingHistoryItems[i].gameObject.SetActive(false);
            }
        }
        if (result)
        {
            OpenDialog("No results found", false);
        }


    }
    internal override void Clear()
    {
        throw new System.NotImplementedException();
    }
}
