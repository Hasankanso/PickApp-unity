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
    class BecomeDriver : Request<Driver> {
        private Driver driver;
        public BecomeDriver(Driver driver) {
            this.driver = driver;
            HttpPath = "/PersonBusiness/BecomeDriver";
        }

        public override Task<Driver> BuildResponse(string response, HttpStatusCode statusCode) //TODO we have to use statusCode
        {
            //return JsonConvert.DeserializeObject<Ride>(response);
            throw new System.NotImplementedException();
        }

        public override string ToJson() {
            JObject driverJ = driver.ToJson();
            return driverJ.ToString();
        }

        protected override string IsValid() {
            throw new System.NotImplementedException();
        }
    }
}