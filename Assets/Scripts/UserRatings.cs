using Requests;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class UserRatings : Panel {
    public ListView listView;
    private Person person = null;
    private List<Rate> rates = null;

    public void ImplementList(List<Rate> rates) {
        if (rates != null) {
            listView.Clear();
            foreach (Rate o in rates) {
                var item = ItemsFactory.CreateRatingItem(listView.scrollContainer, o);
                listView.Add(item.gameObject);
            }
        }
    }
    public void init(Person person) {
        Clear();
        this.person = person;
        GetMyRatings();
    }
    public void GetMyRatings() {
        Request<List<Rate>> request = new GetUserReviews(person);
        Task.Run(() => request.Send(Response));
    }
    private void Response(List<Rate> result, HttpStatusCode code, string message) {
        if (!code.Equals(HttpStatusCode.OK)) {
            Panel p = PanelsFactory.CreateDialogBox("Something went wrong", false);
            OpenDialog(p);
        } else {
            this.rates = result;
            ImplementList(person.Rates);
        }
    }
    internal override void Clear() {
        
    }

}
