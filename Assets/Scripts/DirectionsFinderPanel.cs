using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class DirectionsFinderPanel : Panel
{
  public int mapResolutionX = 640;
  public int mapResolutionY = 640;

  public Image map;
  public ListView roadsListView;

  public Button doneButton;

  private readonly string directionsURL = "https://maps.googleapis.com/maps/api/directions/json?";
  private readonly string staticMapURL = "https://maps.googleapis.com/maps/api/staticmap?";

  private string origin, destination;
  private bool alternatives = true;
  private Action<Texture2D> OnItemPicked;


  public void Init(Texture2D map, string origin, string destination, bool alternatives, Action<Texture2D> OnItemPicked)
  {
    Clear();
    this.origin = origin;
    this.destination = destination;
    this.alternatives = alternatives;
    this.OnItemPicked = OnItemPicked;

    if (map != null)
    {
      DisplayMap(map);
      doneButton.enabled = true;
    }

    if (roadsListView.IsEmpty())
    {
      RequestDirections();
    }
  }

  public void OpenImageViewer()
  {
    if (map.mainTexture != null)
    {
      Panel viewer = PanelsFactory.CreateImageViewer(map.mainTexture);
      OpenDialog(viewer);
    }
  }
  private void RequestDirections()
  {
    StartCoroutine(RequestRoads());
  }

  internal override void Clear()
  {
    //roadsListView.Clear(); we don't want to download roads everytime.
    doneButton.enabled = false;
    map.sprite = null;
    origin = "";
    destination = "";
  }

  public IEnumerator RequestRoads()
  {
    print("drid");
    var uwr = new UnityWebRequest(directionsURL + "origin=" + origin + "&destination=" + destination + "&mode=driving&alternatives=" + alternatives.ToString().ToLower() + "&key=" + Program.googleKey);
    uwr.downloadHandler = new DownloadHandlerBuffer();
    print("gfdg");
    yield return uwr.SendWebRequest();

    if (uwr.isNetworkError || uwr.isHttpError)
    {
      print("adsa");
      Panel dialog = PanelsFactory.CreateDialogBox(uwr.error, false);
      OpenDialog(dialog);
    }
    else
    {
      print("gdfgf");
      Debug.Log(uwr.downloadHandler.text);
      var data = JsonConvert.DeserializeObject<JObject>(uwr.downloadHandler.text);
      var summary = data.Value<JArray>("routes");
      roadsListView.Clear();
      foreach (JObject road in summary)
      {
        var roadPoints = road.Value<JObject>("overview_polyline").Value<string>("points");
        var details = road.Value<string>("summary");
        var roadItem = ItemsFactory.CreateRoadItem(roadsListView.gameObject, this, details, roadPoints);
        roadsListView.Add(roadItem.gameObject);
      }
    }
  }

  public IEnumerator RequestMap(string roadPoints, RoadItem itemToCache)
  {

    string url = staticMapURL + "size=" + mapResolutionX + "x" + mapResolutionY + "&path=enc%3A" + roadPoints + "&key=" + Program.googleKey;
    var uwr = new UnityWebRequest(url);
    DownloadHandlerTexture texDl = new DownloadHandlerTexture(true);
    uwr.downloadHandler = texDl;
    yield return uwr.SendWebRequest();

    if (uwr.isNetworkError || uwr.isHttpError)
    {
      Panel dialog = PanelsFactory.CreateDialogBox(uwr.error, false);
      OpenDialog(dialog);
    }
    else
    {
      var texture = texDl.texture;
      itemToCache.MapImage = texture;
      DisplayMap(texture);
    }
  }

  public void Done()
  {
    OnItemPicked(map.sprite.texture);
  }

  public void DisplayMap(Texture2D mapImage)
  {
    doneButton.enabled = true;
    map.sprite = Program.GetImage(mapImage);
  }

}
