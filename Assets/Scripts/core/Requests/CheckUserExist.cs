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
    public class CheckUserExist : Request<bool> {
        private User user;

        public CheckUserExist(User user) {
            this.user = user;
            HttpPath = "/UserBusiness/CheckUserExist";
        }
        public override bool BuildResponse(JToken response) {
            JObject json = (JObject)response;
            bool exist = true;
            var ex = json["exist"];
            if (ex != null)
                exist = bool.Parse(ex.ToString());
            return exist;
        }

        public override string ToJson() {
            JObject data = new JObject();
            data[nameof(user.phone)] = user.phone;
            return data.ToString();
        }

        protected override string IsValid() {
            return string.Empty;
        }
    }
}