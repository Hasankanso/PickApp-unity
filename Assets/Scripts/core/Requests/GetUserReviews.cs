using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Requests {
    class GetUserReviews : Request<List<Rate>> {
        User user;
        public GetUserReviews(User user) {
            this.user = user;
            HttpPath = "/RateBusiness/GetRate";
        }

        public override List<Rate> BuildResponse(JToken response) //TODO
        {
            JArray rateArray = (JArray)response;
            List<Rate> rates = new List<Rate>();
            if (rateArray == null)
                return null;
            foreach (JToken j in rateArray) {
                rates.Add(Rate.ToObject((JObject)j));
            }
            return rates;
        }

        public override string ToJson() {
            JObject personJ = new JObject();
            personJ[nameof(user)] = user.Id;
            return personJ.ToString();
        }

        protected override string IsValid() //ToDo 
        {
            string validateUser = User.ValidateLogin(user);
            if (!string.IsNullOrEmpty(validateUser)) {
                return validateUser;
            }
            return string.Empty;
        }
    }
}
