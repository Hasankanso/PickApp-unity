// Copyright (C) 2015 Google, Inc.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//      http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;
using System.Reflection;

using GoogleMobileAds.Unity;
using GoogleMobileAds.Api;
using UnityEngine;

namespace GoogleMobileAds.Common
{
    public class DummyClient : IBannerClient, IInterstitialClient, IRewardBasedVideoAdClient,
            IAdLoaderClient, IMobileAdsClient
    {
        public DummyClient()
        {
        }

        // Disable warnings for unused dummy ad events.
#pragma warning disable 67

        public event EventHandler<EventArgs> OnAdLoaded;

        public event EventHandler<AdFailedToLoadEventArgs> OnAdFailedToLoad;

        public event EventHandler<EventArgs> OnAdOpening;

        public event EventHandler<EventArgs> OnAdStarted;

        public event EventHandler<EventArgs> OnAdClosed;

        public event EventHandler<Reward> OnAdRewarded;

        public event EventHandler<EventArgs> OnAdLeavingApplication;

        public event EventHandler<EventArgs> OnAdCompleted;

        public event EventHandler<AdValueEventArgs> OnPaidEvent;

        public event EventHandler<CustomNativeClientEventArgs> OnCustomNativeTemplateAdLoaded;

        public event EventHandler<CustomNativeClientEventArgs> OnCustomNativeTemplateAdClicked;
#pragma warning restore 67

        public string UserId
        {
            get
            {
                Debug.Log("Dummy " + MethodBase.GetCurrentMethod().Name);
                return "UserId";
            }

            set
            {
                Debug.Log("Dummy " + MethodBase.GetCurrentMethod().Name);
            }
        }

        public void Initialize(string appId)
        {
        }

        public void Initialize(Action<IInitializationStatusClient> initCompleteAction)
        {

            var initStatusClient = new InitializationStatusDummyClient();
            initCompleteAction(initStatusClient);
        }

        public void DisableMediationInitialization()
        {
        }

        public void SetApplicationMuted(bool muted)
        {
        }

        public void SetRequestConfiguration(RequestConfiguration requestConfiguration)
        {
        }

        public RequestConfiguration GetRequestConfiguration()
        {
            return null;

        }

        public void SetApplicationVolume(float volume)
        {
        }

        public void SetiOSAppPauseOnBackground(bool pause)
        {
        }

        public float GetDeviceScale()
        {
            return 0;
        }

        public int GetDeviceSafeWidth()
        {
            return 0;
        }

        public void CreateBannerView(string adUnitId, AdSize adSize, AdPosition position)
        {
        }

        public void CreateBannerView(string adUnitId, AdSize adSize, int positionX, int positionY)
        {
        }

        public void LoadAd(AdRequest request)
        {
        }

        public void ShowBannerView()
        {
        }

        public void HideBannerView()
        {
        }

        public void DestroyBannerView()
        {
        }

        public float GetHeightInPixels()
        {
            return 0;
        }

        public float GetWidthInPixels()
        {
            return 0;
        }

        public void SetPosition(AdPosition adPosition)
        {
        }

        public void SetPosition(int x, int y)
        {
        }

        public void CreateInterstitialAd(string adUnitId)
        {
        }

        public bool IsLoaded()
        {
            return true;
        }

        public void ShowInterstitial()
        {
        }

        public void DestroyInterstitial()
        {
        }

        public void CreateRewardBasedVideoAd()
        {
        }

        public void SetUserId(string userId)
        {
        }

        public void LoadAd(AdRequest request, string adUnitId)
        {
        }

        public void DestroyRewardBasedVideoAd()
        {
        }

        public void ShowRewardBasedVideoAd()
        {
        }

        public void CreateAdLoader(AdLoaderClientArgs args)
        {
        }

        public void Load(AdRequest request)
        {
        }

        public void SetAdSize(AdSize adSize)
        {
        }

        public string MediationAdapterClassName()
        {
            return null;
        }

        public IResponseInfoClient GetResponseInfoClient()
        {
            return null;
        }

    }
}
