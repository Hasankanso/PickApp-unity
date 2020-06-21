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
    class GetMyUpcomingRides : Request<List<Ride>> {

        public GetMyUpcomingRides() {
            HttpPath = "";
            Action = "getMyUpcomingRides";
        }

        public override List<Ride> BuildResponse(string response, HttpStatusCode statusCode) {
            return JsonConvert.DeserializeObject<List<Ride>>(response);
        }

        public override string ToJson() {
            return "";
        }

        protected override string IsValid() {
            return string.Empty;
        }
    }
}



