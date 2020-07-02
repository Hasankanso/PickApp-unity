using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BookingHistoryItem : Panel {
    public Text  price, origin,date, target, driverName;
    public Image driverImage;
    public Ride ride;
    public void init( Ride ride) {
        this.ride = ride;
        Person driver = ride.User.Person;
        this.date.text = Program.DateToString(ride.LeavingDate);
        this.price.text = ride.Price.ToString()+ride.CountryInformations.Unit;
        this.origin.text = ride.From.Name;
        this.target.text = ride.To.Name;
        this.driverName.text = driver.FirstName+" "+driver.LastName;
        StartCoroutine(Program.RequestImage(driver.ProfilePictureUrl, ImageSucceed, ImageFailed));
    }
    private void ImageFailed(string obj) {
        Debug.Log("no image");
    }
    private void ImageSucceed(Texture2D img) {
        ride.User.Person.profilePicture = img;
        this.driverImage.sprite = Program.GetImage(ride.User.Person.profilePicture);
    }
    internal override void Clear() {
        this.date.text = "";
        this.price.text = "";
        this.origin.text = "";
        this.target.text = "";
        this.driverName.text = "";
    }
}
