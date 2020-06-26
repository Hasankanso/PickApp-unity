using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

namespace Requests
{
  class Login : Request<User>
  {
    User user;
    Texture2D image;
    public Login(User user)
    {
      this.user = user;
      HttpPath = "/UserBusiness/Login";
    }

    public override User BuildResponse(string response, HttpStatusCode statusCode) //TODO
    {
      JObject json = JObject.Parse(response);

      return User.ToObject(json);
    }

    public override string ToJson()
    {
      JObject personJ = new JObject();
      personJ[nameof(user.phone)] = user.Phone;
      personJ[nameof(user.password)] = user.Password;
      return personJ.ToString();
    }

    protected override string IsValid()
    {
      return string.Empty;
    }
  }
}
