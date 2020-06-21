using Newtonsoft.Json.Linq;
using Requests;
using System;
using System.Collections;
using System.Collections.Generic;

public class SearchInfo {
    Location from, to;
    DateTime minDate, maxDate;
    int passengersNumber;
    int luggagesNumber;

    public SearchInfo(Location from, Location to, DateTime minDate, DateTime maxDate, int passengersNumber) {
        this.from = from;
        this.to = to;
        this.minDate = minDate;
        this.maxDate = maxDate;
        this.passengersNumber = passengersNumber;
    }

    public SearchInfo(Location from, Location to, DateTime minDate, DateTime maxDate, int passengersNumber, int luggagesNumber) {
        this.from = from;
        this.to = to;
        this.minDate = minDate;
        this.maxDate = maxDate;
        this.passengersNumber = passengersNumber;
        this.luggagesNumber = luggagesNumber;
    }


    public JObject ToJson() {

        JObject searchJ = new JObject();

        JObject f = from.ToJson();
        JObject t = To.ToJson();

        searchJ[nameof(from)] = f;
        searchJ[nameof(to)] = t;
        searchJ[nameof(minDate)] = minDate;
        searchJ[nameof(maxDate)] = MaxDate;
        searchJ[nameof(passengersNumber)] = PassengersNumber;

        return searchJ;
    }

    public Location From { get { return from; } }
    public Location To { get { return to; } }
    public DateTime MinDate { get { return minDate; } }
    public DateTime MaxDate { get { return maxDate; } }
    public int PassengersNumber { get { return passengersNumber; } }
    public int LuggagesNumber { get => luggagesNumber; set => luggagesNumber = value; }

}
