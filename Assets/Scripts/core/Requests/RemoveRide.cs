using BackendlessAPI;
using BackendlessAPI.Async;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
    class RemoveRide : Request<bool>
    {
        private Ride ride;

        public RemoveRide(Ride ride)
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

        protected override string IsValid()
        {
            if (ride.id == null || ride.Driver == null)
                return "Please make sure that you have entered the correct information.";
            return string.Empty;
        }
    }
}



