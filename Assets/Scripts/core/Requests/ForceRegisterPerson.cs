using BackendlessAPI;
using BackendlessAPI.Async;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using UnityEngine;

namespace Requests {
    public class ForceRegisterPerson : Request<User> {
        User newUser;
        string verificationToken;
        public ForceRegisterPerson(User newUser, string verificationToken) {
            this.newUser = newUser;
            this.verificationToken = verificationToken;
            HttpPath = "/UserBusiness/ForceRegister";
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
            Regex r = new Regex("^[a-zA-Z ]+$");
            if (string.IsNullOrEmpty(newUser.Person.FirstName) || !r.IsMatch(newUser.Person.FirstName)) {
                return "Your first name must be alphabet only";
            }
            if (string.IsNullOrEmpty(newUser.Person.LastName) || !r.IsMatch(newUser.Person.LastName)) {
                return "Your last name must be alphabet only";
            }
            if (string.IsNullOrEmpty(newUser.Email) || !ValidEmail(newUser.Email)) {
                return "Invalid Email address";
            }
            if (CalculateAge(newUser.Person.Birthday) < 14) {
                return "You are out of legal age.";
            }
            if (string.IsNullOrEmpty(newUser.Person.CountryInformations.Id)) {
                return "Please choose your country";
            }
            if (string.IsNullOrEmpty(verificationToken)) {
                return "Invalid verification token";
            }
            if (!IsPhoneNumber(newUser.phone)) {
                return "Invalid phone number";
            }
            return string.Empty;
        }
    }
}
