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
    private static Person user ;
    private static string userToken;
    private static bool isLoggedIn = false;
    private static CountryInformations countryInformations = FakeCountry();//fake this should be set after finishing registration
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
        chats.Add(new Chat(DateTime.Now, FakeMessages(), FakeNewUser(), true));
        chats.Add(new Chat(DateTime.Now, FakeMessages(), FakeUser(), false));
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
    public static List<ScheduleRide> FakeSchedule(Driver d) {
        List<ScheduleRide> scheduleRide = new List<ScheduleRide>(4);
        scheduleRide.Add(new ScheduleRide(DateTime.Now.AddDays(-12), DateTime.Now.AddDays(2), true, false, true, true, true, false, false, FakeRide(d, d.Cars[0])));
        scheduleRide.Add(new ScheduleRide(DateTime.Now.AddDays(2), DateTime.Now.AddDays(10), true, false, true, true, true, false, false, FakeRide(d, d.Cars[1])));
        return scheduleRide;
    }
    public static Person FakeReviewer() {
        Person p = new Person("wdedwed", "Mr", "Judge", new DateTime(2000, 10, 10), "0111222333", "darkEnergy", "Lebanon", FakeProfileImage(), true, null, 4.0f);
        return p;
    }

    public static Texture2D FakeMap() {
        return new Texture2D(100, 100);
    }

    public static Ride FakeRide(Driver driver, Car c) {
        Ride r = new Ride(driver, c, new Location("fakeid", "Nabatyeh"), new Location("fakeid", "Sidon"), "I drive Slowely", "2000", Program.CountryInformations,
        new DateTime(2020, 4, 1, 20, 50, 0), true, false, false, true, true, 4, 4, 30, FakeMap());
        r.Passengers = FakePassengers();
        return r;
    }
    public static List<Ride> FakeRides() {
        List<Ride> rides = new List<Ride>();
        Driver p = new Driver("wdedwed", "Awesome", "LASTY", new DateTime(1990, 10, 10), "adel@adel.adel", "passwordyy", "Nabatyeh", "Lebanon", FakeProfileImage(), true, null, 4.0f, FakeCars());
        Driver c = new Driver("wdedwed", "adel", "LASTY", new DateTime(1990, 10, 10), "adel@adel.adel", "passwordyy", "Nabatyeh", "Lebanon", FakeProfileImage(), true, null, 4.0f, FakeCars());
        rides.Add(FakeNewRide(p, FakeCars()[0]));
        rides.Add(FakeNewRide(p, FakeCars()[0]));
        rides.Add(FakeNewRide(c, FakeCars()[0]));
        rides.Add(FakeNewRide(p, FakeCars()[0]));
        rides.Add(FakeNewRide(p, FakeCars()[0]));
        rides.Add(FakeNewRide(c, FakeCars()[0]));
        rides.Add(FakeNewRide(p, FakeCars()[0]));
        rides.Add(FakeNewRide(c, FakeCars()[0]));
        return rides;
    }
    public static Ride FakeNewRide(Driver driver, Car c) {
        Ride r = new Ride(driver, c, new Location("fakeid", "Nabatyeh"), new Location("fakeid", "Sidon"), "I drive Slowely", "2000", Program.CountryInformations,
        new DateTime(2020, 4, 1, 20, 50, 0), true, false, false, true, true, 4, 4, 30, FakeMap());
        r.Passengers = FakePassengers();
        return r;
    }
    private static List<Passenger> FakePassengers() {
        List<Passenger> passengers = new List<Passenger>(4);
        passengers.Add(FakePassenger());
        passengers.Add(FakePassenger());
        passengers.Add(FakePassenger());
        passengers.Add(FakePassenger());
        return passengers;
    }

    public static List<Rate> FakeRates(Driver target) {
        List<Rate> rates = new List<Rate>(10);
        rates.Add(new Rate(4, "I would like to drive with Awesome again", new DateTime(2020, 4, 1), FakeReviewer(), FakeNewRide(target, target.Cars[0]), target));
        rates.Add(new Rate(5, "Awesome was a good driver again", new DateTime(2020, 4, 2), FakeReviewer(), FakeNewRide(target, target.Cars[0]), target));
        rates.Add(new Rate(5, "OMG! Awesome is so exciting!", new DateTime(2020, 4, 3), FakeReviewer(), FakeNewRide(target, target.Cars[2]), target));
        rates.Add(new Rate(5, "Awesome has many cars!!!", new DateTime(2020, 4, 4), FakeReviewer(), FakeNewRide(target, target.Cars[3]), target));
        return rates;
    }
    public static List<Rate> FakeNewRates(Person target) {
        List<Rate> rates = new List<Rate>(10);
        rates.Add(new Rate(4, "I would like to drive with Awesome again", new DateTime(2020, 4, 1), FakeReviewer(), target));
        rates.Add(new Rate(5, "Awesome was a good driver again", new DateTime(2020, 4, 2), FakeReviewer(), target));
        rates.Add(new Rate(5, "OMG! Awesome is so exciting!", new DateTime(2020, 4, 3), FakeReviewer(), target));
        rates.Add(new Rate(5, "Awesome has many cars!!!", new DateTime(2020, 4, 4), FakeReviewer(), target));
        return rates;
    }
    public static Person FakeUser() {
        Driver p = new Driver("646C55E6-CEE8-BFA7-FFFF-B2047EF74C00", "Awesome", "LASTY", new DateTime(1990, 10, 10), "adel@adel.adel", "passwordyy", "Nabatyeh", "Lebanon", FakeProfileImage(), true, null, 4.0f, FakeCars());
        p.Bio = "I like to travel around lebanese mountains and roads, I like to eat albeni and lahme mad2ou2a";
        p.CountryInformations = new CountryInformations();
        p.CountryInformations.Name = "Lebanon";
        p.Chattiness = "I talk depending on my mood";
        p.UpcomingRides = FakeUpcomingRides(p);
        p.IsDriver = true;
        return p;
    }

    private static List<Ride> FakeUpcomingRides(Driver p) {
        List<Ride> rides = new List<Ride>(3);
        rides.Add(FakeRide(p, p.Cars[0]));
        rides.Add(FakeRide(p, p.Cars[1]));
        return rides;
    }

    public static Driver FakeNewDriver() {
        Person pr = new Person("wdedwed", "Jaafar", "Ali", new DateTime(1990, 10, 10), "adel@adel.adel", "passwordyy", "Lebanon", FakeProfileImage(), true, null, 4.0f);
        Driver p = new Driver(pr, FakeCars(), FakeSchedule((Driver)pr));
        p.Bio = "I like to travel around lebanese mountains and roads, I like to eat albeni and lahme mad2ou2a";
        p.Rates = FakeRates(p);
        p.CountryInformations = new CountryInformations();
        p.CountryInformations.Name = "Lebanon";
        p.UpcomingRides = FakeUpcomingRides(p);
        return p;
    }

    public static Person FakeNewUser() {
        Person p = new Person("wdedwed", "Jaafar", "Ali", new DateTime(1990, 10, 10), "adel@adel.adel", "passwordyy", "Lebanon", FakeProfileImage(), true, null, 4.0f);
        p.Bio = "I like to travel around lebanese mountains and roads, I like to eat albeni and lahme mad2ou2a";
        p.RateAverage = 3;
        return p;
    }
    public static Passenger FakePassenger() {
        Person p = new Person("wdedwed", "Awesome", "LASTY", new DateTime(1990, 10, 10), "015223753292", "passwordyy", "Lebanon", FakeProfileImage(), true, null, 4.0f);
        p.Bio = "I like to travel around lebanese mountains and roads, I like to eat albeni and lahme mad2ou2a";
        p.Rates = FakeNewRates(p);
        p.Chattiness = "I'm the quit person!";
        p.RateAverage = 3;
        Passenger passenger = new Passenger(p, 4);
        return passenger;
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

    public static Person User { get => user; set => user = value; }
    public static bool IsLoggedIn { get => isLoggedIn; set => isLoggedIn = value; }
    public static CountryInformations CountryInformations { get => countryInformations; set => countryInformations = value; }
    public static Dictionary<string, CountryInformations> CountriesInformations { get => countriesInformations; set => countriesInformations = value; }
    public static List<string> CountriesInformationsNames { get => countriesInformationsNames; set => countriesInformationsNames = value; }

    public static int MaxSchedulesPerUser { get => maxSchedulesPerUser; }

    public static string AppName => appName;

    public static FirebaseApp FirebaseApp { get => firebaseApp; set => firebaseApp = value; }
    public static string UserToken { get => userToken; set => userToken = value; }

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
    public static void Logout() {
        Backendless.UserService.Logout();
        IsLoggedIn = false;
    }
}