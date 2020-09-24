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
        string reason;

        public CancelReservedSeats(Ride ride, string reason) {
            this.ride = ride;
            this.user = Program.User;
            this.reason = reason;
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
            TimeSpan ts = ride.LeavingDate - DateTime.Now;
            if (ts.TotalHours < 48)
            {//need to notify the driver, give a reason and the driver can rate the passenger
                if (string.IsNullOrEmpty(reason))
                {
                    return "You should state a good reason!";
                } 
                else if (reason.Length < 15)
                    return "The reason should be at least 15 characters";
            }
            return string.Empty;
        }
    }
}
