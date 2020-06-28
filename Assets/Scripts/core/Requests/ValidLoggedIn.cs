using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using UnityEngine;
using System.Net;
using System.Web;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace Requests {
    class ValidLoggedIn : Request<string> {
        public ValidLoggedIn() {
            HttpPath = "/UserBusiness/ValidLoggedIn";
        }
        public override async Task<string> BuildResponse(string response, HttpStatusCode statusCode) //ToDo
        {
            return response;
        }

        public override string ToJson() {
            return "";
        }

        protected override string IsValid() {
            return string.Empty;
        }
    }
}