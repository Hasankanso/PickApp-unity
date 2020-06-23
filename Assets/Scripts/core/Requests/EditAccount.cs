using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Requests {
    public class EditAccount : Request<Person> {
        Person newUser;

        public EditAccount(Person newUser) {
            this.newUser = newUser;
            HttpPath = "PersonBusiness/EditPerson";
        }

        public override Person BuildResponse(string response, HttpStatusCode statusCode) //TODO
        {
            return JsonConvert.DeserializeObject<Person>(response);
        }

        public override string ToJson() {
            JObject personJ = newUser.ToJson();
            personJ[nameof(newUser.id)] = newUser.Id;
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
