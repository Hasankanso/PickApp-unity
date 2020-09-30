using GoogleMobileAds.Api;
using GoogleMobileAds.Api.Mediation.AdColony;
using GoogleMobileAds.Api.Mediation.UnityAds;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AdMob : MonoBehaviour {
    private static AdRequest request;
    private static RewardedAd rewardedAd;
    private static BannerView bannerView;
    private static Action rewardedAdAction;

    void Start() {
        //Initialize App
        /*MobileAds.Initialize((initStatus) => {
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
        });*/
        //for testing purpose
        AdColonyAppOptions.SetUserId("myUser");
        AdColonyAppOptions.SetTestMode(true);
        //
        InitialzeAdRequest();
        InitializeRewardAd();
    }
    private void InitialzeAdRequest() {
        UnityAds.SetGDPRConsentMetaData(true);
        AdColonyAppOptions.SetGDPRRequired(true);
        AdColonyAppOptions.SetGDPRConsentString("1");

        //initialize App
        MobileAds.Initialize(initStatus => { });
        //set info if user logged in to optimize user click on ad
        //initialize request
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
            .AddTestDevice("0D194CF269BFB841E3F9F699EE9B5E2E")
            .AddTestDevice("7eafce40-1d8d-47cb-b032-197c154be9d8")
            .AddExtra("color_bg", "D81159")
            .Build();
        } else {
            request = new AdRequest.Builder()
            .TagForChildDirectedTreatment(false)
            .AddTestDevice("0D194CF269BFB841E3F9F699EE9B5E2E")
            .AddTestDevice("7eafce40-1d8d-47cb-b032-197c154be9d8")
            .AddExtra("color_bg", "D81159")
            .Build();
        }
    }
    public static void InitializeBannerView() {

#if UNITY_EDITOR
        string adUnitId = "unused";
#elif UNITY_ANDROID
        string adUnitId = "ca-app-pub-4192057498524161/4614477405";
#elif UNITY_IPHONE
        string adUnitId = "ca-app-pub-4192057498524161/8553722413";
#else
        string adUnitId = "unexpected_platform";
#endif

        // Clean up banner ad before creating a new one.
        DestroyBanner();

        AdSize adaptiveSize =
                AdSize.GetCurrentOrientationAnchoredAdaptiveBannerAdSizeWithWidth(AdSize.FullWidth);

        bannerView = new BannerView(adUnitId, adaptiveSize, AdPosition.Bottom);

        // Load a banner ad.
        LoadBannerAd();
    }

    private static void LoadBannerAd() {
        bannerView.LoadAd(request);
    }

    private static void InitializeRewardAd() {
        //Setup rewarded video
#if UNITY_EDITOR
        string adUnitId = "unused";
#elif UNITY_ANDROID
        string adUnitId = "ca-app-pub-4192057498524161/8496521283";
#elif UNITY_IPHONE
        string adUnitId = "ca-app-pub-4192057498524161/8299234258";
#else
        string adUnitId = "unexpected_platform";
#endif
        //Initialize RewardedAd
        rewardedAd = new RewardedAd(adUnitId);

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
    //destroy banner on panel back and mission completed and admobrequest
    public static void DestroyBanner() {
        if (bannerView != null) {
            bannerView.Destroy();
        }
    }
    //this function to optimize the ad request
    private int CalculateAge(DateTime date) {
        DateTime birthdate = date;
        int years = DateTime.Now.Year - birthdate.Year;
        if (DateTime.Now.Month < birthdate.Month || (DateTime.Now.Month == birthdate.Month && DateTime.Now.Day < birthdate.Day))
            years--;
        return years;
    }
}
