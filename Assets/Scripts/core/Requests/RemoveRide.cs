using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using UnityEngine;
using System.Net;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
//just a comment
namespace Requests
{
    class RemoveRide : Request<Ride>
    {
        private Ride ride;

        public RemoveRide(Ride ride)
        {
            this.ride = ride;
            HttpPath = "/RideBusiness/DeleteRide";
            Action = "deleteRide";
        }

        public override async Task<Ride> BuildResponse(string response, HttpStatusCode statusCode) //TODO
        {
            JObject ride = JObject.Parse(response);
            return null; ///
        }

        public override string ToJson()
        {
            JObject rideJ = ride.ToJson();
            return rideJ.ToString();
        }

        protected override string IsValid()
        {
            if (ride.id == null || ride.Driver == null)
                return "Please make sure that you have entered the correct information.";
            return string.Empty;
        }
    }
}



