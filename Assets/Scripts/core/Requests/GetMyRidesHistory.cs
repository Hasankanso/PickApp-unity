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
    class GetMyRidesHistory : Request<List<Ride>> {
        private User user;

        public GetMyRidesHistory(User user) {
            HttpPath = "/RideBusiness/GetMyRidesHistory";
            this.user = user;
        }

        public override List<Ride> BuildResponse(JToken response)//ToDo
        {
            var ridesArray = (JArray)response;
            List<Ride> rides = new List<Ride>();
            if (ridesArray == null) return null;
            foreach (JToken j in ridesArray) {
                JObject rideJ = (JObject)j;
                Debug.Log(rideJ);
                rides.Add(Ride.ToObject(rideJ));
            }
            return rides;
        }

        public override string ToJson() {
            JObject userJ = new JObject();
            userJ[nameof(this.user)] = this.user.Id;
            return userJ.ToString();
        }

        protected override string IsValid() {
            return string.Empty;
        }

    }
}



