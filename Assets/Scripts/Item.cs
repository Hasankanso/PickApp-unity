using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public abstract class Item : MonoBehaviour {
    public Image background;
    public Image maskImage;
    internal abstract void Clear();
    internal abstract void Select();
    internal abstract void UnSelect();
}
