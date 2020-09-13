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
  class AddRide : Request<Ride>
  {
    private Ride ride;

    public AddRide(Ride ride)
    {
      this.ride = ride;
      HttpPath = "/RideBusiness/AddRide";
    }

    public override Ride BuildResponse(JToken response) //TODO
    {
      return Ride.ToObject((JObject)response);
    }

    public override string ToJson()
    {
      JObject rideJ = ride.ToJson();
      return rideJ.ToString();
    }

    protected override string IsValid()
    {
            return Ride.Valid(ride);
    }
  }
}



