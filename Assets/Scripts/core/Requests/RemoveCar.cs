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
            Action = "removeCar";
        }

        public override Car BuildResponse(string response, HttpStatusCode statusCode) //TODO
        {
            return JsonConvert.DeserializeObject<Car>(response);
        }

        public override string ToJson() {
            JObject carJ = new JObject();
            carJ[nameof(car.id)] = car.Id;
            return carJ.ToString();
        }

        protected override string IsValid() {
            return string.Empty;
        }
    }
}



