using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Requests;
using Newtonsoft.Json.Linq;

public class Alert {
    private int id;
    private Location from, to;
    private DateTime minDate, maxDate;
    private int numberOfPersons, numberOfLuggages;
    private string comment;
    private float price;
    private CountryInformations countryInformations;
    private Person person;
    private DateTime updated;

    public Alert(Person person, Location from, Location to, string price, DateTime minDate, DateTime maxDate, int numberOfPersons, int numberOfLuggages, CountryInformations countryInformations, string comment) {
        this.Person = person;
        this.from = from;
        this.to = to;
        this.minDate = minDate;
        this.maxDate = maxDate;
        this.numberOfPersons = numberOfPersons;
        this.numberOfLuggages = numberOfLuggages;
        this.price = float.Parse(price);
        this.countryInformations = countryInformations;
        this.comment = comment;
    }

    public JObject ToJson() {
        JObject alertJ = new JObject();

        alertJ[nameof(From)] = From.ToJson();
        alertJ[nameof(Person)] = new JObject { [nameof(Person.Id)] = this.Person.Id };

        alertJ[nameof(To)] = To.ToJson();
        
        alertJ[nameof(Price)] = price;

        alertJ[nameof(MinDate)] = minDate;
        alertJ[nameof(MaxDate)] = maxDate;
        alertJ[nameof(NumberOfPersons)] = numberOfPersons;
        alertJ[nameof(NumberOfLuggages)] = numberOfLuggages;
        alertJ[nameof(Comment)] = comment;

        alertJ[nameof(CountryInfo)] = CountryInfo.ToJson();

        return alertJ;
    }

    public CountryInformations CountryInfo { get => countryInformations; set => countryInformations = value; }
    public int NumberOfPersons { get => numberOfPersons; set => numberOfPersons = value; }
    public int NumberOfLuggages { get => numberOfLuggages; set => numberOfLuggages = value; }
    public DateTime MinDate { get => minDate; set => minDate = value; }
    public DateTime MaxDate { get => maxDate; set => maxDate = value; }
    public Location From { get => from; set => from = value; }
    public Location To { get => to; set => to = value; }
    public int Id { get => id; set => id = value; }
    public Person Person { get => person; set => person = value; }
    public string Comment { get => comment; set => comment = value; }
    public DateTime Updated { get => updated; set => updated = value; }
    public float Price { get => price; set => price = value; }
}
