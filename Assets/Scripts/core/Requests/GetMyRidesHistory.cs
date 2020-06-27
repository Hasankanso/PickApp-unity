using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using System.Net;
using Newtonsoft.Json.Linq;

namespace Requests {
    class GetMyRidesHistory : Request<List<Ride>> {


        public GetMyRidesHistory() {
            HttpPath = "";
            Action = "getMyRidesHistory";
        }

        public override async Task<List<Ride>> BuildResponse(string response, HttpStatusCode statusCode)//ToDo
        {
            return JsonConvert.DeserializeObject<List<Ride>>(response);
        }

        public override string ToJson() {
            return "";
        }

        protected override string IsValid() {
            return string.Empty;
        }

    }
}



