using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Requests {
    public class EditAccount : Request<Person> {
        Person newUser;
        string email;

        public EditAccount(Person newUser, string email) {
            this.newUser = newUser;
            this.email = email;
            HttpPath = "/PersonBusiness/EditPerson";
        }
        public EditAccount(Person newUser) {
            this.newUser = newUser;
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
            JObject personJ = newUser.ToJson();
            personJ["user"] = Program.User.Id;
            personJ["email"] = email;
            return personJ.ToString();
        }

        protected override string IsValid() {
            /*if (string.IsNullOrEmpty(newUser.FirstName) || string.IsNullOrEmpty(newUser.LastName)
              || string.IsNullOrEmpty(newUser.Email) || string.IsNullOrEmpty(newUser.Phone)
              || string.IsNullOrEmpty(newUser.Password)) {
                return "Please fill empty fields.";
            }

            if (!newUser.Email.Contains("@")) {
                return "This email doesn't match.";
            }
            if (!IsPhoneNumber(newUser.Phone)) {
                return "The phone number is wrong, please enter a valid one";
            }

            if (!ValidPassword(newUser.Password)) {
                return "Make sure your password has at least 8 characters and contains at least one number and one letter";
            }*/
            return string.Empty;
        }
    }
}
