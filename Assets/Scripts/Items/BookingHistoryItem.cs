using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BookingHistoryItem : Panel {
    public Text  price, origin,date, target, driverName;
    public Image driverImage;
    public void init( string date, string price, string origin, string target, string driverName, Texture2D driverImage) {
        this.date.text = date;
        this.price.text = price;
        this.origin.text = origin;
        this.target.text = target;
        this.driverName.text = driverName;
        this.driverImage.sprite = Program.GetImage(driverImage);
    }
    internal override void Clear() {
        this.date.text = "";
        this.price.text = "";
        this.origin.text = "";
        this.target.text = "";
        this.driverName.text = "";
    }
}
