using Requests;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chat {
    private int id;
    private DateTime date;
    private List<Message> messages = new List<Message>();
    private Person person;
    private bool isNewMessage;

    public Chat(int id, DateTime date, List<Message> messages, Person person, bool isNewMessage) {
        Id = id;
        Date = date;
        Messages = messages;
        Person = person;
        IsNewMessage = isNewMessage;
    }

    public Chat(DateTime date, List<Message> messages, Person person, bool isNewMessage) {
        Date = date;
        Messages = messages;
        Person = person;
        IsNewMessage = isNewMessage;
    }

    public Chat(Person person) {
        Person = person;
    }

    public int Id { get => id; set => id = value; }
    public DateTime Date { get => date; set => date = value; }
    public List<Message> Messages { get => messages; set => messages = value; }
    public Person Person { get => person; set => person = value; }
    public bool IsNewMessage { get => isNewMessage; set => isNewMessage = value; }

    public override string ToString() {
        return base.ToString();
    }
    public static string DisplayPerson(Person p1) {
        return p1.ToString();
    }
}