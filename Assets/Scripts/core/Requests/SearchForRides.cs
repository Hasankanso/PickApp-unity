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
  class SearchForRides : Request<List<Ride>>
  {
    private SearchInfo searchInfo;

    public SearchForRides(SearchInfo searchInfo)
    {
      this.searchInfo = searchInfo;
      HttpPath = "/SearchBusiness/SearchRides";
      Action = "searchForRides";
    }

    public override List<Ride> BuildResponse(JToken response) //TODO
    {

      var ridesArray = (JArray)response;
      List<Ride> rides = new List<Ride>();
      if (ridesArray == null) return null;

      foreach (JToken j in ridesArray)
      {
        JObject rideJ = (JObject)j;
        rides.Add(Ride.ToObject(rideJ));
      }

      return rides;
    }

    public override string ToJson()
    {
      JObject searchInfoJ = searchInfo.ToJson();

      if (Program.User != null)
      {
        searchInfoJ[nameof(Program.User.id)] = Program.User.Id;
      }

      return searchInfoJ.ToString();
    }

    protected override string IsValid()
    {
      if (searchInfo.From == null || (searchInfo.To == null || searchInfo.MinDate == null)
          || searchInfo.MaxDate == null || searchInfo.PassengersNumber <= 0)

        return "The entered information are not correct!";

      return string.Empty;
    }

  }
}
