using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Requests {
    public class ReportUser : Request<Person> {
        private string reason, comment;
        private Person person;
        public ReportUser(Person person, string reason, string comment) {
            this.person = person;
            this.reason = reason;
            this.comment = comment;
            HttpPath = "";
            Action = "ReportUser";
        }

        public override async Task<Person> BuildResponse(JToken response, int statusCode) {
            throw new System.NotImplementedException();
        }

        public override string ToJson() {
            JObject reportJ = new JObject();
            reportJ[nameof(person)] = new JObject { [nameof(person.id)] = person.Id };
            reportJ[nameof(reason)] = this.reason;
            reportJ[nameof(comment)] = this.comment;
            return reportJ.ToString();
        }

        protected override string IsValid() {
            throw new System.NotImplementedException();
        }
    }
}