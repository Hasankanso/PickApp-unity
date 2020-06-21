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
    internal class SendCode : Request<Person> {
        private Person person;

        public SendCode(Person person) {
            this.person = person;
            HttpPath = "";
        }

        public override Person BuildResponse(string response, HttpStatusCode statusCode) {
            throw new System.NotImplementedException();
        }

        public override string ToJson() {
            JObject sendCodeJ = new JObject();
            sendCodeJ[nameof(person.phone)] = new JObject { [nameof(person.phone)] = person.phone };
            return sendCodeJ.ToString();
        }

        protected override string IsValid() {
            throw new System.NotImplementedException();
        }
    }
}