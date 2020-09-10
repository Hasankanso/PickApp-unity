using System;
using UnityEngine;

public static class Cache {
    public static void User(User user) {
        if (user != null) {
            if (user.Person != null)
                SetPhoneCode(user.Person.CountryInformations.Code.Split(new string[] { "+" }, StringSplitOptions.None)[1]);
            if (user.phone != null)
                SetPhone(user.phone.Split(new string[] { user.Person.CountryInformations.Code }, StringSplitOptions.None)[1]);
            if (user.Email != null)
                SetEmail(user.Email);
            SetUserId(user.Id);
        }
    }
    public static bool GetNewsCheckbox() {
        return bool.Parse(PlayerPrefs.GetString("news", "true"));
    }
    public static bool GetDisableAllCheckbox() {
        return bool.Parse(PlayerPrefs.GetString("disableAll", "true"));
    }
    public static void SetNewsCheckbox(bool news) {
        PlayerPrefs.SetString("news", news.ToString());
    }

    //Cache for user
    public static void SetPhone(string phone) {
        PlayerPrefs.SetString("phone", phone);
    }
    public static void SetEmail(string email) {
        PlayerPrefs.SetString("email", email);
    }
    public static void SetPhoneCode(string phoneCode) {
        PlayerPrefs.SetString("phoneCode", phoneCode);
    }
    public static void SetUserId(string userId) {
        PlayerPrefs.SetString("userId", userId);
    }
    public static string GetEmail() {
        return PlayerPrefs.GetString("email", "");
    }
    public static string GetUserId() {
        return PlayerPrefs.GetString("userId", "");
    }
    public static string GetPhone() {
        return PlayerPrefs.GetString("phone", "");
    }
    public static string GetPhoneCode() {
        return PlayerPrefs.GetString("phoneCode", "");
    }
    public static void SetDisableAllCheckbox(bool disableAll) {
        PlayerPrefs.SetString("disableAll", disableAll.ToString());
    }

    //Person cache
    public static void SetPersonId(string id) {
        PlayerPrefs.SetString("personId", id);
    }
    public static string GetPersonId() {
        return PlayerPrefs.GetString("personId", "");
    }
    public static void SetFirstName(string firstName) {
        PlayerPrefs.SetString("firstName", firstName);
    }
    public static string GetFirstName() {
        return PlayerPrefs.GetString("firstName", "");
    }
    public static void SetLastName(string lastName) {
        PlayerPrefs.SetString("lastName", lastName);
    }
    public static string GetLastName() {
        return PlayerPrefs.GetString("lastName", "");
    }
    public static void SetBirthday(string birthday) {
        PlayerPrefs.SetString("birthday", birthday);
    }
    public static string GetBirthday() {
        return PlayerPrefs.GetString("birthday", "");
    }
    public static void SetBio(string bio) {
        PlayerPrefs.SetString("bio", bio);
    }
    public static string GetBio() {
        return PlayerPrefs.GetString("bio", "");
    }
    public static void SetChattiness(string chattiness) {
        PlayerPrefs.SetString("chattiness", chattiness);
    }
    public static string GetChattiness() {
        return PlayerPrefs.GetString("chattiness", "");
    }
    public static void SetGender(string gender) {
        PlayerPrefs.SetString("gender", gender);
    }
    public static string GetGender() {
        return PlayerPrefs.GetString("gender", "");
    }
    public static void SetRateAverage(string gender) {
        PlayerPrefs.SetString("rateAverage", gender);
    }
    public static string GetRateAverage() {
        return PlayerPrefs.GetString("rateAverage", "");
    }
    public static void SetProfilePictureUrl(string profilePictureUrl) {
        PlayerPrefs.SetString("profilePictureUrl", profilePictureUrl);
    }
    public static string GetProfilePictureUrl() {
        return PlayerPrefs.GetString("profilePictureUrl", "");
    }
    //Person's country informations
    public static void SetUnit(string unit) {
        PlayerPrefs.SetString("unit", unit);
    }
    public static string GetUnit() {
        return PlayerPrefs.GetString("unit", "");
    }
    public static void SetCountryInformationsId(string countryInformationsId) {
        PlayerPrefs.SetString("countryInformationsId", countryInformationsId);
    }
    public static string GetCountryInformationsId() {
        return PlayerPrefs.GetString("countryInformationsId", "");
    }
    public static void SetCountryName(string countryName) {
        PlayerPrefs.SetString("countryName", countryName);
    }
    public static string GetCountryName() {
        return PlayerPrefs.GetString("countryName", "");
    }
    public static void SetDigits(string digits) {
        PlayerPrefs.SetString("digits", digits);
    }
    public static string GetDigits() {
        return PlayerPrefs.GetString("digits", "");
    }
    public static void SetCode(string code) {
        PlayerPrefs.SetString("code", code);
    }
    public static string GetCode() {
        return PlayerPrefs.GetString("code", "");
    }

    //Driver
    public static void SetDriverId(string id) {
        PlayerPrefs.SetString("driverId", id);
    }
    public static string GetDriverId() {
        return PlayerPrefs.GetString("driverId", "");
    }

    //Driver's regions
    public static void SetRegionId(string regionId) {
        PlayerPrefs.SetString("regionId", regionId);
    }
    public static string GetRegionId() {
        return PlayerPrefs.GetString("regionId", "");
    }
    public static void SetRegionName(string regionName) {
        PlayerPrefs.SetString("regionName", regionName);
    }
    public static string GetRegionName() {
        return PlayerPrefs.GetString("regionName", "");
    }
    public static void SetRegionPlaceId(string regionPlaceId) {
        PlayerPrefs.SetString("regionPlaceId", regionPlaceId);
    }
    public static string GetRegionPlaceId() {
        return PlayerPrefs.GetString("regionPlaceId", "");
    }
    public static void SetRegionLatitude(string regionLatitude) {
        PlayerPrefs.SetString("regionLatitude", regionLatitude);
    }
    public static string GetRegionLatitude() {
        return PlayerPrefs.GetString("regionLatitude", "");
    }
    public static void SetRegionLongitude(string regionLongitude) {
        PlayerPrefs.SetString("regionLongitude", regionLongitude);
    }
    public static string GetRegionLongitude() {
        return PlayerPrefs.GetString("regionLongitude", "");
    }
}
