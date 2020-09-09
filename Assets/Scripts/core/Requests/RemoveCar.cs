using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using UnityEngine;
using System.Net;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace Requests {
    class RemoveCar : Request<Car> {
        private Person user;
        private Car car;

        public RemoveCar(Person user, Car car) {
            this.user = user;
            this.car = car;
            HttpPath = "";
        }

        public override Car BuildResponse(JToken response) //TODO
        {
            // return JsonConvert.DeserializeObject<Car>(response);
            return null;
        }

        public override string ToJson() {
            JObject carJ = new JObject();
            carJ[nameof(car.id)] = car.Id;
            return carJ.ToString();
        }

        protected override string IsValid() {
            string validateUser = User.ValidateLogin(user);
            if (!string.IsNullOrEmpty(validateUser)) {
                return validateUser;
            }
            if (string.IsNullOrEmpty(car.id)) {
                return "Objectid should not be null";
            }
            foreach (var item in Program.Person.UpcomingRides) {
                if (item.Car.id.Equals(car.id)) {
                    return "You have an upcoming ride in this car";
                }
            }
            return string.Empty;
        }
    }
}



