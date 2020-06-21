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
    class VerifySmsCode : Request<Person> {
        Person person;
        int code;

        public VerifySmsCode(Person person, string code) {
            this.person = person;
            this.code = int.Parse(code);
            HttpPath = "";
            Action = "resetPasswodCode";
        }

        public override Person BuildResponse(string response, HttpStatusCode statusCode) //TODO
        {
            return JsonConvert.DeserializeObject<Person>(response);
        }

        public override string ToJson() {
            JObject json = new JObject();
            json[nameof(person.id)] = person.Id;
            json["code"] = code;
            return json.ToString();
        }

        protected override string IsValid() {
            return string.Empty;
        }
    }
}
