using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

namespace Requests {
    class Logout : Request<string> {
        public Logout() {
            HttpPath = "/UserBusiness/Logout";
        }
        public override string BuildResponse(string response, HttpStatusCode statusCode) {
            return response;
        }

        public override string ToJson() {
            return "{}";
        }

        protected override string IsValid() {
            return string.Empty;
        }
    }
}
