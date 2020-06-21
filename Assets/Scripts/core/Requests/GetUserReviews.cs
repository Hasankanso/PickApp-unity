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
    Person person;
    public GetUserReviews(Person person)
    {
      this.person = person;
      HttpPath = "";
      Action = "getUserReviews";
    }

    public override List<Rate> BuildResponse(string response, HttpStatusCode statusCode) //TODO
    {
      return JsonConvert.DeserializeObject<List<Rate>>(response);

    }

    public override string ToJson()
    {
        JObject personJ = new JObject();
        personJ[nameof(person.id)] = person.Id;
        return personJ.ToString();
    }

    protected override string IsValid() //ToDo 
    {

      return string.Empty;
    }
  }
}
