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


        public AddRate(Rate rate) {
            this.rate = rate;
            HttpPath = "/RateBusiness/AddRate";
        }

        public override Rate BuildResponse(JToken response) //TODO we have to use statusCode
        {
            return Rate.ToObject((JObject)response);
        }

        public override string ToJson() {
            return rate.ToJson().ToString();
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



