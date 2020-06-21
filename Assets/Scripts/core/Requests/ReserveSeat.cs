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
        Person person;
        int seats;

        public ReserveSeat(Ride ride, Person user, int seats) {
            this.ride = ride;
            this.seats = seats;
            this.person = user;
            HttpPath = "";
            Action = "reserveRide";
        }

        public override Ride BuildResponse(string response, HttpStatusCode statusCode) //TODO
        {
            return JsonConvert.DeserializeObject<Ride>(response);

        }

        public override string ToJson() {
            JObject reserveJ = new JObject();
            reserveJ[nameof(ride)] = new JObject { [nameof(ride.id)] = ride.Id };
            reserveJ[nameof(person)] = new JObject { [nameof(person.id)] = person.Id };
            reserveJ[nameof(seats)] = this.seats;
            return reserveJ.ToString();
        }

        protected override string IsValid() {

            return string.Empty;
        }
    }
}
