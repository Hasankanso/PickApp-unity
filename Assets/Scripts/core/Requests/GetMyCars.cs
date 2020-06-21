using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using System.Net;
using Newtonsoft.Json.Linq;

namespace Requests {
    class GetMyCars : Request<List<Car>> {
        private Driver user;
        public GetMyCars(Driver user) {
            this.user = user;
            HttpPath = "";
            Action = "getMyCars";
        }

        public override List<Car> BuildResponse(string response, HttpStatusCode statusCode) //TODO
        {
            return JsonConvert.DeserializeObject<List<Car>>(response);
        }

        public override string ToJson() {
            JObject driverJ = new JObject();
            driverJ[nameof(user.id)] = user.Id;
            return driverJ.ToString();
        }

        protected override string IsValid() {
            return string.Empty;
        }
    }
}



