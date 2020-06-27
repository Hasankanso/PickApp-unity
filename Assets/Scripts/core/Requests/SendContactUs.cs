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
        private Person person;
        private string subject, description;

        public SendContactUs(Person user, string subject, string description) {
            this.person = user;
            this.subject = subject;
            this.description = description;
            HttpPath = "";
            Action = "sendContactUs";
        }

        public override async Task<string> BuildResponse(string response, HttpStatusCode statusCode) //TODO
        {
            return JsonConvert.DeserializeObject<string>(response);

        }

        public override string ToJson() {
            JObject sendContactUsJ = new JObject();
            sendContactUsJ[nameof(person)] = new JObject { [nameof(person.Id)] = person.Id };
            sendContactUsJ[nameof(subject)] = this.subject;
            sendContactUsJ[nameof(description)] = this.description;
            return sendContactUsJ.ToString();
        }

        protected override string IsValid() {
            // if (string.IsNullOrEmpty(reason))
            //  return "Please make sure that you have entered the correct information.";
            return string.Empty;

        }
    }
}
