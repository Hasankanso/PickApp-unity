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
    class CancelRide : Request<Ride> {
        private Ride ride;
        private string reason;

        public CancelRide( Ride ride, string reason) {
            this.ride = ride;
            this.reason = reason;
            HttpPath = "";
            Action = "cancelRide";
        }

        public override async Task<Ride> BuildResponse(string response, HttpStatusCode statusCode) //TODO
        {
            return JsonConvert.DeserializeObject<Ride>(response);
        }

        public override string ToJson() {
            JObject json = new JObject();
            json[nameof(ride.Id)] = ride.Id;
            json["reason"] =reason;
            return json.ToString();
        }

        protected override string IsValid() {
            if (string.IsNullOrEmpty(reason))
                return "Please make sure that you have entered the correct information.";
            return string.Empty;
        }

    }
}



