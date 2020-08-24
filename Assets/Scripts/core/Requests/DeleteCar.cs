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
    class DeleteCar : Request<Car>
    {
        private Car car;

        public DeleteCar(Car car)
        {
            this.car = car;
            HttpPath = "/CarBusiness/DeleteCar";
        }

        public override Car BuildResponse(JToken response) //TODO
        {
            return Car.ToObject((JObject)response);
        }

        public override string ToJson()
        {
            JObject carJ = car.ToJson();
            carJ[nameof(car.id)] = car.Id;
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



