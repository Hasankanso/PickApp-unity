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
    private Person reviewer;
    private Person target;
    private Ride ride;
    private DateTime updated;
    public int Grade { get => grade; set => grade = value; }
    public string Reason { get => reason; set => reason = value; }
    public string Comment { get => comment; set => comment = value; }
    public DateTime Date { get => creationDate; set => creationDate = value; }
    public Person Reviewer { get => reviewer; set => reviewer = value; }
    public Person Target { get => target; set => target = value; }
    public Ride Ride { get => ride; set => ride = value; }
    public DateTime Updated { get => updated; set => updated = value; }

    //add new rate
    public Rate( int grade, string comment, string reason, DateTime date, Person reviewer, Ride ride, Person target) {
        this.Grade = grade;
        this.Date = date;
        this.reason = reason;
        this.comment = comment;
        this.Reviewer = reviewer;
        this.Ride = ride;
        this.Target = target;
    }


    public JObject ToJson()
    {
        JObject rateJ = new JObject();
        rateJ[nameof(this.Comment)] = this.comment;
        rateJ[nameof(this.Grade)] = this.grade;
        rateJ[nameof(this.Date)] = this.creationDate;

        JObject reviewerJ = new JObject();
        reviewerJ[nameof(this.Reviewer.Id)] = this.reviewer.Id;
        rateJ[nameof(this.Reviewer)] = reviewerJ;

        JObject rideJ = new JObject();
        reviewerJ[nameof(this.ride.Id)] = this.ride.Id;
        rateJ[nameof(this.Ride)] = rideJ;

        JObject targetJ = new JObject();
        reviewerJ[nameof(this.target.Id)] = this.target.Id;
        rateJ[nameof(this.Target)] = targetJ;

        return rateJ;
    }
    public static int ToObject(JObject rate)
    {
        return 1;
    }
    public static string Validate(Rate rate) {
        if (rate.Grade < 0 || rate.Grade > 5) {
            return "Invalid rate";
        }
        if (rate.Grade < 3 && string.IsNullOrEmpty(rate.Comment)) {
            return "Please state the reason of low rate";
        }
        if (string.IsNullOrEmpty(rate.Reviewer.id)) {
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
