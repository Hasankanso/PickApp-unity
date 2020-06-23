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
    class AddScheduleRide : Request<ScheduleRide> {
        private ScheduleRide scheduleRide;

        public AddScheduleRide(ScheduleRide scheduleRide) {
            this.scheduleRide = scheduleRide;
            HttpPath = "";
            Action = "/AddScheduleRide";
        }

        public override ScheduleRide BuildResponse(string response, HttpStatusCode statusCode) {
            throw new System.NotImplementedException();
        }

        public override string ToJson() {
            JObject scheduleRideJ = scheduleRide.ToJson();
            return scheduleRideJ.ToString();
        }

        protected override string IsValid() {
            if ((scheduleRide.IsMonday != false && scheduleRide.IsMonday != true) || (scheduleRide.IsTuesday != false && scheduleRide.IsTuesday != true) || (scheduleRide.IsWednesday != false && scheduleRide.IsWednesday != true) || (scheduleRide.IsThursday != false && scheduleRide.IsThursday != true) || (scheduleRide.IsFriday != false && scheduleRide.IsFriday != true) || (scheduleRide.IsSaturday != false && scheduleRide.IsSaturday != true) || (scheduleRide.IsSunday != false && scheduleRide.IsSunday != true) || scheduleRide.StartDate == null || scheduleRide.EndDate == null || scheduleRide.Ride == null || scheduleRide.Ride.From == null || scheduleRide.Ride.To == null || scheduleRide.Ride.LeavingDate == null
                || string.IsNullOrEmpty(scheduleRide.Ride.Comment) || (scheduleRide.Ride.MusicAllowed != false && scheduleRide.Ride.MusicAllowed != true) ||
                (scheduleRide.Ride.AcAllowed != false && scheduleRide.Ride.AcAllowed != true) || (scheduleRide.Ride.SmokingAllowed != false && scheduleRide.Ride.SmokingAllowed != true)
                || (scheduleRide.Ride.PetsAllowed != false && scheduleRide.Ride.PetsAllowed != true) || (scheduleRide.Ride.KidSeat != false && scheduleRide.Ride.KidSeat != true)
                || scheduleRide.Ride.AvailableSeats < 1 || scheduleRide.Ride.AvailableLuggages < 0 || scheduleRide.Ride.StopTime < 0 || scheduleRide.Ride.CountryInformations == null ||
                 scheduleRide.Ride.Map == null || scheduleRide.Ride.ReservedLuggages < 0 || scheduleRide.Ride.ReservedSeats < 0)
                return "Please make sure that you have entered the correct information.";
            return string.Empty;
        }
    }
}
