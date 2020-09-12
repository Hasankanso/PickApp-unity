using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Requests {
    class CancelReservedSeats : Request<Ride> {
        Ride ride;
        User user;

        public CancelReservedSeats(Ride ride, User user) {
            this.ride = ride;
            this.user = user;
            HttpPath = "/RideBusiness/CancelReserved";
        }

        public override Ride BuildResponse(JToken response) //TODO
        {
            return Ride.ToObject((JObject)response);

        }

        public override string ToJson() {
            JObject cancelJ = new JObject();
            cancelJ[nameof(ride)] = new JObject { [nameof(ride.id)] = ride.Id };
            cancelJ[nameof(user)] = new JObject { [nameof(user.id)] = user.Id };
            return cancelJ.ToString();
        }

        protected override string IsValid() {
            string validateUser = User.ValidateLogin(Program.User);
            if (!string.IsNullOrEmpty(validateUser)) {
                return validateUser;
            }
            if (string.IsNullOrEmpty(ride.id)) {
                return "Invalid id of ride";
            }
            TimeSpan ts = ride.LeavingDate.Subtract(DateTime.Now);
            Double hours = ts.TotalHours;

            if (ride.Passengers[0] != null && hours > 48) {
                /*
             if (string.IsNullOrEmpty(reason)||reason.Length<15) {
                 return "Your reason must be at least 15 character";
             }*/
            }
            return string.Empty;
        }
    }
}
