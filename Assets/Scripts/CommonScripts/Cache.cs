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
        PlayerPrefs.SetString("id", userId);
    }
    public static string GetEmail() {
        return PlayerPrefs.GetString("email", "");
    }
    public static string GetUserId() {
        return PlayerPrefs.GetString("id", "");
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
}
