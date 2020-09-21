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

        public override Ride BuildResponse(JToken response) //TODO
        {
            return Ride.ToObject((JObject)response);
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
            string validateUser = User.ValidateLogin(user);
            if (!string.IsNullOrEmpty(validateUser)) {
                return validateUser;
            }
            if (string.IsNullOrEmpty(ride.id)) {
                return "Ride id is null";
            }
            if (seats < 0) {
                return "Please select seats";
            }
            if (seats > ride.AvailableSeats) {
                return "There is " + ride.AvailableSeats + " available seats";
            }
            if (luggages < 0) {
                return "Please select luggage";
            }
            if (luggages > ride.AvailableLuggages) {
                return "There is " + ride.AvailableLuggages + " available luggage";
            }
            return string.Empty;
        }
    }
}
