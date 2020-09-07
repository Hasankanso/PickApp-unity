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
  class EditCar : Request<List<Car>>
  {
    private Car car;
    private User user;

    public EditCar(Car car,User user)
    {
      this.user = user;
      this.car = car;
      HttpPath = "/CarBusiness/UpdateCar";
    }

    public override List<Car> BuildResponse(JToken response) //TODO
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



