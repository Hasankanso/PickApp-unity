using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Requests {
    public class GetMyAccountInfo : Request<Person> {

        public GetMyAccountInfo() {
            HttpPath = "";
            Action = "getMyAccountInfo";
        }

        public override async Task<Person> BuildResponse(string response, HttpStatusCode statusCode) //TODO
        {
            return JsonConvert.DeserializeObject<Person>(response);
        }

        public override string ToJson() {
            return "";
        }

        protected override string IsValid() {
            return string.Empty;
        }

    }
}
