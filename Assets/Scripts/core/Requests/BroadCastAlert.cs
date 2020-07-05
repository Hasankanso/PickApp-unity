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
  class BroadCastAlert : Request<Alert>
  {
    private Alert alert;

    public BroadCastAlert(Alert alert)
    {
      this.alert = alert;
      HttpPath = "";
      Action = "broadCastAlert";
    }

    public override Alert BuildResponse(JToken response) //TODO
    {
      return null;
      //return JsonConvert.DeserializeObject<Alert>(response);
    }

    public override string ToJson()
    {
      return alert.ToJson().ToString();
    }

    protected override string IsValid()
    {
      return string.Empty;

    }
  }
}



