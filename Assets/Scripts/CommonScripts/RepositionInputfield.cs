using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepositionInputfield : MonoBehaviour {
    public GameObject inputFieldContainer;
    private static bool isClicked = false;
    public void ChangePositionUpOnTyping() {
        if(isClicked == false) {
            StartCoroutine(ExecuteAfterTime());
        }
    }
    public void ChangePositionDownOnEndTyping() {
        if(isClicked == true) {
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
        if(Application.isEditor) {
            return 100.0f; // fake TouchScreenKeyboard height ratio for debug in editor        
        }

#if UNITY_ANDROID
        using(AndroidJavaClass UnityClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer")) {
            AndroidJavaObject View = UnityClass.GetStatic<AndroidJavaObject>("currentActivity").Get<AndroidJavaObject>("mUnityPlayer").Call<AndroidJavaObject>("getView");

            using(AndroidJavaObject Rct = new AndroidJavaObject("android.graphics.Rect")) {
                View.Call("getWindowVisibleDisplayFrame", Rct);

                return Screen.height - Rct.Call<int>("height");
            }
        }
#else
        return (float)TouchScreenKeyboard.area.height / Screen.height;
#endif
    }

}
