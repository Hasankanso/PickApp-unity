using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoadItem : MonoBehaviour
{
  public Text details;

  private DirectionsFinderPanel directionsPanel;
  private Texture2D mapImage;
  private string roadPoints;
  
  public Texture2D MapImage { set => mapImage = value; }

  private void Start()
  {
    details.color = Program.mainTextColor;
    details.fontSize = Program.fontSize;
  }
  public void OnClick(){
    if (mapImage == null)
    {
      directionsPanel.StartCoroutine(directionsPanel.RequestMap(roadPoints, this));
    } else {
      directionsPanel.DisplayMap(mapImage);
    }
  }
  internal void Init(DirectionsFinderPanel panel, string text, string roadPoints)
  {
    directionsPanel = panel;
    details.text = text;
    this.roadPoints = roadPoints;
  }
}
