using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using UnityEngine;
using System.Net;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using System;

namespace Requests
{
    class AddRide : Request<Ride>
    {
        private Ride ride;

        public AddRide(Ride ride)
        {
            this.ride = ride;
            HttpPath = "/RideBusiness/AddRide";
        }

        public override Ride BuildResponse(JToken response) //TODO
        {
            return Ride.ToObject((JObject)response);
        }

        public override string ToJson()
        {
            JObject rideJ = ride.ToJson();
            return rideJ.ToString();
        }

        protected override string IsValid()
        {
            string fromValidation = Location.Validate(ride.From);
            if (!string.IsNullOrEmpty(fromValidation)) {
                return fromValidation;
            }
            string toValidation = Location.Validate(ride.To);
            if (!string.IsNullOrEmpty(toValidation)) {
                return toValidation;
            }
            if (DateTime.Compare(ride.LeavingDate, DateTime.Now.AddHours(1)) < 0) {
                return "Your ride must be after one hour or more from now";
            }
            if (ride.AvailableSeats<=0||ride.AvailableSeats>ride.Car.MaxSeats) {
                return "Invalid number of seats";
            }
            if (ride.AvailableLuggages < 0 || ride.AvailableLuggages > ride.Car.MaxLuggage) {
                return "Invalid number of luggage";
            }
            if (ride.StopTime!=0&&(ride.StopTime<5||ride.StopTime>30)) {
                return "Your stop time must be between 5 and 30 minutes";
            }
            if (string.IsNullOrEmpty(ride.Comment)|| ride.Comment.Length<25|| ride.Comment.Length > 400) {
                return "Please add a comment between 25 and 400 characters";
            }
            if (string.IsNullOrEmpty(ride.MapBase64)) {
                return "Please choose your ride's road";
            }
            if (string.IsNullOrEmpty(ride.Car.id)) {
                return "Please choose a car";
            }
            if (ride.Price==0) {
                return "Please set a price";
            }
            if (string.IsNullOrEmpty(ride.CountryInformations.Id)) {
                return "Please choose a country info";
            }
            if (string.IsNullOrEmpty(ride.Driver.Id)) {
                return "You're not a driver";
            }
            foreach (var item in Program.Person.UpcomingRides) {
                if (DateTime.Compare(ride.LeavingDate,item.LeavingDate)==0) {
                    return "you have an upcoming ride in this ride time";
                }
            }
            bool carExist = false;
            foreach (var item in Program.Driver.Cars) {
                if (ride.Car.id.Equals(item.id)) {
                    carExist = true;
                }
            }
            if (carExist==false) {
                return "Please choose one of your cars";
            }
            return string.Empty;
        }
    }
}



