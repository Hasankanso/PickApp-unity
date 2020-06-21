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
    class AddRide : Request<Ride> {
        private Ride ride;

        public AddRide(Ride ride) {
            this.ride = ride;
            HttpPath = "RideBusiness/AddRide";
            Action = "addRide";
        }

        public override Ride BuildResponse(string response, HttpStatusCode statusCode) //TODO we have to use statusCode
        {
            return JsonConvert.DeserializeObject<Ride>(response);
        }

        public override string ToJson() {
            JObject rideJ = ride.ToJson();
            return rideJ.ToString();
        }

        protected override string IsValid() {
            if (ride.From == null || ride.To == null || ride.LeavingDate == null || ride.Car == null || ride.Driver == null
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



