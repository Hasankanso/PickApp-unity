using ArabicSupport;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class LocationsFinderPanel : Panel {
    public InputField searchField;
    public ListView listView;
    public LocationItem locItem;

    private string token;

    private readonly string directionsURL = "https://maps.googleapis.com/maps/api/place/autocomplete/json?";

    public Action<Location> itemClicked;

    public static string GenerateToken() {
        Guid token = Guid.NewGuid();
        return token.ToString();
    }
    internal override void Clear() {
        searchField.text = "";
    }

    public void RequestSuggestions() {
        StartCoroutine(LoadLocations());
    }

    internal void Init(string initText, Action<Location> OnLocationPicked) {
        itemClicked = OnLocationPicked;
        searchField.text = initText;
        token = GenerateToken();
        searchField.Select();
        searchField.ActivateInputField();
    }

    private IEnumerator LoadLocations() {
        var Ar = "&types=geocode&language=ar";
        var En = "&types=geocode&language=ar";


        if (Program.language.Arabic==true)
        {
            if (searchField.text != "")
            {
                var loaded = new UnityWebRequest(directionsURL + "input=" + searchField.text + Ar + "&components=" + Program.CountryComponent + "&key=" + Program.googleKey + "&sessiontoken=" + token);
                loaded.downloadHandler = new DownloadHandlerBuffer();
                yield return loaded.SendWebRequest();

                var list = JsonConvert.DeserializeObject<JObject>(loaded.downloadHandler.text).Value<JArray>("predictions");
                if (list != null)
                {
                    var results = list.Values<JObject>();
                    listView.Clear();
                    foreach (JObject o in results)
                    {
                        var description = ArabicFixer.Fix(o.Value<string>("description").ToString(), true, true);
                        var id = o.Value<string>("place_id").ToString();

                        LocationItem obj = Instantiate(locItem);
                        obj.Init(this, id, description, token);

                        listView.Add(obj.gameObject);
                    }

                }
            }
        }
        else 
        {
            if (searchField.text != "")
            {
                var loaded = new UnityWebRequest(directionsURL + "input=" + searchField.text + En + "&components=" + Program.CountryComponent + "&key=" + Program.googleKey + "&sessiontoken=" + token);
                loaded.downloadHandler = new DownloadHandlerBuffer();
                yield return loaded.SendWebRequest();

                var list = JsonConvert.DeserializeObject<JObject>(loaded.downloadHandler.text).Value<JArray>("predictions");
                if (list != null)
                {
                    var results = list.Values<JObject>();
                    listView.Clear();
                    foreach (JObject o in results)
                    {
                        var description = o.Value<string>("description").ToString();
                        var id = o.Value<string>("place_id").ToString();

                        LocationItem obj = Instantiate(locItem);
                        obj.Init(this, id, description);

                        listView.Add(obj.gameObject);
                    }

                }
            }
        }
    }


}