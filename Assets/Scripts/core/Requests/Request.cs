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
        public abstract Task<T> BuildResponse(string response, HttpStatusCode statusCode);


        public async void Send(Action<T, HttpStatusCode, string> callback) {
            string valid = IsValid();
            Debug.Log(valid);
            if (!string.IsNullOrEmpty(valid)) {
                Debug.Log("error");
                callback(default, HttpStatusCode.NotAcceptable, valid);
            } else {
                string data = ToJson();
                Debug.Log(data);
                var content = new StringContent(data, Encoding.UTF8, "application/json");
                if (!string.IsNullOrEmpty(Program.UserToken)) {
                    content.Headers.Add("user-token", Program.UserToken);
                }
                var answer=new HttpResponseMessage();
                string result="";
                try {
                     answer = await Client.PostAsync(Ip + HttpPath, content);
                     result = await answer.Content.ReadAsStringAsync();

                } catch (HttpRequestException e) {
                    Debug.Log(e.Message);
                }
                Debug.Log(result);
                /*
                JObject j = JObject.Parse(result);
                    string  codeJ = j["code"].ToString();
                    if (codeJ != null) {
                        Debug.Log(codeJ);
                        string code = codeJ.ToString();
                        callback(default(T), answer.StatusCode, CheckCode(code));
                        return;
                    }*/
                callback(await BuildResponse(result, answer.StatusCode), answer.StatusCode, answer.ReasonPhrase);
            }
        }
        private string CheckCode(string code) {
            int codeInt = -1;
            if (!string.IsNullOrEmpty(code))
                int.TryParse(code, out codeInt);
            if (codeInt == 3003) {
                return "Wrong phone or password";
            }
            return "";
        }
        protected static async Task<Texture2D> DownloadImage(string urlLink) {
            Debug.Log("link"+urlLink);
            Texture2D tex = new Texture2D(1, 1);
            Debug.Log("texture");
            var data = await Client.GetByteArrayAsync(new Uri(urlLink));
            Debug.Log("after await");
            tex.LoadImage(data);
            tex.Apply();
            return tex;
        }

        public static bool IsPhoneNumber(string number) {
            return Regex.Match(number, @"^(\+[0-9]{9})$").Success;
        }

        public static bool ValidPassword(string password) {
            return Regex.Match(password, @"^(?=.*[0-9] +.*)(?=.*[a-zA-Z] +.*)[0-9a-zA-Z]{8,30}
    $").Success;
        }
    }
}
