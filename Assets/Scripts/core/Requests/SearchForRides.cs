using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Requests {
    class SearchForRides : Request<List<Ride>> {
        private SearchInfo searchInfo;

        public SearchForRides(SearchInfo searchInfo) {
            this.searchInfo = searchInfo;
            HttpPath = "/ReserveBusiness/SearchRides";
        }

        public override List<Ride> BuildResponse(JToken response) //TODO
        {

            var ridesArray = (JArray)response;
            List<Ride> rides = new List<Ride>();
            if (ridesArray == null) return null;

            foreach (JToken j in ridesArray) {
                JObject rideJ = (JObject)j;
                rides.Add(Ride.ToObject(rideJ));
            }

            return rides;
        }

        public override string ToJson() {
            JObject searchInfoJ = searchInfo.ToJson();

            if (Program.User != null) {
                searchInfoJ[nameof(Program.User.id)] = Program.User.Id;
            }

            return searchInfoJ.ToString();
        }

        protected override string IsValid() {
            string fromValidation = Location.Validate(searchInfo.From);
            if (!string.IsNullOrEmpty(fromValidation)) {
                return fromValidation;
            }
            string toValidation = Location.Validate(searchInfo.To);
            if (!string.IsNullOrEmpty(toValidation)) {
                return toValidation;
            }
            if (DateTime.Compare(searchInfo.MinDate, DateTime.Now) < 0) {
                return "Min date must be greater than now date";
            }
            if (DateTime.Compare(searchInfo.MinDate, searchInfo.MaxDate) > 0) {
                return "Min date must be greater than max date";
            }
            if (DateTime.Compare(searchInfo.MaxDate, searchInfo.MinDate) < 0) {
                return "Max date must be smaller than max date";
            }
            if (searchInfo.PassengersNumber < 1 || searchInfo.PassengersNumber > 8) {
                return "Please choose 1 to 8 persons.";
            }
            if (searchInfo.From.Latitude == searchInfo.To.Latitude && searchInfo.From.Longitude == searchInfo.To.Longitude) {
                return "From and To must be different";
            }
            return string.Empty;
        }

    }
}
