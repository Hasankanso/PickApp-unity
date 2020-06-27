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

namespace Requests {
    class AddCar : Request<Car> {
        private Car car;
        private Driver driver;
        public AddCar(Car car,Driver driver) {
            this.driver = driver;
            this.car = car;
            HttpPath = "/CarBusiness/AddCar";
        }

        public override async Task<Car> BuildResponse(string response, HttpStatusCode statusCode) //ToDo
        {
            //  if (statusCode == HttpStatusCode.Accepted) 
            return JsonConvert.DeserializeObject<Car>(response);
        }

        public override string ToJson() {
            JObject carJ = car.ToJson();
            carJ[nameof(driver)] = driver.Did;
            return carJ.ToString();
        }

        protected override string IsValid() {
            if (string.IsNullOrEmpty(car.Name) || car.Year < 1970 ||
                car.MaxSeats < 1 || string.IsNullOrEmpty(car.Color) ||
                car.MaxLuggage < 0)
                return "Please make sure that you have entered the correct information.";
            return string.Empty;

        }
    }
}



