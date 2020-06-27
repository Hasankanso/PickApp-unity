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
        private User user;
        private string newPassword;

        public ForgetPassword(User user, string newPassword) {
            this.user = user;
            this.newPassword = newPassword;
            HttpPath = "";
            Action = "newPassword";
        }

        public override async Task<Person> BuildResponse(string response, HttpStatusCode statusCode) {
            throw new System.NotImplementedException();
        }

        public override string ToJson() {
            JObject json = new JObject();
            json[nameof(user.id)] = user.Id;
            json["forgetPassword"] = newPassword;
            return json.ToString();
        }

        protected override string IsValid() {
            throw new System.NotImplementedException();
        }
    }
}