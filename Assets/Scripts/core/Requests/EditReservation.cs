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
    class EditReservation : Request<Ride> {
        Ride ride;
        int seats, luggage;

        public EditReservation(Ride ride, int seats, int luggage) {
            this.ride = ride;
            this.seats = seats;
            this.luggage = luggage;
            HttpPath = "/ReserveBusiness/EditReservation";
        }

        public override Ride BuildResponse(JToken response) //TODO
        {
            return Ride.ToObject((JObject)response);
        }

        public override string ToJson() {
            JObject reserveJ = new JObject();
            reserveJ[nameof(ride)] = new JObject { [nameof(ride.id)] = ride.Id };
            reserveJ["user"] = Program.User.Id;
            reserveJ[nameof(seats)] = seats;
            reserveJ[nameof(luggage)] = luggage;
            return reserveJ.ToString();
        }

        protected override string IsValid() {
            string validateUser = User.ValidateLogin(Program.User);
            if (!string.IsNullOrEmpty(validateUser)) {
                return validateUser;
            }
            if (string.IsNullOrEmpty(ride.id)) {
                return "Ride id is null";
            }
            if (seats < 0) {
                return "Please select seats";
            }
            if (seats == (ride.AvailableSeats + ride.Passengers[0].seats)) {
                return "You have been already reserved " + ride.AvailableSeats + ride.Passengers[0].seats + " seats";
            }
            if (luggage == (ride.AvailableLuggages + ride.Passengers[0].luggages)) {
                return "You have been already reserved " + ride.AvailableLuggages + ride.Passengers[0].luggages + " luggage";
            }
            if (seats > (ride.AvailableSeats + ride.Passengers[0].seats)) {
                return "There is " + ride.AvailableSeats + ride.Passengers[0].seats + " available seats";
            }
            if (luggage < 0) {
                return "Please select luggage";
            }
            if (luggage > ride.AvailableLuggages + ride.Passengers[0].luggages) {
                return "There is " + ride.AvailableLuggages + ride.Passengers[0].luggages + " available luggage";
            }
            return string.Empty;
        }
    }
}
