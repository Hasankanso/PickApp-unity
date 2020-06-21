using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class LocationsFinderPanel : Panel
{
  public InputField searchField;
  public ListView listView;
  public LocationItem locItem;

  public string radius = "2000";
  public Double longtitude = 33.8547;
  public Double latitude = 35.8623;

  private readonly string directionsURL = "https://maps.googleapis.com/maps/api/place/autocomplete/json?";

  public Action<Location> itemClicked;


  internal override void Clear()
  {
    searchField.text = "";
  }

  public void RequestSuggestions()
  {
    StartCoroutine(LoadLocations());
  }

  internal void Init(string initText, Action<Location> OnLocationPicked)
  {
    itemClicked = OnLocationPicked;
    searchField.text = initText;
    searchField.Select();
  }

  private IEnumerator LoadLocations()
  {
    if (searchField.text != "")
    {
      var loaded = new UnityWebRequest(directionsURL + "input=" + searchField.text + "&location=" + longtitude + "," + latitude + "&radius=" + radius + "&key=" + Program.googleKey);
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