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
  class GetUserReviews : Request<List<Rate>>
  {
    User user;
    public GetUserReviews(User user)
    {
      this.user = user;
      HttpPath = "";
      Action = "getUserReviews";
    }

    public override async Task<List<Rate>> BuildResponse(JToken response, int statusCode) //TODO
    {
      //return JsonConvert.DeserializeObject<List<Rate>>(response);
      return null;
    }

    public override string ToJson()
    {
        JObject personJ = new JObject();
        personJ[nameof(user.id)] = user.Id;
        return personJ.ToString();
    }

    protected override string IsValid() //ToDo 
    {

      return string.Empty;
    }
  }
}
