﻿using ArabicSupport;
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
        if (!Program.language.English)
        {
            Text textUI = GetComponent<Text>();
            textUI.text = Program.language.GetString(key);
        }
    }

}
