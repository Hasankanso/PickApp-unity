using ArabicSupport;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MultiLangText : MonoBehaviour
{
  public string key;
  // Start is called before the first frame update
  void Start()
  {
        if (!Program.language.english)
        {
            Text textUI = GetComponent<Text>();
            textUI.text = Program.language.GetString(key);
        }

        //if (Program.language.arabic)
        //{
        //    textUI.text = ArabicFixer.Fix(textUI.text, true, true);
        //    textUI.alignment = TextAnchor.MiddleRight;
        //    textUI.fontSize = 60;
        //}
    }

}
