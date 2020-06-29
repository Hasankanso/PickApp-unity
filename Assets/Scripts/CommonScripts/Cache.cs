using UnityEngine;

public static class Cache {
    public static bool GetNewsCheckbox() {
        return bool.Parse(PlayerPrefs.GetString("news", "true"));
    }
    public static bool GetDisableAllCheckbox() {
        return bool.Parse(PlayerPrefs.GetString("disableAll", "true"));
    }
    public static void SetNewsCheckbox(bool news) {
        PlayerPrefs.SetString("news", news.ToString());
    }
    public static void SetPhone(string phone) {
        PlayerPrefs.SetString("phone", phone);
    }
    public static void SetPhoneCode(string phoneCode) {
        PlayerPrefs.SetString("phoneCode", phoneCode);
    }
    public static void SetPassword(string password) {
        PlayerPrefs.SetString("password", password);
    }
    public static void SetToken(string token) {
        PlayerPrefs.SetString("token", token);
    }
    public static void SetUserId(string userId) {
        PlayerPrefs.SetString("id", userId);
    }
    public static string GetUserId() {
        return PlayerPrefs.GetString("id", "");
    }
    public static string GetToken() {
        return PlayerPrefs.GetString("token", "");
    }
    public static string GetPhone() {
        return PlayerPrefs.GetString("phone", "");
    }
    public static string GetPhoneCode() {
        return PlayerPrefs.GetString("phoneCode", "");
    }
    public static string GetPassword() {
        return PlayerPrefs.GetString("password", "");
    }
    public static void SetDisableAllCheckbox(bool disableAll) {
        PlayerPrefs.SetString("disableAll", disableAll.ToString());
    }
}
