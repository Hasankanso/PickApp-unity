using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using UnityEngine;

namespace Requests {
    class Login : Request<User> {
        User user;
        Texture2D image;
        public Login(User user) {
            this.user = user;
            HttpPath = "/UserBusiness/Login";
        }

        public override User BuildResponse(JToken response) //TODO
        {
            JObject json = (JObject)response;
            User u = User.ToObject(json);
            return u;
        }

        public override string ToJson() {
            JObject personJ = new JObject();
            personJ[nameof(user.phone)] = user.Phone;
            personJ[nameof(user.verificationCode)] = user.verificationCode;
            return personJ.ToString();
        }

        protected override string IsValid() {
            if (string.IsNullOrEmpty(user.verificationCode)|| user.verificationCode.Length!=5) {
                return "Please insert code";
            }
            if (!IsPhoneNumber(user.phone)) {
                return "Invalid phone number";
            }
            return string.Empty;
        }
    }
}
