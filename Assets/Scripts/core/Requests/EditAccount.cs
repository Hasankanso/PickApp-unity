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
    public class EditAccount : Request<Person> {
        Person newPerson;
        string email;

        public EditAccount(Person newUser, string email) {
            this.newPerson = newUser;
            this.email = email;
            HttpPath = "/PersonBusiness/EditPerson";
        }
        public EditAccount(Person newUser) {
            this.newPerson = newUser;
            this.email = Program.User.Email;
            HttpPath = "/PersonBusiness/EditPerson";
        }
        public override Person BuildResponse(JToken response) //TODO
        {
            JObject json = (JObject)response;
            Program.User.Email = (string)json["email"];
            Cache.SetEmail(Program.User.Email);
            Person p = Person.ToObject(json);
            return p;
        }

        public override string ToJson() {
            JObject personJ = newPerson.ToJson();
            personJ["user"] = Program.User.Id;
            personJ["email"] = email;
            return personJ.ToString();
        }

        protected override string IsValid() {
            string validateUser = User.ValidateLogin(Program.User);
            if (!string.IsNullOrEmpty(validateUser)) {
                return validateUser;
            }
            Regex r = new Regex("^[a-zA-Z ]+$");
            if (string.IsNullOrEmpty(newPerson.FirstName) || !r.IsMatch(newPerson.FirstName)) {
                return "Your first name must be alphabet only";
            }
            if (string.IsNullOrEmpty(newPerson.LastName) || !r.IsMatch(newPerson.LastName)) {
                return "Your first name must be alphabet only";
            }
            if (string.IsNullOrEmpty(email) || !ValidEmail(email)) {
                return "Invalid Email address";
            }
            if (CalculateAge(newPerson.Birthday)<14) {
                return "You are out of legal age.";
            }
            if (string.IsNullOrEmpty(newPerson.CountryInformations.Id)) {
                return "Please choose your country";
            }
            return string.Empty;
        }
    }
}
