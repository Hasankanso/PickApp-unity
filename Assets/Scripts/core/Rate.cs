using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Requests;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

public class Rate {
    private int grade;
    private string comment;
    private string reason;
    private DateTime creationDate;
    private Person rater;
    private Person target;
    private Ride ride;
    private DateTime updated;
    public int Grade { get => grade; set => grade = value; }
    public string Reason { get => reason; set => reason = value; }
    public string Comment { get => comment; set => comment = value; }
    public DateTime Date { get => creationDate; set => creationDate = value; }
    public Person Rater { get => rater; set => rater = value; }
    public Person Target { get => target; set => target = value; }
    public Ride Ride { get => ride; set => ride = value; }
    public DateTime Updated { get => updated; set => updated = value; }

    //add new rate
    public Rate(int grade, string comment, string reason, DateTime date, Person rater, Ride ride, Person target) {
        this.Grade = grade;
        this.Date = date;
        this.reason = reason;
        this.comment = comment;
        this.Rater = rater;
        this.Ride = ride;
        this.Target = target;
    }

    //add new rate
    public Rate(int grade, string comment, string reason, DateTime date, Ride ride, Person target) {
        this.Grade = grade;
        this.Date = date;
        this.reason = reason;
        this.comment = comment;
        this.Ride = ride;
        this.Target = target;
    }
    public JObject ToJson() {
        JObject rateJ = new JObject();
        rateJ[nameof(this.Comment)] = this.comment;
        rateJ[nameof(this.Grade)] = this.grade;
        rateJ[nameof(this.Date)] = this.creationDate;
        rateJ[nameof(this.reason)] = this.reason;
        rateJ["user"] = Program.User.id;
        rateJ[nameof(this.ride)] = this.ride.Id;
        rateJ[nameof(this.target)] = this.target.Id;
        return rateJ;
    }
    public static Rate ToObject(JObject json) {
        int grade = -1;
        var oGrade = json[nameof(Rate.grade)];
        if (oGrade != null) int.TryParse(oGrade.ToString(), out grade);
        string comment = "";
        var ocomment = json["comment"];
        if (ocomment != null) comment = ocomment.ToString();
        string reason = "";
        var oReason = json["reason"];
        if (oReason != null) reason = oReason.ToString();

        double creationDate = -1;
        var cD = json[nameof(Rate.creationDate)];
        if (cD != null) {
            double.TryParse(cD.ToString(), out creationDate);
        }
        DateTime creationDate1 = Program.UnixToUtc(creationDate);

        var rater = json["rater"];
        Person rater1 = null;
        if (rater != null && rater.HasValues) {
            rater1 = Person.ToObject((JObject)rater);
        }
        var target = json["target"];
        Person target1 = null;
        if (target != null && target.HasValues) {
            target1 = Person.ToObject((JObject)target);
        }
        var ride = json["ride"];
        Ride ride1 = null;
        if (ride != null && ride.HasValues) {
            ride1 = Ride.ToObject((JObject)ride);
        }
        String updated = "";
        var Oupdated = json["updated"];
        if (Oupdated != null) updated = Oupdated.ToString();

        return new Rate(grade, comment, reason, creationDate1, rater1, ride1, target1);
    }
    public static string Validate(Rate rate) {
        if (rate.Grade < 0 || rate.Grade > 5) {
            return "Invalid rate";
        }
        if (rate.Grade < 3 && string.IsNullOrEmpty(rate.Comment)) {
            return "Please state the reason of low rate";
        }
        if (string.IsNullOrEmpty(rate.Rater.id)) {
            return "Invalid reviewer object id";
        }
        if (string.IsNullOrEmpty(rate.Target.id)) {
            return "Invalid target object id";
        }
        if (string.IsNullOrEmpty(rate.Ride.id)) {
            return "Invalid ride object id";
        }
        return string.Empty;
    }

}
