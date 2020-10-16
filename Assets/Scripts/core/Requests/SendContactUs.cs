using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using UnityEngine;
using System.Net;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace Requests {
    class SendContactUs : Request<string> {
        private string subject, message;

        public SendContactUs(string subject, string description) {
            this.subject = subject;
            this.message = description;
            HttpPath = "/UserBusiness/ContactUs";
        }

        public override string BuildResponse(JToken response) {
            JObject json = (JObject)response;
            return json["received"].ToString();
        }

        public override string ToJson() {
            JObject sendContactUsJ = new JObject();
            sendContactUsJ["user"] = Program.User.Id;
            sendContactUsJ[nameof(subject)] = this.subject;
            sendContactUsJ[nameof(message)] = this.message;
            return sendContactUsJ.ToString();
        }

        protected override string IsValid() {
            if (string.IsNullOrEmpty(subject)) {
                return "Subject cannot be empty";
            }
            if (subject.Length<10) {
                return "Subject is too short.";
            }
            if (string.IsNullOrEmpty(message)) {
                return "Message cannot be empty";
            }
            if (message.Length<70) {
                return "Message is too short.";
            }
            return string.Empty;
        }
    }
}
