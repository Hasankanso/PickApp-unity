using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Requests {
    class ReserveSeat : Request<Ride> {
        Ride ride;
        User user;
        int seats;

        public ReserveSeat(Ride ride, User user, int seats) {
            this.ride = ride;
            this.seats = seats;
            this.user = user;
            HttpPath = "/ReserveBusiness/ReserveSeat";
        }

        public override async Task<Ride> BuildResponse(string response, HttpStatusCode statusCode) //TODO
        {
            return JsonConvert.DeserializeObject<Ride>(response);

        }

        public override string ToJson() {
            JObject reserveJ = new JObject();
            reserveJ[nameof(ride)] = new JObject { [nameof(ride.id)] = ride.Id };
            reserveJ[nameof(user)] = new JObject { [nameof(user.id)] = user.Id };
            reserveJ[nameof(seats)] = this.seats;
            return reserveJ.ToString();
        }

        protected override string IsValid() {

            return string.Empty;
        }
    }
}
