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
    class AddRate : Request<Rate> {
        private Rate rate;
        private User user;
        private Ride ride;
        private Person reviewer, target;


        public AddRate(Rate rate,Ride ride,User user,Person target,Person reviewer) {
            this.rate = rate;
            this.user = user;
            this.ride = ride;
            this.reviewer = reviewer;
            this.target = target;
            HttpPath = "/RateBusiness/AddRate";
        }

        public override Rate BuildResponse(JToken response) //TODO we have to use statusCode
        {
            return Rate.ToObject((JObject)response);
        }

        public override string ToJson() {
            JObject rateJ = rate.ToJson();
            rateJ[nameof(user)] = user.id;
            rateJ[nameof(ride)] = ride.id;
            rateJ[nameof(reviewer)] = reviewer.id;
            rateJ[nameof(target)] = target.id;
            return rateJ.ToString();
        }

        protected override string IsValid() {
         /*   string rateValidation = Rate.Validate(rate);
            if (!string.IsNullOrEmpty(rateValidation)) {
                return rateValidation;
            }*/
            return string.Empty;
        }
    }
}



