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
    private List<Rate> rates = null;
    private User user = null;
    public GameObject scrollContainer;
    float distance;
    public override void Init() {
        Clear();
        this.user = Program.User;
        GetMyRating();
    }

    public void GetMyRatingOnPull() {
        float newDistance = Vector3.Distance(listView.transform.position, scrollContainer.transform.position);
        if (newDistance + 150 < distance) {
            GetMyRating();
        }
    }
    public void ImplementList(List<Rate> rates) {
        if (rates != null) {
            listView.Clear();
            foreach (Rate o in rates) {
                var item = ItemsFactory.CreateRatingItem(listView.scrollContainer, o);
                listView.Add(item.gameObject);
            }
        }
    }

    private void GetMyRating() {
        Request<List<Rate>> request = new GetUserReviews(Program.User);
        request.AddSendListener(OpenSpinner);
        request.AddReceiveListener(CloseSpinner);
        request.Send(Response);
    }
    private void Response(List<Rate> result, int code, string message) {
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
        }
    }
    internal override void Clear() {

    }

}
