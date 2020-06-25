using BackendlessAPI;
using BackendlessAPI.Async;
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
  public class RegisterPerson : Request<Person>
  {
    Person newUser;
    string verificationToken;
    public RegisterPerson(Person newUser, string verificationToken)
    {
      this.newUser = newUser;
      this.verificationToken = verificationToken;
      HttpPath = "/UserBusiness/Register";
    }

    public override Person BuildResponse(string response, HttpStatusCode statusCode) //TODO
    {
      return JsonConvert.DeserializeObject<Person>(response);
    }

    public override string ToJson()
    {
      JObject data = new JObject();
      JObject personJ = newUser.ToJson();

      if (!string.IsNullOrEmpty(newUser.image))
      {
        personJ[nameof(newUser.image)] = newUser.Image;
      }

      data["person"] = personJ;
      data["idToken"] = verificationToken;
      return data.ToString();
    }

    protected override string IsValid()
    {
      /* if (string.IsNullOrEmpty(newUser.FirstName) || string.IsNullOrEmpty(newUser.LastName)
         || string.IsNullOrEmpty(newUser.Email) || string.IsNullOrEmpty(newUser.Phone)
         || string.IsNullOrEmpty(newUser.Password)) {
           return "Please fill empty fields.";
       }
       if (!newUser.Email.Contains("@")) {
           return "Your email is Invalid, please correct it";
       }
       if (!IsPhoneNumber(newUser.Phone)) {
           return "The phone number is wrong, please enter valid one";
       }
       if (!ValidPassword(newUser.Password)) {
           return "make sure your password has at least 8 characters and contains at least one number and one letter";
       }*/
      return string.Empty;
    }
  }
}
