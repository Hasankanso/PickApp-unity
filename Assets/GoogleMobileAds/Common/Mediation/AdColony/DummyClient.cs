// Copyright 2019 Google LLC
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

using System.Reflection;

using UnityEngine;

namespace GoogleMobileAds.Common.Mediation.AdColony
{
    public class DummyClient : IAdColonyAppOptionsClient
    {
        public DummyClient()
        {
        }

        public void SetGDPRConsentString(string consentString)
        {
        }

        public void SetGDPRRequired(bool gdprRequired)
        {
        }

        public void SetUserId(string userId)
        {
        }

        public void SetTestMode(bool isTestMode)
        {
        }

        public string GetGDPRConsentString()
        {
            return "";
        }

        public bool IsGDPRRequired()
        {
            return false;
        }

        public string GetUserId()
        {
            return "";
        }

        public bool IsTestMode()
        {
            return false;
        }
    }
}
