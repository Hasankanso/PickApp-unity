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
    class BroadCastAlert : Request<string> {
        private Alert alert;

        public BroadCastAlert(Alert alert) {
            this.alert = alert;
            HttpPath = "/AlertBusiness/AddAlert";
        }

        public override string BuildResponse(JToken response) //TODO
        {
            return (((JObject)response)["message"]).ToString();
        }

        public override string ToJson() {
            return alert.ToJson().ToString();
        }

        protected override string IsValid() {
            //need detailed checking
            if (alert.MinDate < DateTime.Now) {
                return "Minimum date range can't be empty!";
            }
            if (alert.MinDate > Program.MaxAlertDate) {
                return "The max period of alert is six months";
            }

            if (alert.MaxDate < DateTime.Now) {
                return "Invalid maximum date range";
            }
            if (alert.MaxDate > Program.MaxAlertDate) {
                return "The max period of alert is six months";
            }
            if (alert.MaxDate < alert.MinDate) {
                return "The maximum date range couldn't be less tham the minimum.";
            }
            if (alert.Price == 0) {
                return "Set your alert price";
            }
            if (string.IsNullOrEmpty(alert.Comment) || alert.Comment.Length < 25 || alert.Comment.Length > 400) {
                return "Please add a comment between 25 and 400 characters";
            }
            return string.Empty;
        }
    }
}



