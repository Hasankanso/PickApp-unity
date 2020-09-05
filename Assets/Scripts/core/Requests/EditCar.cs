using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using UnityEngine;
using System.Net;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace Requests
{
  class EditCar : Request<Car>
  {
    private Car car;
    private User user;

    public EditCar(Car car,User user)
    {
      this.user = user;
      this.car = car;
      HttpPath = "/CarBusiness/UpdateCar";
    }

    public override Car BuildResponse(JToken response) //TODO
    {
            return Car.ToObject((JObject)response);
            this.user = Program.User;
        }

    public override string ToJson()
    {
      JObject carJ = car.ToJson();
      carJ[nameof(car.id)] = car.Id;
      carJ[nameof(user)] = user.Id;
      return carJ.ToString();
    }

    protected override string IsValid()
    {
      if (string.IsNullOrEmpty(car.Name) || car.Year < 1970 || car.Year > 2020 ||
          car.MaxSeats < 1 || string.IsNullOrEmpty(car.Color) ||
          car.MaxLuggage < 0)
        return "Please make sure that you have entered the correct information.";
      return string.Empty;

    }
  }
}



