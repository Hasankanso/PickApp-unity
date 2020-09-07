using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Requests
{
  public class GetMyAccountInfo : Request<Person>
  {

    public GetMyAccountInfo()
    {
      HttpPath = "";
    }

    public override Person BuildResponse(JToken response) //TODO
    {
      return null;
      //return JsonConvert.DeserializeObject<Person>(response);
    }

    public override string ToJson()
    {
      return "";
    }

    protected override string IsValid()
    {
      return string.Empty;
    }

  }
}
