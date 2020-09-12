using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using UnityEngine;
using System.Net;
using System.Web;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace Requests
{
  class DeleteCar : Request<List<Car>>
  {
    private Car car;
    private User user;
    public DeleteCar(Car car, User user)
    {
      this.user = user;
      this.car = car;
      HttpPath = "/CarBusiness/DeleteCar";
    }

    public override List<Car> BuildResponse(JToken response) //ToDo
    {
      JArray carsJ = (JArray)response;
      List<Car> cars = null;

      if (carsJ != null)
      {
        cars = new List<Car>();
        foreach (var car in carsJ)
        {
          cars.Add(Car.ToObject((JObject)car));
        }
      }
      return cars;
    }

    public override string ToJson()
    {
      JObject carJ = car.ToJson();
      carJ[nameof(user)] = user.Id;
      return carJ.ToString();
    }

    protected override string IsValid()
    {
      string validateUser = User.ValidateLogin(user);
      if (!string.IsNullOrEmpty(validateUser))
      {
        return validateUser;
      }

      var rides = Program.Person.UpcomingRides;
      foreach (var item in rides)
      {
        if (item.Car.id.Equals(car.id))
        {
          return "You have an upcoming ride in this car";
        }
      }

      if (string.IsNullOrEmpty(car.id))
      {
        return "Objectid should not be null";
      }
      return string.Empty;
    }
  }
}