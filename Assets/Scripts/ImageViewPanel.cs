using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageViewPanel : Panel
{
  public RawImage image;

  public void Init(Texture img){
    image.texture = img;
  }

  internal override void Clear()
  {
    image.texture = null;
  }
    
}
