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
  class GetCar : Request<Car>
  {
    private Car car;

    public GetCar(Car car)
    {
      this.car = car;
      HttpPath = "";
    }

    public override Car BuildResponse(JToken response) //TODO
    {
      return null;
      // return JsonConvert.DeserializeObject<Car>(response);

    }

    public override string ToJson()
    {
      JObject carJ = new JObject();
      carJ[nameof(car.id)] = car.Id;
      return carJ.ToString();
    }

    protected override string IsValid()
    {
      return string.Empty;

    }
  }
}



