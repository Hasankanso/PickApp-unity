using BackendlessAPI;
using BackendlessAPI.Async;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Requests {
    public class RegisterPerson : Request<User> {
        User newUser;
        string verificationToken;
        public RegisterPerson(User newUser, string verificationToken) {
            this.newUser = newUser;
            this.verificationToken = verificationToken;
            HttpPath = "/UserBusiness/Register";
        }

        public override User BuildResponse(JToken response) {
            JObject user = (JObject)response;
            User u = User.ToObject(user);
            return u;
        }

        public override string ToJson() {
            JObject data = new JObject();
            JObject personJ = newUser.ToJson();

            if (!string.IsNullOrEmpty(newUser.Person.image)) {
                personJ[nameof(newUser.Person.image)] = newUser.Person.Image;
            }

            data["person"] = personJ;
            data["idToken"] = verificationToken;

            return data.ToString();
        }

        protected override string IsValid() {
            /* if (string.IsNullOrEmpty(newUser.FirstName) || string.IsNullOrEmpty(newUser.LastName)
               || string.IsNullOrEmpty(newUser.Email) || string.IsNullOrEmpty(newUser.Phone)
               || string.IsNullOrEmpty(newUser.Password)) {
                 return "Please fill empty fields.";
             }
             if (!newUser.Email.Contains("@")) {
                 return "Your email is Invalid, please correct it";
             }
             if (!IsPhoneNumber(newUser.Phone)) {
                 return "The phone number is wrong, please enter valid one";
             }
             if (!ValidPassword(newUser.Password)) {
                 return "make sure your password has at least 8 characters and contains at least one number and one letter";
             }*/
            return string.Empty;
        }
    }
}
