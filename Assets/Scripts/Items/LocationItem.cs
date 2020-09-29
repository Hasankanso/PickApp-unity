using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class LocationItem : MonoBehaviour
{
  public LocationsFinderPanel locPanel;
  public Location location;
  public Text itemText;
  private static readonly string directionsURL = "https://maps.googleapis.com/maps/api/place/details/json?";

  internal void Init(LocationsFinderPanel locationResultsPanel, string placeId, string name)
  {
    if (Program.language.Arabic==true) {
        this.itemText.alignment = TextAnchor.MiddleRight;
        this.itemText.fontSize = 70;
    }
    location = new Location(placeId, name);
    itemText.text = name;
    locPanel = locationResultsPanel;
  }

  private void Start()
  {
    itemText.color = Program.mainTextColor;
    itemText.fontSize = Program.fontSize;
  }


  private IEnumerator LoadLocationDetails(Action<double, double> OnLoaded)
  {
    var loaded = new UnityWebRequest(directionsURL + "place_id=" + location.PlaceId + "&fields=geometry&key=" + Program.googleKey);
    loaded.downloadHandler = new DownloadHandlerBuffer();
    yield return loaded.SendWebRequest();

    var locJ = JsonConvert.DeserializeObject<JObject>(loaded.downloadHandler.text).Value<JObject>("result").Value<JObject>("geometry").Value<JObject>("location");

    var lat = locJ.Value<string>("lat").ToString();
    var lng = locJ.Value<string>("lng").ToString();

    OnLoaded(Double.Parse(lng, CultureInfo.InvariantCulture), Double.Parse(lat, CultureInfo.InvariantCulture));

  }

  public void Click()
  {
    StartCoroutine(LoadLocationDetails(AfterLoad));
  }

  private void AfterLoad(double longitude, double latitude){
    Location fullLoc = new Location(location.PlaceId, location.Name, latitude, longitude);
    locPanel.itemClicked(fullLoc);
    locPanel.CloseDialog();
  }

}
