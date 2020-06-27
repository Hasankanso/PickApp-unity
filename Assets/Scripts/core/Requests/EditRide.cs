using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using UnityEngine;
using System.Net;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace Requests {
    class EditRide : Request<Ride> {
        private Person user;
        private Ride ride;

        public EditRide(Person user, Ride ride) {
            this.user = user;
            this.ride = ride;
            HttpPath = "";
            Action = "editRide";
        }

        public override async Task<Ride> BuildResponse(string response, HttpStatusCode statusCode) //TODO
        {
            JObject ride = JObject.Parse(response);
            if (ride == null) return null;
            return Ride.ToObject(ride);
        }

        public override string ToJson() {
            JObject rideJ = ride.ToJson();
            rideJ[nameof(ride.id)] = ride.Id;
            return rideJ.ToString();
        }

        protected override string IsValid() {
            if (ride.From == null || ride.To == null || ride.LeavingDate == null
                || string.IsNullOrEmpty(ride.Comment) || (ride.MusicAllowed != false && ride.MusicAllowed != true) ||
                (ride.AcAllowed != false && ride.AcAllowed != true) || (ride.SmokingAllowed != false && ride.SmokingAllowed != true)
                || (ride.PetsAllowed != false && ride.PetsAllowed != true) || (ride.KidSeat != false && ride.KidSeat != true)
                || ride.AvailableSeats < 1 || ride.AvailableLuggages < 0 || ride.StopTime < 0 || ride.CountryInformations == null ||
                ride.Map == null || ride.ReservedLuggages < 0 || ride.ReservedSeats < 0)
                return "Please make sure that you have entered the correct information.";
            return string.Empty;
        }
    }
}



