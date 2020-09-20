using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RatingItem : Panel {
    public Text dateTime, grade, comment, fullName;
    public Image profileImage;
    public Rate rate;
    public void init(Rate rate) {
        this.rate = rate;
        dateTime.text = Program.DateToString(rate.Date);
        grade.text = rate.Grade + "/5";
        comment.text = rate.Comment;
        fullName.text = rate.Rater.FirstName + " " + rate.Rater.LastName;
        if (rate.Rater.profilePicture != null) {
            profileImage.sprite = Program.GetImage(rate.Rater.profilePicture);
        }
    }
    internal void SetPicture(Texture2D img) {
        rate.Rater.profilePicture = img;
        this.profileImage.sprite = Program.GetImage(rate.Rater.profilePicture);
    }
    internal override void Clear() {
        this.dateTime.text = "";
        this.grade.text = "";
        this.comment.text = "";
        this.fullName.text = "";
    }
}
