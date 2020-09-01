using Newtonsoft.Json.Linq;
using Requests;
using System;

internal class VerifyAccount : Request<string>
{
  string phoneNumber;
  public VerifyAccount(string phoneNumber)
  {
    this.phoneNumber = phoneNumber;
    HttpPath = "/UserBusiness/RequestCode";
  }

  public override string BuildResponse(JToken response)
  {
    JObject json = (JObject)response;
    string email = json["email"].ToString();
    return email;
  }

  public override string ToJson()
  {
    JObject jPhone = new JObject();
    jPhone[nameof(User.phone)] = phoneNumber;
    return jPhone.ToString();
  }

  protected override string IsValid()
  {
    return string.Empty;
  }
}