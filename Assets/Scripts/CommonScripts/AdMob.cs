using GoogleMobileAds.Api;
using GoogleMobileAds.Api.Mediation.AdColony;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdMob : MonoBehaviour {
    private static string addUnitId;
    private static AdRequest request;
    private static RewardedAd rewardedAd;
    private static Action rewardedAdAction;

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
                        break;
                    case AdapterState.Ready:
                        // The adapter was successfully initialized.
                        break;
                }
            }
        });
        //for testing purpose
        AdColonyAppOptions.SetUserId("myUser");
        AdColonyAppOptions.SetTestMode(true);
        //

        AdColonyAppOptions.SetGDPRRequired(true);
        AdColonyAppOptions.SetGDPRConsentString("1");
        AdColonyMediationExtras extras = new AdColonyMediationExtras();
        extras.SetShowPrePopup(false);
        extras.SetShowPostPopup(false);
        //initialize request
        //set info if user logged in
        if (Program.Person != null) {
            GoogleMobileAds.Api.Gender gender;
            if (Program.Person.Gender == true) {
                gender = Gender.Male;
            } else {
                gender = Gender.Female;
            }
            bool tagForChildDirected = true;
            if (CalculateAge(Program.Person.Birthday) >= 18) {
                tagForChildDirected = false;
            }
            request = new AdRequest.Builder()
            .SetGender(gender)
            .SetBirthday(Program.Person.Birthday)
            .TagForChildDirectedTreatment(tagForChildDirected)
            .AddMediationExtras(extras)
            .Build();
        } else {
            request = new AdRequest.Builder()
            .TagForChildDirectedTreatment(false)
            .AddMediationExtras(extras)
            .Build();
        }
        //Setup rewarded video
#if UNITY_EDITOR
        addUnitId = "unused";
#elif UNITY_ANDROID
        addUnitId = "ca-app-pub-3940256099942544/5224354917";
#elif UNITY_IPHONE
        addUnitId = "ca-app-pub-3940256099942544/1712485313";
#else
        adUnitId = "unexpected_platform";
#endif
        //Initialize skippableRewardedAd
        rewardedAd = new RewardedAd(addUnitId);

        rewardedAd.OnAdFailedToLoad += AdFaildLoading;
        rewardedAd.OnAdOpening += AdIsShowen;
        rewardedAd.OnAdFailedToShow += AdFaildToShow;

        // Called when the user should be rewarded for interacting with the ad.
        rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
        // Called when the ad is closed.
        rewardedAd.OnAdClosed += HandleRewardedAdClosed;
        //load rewarded video
        LoadRewardedAd();
    }
    private static void LoadRewardedAd() {
        rewardedAd.LoadAd(request);
    }
    public static void ShowRewardedAd(Action afterShowAction) {
        if (rewardedAd.IsLoaded()) {
            rewardedAd.Show();
        }
        rewardedAdAction = afterShowAction;
    }
    public static void AdFaildLoading(object sender, AdErrorEventArgs args) {
    }

    public static void AdIsShowen(object sender, EventArgs args) {
    }

    public static void AdFaildToShow(object sender, AdErrorEventArgs args) {
        LoadRewardedAd();
    }
    public static void HandleRewardedAdClosed(object sender, EventArgs args) {
        LoadRewardedAd();
    }
    public static void HandleUserEarnedReward(object sender, Reward args) {
        string type = args.Type;
        double amount = args.Amount;
        if (rewardedAdAction != null) {
            rewardedAdAction.Invoke();
            rewardedAdAction = null;
        }
        LoadRewardedAd();
    }
    private int CalculateAge(DateTime date) {
        DateTime birthdate = date;
        int years = DateTime.Now.Year - birthdate.Year;
        if (DateTime.Now.Month < birthdate.Month || (DateTime.Now.Month == birthdate.Month && DateTime.Now.Day < birthdate.Day))
            years--;
        return years;
    }
}
