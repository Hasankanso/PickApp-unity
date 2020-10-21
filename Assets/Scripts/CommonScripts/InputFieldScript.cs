using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using ArabicSupport;

public class InputFieldScript : MonoBehaviour
{
  public Text placeHolder;
  public Text text;
  private Text placeHolderText;
  public Sprite inputFieldClicked;
  public Sprite inputFieldUnclicked;
  public Sprite inputFieldError;
  public GameObject inputFieldContainer;
  private static bool isClicked = false;
  private InputField inputfield;
  string sentenceUnfixed = "";

  private void Start()
  {
    text.color = new Color(255f / 255f, 188f / 255f, 66f / 255f);
    inputfield = GetComponent<InputField>();

    if (Program.language.Arabic)
      inputfield.onValueChanged.AddListener(FixArabic);
  }

  public void FixArabic(string newLang)
  {
    if (newLang.Length < sentenceUnfixed.Length)
    {
      sentenceUnfixed = sentenceUnfixed.Substring(0, sentenceUnfixed.Length - 1);
    }
    else
    {
      var newLetter = newLang.Substring(newLang.Length - 1);
      sentenceUnfixed += newLetter;
    }
    string fixedInput = ArabicFixer.Fix(sentenceUnfixed);
    inputfield.SetTextWithoutNotify(fixedInput);
  }

  public void PlaceHolder()
  {
    placeHolderText = placeHolder.GetComponent<Text>();
    text.color = new Color(255f / 255f, 188f / 255f, 66f / 255f);
    placeHolderText.color = new Color(255f / 255f, 188f / 255f, 66f / 255f);
    gameObject.GetComponent<Image>().sprite = inputFieldClicked;
    if (placeHolderText.fontSize != 28)
    {
      placeHolderText.fontSize = 28;
      placeHolderText.alignment = TextAnchor.UpperLeft;
      placeHolderText.transform.position += new Vector3(-19.7f, -8.1f, 0);
    }
  }
  public void Error()
  {
    this.GetComponent<Image>().sprite = inputFieldError;
    this.GetComponent<InputFieldScript>().placeHolder.color = new Color(236f / 255f, 28f / 255f, 36f / 255f);
  }
  public void SetText(string text)
  {
    this.GetComponent<InputField>().SetTextWithoutNotify(text);
    this.PlaceHolder();
  }
  public void clickedOut()
  {
    if (Input.GetKeyDown(KeyCode.Escape))
    {
      SetText("");
      sentenceUnfixed = "";
    }
    placeHolderText = placeHolder.GetComponent<Text>();
    if (placeHolderText.fontSize != 40 && string.IsNullOrEmpty(text.GetComponent<Text>().text))
    {
      placeHolderText.transform.position -= new Vector3(-19.7f, -8.1f, 0);
      placeHolderText.fontSize = 40;
      placeHolderText.alignment = TextAnchor.MiddleLeft;
      placeHolderText.color = new Color(168f / 255f, 168f / 255f, 168f / 255f);
      gameObject.GetComponent<Image>().sprite = inputFieldUnclicked;
    }
  }
  public void Reset()
  {
    this.GetComponent<InputField>().SetTextWithoutNotify("");
    text.text = "";
    sentenceUnfixed = "";
    clickedOut();
  }
  public void ChangePositionUpOnTyping()
  {
    if (isClicked == false)
    {
      inputFieldContainer.transform.SetAsLastSibling();
      inputFieldContainer.GetComponent<Image>().color = new Color(255f / 255f, 255f / 255f, 255f / 255f, 255f / 255f);
      this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + inputFieldContainer.transform.position.y / 2, this.transform.position.z);
      isClicked = true;
    }
  }
  public void ChangePositionDownOnEndTyping()
  {
    if (isClicked == true)
    {
      inputFieldContainer.GetComponent<Image>().color = new Color(255f / 255f, 255f / 255f, 255f / 255f, 0f / 255f);
      this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y - inputFieldContainer.transform.position.y / 2, this.transform.position.z);
      isClicked = false;
    }
  }
}
