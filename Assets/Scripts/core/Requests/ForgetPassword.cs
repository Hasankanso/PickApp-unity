using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Net;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace Requests {
    internal class ForgetPassword : Request<Person> {
        private Person person;
        private string newPassword;

        public ForgetPassword(Person person, string newPassword) {
            this.person = person;
            this.newPassword = newPassword;
            HttpPath = "";
            Action = "newPassword";
        }

        public override Person BuildResponse(string response, HttpStatusCode statusCode) {
            throw new System.NotImplementedException();
        }

        public override string ToJson() {
            JObject json = new JObject();
            json[nameof(person.id)] = person.Id;
            json["forgetPassword"] = newPassword;
            return json.ToString();
        }

        protected override string IsValid() {
            throw new System.NotImplementedException();
        }
    }
}