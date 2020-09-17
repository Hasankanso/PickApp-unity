using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using UnityEngine;
using System.Net;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using System;

namespace Requests {
    class BecomeDriverRequest : Request<Driver> {
        private Driver driver;
        private User user;
        public BecomeDriverRequest(User user,Driver driver) {
            this.user = user;
            this.driver = driver;
            HttpPath = "/DriverBusiness/BecomeDriver";
        }

        public override Driver BuildResponse(JToken response)
        {
            return Driver.ToObject((JObject)response);
        }

        public override string ToJson() {
            JObject driverJ = new JObject();
            JArray regionsArray = new JArray();
            driverJ["car"] = driver.Cars[0].ToJson();
            foreach (Location r in driver.Regions)
            {
                regionsArray.Add(r.ToJson());
            }
            driverJ[nameof(driver.regions)] = regionsArray;
            driverJ[nameof(user)] = user.id;
            return driverJ.ToString();
        }

        protected override string IsValid() {
            DateTime localDate = DateTime.Now;
            string validateUser = User.ValidateLogin(Program.User);
            if (!string.IsNullOrEmpty(validateUser))
            {
                return validateUser;
            }
            if (localDate.Year-user.Person.Birthday.Year   < 18)
            {
                return "You can't be driver since you are under 18 years old";
            }
            if (driver.regions[0] == null)
            {
                return "You should add at least 1 region";
            }
            if (driver.regions.Count==2)
            {
                if (driver.regions[0].Latitude== driver.regions[1].Latitude && driver.regions[0].Longitude== driver.regions[1].Longitude)
                {
                    return "Regions can't be dublicated";
                }
            }
            if (driver.regions.Count==3)
            {
                if (driver.regions[0].Latitude == driver.regions[2].Latitude && driver.regions[0].Longitude == driver.regions[2].Longitude)
                {
                    return "Regions can't be dublicated";
                }
                if (driver.regions[1].Latitude == driver.regions[2].Latitude && driver.regions[1].Longitude == driver.regions[2].Longitude)
                {
                    return "Regions can't be dublicated";
                }
            }
            if (driver.regions.Count > 3)
            {
                return "You can't add more than 3 regions";
            }
            if (driver.Cars[0] != null)
            {
               return  Car.Validate(driver.Cars[0]);
            }


            return String.Empty;
        }
    }
}