using System.Collections;
using System.Collections.Generic;
using System;
using Requests;
using Newtonsoft.Json.Linq;
using UnityEngine;

public class ScheduleRide {
    public int id;
    private DateTime startDate, endDate;
    private bool[] days = new bool[7];
    private Ride ride;
    private DateTime updated;

    public JObject ToJson() {
        JObject scheduleJ = new JObject();
        scheduleJ[nameof(startDate)] = startDate;
        scheduleJ[nameof(endDate)] = endDate;

        JArray daysArray = new JArray();
        scheduleJ[nameof(Days)] = daysArray;
        daysArray.Add(new JObject()["Monday"] = days[0]);
        daysArray.Add(new JObject()["Tuesday"] = days[1]);
        daysArray.Add(new JObject()["Wednesday"] = days[2]);
        daysArray.Add(new JObject()["Thursday"] = days[3]);
        daysArray.Add(new JObject()["Friday"] = days[4]);
        daysArray.Add(new JObject()["Saturday"] = days[5]);
        daysArray.Add(new JObject()["Sunday"] = days[6]);

        return scheduleJ;
    }
    public static ScheduleRide ToObject(JObject scheduleRide) {
        JObject json = (JObject)scheduleRide["schedule"];
        bool monday = false;
        var md = json[nameof(monday)];
        if (md != null)
            bool.TryParse(md.ToString(), out monday);
        bool tuesday = false;
        var td = json[nameof(tuesday)];
        if (td != null)
            bool.TryParse(td.ToString(), out tuesday);
        bool wednesday = false;
        var wd = json[nameof(wednesday)];
        if (wd != null)
            bool.TryParse(wd.ToString(), out wednesday);
        bool thursday = false;
        var tdd = json[nameof(thursday)];
        if (tdd != null)
            bool.TryParse(tdd.ToString(), out thursday);
        bool friday = false;
        var fd = json[nameof(friday)];
        if (fd != null)
            bool.TryParse(fd.ToString(), out friday);
        bool saturday = false;
        var sd = json[nameof(saturday)];
        if (sd != null)
            bool.TryParse(sd.ToString(), out saturday);
        bool sunday = false;
        var sn = json[nameof(sunday)];
        if (sn != null)
            bool.TryParse(sn.ToString(), out sunday);

        double startDateDouble = -1;
        var ld = json[nameof(ScheduleRide.startDate)];
        if (ld != null) {
            double.TryParse(ld.ToString(), out startDateDouble);
        }
        DateTime startDate = Program.UnixToUtc(startDateDouble);
        double endDateDouble = -1;
        var ed = json[nameof(ScheduleRide.endDate)];
        if (ed != null) {
            double.TryParse(ed.ToString(), out endDateDouble);
        }
        DateTime endDate = Program.UnixToUtc(endDateDouble);

        Ride rides = Ride.ToObject((JObject)scheduleRide["ride"][0]);
        return new ScheduleRide(startDate,endDate,monday,tuesday,wednesday,thursday,friday,saturday,sunday,rides);
    }
    public ScheduleRide(DateTime startDate, DateTime endDate, bool isMonday, bool isTuesday, bool isWednesday, bool isThursday, bool isFriday, bool isSaturday, bool isSunday, Ride ride) {
        this.StartDate = startDate;
        this.EndDate = endDate;
        this.IsMonday = isMonday;
        this.IsTuesday = isTuesday;
        this.IsWednesday = isWednesday;
        this.IsThursday = isThursday;
        this.IsFriday = isFriday;
        this.IsSaturday = isSaturday;
        this.IsSunday = isSunday;
        this.ride = ride;
    }

    public ScheduleRide(DateTime startDate, DateTime endDate, bool isMonday, bool isTuesday, bool isWednesday, bool isThursday, bool isFriday, bool isSaturday, bool isSunday) {
        this.StartDate = startDate;
        this.EndDate = endDate;
        this.IsMonday = isMonday;
        this.IsTuesday = isTuesday;
        this.IsWednesday = isWednesday;
        this.IsThursday = isThursday;
        this.IsFriday = isFriday;
        this.IsSaturday = isSaturday;
        this.IsSunday = isSunday;
    }

    public Ride Ride { get => ride; set => ride = value; }
    public int Id { get => id; set => id = value; }
    public DateTime StartDate { get => startDate; set => startDate = value; }
    public DateTime EndDate { get => endDate; set => endDate = value; }
    public bool IsMonday { get => days[0]; set => days[0] = value; }
    public bool IsTuesday { get => days[1]; set => days[1] = value; }
    public bool IsWednesday { get => days[2]; set => days[2] = value; }
    public bool IsThursday { get => days[3]; set => days[3] = value; }
    public bool IsFriday { get => days[4]; set => days[4] = value; }
    public bool IsSaturday { get => days[5]; set => days[5] = value; }
    public bool IsSunday { get => days[6]; set => days[6] = value; }
    public bool[] Days { get => days; }
    public DateTime Updated { get => updated; set => updated = value; }
}

