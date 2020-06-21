using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RatingItem : Panel {
    public Text dateTime, grade, comment, fullName;
    public Image profileImage;
    public void init(DateTime dateTime, int grade, string comment, string fullName, Texture2D profileImage) {
        this.dateTime.text = Program.DateToString(dateTime) +" "+ Program.DateToString(dateTime);
        this.grade.text = grade + "/5";
        this.comment.text = comment;
        this.fullName.text = fullName;
        this.profileImage.sprite = Program.GetImage(profileImage);
    }
    internal override void Clear() {
        this.dateTime.text = "";
        this.grade.text = "";
        this.comment.text = "";
        this.fullName.text = "";
    }
}
