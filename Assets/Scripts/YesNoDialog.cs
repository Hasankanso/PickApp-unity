using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class YesNoDialog : Panel
{

  Action<bool> OnDecision;
  public Text questionText;
  public void Init(string question, Action<bool> decisionCallback){
    questionText.text = question;
    OnDecision = decisionCallback;
  }

  public void YesClicked()
  {
    OnDecision(true);
  }

  public void NoClick()
  {
    OnDecision(false);
  }

  internal override void Clear()
  {
    throw new NotImplementedException();
  }
}
