using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Passenger {
    private Person person;
    private int numberOfPersons,id;
    private DateTime updated;
    public Passenger(Person person, int numberOfPerson, int id) {
        Person = person;
        NumberOfPersons = numberOfPerson;
        Id = id;
    }
    public Passenger(Person person, int numberOfPerson) {
        Person = person;
        NumberOfPersons = numberOfPerson;
    }
    public Person Person { get => person; set => person = value; }
    public int NumberOfPersons { get => numberOfPersons; set => numberOfPersons = value; }
    public int Id { get => id; set => id = value; }
    public DateTime Updated { get => updated; set => updated = value; }
}
