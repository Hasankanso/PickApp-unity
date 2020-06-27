using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Requests;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Requests {

    class RemoveAccount : Request<Person> {
        private Person user;
        public RemoveAccount(Person user) {
            this.user = user;
            HttpPath = "";
            Action = "removeAccount";
        }

        public override async Task<Person> BuildResponse(string response, HttpStatusCode statusCode) //TODO
        {
            return JsonConvert.DeserializeObject<Person>(response);

        }

        public override string ToJson() {
            JObject personJ = new JObject();
            personJ[nameof(user.id)] = user.Id;
            return personJ.ToString();
        }

        protected override string IsValid() {
            return string.Empty;
        }

    }
}

