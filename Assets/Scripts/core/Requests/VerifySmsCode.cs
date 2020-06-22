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
  class VerifySmsCode : Request<Person>
  {
    Person person;
    string token;

    public VerifySmsCode(Person person, string token)
    {
      this.person = person;
      this.token = token;
      HttpPath = "SendSmsVerification/Send";
      Action = "resetPasswodCode";
    }

    public override Person BuildResponse(string response, HttpStatusCode statusCode) //TODO
    {
      return JsonConvert.DeserializeObject<Person>(response);
    }

    public override string ToJson()
    {
      JObject json = new JObject();
      json["token"] = token;
     // json[nameof(person.id)] = person.Id;
      return json.ToString();
    }

    protected override string IsValid()
    {
      return string.Empty;
    }
  }
}
