using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using UnityEngine;
using System.Net;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace Requests
{
    class EditRide : Request<Ride>
    {
        private Person user;
        private Ride ride;

        public EditRide(Person user, Ride ride)
        {
            this.user = user;
            this.ride = ride;
            HttpPath = "/RideBusiness/EditRide";
        }

        public override Ride BuildResponse(JToken response) //TODO
        {

            if (response == null) return null;
            return Ride.ToObject((JObject)response);
        }

        public override string ToJson()
        {
            JObject rideJ = ride.ToJson();
            rideJ[nameof(ride.id)] = ride.Id;
            Debug.Log(ride.Id);
            return rideJ.ToString();
        }

        protected override string IsValid()
        {
            return Ride.Valid(ride);
        }
    }
}



