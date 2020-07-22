using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogBox : Panel
{
    public Text messageText;
    public Image background;
    private void Update() {
        if (!gameObject.GetComponent<Animation>().isPlaying) {
            Destroy(gameObject);
        }
    }
    public void Init(string text, bool isSuccess) {
        messageText.text = text;
        if (isSuccess)
            background.color = new Color(0f / 255f, 255f / 255f, 21f / 255f);
        else
            background.color = new Color(255f / 255f, 0f / 255f, 21f / 255f);
    }
    internal override void Clear() {
        throw new System.NotImplementedException();
    }
}
