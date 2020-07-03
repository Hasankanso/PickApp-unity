using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using UnityEngine;

namespace Requests {
    class Logout : Request<string> {
        public Logout() {
            HttpPath = "/UserBusiness/Logout";
        }
        public override async Task<string> BuildResponse(JToken response, int statusCode) {
            return "{}";
        }

        public override string ToJson() {
            return "{}";
        }

        protected override string IsValid() {
            return string.Empty;
        }
    }
}
