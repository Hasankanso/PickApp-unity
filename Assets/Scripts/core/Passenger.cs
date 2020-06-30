using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Passenger
{
  private User user;
    private int numberOfPersons;
        private string id;
  private DateTime updated;
  public Passenger(User user, int numberOfPerson, string id)
  {
    User = user;
    NumberOfPersons = numberOfPerson;
    Id = id;
  }
  public Passenger(User user, int numberOfPerson)
  {
    User = user;
    NumberOfPersons = numberOfPerson;
  }
    public bool isReserved(Passenger passenger)
    {
        return Program.User.Id == passenger.Id;
    }
  public User User { get => user; set => user = value; }
  public int NumberOfPersons { get => numberOfPersons; set => numberOfPersons = value; }
  public string Id { get => id; set => id = value; }
  public DateTime Updated { get => updated; set => updated = value; }
}
