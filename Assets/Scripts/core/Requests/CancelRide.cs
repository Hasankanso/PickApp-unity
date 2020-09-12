using BackendlessAPI;
using BackendlessAPI.Async;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using UnityEngine;
//just a comment
namespace Requests
{
    class CancelRide : Request<bool>
    {
        private Ride ride;

        public CancelRide(Ride ride)
        {
            this.ride = ride;
            HttpPath = "/RideBusiness/DeleteRide";
        }

        public override bool BuildResponse(JToken response) //TODO
        {
            JObject json = (JObject)response;
            bool deleted = true;
            var del = json["deleted"];
            if (del != null)
                deleted = bool.Parse(del.ToString());
            return deleted;
        }

        public override string ToJson()
        {
            JObject rideJ = ride.removeToJson();
            return rideJ.ToString();
        }

        protected override string IsValid() // ToDo
        {

            if (ride.LeavingDate > DateTime.Now)
                return "Ride had started" ;
            if (string.IsNullOrEmpty(ride.Id))
            {
                return "Ride object Id is null";
            }

            string validateUser = User.ValidateLogin(Program.User);
            if (!string.IsNullOrEmpty(validateUser))
            {
                return validateUser;
            }
            TimeSpan ts = ride.LeavingDate.Subtract(DateTime.Now);
            Double hours = ts.TotalHours;
            if (ride.Passengers[0] != null && hours<=48 )
            {
                return "Your ride has been deleted";  //need to notify the passengers
            }
            if (ride.Passengers[0] != null && hours>48)
            {
                /*if (string.IsNullOrEmpty(ride.Reason))
                {
                    return "Reason must not be null!";
                }*/
                return "Your ride has been deleted";  //need to notify the passengers, give a reason and the users can rate him
            }
            return string.Empty;
        }
    }
}



