using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using ArabicSupport;

public class InputFieldScript : MonoBehaviour {
    public Text placeHolder;
    public Text text;
    private Text placeHolderText;
    public Sprite inputFieldClicked;
    public Sprite inputFieldUnclicked;
    public Sprite inputFieldError;
    public GameObject inputFieldContainer;
    private static bool isClicked = false;
    string stringEdit = "";

    private void Start() {
        text.color = new Color(255f / 255f, 188f / 255f, 66f / 255f);
        this.GetComponent<InputField>().onValueChanged.AddListener(delegate { OnEditting(); });
       // this.placeHolder.text = ArabicFixer.Fix(text.text, true, true);
       // this.text.text = ArabicFixer.Fix(text.text, true, true);
       // this.placeHolderText.text = ArabicFixer.Fix(text.text, true, true);
    }
    public void OnEditting() {
        stringEdit = text.text;
    }
    public void PlaceHolder() {
        placeHolderText = placeHolder.GetComponent<Text>();
        text.color = new Color(255f / 255f, 188f / 255f, 66f / 255f);
        placeHolderText.color = new Color(255f / 255f, 188f / 255f, 66f / 255f);
        gameObject.GetComponent<Image>().sprite = inputFieldClicked;
        if (placeHolderText.fontSize != 28) {
            placeHolderText.fontSize = 28;
            placeHolderText.alignment = TextAnchor.UpperLeft;
            placeHolderText.transform.position += new Vector3(-19.7f, -8.1f, 0);
        }
    }
    public void Error() {
        this.GetComponent<Image>().sprite = inputFieldError;
        this.GetComponent<InputFieldScript>().placeHolder.color = new Color(236f / 255f, 28f / 255f, 36f / 255f);
    }
    public void SetText(string text) {
        this.GetComponent<InputField>().SetTextWithoutNotify(text);
        this.PlaceHolder();
    }
    public void clickedOut() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            SetText(stringEdit);
        }
        placeHolderText = placeHolder.GetComponent<Text>();
        if (placeHolderText.fontSize != 40 && String.IsNullOrEmpty(text.GetComponent<Text>().text)) {
            placeHolderText.transform.position -= new Vector3(-19.7f, -8.1f, 0);
            placeHolderText.fontSize = 40;
            placeHolderText.alignment = TextAnchor.MiddleLeft;
            placeHolderText.color = new Color(168f / 255f, 168f / 255f, 168f / 255f);
            gameObject.GetComponent<Image>().sprite = inputFieldUnclicked;
        }
    }
    public void Reset() {
        this.GetComponent<InputField>().SetTextWithoutNotify("");
        text.text = "";
        clickedOut();
    }
    public void ChangePositionUpOnTyping() {
        if (isClicked == false) {
            StartCoroutine(ExecuteAfterTime());
        }
    }
    public void ChangePositionDownOnEndTyping() {
        if (isClicked == true) {
            inputFieldContainer.transform.position = new Vector3(inputFieldContainer.transform.position.x, inputFieldContainer.transform.position.y - (float)(GetKeyboardHeightRatio()) / 1.6f, 0);
            isClicked = false;
        }
    }
    IEnumerator ExecuteAfterTime() {
        //this to handle for certain time
        yield return new WaitUntil(() => TouchScreenKeyboard.visible == true);
        StartCoroutine(ExecuteAfterTime(0.2f));
    }
    IEnumerator ExecuteAfterTime(float time) {

        yield return new WaitForSeconds(time);
        inputFieldContainer.transform.position = new Vector3(inputFieldContainer.transform.position.x, inputFieldContainer.transform.position.y + (float)(GetKeyboardHeightRatio()) / 1.6f, 0);
        isClicked = true;
    }

    private float GetKeyboardHeightRatio() {
        if (Application.isEditor) {
            return 100.0f; // fake TouchScreenKeyboard height ratio for debug in editor        
        }

#if UNITY_ANDROID
        using (AndroidJavaClass UnityClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer")) {
            AndroidJavaObject View = UnityClass.GetStatic<AndroidJavaObject>("currentActivity").Get<AndroidJavaObject>("mUnityPlayer").Call<AndroidJavaObject>("getView");

            using (AndroidJavaObject Rct = new AndroidJavaObject("android.graphics.Rect")) {
                View.Call("getWindowVisibleDisplayFrame", Rct);

                return Screen.height - Rct.Call<int>("height");
            }
        }
#else
        return (float)TouchScreenKeyboard.area.height / Screen.height;
#endif
    }
}
