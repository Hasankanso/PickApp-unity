using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageItem : MonoBehaviour
{
  [SerializeField]
  private Text contentText;
  [SerializeField]
  private Text dateTime;
  [SerializeField]
  private RawImage image;
  public RectTransform messageContainer;
  public RectTransform rectTransform;

  bool isOwner = false;
  public void Init(Message msg)
  {
    isOwner = msg.Owner;

    if(isOwner){
      image.color = new Color(200f / 255f, 196f / 255f, 183f / 255f, 1);
      image.uvRect = new Rect(image.uvRect.position, new Vector2(-1, image.uvRect.height));
      contentText.color = new Color(0, 0, 0);
      dateTime.color = new Color(0, 0, 0);
    }

    contentText.text = msg.Content;
    dateTime.text = Program.DateToString(msg.SendDate);
  }

  public void UpdateSize()
  {
    if (isOwner)
    {
      Rect rect = image.uvRect;
      messageContainer.anchorMin = new Vector2(1, 1);
      messageContainer.anchorMax = new Vector2(1, 1);

      messageContainer.pivot = new Vector2(1, 1);
      Vector3 position = messageContainer.anchoredPosition;
      messageContainer.anchoredPosition = new Vector3(0, position.y);
    }

 
    rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, messageContainer.rect.height);

  }

}
