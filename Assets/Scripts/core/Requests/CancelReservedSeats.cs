using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Requests
{
    class CancelReservedSeats : Request<Ride>
    {
        Ride ride;
        User user;

        public CancelReservedSeats(Ride ride, User user)
        {
            this.ride = ride;
            this.user = user;
            HttpPath = "/RideBusiness/CancelReserved";
        }

        public override async Task<Ride> BuildResponse(string response, HttpStatusCode statusCode) //TODO
        {
            JObject ride = JObject.Parse(response);
            return Ride.ToObject(ride);

        }

        public override string ToJson()
        {
            JObject cancelJ = new JObject();
            cancelJ[nameof(ride)] = new JObject { [nameof(ride.id)] = ride.Id };
            cancelJ[nameof(user)] = new JObject { [nameof(user.id)] = user.Id };
            return cancelJ.ToString();
        }

        protected override string IsValid()
        {

            return string.Empty;
        }
    }
}
