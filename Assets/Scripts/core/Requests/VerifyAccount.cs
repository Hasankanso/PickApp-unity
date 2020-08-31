using Newtonsoft.Json.Linq;
using Requests;

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
    JObject json = new JObject();
    json[nameof(User.phone)] = phoneNumber;
    return json.ToString();
  }

  protected override string IsValid()
  {
    return string.Empty;
  }
}