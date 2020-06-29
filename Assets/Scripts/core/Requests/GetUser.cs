using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using UnityEngine;
using System.Net;
using Newtonsoft.Json.Linq;

namespace Requests {
    class GetUser : Request<User> {
        User returningUser;
        public GetUser(User returningUser) {
            this.returningUser = returningUser;
            HttpPath = "/UserBusiness/ValidLoggedIn";
        }
        public override async Task<User> BuildResponse(string response, HttpStatusCode statusCode) //ToDo
        {
            JObject json = JObject.Parse(response);
            User u = User.ToObject(json);
            Texture2D image = await DownloadImage(u.Person.ProfilePictureUrl);
            u.Person.ProfilePicture = image;
            return u;
        }

        public override string ToJson() {
            JObject personJ = new JObject();
            personJ[nameof(returningUser.id)] = returningUser.id;
            return personJ.ToString();
        }

        protected override string IsValid() {
            return string.Empty;
        }
    }
}