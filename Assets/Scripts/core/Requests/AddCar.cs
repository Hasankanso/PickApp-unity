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
  class AddCar : Request<Car>
  {
    private Car car;
    private User user;
    public AddCar(Car car, User user)
    {
      this.user = user;
      this.car = car;
      HttpPath = "/CarBusiness/AddCar";
    }

    public override Car BuildResponse(JToken response) //ToDo
    {
      return Car.ToObject((JObject)response);
    }

    public override string ToJson()
    {
      JObject carJ = car.ToJson();
      carJ[nameof(user)] = user.Id;
      return carJ.ToString();
    }

    protected override string IsValid()
    {
      if (string.IsNullOrEmpty(car.Name) || car.Year < 1970 ||
          car.MaxSeats < 1 || string.IsNullOrEmpty(car.Color) ||
          car.MaxLuggage < 0)
        return "Please make sure that you have entered the correct information.";
      return string.Empty;

    }
  }
}



