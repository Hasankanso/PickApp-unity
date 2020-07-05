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
        public override string BuildResponse(JToken response) {
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
