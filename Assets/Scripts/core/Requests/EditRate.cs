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

namespace Requests
{
  class EditRate : Request<Rate>
  {
    private Rate rate;

    public EditRate(Rate rate)
    {
      this.rate = rate;
      HttpPath = "";
    }

    public override Rate BuildResponse(JToken response) //TODO
    {
      return null;
      //  return JsonConvert.DeserializeObject<Rate>(response);

    }

    public override string ToJson()
    {
      JObject rateJ = rate.ToJson();
      return rateJ.ToString();
    }

    protected override string IsValid() {
            if (rate.Grade < 0 || rate.Grade > 5) {
                return "Invalid rate";
            }
            if (rate.Grade < 3 && string.IsNullOrEmpty(rate.Comment)) {
                return "Please comment the reason of low rate";
            }
            if (DateTime.Compare(rate.Date,DateTime.Now.AddDays(-1))<0) {
                return "You can't edit rate after one day of its publishing";
            }
            if (string.IsNullOrEmpty(rate.Reviewer.id)) {
                return "Invalid reviewer object id";
            }
            if (string.IsNullOrEmpty(rate.Target.id)) {
                return "Invalid target object id";
            }
            if (string.IsNullOrEmpty(rate.Ride.id)) {
                return "Invalid ride object id";
            }
            return string.Empty;
        }

    }
}



