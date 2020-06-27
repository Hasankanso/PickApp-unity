using BackendlessAPI;
using Firebase;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public static class Program {
    public static Color mainTextColor = new Color(255f / 255f, 188f / 255f, 66f / 255f);
    public static Color labelColor;
    public static Color activePlaceHolderColor = new Color(255f / 255f, 188f / 255f, 66f / 255f);
    public static int fontSize = 28;
    public static Font font;
    public static Language language = Language.language;
    private static User user;
    private static string userToken;
    private static bool isLoggedIn = false;
    private static FirebaseApp firebaseApp = FirebaseApp.Create();
    private static CountryInformations FakeCountry() {
        //  return new CountryInformations("0", "EUR", "Germany", 11, "+49");
        return new CountryInformations("F85258BF-63A7-F939-FF31-C78BB1837300", "L.L", "Lebanon", 8, "+961");
    }

    private static Dictionary<string, CountryInformations> countriesInformations;//this list is for countries dropdown in registeration panel
    private static string cultureInfo = CultureInfo.CurrentCulture.Name;
    private static DateTime maxAlertDate;
    private static DateTime maxScheduleDate;
    private static int maxSchedulesPerUser = 3;
    public static bool time24 = false;
    private static List<string> countriesInformationsNames;//this variable should implemented after filling countriesInformations list ,this variable is to insert countries name to dropdown in register
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
    public static List<Car> FakeCars() {
        List<Car> cars = new List<Car>(4);
        cars.Add(new Car("wizii", 1995, 4, 4, "Nissan", "#000000", FakeCarImage()));
        cars.Add(new Car("c300", 2000, 4, 4, "Mercedes", "#ffffff", FakeCarImage()));
        cars.Add(new Car("pikanto", 2003, 4, 2, "PIKA", "#ffffff", FakeCarImage()));
        //  cars.Add(new Car("HammerX", 2005, 5, 5, "Hammer", "#ffffff", FakeCarImage()));
        return cars;
    }
    public static List<Chat> FakeChats() {
        List<Chat> chats = new List<Chat>();
        return chats;
    }
    public static List<Message> FakeMessages() {
        List<Message> messages = new List<Message>();
        messages.Add(new Message("hello", DateTime.Now, false));
        messages.Add(new Message("Kifak", DateTime.Now, false));
        messages.Add(new Message("yalla", DateTime.Now, false));
        messages.Add(new Message("waynak", DateTime.Now, false));
        messages.Add(new Message("!!", DateTime.Now, false));
        messages.Add(new Message("ana 5ara bb3at kll klmi message", DateTime.Now, true));
        messages.Add(new Message("riselii tawilii ktiir, yalla rakkez m3i shway bddi e7ki ktiir ymkn ma tsma3 menni shi, 5allina ntwasal wen l 100$?", DateTime.Now, false));
        messages.Add(new Message("100$ miin?", DateTime.Now, true));
        return messages;
    }


    public static Person FakeReviewer() {
        Person p = new Person("wdedwed", "Mr", "Judge", new DateTime(2000, 10, 10), "0111222333", FakeProfileImage(), true, null, 4.0f);
        return p;
    }

    public static Texture2D FakeMap() {
        return new Texture2D(100, 100);
    }



    public static Dictionary<string, CountryInformations> FakeCountries() {
        Dictionary<string, CountryInformations> countriesInformations = new Dictionary<string, CountryInformations>();
        var c1 = new CountryInformations("id asnhdjasdnasd", "L.L", "Lebanon", 8, "+961");
        var c2 = new CountryInformations("asdasdasdsad", "USD", "USA", 10, "+1");
        countriesInformations.Add(c1.Name, c1);
        countriesInformations.Add(c2.Name, c2);
        return countriesInformations;
    }
    public static readonly string googleKey = "AIzaSyC7U0OEb9200tGZFFFTyLjQdo3goKyuSsw";

    public static User User {
        get => user; set {
            user = value;
            if (user != null) {
                Debug.Log(user.phone.Split(new string[] { user.Person.CountryInformations.Code }, StringSplitOptions.None)[1]);
                SetPhoneCode(user.Person.CountryInformations.Code.Split(new string[] { "+" }, StringSplitOptions.None)[1]);
                SetPhone(user.phone.Split(new string[] { user.Person.CountryInformations.Code }, StringSplitOptions.None)[1]);
                SetPassword(user.Password);
            }
        }
    }
    public static bool IsLoggedIn { get => isLoggedIn; set => isLoggedIn = value; }
    public static CountryInformations CountryInformations { get => User.Person.CountryInformations; }
    public static Dictionary<string, CountryInformations> CountriesInformations { get => countriesInformations; set => countriesInformations = value; }
    public static List<string> CountriesInformationsNames { get => countriesInformationsNames; set => countriesInformationsNames = value; }

    public static int MaxSchedulesPerUser { get => maxSchedulesPerUser; }

    public static string AppName => appName;

    public static FirebaseApp FirebaseApp { get => firebaseApp; set => firebaseApp = value; }
    public static string UserToken { get => User == null ? null : User.Token; }
    public static Driver Driver { get => User == null ? null : User.Driver; }
    public static Person Person { get => User == null ? null : User.Person; }

    public static Sprite GetImage(Texture2D texture2D) {
        return Sprite.Create(texture2D, new Rect(0, 0, texture2D.width, texture2D.height), new Vector2(0.5f, 0.5f), 1024);
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
    public static void SetPhone(string phone) {
        PlayerPrefs.SetString("phone", phone);
    }
    public static void SetPhoneCode(string phoneCode) {
        PlayerPrefs.SetString("phoneCode", phoneCode);
    }
    public static void SetPassword(string password) {
        PlayerPrefs.SetString("password", password);
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

    public static string DateToString(DateTime date) {
        if (time24) {
            return date.ToString("dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);
        } else {
            return date.ToString("dd/MM/yyyy hh:mm tt", CultureInfo.InvariantCulture);
        }
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
}