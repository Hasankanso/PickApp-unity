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
        private string reason;

        public CancelRide(Ride ride, string reason)
        {
            this.ride = ride;
            this.reason = reason;
            HttpPath = "/RideBusiness/CancelRide";
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

            if (ride.LeavingDate < DateTime.Now)
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
            TimeSpan ts = ride.LeavingDate - DateTime.Now;
            if ((ride.Passengers.Count !=0 && ts.TotalHours < 48))
            {  //need to notify the passengers, give a reason and the users can rate him
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



