using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using UnityEngine;
using System.Net;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace Requests
{
    class EditRegions : Request<Driver>
    {
        private Driver driver;
        private User user;
        public EditRegions(User user, Driver driver)
        {
            this.user = user;
            this.driver = driver;
            HttpPath = "/DriverBusiness/EditRegions";
        }

        public override Driver BuildResponse(JToken response)
        {
            return Driver.ToObject((JObject)response);
        }

        public override string ToJson()
        {
            JObject driverJ = new JObject();
            JArray regionsArray = new JArray();
            foreach (Location r in driver.Regions)
            {
                regionsArray.Add(r.ToJson());
            }
            driverJ[nameof(driver.regions)] = regionsArray;
            driverJ[nameof(user)] = user.id;
            return driverJ.ToString();
        }

        protected override string IsValid()
        {
            return "";
        }
    }
}