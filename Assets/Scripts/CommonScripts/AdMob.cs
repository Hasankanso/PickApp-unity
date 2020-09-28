using GoogleMobileAds.Api;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdMob : MonoBehaviour {
    private static string addUnitId;
    private static AdRequest request;
    private static RewardedAd skippableAd;
    private static RewardedAd unSkippableAd;
    private static Action skippableAdsAction;
    private static Action unSkippableAdsAction;

    void Start() {
        //Initialize App
        MobileAds.Initialize((initStatus) => {
            Dictionary<string, AdapterStatus> map = initStatus.getAdapterStatusMap();
            foreach (KeyValuePair<string, AdapterStatus> keyValuePair in map) {
                string className = keyValuePair.Key;
                AdapterStatus status = keyValuePair.Value;
                switch (status.InitializationState) {
                    case AdapterState.NotReady:
                        // The adapter initialization did not complete.
                        MonoBehaviour.print("Adapter: " + className + " not ready.");
                        break;
                    case AdapterState.Ready:
                        // The adapter was successfully initialized.
                        MonoBehaviour.print("Adapter: " + className + " is initialized.");
                        break;
                }
            }
        });
        //initialize request
        if (Program.Person != null) {
            GoogleMobileAds.Api.Gender gender;
            if (Program.Person.Gender == true) {
                gender = Gender.Male;
            } else {
                gender = Gender.Female;
            }
            bool tagForChildDirected = true;
            if (CalculateAge(Program.Person.Birthday)>=18) {
                tagForChildDirected = false;
            }
            request = new AdRequest.Builder()
            .SetGender(gender)
            .SetBirthday(Program.Person.Birthday)
            .TagForChildDirectedTreatment(tagForChildDirected)
            .Build();
        } else {
            request = new AdRequest.Builder()
            .TagForChildDirectedTreatment(false)
            .Build();
        }

#if UNITY_ANDROID
        addUnitId = "ca-app-pub-3940256099942544/5224354917";
#elif UNITY_IPHONE
        addUnitId = "ca-app-pub-3940256099942544/1712485313";
#else
        adUnitId = "unexpected_platform";
#endif
        //Initialize skippableRewardedAd
        skippableAd = new RewardedAd(addUnitId);

        skippableAd.OnAdFailedToLoad += AdFaildLoading;
        skippableAd.OnAdOpening += AdIsShowen;
        skippableAd.OnAdFailedToShow += AdFaildToShow;

        // Called when the user should be rewarded for interacting with the ad.
        skippableAd.OnUserEarnedReward += HandleUserEarnedReward;
        // Called when the ad is closed.
        skippableAd.OnAdClosed += HandleRewardedAdClosed;

#if UNITY_ANDROID
        addUnitId = "ca-app-pub-3940256099942544/5224354917";
#elif UNITY_IPHONE
        addUnitId = "ca-app-pub-3940256099942544/1712485313";
#else
        adUnitId = "unexpected_platform";
#endif
        //Initialize skippableRewardedAd
        unSkippableAd = new RewardedAd(addUnitId);

        unSkippableAd.OnAdFailedToLoad += AdFaildLoading;
        unSkippableAd.OnAdOpening += AdIsShowen;
        unSkippableAd.OnAdFailedToShow += AdFaildToShow;

        // Called when the user should be rewarded for interacting with the ad.
        unSkippableAd.OnUserEarnedReward += HandleUserEarnedReward;
        // Called when the ad is closed.
        unSkippableAd.OnAdClosed += HandleRewardedAdClosed;
        LoadUnskippableAd();
        LoadSkippableAd();
    }
    private static void LoadUnskippableAd() {
        unSkippableAd.LoadAd(request);
    }
    private static void LoadSkippableAd() {
        skippableAd.LoadAd(request);
    }
    public static void ShowSkippableAd(Action afterShowAction) {
        if (skippableAd.IsLoaded()) {
            skippableAd.Show();
        }
        skippableAdsAction = afterShowAction;
    }
    public static void ShowUnSkippableAd(Action afterShowAction) {
        if (unSkippableAd.IsLoaded()) {
            unSkippableAd.Show();
        }
        unSkippableAdsAction = afterShowAction;
    }
    public static void AdFaildLoading(object sender, AdErrorEventArgs args) {
    }

    public static void AdIsShowen(object sender, EventArgs args) {
    }

    public static void AdFaildToShow(object sender, AdErrorEventArgs args) {
    }

    public static void HandleRewardedAdClosed(object sender, EventArgs args) {
        if (skippableAdsAction != null) {
            skippableAdsAction.Invoke();
            skippableAdsAction = null;
            LoadSkippableAd();
        }
    }
    private int CalculateAge(DateTime date) {
        DateTime birthdate = date;
        int years = DateTime.Now.Year - birthdate.Year;
        if (DateTime.Now.Month < birthdate.Month || (DateTime.Now.Month == birthdate.Month && DateTime.Now.Day < birthdate.Day))
            years--;
        return years;
    }
    public static void HandleUserEarnedReward(object sender, Reward args) {
        string type = args.Type;
        double amount = args.Amount;
        if (skippableAdsAction != null) {
            skippableAdsAction.Invoke();
            skippableAdsAction = null;
            LoadSkippableAd();
        } else if (unSkippableAdsAction != null) {
            unSkippableAdsAction.Invoke();
            unSkippableAdsAction = null;
            LoadUnskippableAd();
        }
    }
}
