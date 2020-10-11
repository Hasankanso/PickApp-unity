using Firebase;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.Networking;

public static class Program {
    public static Color mainTextColor = new Color(255f / 255f, 188f / 255f, 66f / 255f);
    public static Color labelColor;
    public static Color activePlaceHolderColor = new Color(255f / 255f, 188f / 255f, 66f / 255f);
    public static int fontSize = 28;
    public static Font font;
    public static Language language = Language.defaultInstance;
    private static User user;
    private static bool isLoggedIn = false;
    private static FirebaseApp firebaseApp = FirebaseApp.Create();
    private static Dictionary<string, CountryInformations> countriesInformations;//this list is for countries dropdown in registeration panel
    private static string cultureInfo = CultureInfo.CurrentCulture.Name;
    private static DateTime maxAlertDate;
    private static DateTime maxScheduleDate;
    private static int maxSchedulesPerUser = 3;
    public static bool time24 = false;
    private static List<string> countriesInformationsNames;//this variable should implemented after filling countriesInformations list ,this variable is to insert countries name to dropdown in register
    private static string countryComponent = "";
    private static readonly string appName = "Pickapp";
    internal static Color SelectedItemColor() {
        return new Color(179f / 255f, 230f / 255f, 255f / 255f, 90f / 255f);
    }
    internal static Color UnSelectedItemColor() {
        return new Color(255f / 255f, 255f / 255f, 255f / 255f);
    }
    public static DateTime MaxScheduleDate {
        get { return DateTime.Now.AddMonths(6); }

    }
    public static DateTime MaxAlertDate {
        get { return DateTime.Now.AddMonths(6); }

    }
    public static Texture2D FakeProfileImage() {
        return new Texture2D(400, 500);
    }
    public static Texture2D FakeCarImage() {
        return new Texture2D(400, 500);
    }
    public static List<Chat> FakeChats() {
        List<Chat> chats = new List<Chat>();
        return chats;
    }
    public static readonly string googleKey = "AIzaSyC7U0OEb9200tGZFFFTyLjQdo3goKyuSsw";

    public static User User {
        get => user; set { user = value; }
    }
    public static bool IsLoggedIn { get => isLoggedIn; set => isLoggedIn = value; }
    public static CountryInformations CountryInformations { get => User.Person.CountryInformations; }
    public static Dictionary<string, CountryInformations> CountriesInformations { get => countriesInformations; set => countriesInformations = value; }
    public static List<string> CountriesInformationsNames { get => countriesInformationsNames; set => countriesInformationsNames = value; }

    public static int MaxSchedulesPerUser { get => maxSchedulesPerUser; }

    public static string AppName => appName;

    public static FirebaseApp FirebaseApp { get => firebaseApp; set => firebaseApp = value; }
    public static Driver Driver { get => User == null ? null : User.Driver; }
    public static Person Person { get => User == null ? null : User.Person; }
    public static string CountryComponent { get => countryComponent; set => countryComponent = value; }

    public static Sprite GetImage(Texture2D texture2D) {
        return Sprite.Create(texture2D, new Rect(0, 0, texture2D.width, texture2D.height), new Vector2(0.5f, 0.5f), 1024);
    }


    public static string DateToString(DateTime date) {
        return DateToString(date, false);
    }

    public static string DateToString(DateTime date, bool simplify) {
        DateTime now = DateTime.Now;
        string hourFormat = time24 ? "HH:mm" : "hh:mm tt";
        string format = "dd/MM/yyyy " + hourFormat;

        if (simplify && date.Year == now.Year) //same year
        {
            format = "dd/MM " + hourFormat;

            if (date.Month == now.Month) { //same year and month
                if (date.Day == now.Day) { //same date
                    return "Today, " + date.ToString(hourFormat);
                }
                if (date.Day == now.AddDays(1).Day) { //tomorrow
                    return "Tomorrow, " + date.ToString(hourFormat);
                }
            }
        }

        return date.ToString(format, CultureInfo.InvariantCulture);
    }

    public static DateTime StringToDate(string date) {
        if (time24) {
            return DateTime.ParseExact(date, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);
        } else {
            return DateTime.ParseExact(date, "dd/MM/yyyy hh:mm tt", CultureInfo.InvariantCulture);
        }
    }
    public static DateTime CombineDateTime(DateTime date, DateTime time) {
        return new DateTime(date.Year, date.Month, date.Day, time.Hour, time.Minute, 0);
    }
    public static string BirthdayToString(DateTime date) {
        return date.ToString("dd/MM/yyyy");
    }
    public static DateTime StringToBirthday(string date) {
        return DateTime.ParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
    }
    public static DateTime UnixToUtc(double milliseconds) {
        DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
        dtDateTime = dtDateTime.AddMilliseconds(milliseconds).ToLocalTime();
        return dtDateTime;
    }
    public static DateTime CombineDateTime(String date, String time) {
        return CombineDateTime(StringToDate(date), StringToDate(time));
    }

    public static IEnumerator RequestImage(string url, Action<Texture2D> callBack, Action<string> callBackError) {
        if (!string.IsNullOrEmpty(url)) {
            var uwr = new UnityWebRequest(url);
            DownloadHandlerTexture texDl = new DownloadHandlerTexture(true);
            uwr.downloadHandler = texDl;
            yield return uwr.SendWebRequest();
            if (uwr.isNetworkError || uwr.isHttpError) {
                callBackError(uwr.error);
            } else {
                callBack(texDl.texture);
            }
        }
    }
}