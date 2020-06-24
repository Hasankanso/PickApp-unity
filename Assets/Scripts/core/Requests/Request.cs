using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;

namespace Requests
{

  public abstract class Request<T>
  {

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
    public abstract T BuildResponse(string response, HttpStatusCode statusCode);

    public async void Send(Action<T, HttpStatusCode, string> callback)
    {
      string valid = IsValid();
      Debug.Log(valid);
      if (!string.IsNullOrEmpty(valid))
      {
        Debug.Log("error");
        callback(default, HttpStatusCode.NotAcceptable, valid);
      }
      else
      {
        string data = ToJson();
        Debug.Log(data);

        if (!String.IsNullOrEmpty(Program.UserToken))
        {
          Client.DefaultRequestHeaders.Add("user-token", Program.UserToken);
        }

        var content = new StringContent(data, Encoding.UTF8, "application/json");
        var answer = await Client.PostAsync(Ip + HttpPath, content);
        string result = await answer.Content.ReadAsStringAsync();
        Debug.Log(result);

        callback(BuildResponse(result, answer.StatusCode), answer.StatusCode, answer.ReasonPhrase);
      }
    }

    public static List<KeyValuePair<string, string>> ToList(object obj, List<KeyValuePair<string, string>> list, string parentKey)
    {
      if (list == null) list = new List<KeyValuePair<string, string>>();
      if (parentKey != null) parentKey = parentKey + ".";

      foreach (var property in obj.GetType().GetProperties())
      {
        object variable = property.GetValue(obj);
        if (variable == null) continue;
        list.Add(new KeyValuePair<string, string>(parentKey + property.Name, variable.ToString()));
      }
      return list;
    }

    public static bool IsPhoneNumber(string number)
    {
      return Regex.Match(number, @"^(\+[0-9]{9})$").Success;
    }

    public static bool ValidPassword(string password)
    {
      return Regex.Match(password, @"^(?=.*[0-9] +.*)(?=.*[a-zA-Z] +.*)[0-9a-zA-Z]{8,30}
    $").Success;
    }
  }
}
