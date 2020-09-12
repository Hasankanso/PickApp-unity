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
using System;
using System.Text.RegularExpressions;

namespace Requests {
    class AddCar : Request<List<Car>> {
        private Car car;
        private User user;
        public AddCar(Car car, User user) {
            this.user = user;
            this.car = car;
            HttpPath = "/CarBusiness/AddCar";
        }

        public override List<Car> BuildResponse(JToken response) //ToDo
        {
            JArray carsJ = (JArray)response;
            List<Car> cars = null;

            if (carsJ != null) {
                cars = new List<Car>();
                foreach (var car in carsJ) {
                    cars.Add(Car.ToObject((JObject)car));
                }
            }
            return cars;
        }

        public override string ToJson() {
            JObject carJ = car.ToJson();
            carJ[nameof(user)] = user.Id;
            return carJ.ToString();
        }

        protected override string IsValid() {
            string validateUser = User.ValidateLogin(user);
            if (!string.IsNullOrEmpty(validateUser)) {
                return validateUser;
            }
            string carValidation = Car.Validate(car);
            if (!string.IsNullOrEmpty(carValidation)) {
                return carValidation;
            }
            return string.Empty;
        }
    }
}