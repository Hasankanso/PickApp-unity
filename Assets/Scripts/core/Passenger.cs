using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Requests;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class Passenger {
    private Person person;
    private string id;
    public int luggages, seats;
    private DateTime updated;
    public Passenger(Person person, int luggages, int seats, string id) {
        this.id = id;
        this.person = person;
        this.seats = seats;
        this.luggages = luggages;
    }

    public static Passenger ToObject(JObject json) {
        string did = "";
        var dId = json["objectId"];
        if (dId != null) did = dId.ToString();

        int seats = -1;
        var sJ = json[nameof(Passenger.seats)];
        if (sJ != null)
            int.TryParse(sJ.ToString(), out seats);

        int luggages = -1;
        var lJ = json[nameof(Passenger.luggages)];
        if (lJ != null)
            int.TryParse(lJ.ToString(), out luggages);

        JObject personJ = (JObject)json["person"];
        Person person = null;
        if (personJ == null) {
            person = Program.Person;
        } else {
            person = Person.ToObject(personJ);

        }
        return new Passenger(person, luggages, seats, did);
    }

    public Person Person { get => person; set => person = value; }
    public int Seats { get => seats; set => seats = value; }
    public int Luggages { get => luggages; set => luggages = value; }
    public string Id { get => id; set => id = value; }
    public DateTime Updated { get => updated; set => updated = value; }
}
