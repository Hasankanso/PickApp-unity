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
  class SendContactUs : Request<string>
  {
    private Person person;
    private string subject, body;

    public SendContactUs(Person user, string subject, string description)
    {
      this.person = user;
      this.subject = subject;
      this.body = description;
      HttpPath = "/UserBusiness/ContactUs";
    }

    public override string BuildResponse(JToken response) //TODO
    {
      return null;
      //return JsonConvert.DeserializeObject<string>(response);

    }

    public override string ToJson()
    {
      JObject sendContactUsJ = new JObject();
      sendContactUsJ[nameof(person)] = person.Id;
      sendContactUsJ[nameof(subject)] = this.subject;
      sendContactUsJ[nameof(body)] = this.body;
      return sendContactUsJ.ToString();
    }

    protected override string IsValid()
    {
      // if (string.IsNullOrEmpty(reason))
      //  return "Please make sure that you have entered the correct information.";
      return string.Empty;

    }
  }
}
