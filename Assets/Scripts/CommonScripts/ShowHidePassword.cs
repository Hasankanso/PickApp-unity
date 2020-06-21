using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowHidePassword : MonoBehaviour
{
    public InputField inputField;

    public void ShowPassword() {
        inputField.contentType = InputField.ContentType.Standard;
        inputField.ForceLabelUpdate();
    }
    public void HidePassword() {
        inputField.contentType = InputField.ContentType.Password;
        inputField.ForceLabelUpdate();
    }
}
