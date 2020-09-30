using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HowItWorksPanel : Panel {
    public override void Init() {
        AdMob.InitializeBannerView();
    }
    internal override void Clear() {

    }
}
