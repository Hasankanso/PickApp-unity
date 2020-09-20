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
    private List<RatingItem> ratesItem = new List<RatingItem>();
    public GameObject scrollContainer;
    float distance;

    public override void Init() {
        GetMyRating();
        distance = Vector3.Distance(listView.transform.position, scrollContainer.transform.position);
    }
    public void GetMyRatingOnPull() {
        float newDistance = Vector3.Distance(listView.transform.position, scrollContainer.transform.position);
        if (newDistance + 150 < distance) {
            GetMyRating();
        }
    }
    public void AddRateToList(List<Rate> rates) {
        if (rates != null) {
            listView.Clear();
            foreach (Rate o in rates) {
                var item = ItemsFactory.CreateRatingItem(listView.scrollContainer, o);
                listView.Add(item.gameObject);
                ratesItem.Add(item);
            }
        }
    }
    private async void DownloadAndAddRaterImages() {
        for (int i = 0; i < ratesItem.Count; i++)
            if (ratesItem[i].rate.Rater.profilePicture == null) {
                Texture2D downloadedImg = await Request<object>.DownloadImage(ratesItem[i].rate.Rater.ProfilePictureUrl);
                ratesItem[i].SetPicture(downloadedImg);
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
            OpenDialog(message, false);
            Debug.Log(code);
        } else {
            AddRateToList(result);
            DownloadAndAddRaterImages();
        }
    }
    internal override void Clear() {
        listView.Clear();
        ratesItem.Clear();
    }
}
