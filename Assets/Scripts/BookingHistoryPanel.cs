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
    private Person person = null;
    private List<Ride> rides = null;
    private List<BookingHistoryItem> bookingHistoryItems = new List<BookingHistoryItem>();

    public void init(Person person)
    {
        this.person = person;
        GetMyRidesHistory();
    }
    public void GetMyRidesHistory()
    {
        Request<List<Ride>> request = new GetMyRidesHistory();
        Task.Run(() => request.Send(Response));
    }
    private void Response(List<Ride> result, HttpStatusCode code, string message)
    {
        if (!code.Equals(HttpStatusCode.OK))
        {
            Panel p = PanelsFactory.CreateDialogBox("Something went wrong", false);
            OpenDialog(p);
        }
        else
        {
            this.rides = result;
            foreach (Ride r in rides)
            {
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
