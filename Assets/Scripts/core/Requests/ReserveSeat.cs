using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Requests;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using UnityEngine;

namespace Requests {
    class ReserveSeat : Request<Ride> {
        Ride ride;
        User user;
        int seats, luggages;

        public ReserveSeat(Ride ride, User user, int seats, int luggages) {
            this.ride = ride;
            this.seats = seats;
            this.luggages = luggages;
            this.user = user;
            HttpPath = "/ReserveBusiness/ReserveSeat";
        }


        public override async Task<Ride> BuildResponse(JToken response, int statusCode) //TODO
        {
            JObject json = (JObject) (response);
            JObject rideJ = (JObject)json["ride"];
            return Ride.ToObject(rideJ);
        }

        public override string ToJson() {
            JObject reserveJ = new JObject();
            reserveJ[nameof(ride)] = new JObject { [nameof(ride.id)] = ride.Id };
            reserveJ[nameof(user)] = new JObject { [nameof(user.id)] = user.Id };
            reserveJ[nameof(seats)] = this.seats;
            reserveJ[nameof(luggages)] = this.luggages;
            return reserveJ.ToString();
        }

        protected override string IsValid() {

            return string.Empty;
        }
    }
}
