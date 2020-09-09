using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using UnityEngine;
using System.Net;
using Newtonsoft.Json.Linq;

namespace Requests {
    class GetLoggedInUser : Request<Person> {
        User returningUser;
        public GetLoggedInUser(User returningUser) {
            this.returningUser = returningUser;
            HttpPath = "/UserBusiness/GetLoggedInUser";
        }
        public override Person BuildResponse(JToken response) //ToDo
        {
            JObject json = (JObject)response;
            Person u = Person.ToObject((JObject)json["person"]);
            string userStatus = json["userStatus"].ToString();
            Program.User.UserStatus = userStatus;
            Driver driver = null;
            JObject driverJ = (JObject)json["driver"];
            if (driverJ != null) {
                driver = Driver.ToObject(driverJ);
                Program.User.Driver = driver;
            }
            return u;
        }

        public override string ToJson() {
            JObject personJ = new JObject();
            personJ[nameof(returningUser.id)] = returningUser.id;
            return personJ.ToString();
        }

        protected override string IsValid() {
            string validateUser = User.ValidateLogin(returningUser);
            if (!string.IsNullOrEmpty(validateUser)) {
                return validateUser;
            }
            return string.Empty;
        }
    }
}