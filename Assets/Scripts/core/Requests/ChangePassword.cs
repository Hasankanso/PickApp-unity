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
    class ChangePassword : Request<Person> {
        private Person user;
        private string newPassword, currentPassword;
        public ChangePassword(Person user, string currentPassword, string newPassword) {
            this.user = user;
            this.newPassword = newPassword;
            this.currentPassword = currentPassword;
            HttpPath = "";
            Action = "changePassword";
        }
        public override string ToJson() {
            JObject json = new JObject();
            json[nameof(user.id)] = user.Id;
            json["currentPassword"] = currentPassword;
            json["newPassword"] = newPassword;
            return json.ToString();
        }
        public override Person BuildResponse(string response, HttpStatusCode statusCode) {
            return JsonConvert.DeserializeObject<Person>(response);
        }
        protected override string IsValid() {
            /*if (string.IsNullOrEmpty(reason))
                return "Please make sure that you have entered the correct information.";
            */
            //todo
            //validate passwords
            return string.Empty;
        }
    }
}