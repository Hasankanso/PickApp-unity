using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Requests;
using Newtonsoft.Json.Linq;

public class Alert {
    private string id;
    private Location from, to;
    private DateTime minDate, maxDate;
    private int numberOfPersons, numberOfLuggage;
    private string comment;
    private float price;
    private User user;
    private DateTime updated;

    public Alert(User user, Location from, Location to, string price, DateTime minDate, DateTime maxDate, int numberOfPersons, int numberOfLuggages, string comment) {
        this.User = user;
        this.from = from;
        this.to = to;
        this.minDate = minDate;
        this.maxDate = maxDate;
        this.numberOfPersons = numberOfPersons;
        this.numberOfLuggage = numberOfLuggages;
        this.price = float.Parse(price);
        this.comment = comment;
    }
    public Alert(string id,Location from, Location to, string price, DateTime minDate, DateTime maxDate, int numberOfPersons, int numberOfLuggages, string comment) {
        this.id = id;
        this.from = from;
        this.to = to;
        this.minDate = minDate;
        this.maxDate = maxDate;
        this.numberOfPersons = numberOfPersons;
        this.numberOfLuggage = numberOfLuggages;
        this.price = float.Parse(price);
        this.comment = comment;
    }
    public JObject ToJson() {
        JObject alertJ = new JObject();

        alertJ[nameof(From)] = From.ToJson();
        alertJ[nameof(user)] = new JObject { [nameof(User.id)] = this.User.Id };

        alertJ[nameof(To)] = To.ToJson();

        alertJ[nameof(Price)] = price;

        alertJ[nameof(MinDate)] = minDate;
        alertJ[nameof(MaxDate)] = maxDate;
        alertJ[nameof(NumberOfPersons)] = numberOfPersons;
        alertJ[nameof(NumberOfLuggage)] = numberOfLuggage;
        alertJ[nameof(Comment)] = comment;


        return alertJ;
    }
    public static Alert ToObject(JObject json) {
        string id = "";
        var oId = json["objectId"];
        if (oId != null)
            id = oId.ToString();

        Location from = Location.ToObject((JObject)json[nameof(Alert.from)]);
        Location to = Location.ToObject((JObject)json[nameof(Alert.to)]);

        double minDateDouble = -1;
        var mid = json[nameof(Alert.minDate)];
        if (mid != null) {
            double.TryParse(mid.ToString(), out minDateDouble);
        }

        DateTime minDate = Program.UnixToUtc(minDateDouble);

        double maxDateDouble = -1;
        var md = json[nameof(Alert.maxDate)];
        if (md != null) {
            double.TryParse(md.ToString(), out maxDateDouble);
        }

        DateTime maxDate = Program.UnixToUtc(maxDateDouble);

        string comment = "";
        var c = json[nameof(Alert.comment)];
        if (c != null)
            comment = c.ToString();

        string price = "";
        var p = json[nameof(price)];
        if (p != null)
            price = p.ToString();

        int numberOfPersons = -1;
        var nP = json[nameof(Alert.numberOfPersons)];
        if (nP != null)
            int.TryParse(nP.ToString(), out numberOfPersons);
        int numberOfLuggage = -1;
        var nl = json[nameof(Alert.numberOfLuggage)];
        if (nl != null)
            int.TryParse(nl.ToString(), out numberOfLuggage);

        return new Alert(id,from,to,price,minDate,maxDate,numberOfPersons,numberOfLuggage,comment);
    }
    public int NumberOfPersons { get => numberOfPersons; set => numberOfPersons = value; }
    public int NumberOfLuggage { get => numberOfLuggage; set => numberOfLuggage = value; }
    public DateTime MinDate { get => minDate; set => minDate = value; }
    public DateTime MaxDate { get => maxDate; set => maxDate = value; }
    public Location From { get => from; set => from = value; }
    public Location To { get => to; set => to = value; }
    public string Id { get => id; set => id = value; }
    public string Comment { get => comment; set => comment = value; }
    public DateTime Updated { get => updated; set => updated = value; }
    public float Price { get => price; set => price = value; }
    public User User { get => user; set => user = value; }
}
