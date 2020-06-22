using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

namespace Requests {
    class Login : Request<Person> {
        Person user;
        Texture2D image;
        public Login(Person user) {
            this.user = user;
            HttpPath = "UserBusiness/Login";
        }

        public override Person BuildResponse(string response, HttpStatusCode statusCode) //TODO
        {
            JObject json = JObject.Parse(response);
            /*if (json.GetValue("driver") != null && !string.IsNullOrEmpty(json.GetValue("driver").ToString()))
               return Driver.ToObject(json);*/
            return Person.ToObject(json);
        }

        public override string ToJson() {
            JObject personJ = new JObject();
            personJ[nameof(user.phone)] = user.Phone;
            personJ[nameof(user.password)] = user.Password;
            return personJ.ToString();
        }

        protected override string IsValid() {
            return string.Empty;
        }
    }
}
