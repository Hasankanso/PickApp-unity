using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

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
        private string result;
        private HttpResponseMessage answer;
        public UnityEvent sendRequest = new UnityEvent();
        public UnityEvent receiveResponse = new UnityEvent();
        public void AddSendListener(UnityAction unityAction) {
            sendRequest.AddListener(unityAction);
        }
        public void AddReceiveListener(UnityAction unityAction) {
            receiveResponse.AddListener(unityAction);
        }
        /*<summary>
            * return null if there's no response
            * </summary>
            * <param name = "response" >json response</param>
            * <param name = "statusCode" >check https://docs.microsoft.com/en-us/dotnet/api/system.net.httpstatuscode?view=netframework-4.8</param>
            */
        public abstract T BuildResponse(JToken response);
        private bool CheckConnection() {
            try {
                var client = new WebClient();
                client.OpenRead("http://google.com/generate_204");
                return true;
            } catch {
                return false;
            }
        }
        public async void Send(Action<T, int, string> callback) {
            sendRequest.Invoke();
            if (!CheckConnection()) {
                Debug.Log(123321);
                string code = "503";
                string message = "Please connect to the internet";
                callback(default, int.Parse(code), message);
                return;
            }
            string valid = IsValid();
            Debug.Log(valid);
            if (!string.IsNullOrEmpty(valid)) {
                Debug.Log("error");
                callback(default, (int)HttpStatusCode.NotAcceptable, valid);
            } else {
                string data = ToJson();
                Debug.Log(data);
                var content = new StringContent(data, Encoding.UTF8, "application/json");
                try {
                    answer = await Client.PostAsync(Ip + HttpPath, content);
                    result = await answer.Content.ReadAsStringAsync();
                } catch (InvalidOperationException e) {
                    //The request message was already sent by the HttpClient instance.
                    BuildCatchError(HttpStatusCode.Found, e);
                } catch (ArgumentNullException e) {
                    //The request was null
                    BuildCatchError(HttpStatusCode.Found, e);
                } catch (TaskCanceledException e) {
                    //The request timed-out or the user canceled the request's Task
                    BuildCatchError(HttpStatusCode.Found, e);
                }
                Debug.Log(result);
                JToken json = JToken.Parse(result);

                if (json.Type == JTokenType.Array) {
                    callback(BuildResponse(json), (int)answer.StatusCode, answer.ReasonPhrase);
                    receiveResponse.Invoke();
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
                    string message = jMessage.ToString();
                    callback(default, int.Parse(code), message);
                    receiveResponse.Invoke();
                    return;
                } else {
                    callback(BuildResponse(json), (int)answer.StatusCode, answer.ReasonPhrase);
                    receiveResponse.Invoke();
                }
            }
        }

        void BuildCatchError(HttpStatusCode statusCode, Exception exception) {
            answer = new HttpResponseMessage(statusCode);
            var message = exception.InnerException;
            JObject js = new JObject();
            js["code"] = "302";
            js["message"] = message.ToString();
            result = js.ToString();
        }

        public static async Task<Texture2D> DownloadImage(string urlLink) {
            Texture2D tex = new Texture2D(1, 1);
            var data = await Client.GetByteArrayAsync(new Uri(urlLink));
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
