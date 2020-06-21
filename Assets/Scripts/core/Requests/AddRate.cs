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
            HttpPath = "RateBusiness/AddRate";
            Action = "addRate";
        }

        public override Rate BuildResponse(string response, HttpStatusCode statusCode) //TODO we have to use statusCode
        {
            return JsonConvert.DeserializeObject<Rate>(response);
        }

        public override string ToJson() {
            JObject rateJ = rate.ToJson();
            return rateJ.ToString();
        }

        protected override string IsValid() {
            if (string.IsNullOrEmpty(rate.Comment) && (rate.Grade == 1 || rate.Grade == 2))
                return "Please make sure that you have entered the correct information.";
            return string.Empty;
        }
    }
}



