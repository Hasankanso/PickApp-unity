using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using UnityEngine;

namespace Requests {

    public abstract class Request<T> {

        private readonly static string ip = "https://api.backendless.com/" + BackendlessPlugin.Instance.applicationId + "/" + BackendlessPlugin.Instance.APIKey + "/services";
        private static readonly HttpClient client = new HttpClient();
        private string httpPath;
        private string action;
        private static string Ip => ip;
        private static HttpClient Client => client;
        protected string Action { get => action; set => action = value; }
        protected string HttpPath { get => httpPath; set => httpPath = value; }
        protected abstract string IsValid();
        public abstract string ToJson();

        /*<summary>
         * return null if there's no response
         * </summary>
         * <param name = "response" >json response</param>
         * <param name = "statusCode" >check https://docs.microsoft.com/en-us/dotnet/api/system.net.httpstatuscode?view=netframework-4.8</param>
         */
        public abstract T BuildResponse(JToken response);


        public async void Send(Action<T, int, string> callback) {
            string valid = IsValid();
            Debug.Log(valid);
            if (!string.IsNullOrEmpty(valid)) {
                Debug.Log("error");
                callback(default, (int)HttpStatusCode.NotAcceptable, valid);
            } else {
                string data = ToJson();
                Debug.Log(data);
                var content = new StringContent(data, Encoding.UTF8, "application/json");
                if (!string.IsNullOrEmpty(Program.UserToken)) {
                    Debug.Log("setting token"+ Program.UserToken);
                    content.Headers.Add("user-token", Program.UserToken);
                }
                string result;
                HttpResponseMessage answer;
                try {
                    Debug.Log("before");
                    answer = await Client.PostAsync(Ip + HttpPath, content);
                    result = await answer.Content.ReadAsStringAsync();
                    Debug.Log("after");
                } catch (Exception e) {
                    Debug.Log(e.Message);
                    answer = new HttpResponseMessage(HttpStatusCode.Found);
                    JObject js = new JObject();
                    js["code"] = "302";
                    js["message"] = "please login";
                    result = js.ToString();
                }
                Debug.Log(result);

                JToken json = JToken.Parse(result);

                if (json.Type == JTokenType.Array) {
                    callback(BuildResponse(json), (int)answer.StatusCode, answer.ReasonPhrase);
                    return;
                }

                //extracting code and message
                var jCode = json["code"];
                var jMessage = json["message"];

                if (jCode == null) {
                    var jbody = json["body"];

                    if (jbody != null) {
                        jCode = jbody["code"];
                        jMessage = jbody["message"];
                    }
                }

                //check if there's error
                if (jCode != null) {
                    string code = jCode.ToString();
                    Debug.Log(code);
                    string message = jMessage.ToString();
                    callback(default(T), int.Parse(code), message);
                    return;
                } else {
                    callback(BuildResponse(json), (int)answer.StatusCode, answer.ReasonPhrase);
                }
            }
        }


        /*protected static async Task<Texture2D> DownloadImage(string urlLink) {
            Debug.Log("link" + urlLink);
            Texture2D tex = new Texture2D(1, 1);
            Debug.Log("texture");
            var data = await Client.GetByteArrayAsync(new Uri(urlLink));
            Debug.Log("after await");
            tex.LoadImage(data);
            tex.Apply();
            return tex;
        }*/

        public static bool IsPhoneNumber(string number) {
            return Regex.Match(number, @"^(\+[0-9]{9})$").Success;
        }

        public static bool ValidPassword(string password) {
            return Regex.Match(password, @"^(?=.*[0-9] +.*)(?=.*[a-zA-Z] +.*)[0-9a-zA-Z]{8,30}
    $").Success;
        }
    }
}
