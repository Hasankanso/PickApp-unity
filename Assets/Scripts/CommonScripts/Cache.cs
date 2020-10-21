using System;
using System.Collections.Generic;
using UnityEngine;

public static class Cache {
    private static readonly string currentLanguageKey = "LANG_CURR";
    private static readonly string defaultLanguage = "";

    public static bool GetNewsCheckbox() {
        return bool.Parse(PlayerPrefs.GetString("news", "true"));
    }
    public static bool GetDisableAllCheckbox() {
        return bool.Parse(PlayerPrefs.GetString("disableAll", "true"));
    }
    public static void SetDisableAllCheckbox(bool disableAll) {
        PlayerPrefs.SetString("disableAll", disableAll.ToString());
    }
    public static void SetNewsCheckbox(bool news) {
        PlayerPrefs.SetString("news", news.ToString());
    }

    public static void SetLanguage(string value) {
        PlayerPrefs.SetString(currentLanguageKey, value);
    }
    public static string GetLanguage() {
        return PlayerPrefs.GetString(currentLanguageKey, "English");
    }
    public static void SetEmail(string email) {
        PlayerPrefs.SetString("email", email);
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

    //User cache
    public static void SetUser(User user) {
        if (user != null) {
            if (user.Person != null)
                PlayerPrefs.SetString("phoneCode", user.Person.CountryInformations.Code.Split(new string[] { "+" }, StringSplitOptions.None)[1]);
            if (user.phone != null) {
                PlayerPrefs.SetString("phone", user.phone.Split(new string[] { user.Person.CountryInformations.Code }, StringSplitOptions.None)[1]);
            }
            if (user.Email != null)
                SetEmail(user.Email);
            if (user.Id != null)
                SetUserId(user.Id);
            SetPerson(user.Person);
            if (user.Driver != null) {
                SetDriver(user.Driver);
            }
        }
    }
    public static User GetUser() {
        return new User(GetPerson(), GetDriver(), "+" + GetPhoneCode() + GetPhone(), GetEmail(), GetUserId(), null);
    }
    public static void NullifyUser() {
        PlayerPrefs.SetString("phoneCode", "");
        PlayerPrefs.SetString("phone", "");
        SetEmail("");
        SetUserId("");
        NullifyPerson();
        NullifyDriver();
    }

    //Person cache
    public static Person GetPerson() {
        //get person from cache
        Person person = new Person();
        person.Id = PlayerPrefs.GetString("personId", "");
        person.FirstName = PlayerPrefs.GetString("firstName", "");
        person.LastName = PlayerPrefs.GetString("lastName", "");
        person.Bio = PlayerPrefs.GetString("bio", "");
        person.Chattiness = PlayerPrefs.GetString("chattiness", "");
        person.Birthday = Program.StringToBirthday(PlayerPrefs.GetString("birthday", ""));
        person.Gender = bool.Parse(PlayerPrefs.GetString("gender", ""));
        person.ProfilePictureUrl = PlayerPrefs.GetString("profilePictureUrl", "");
        person.RateAverage = float.Parse(PlayerPrefs.GetString("rateAverage", ""));
        person.AcomplishedRides = int.Parse(PlayerPrefs.GetString("acomplishedRides", ""));
        person.CanceledRides = int.Parse(PlayerPrefs.GetString("canceledRides", ""));
        person.RateCount = int.Parse(PlayerPrefs.GetString("rateCount", ""));
        //get country from cache
        CountryInformations countryInformations = new CountryInformations();
        countryInformations.Id = PlayerPrefs.GetString("countryInformationsId", "");
        countryInformations.Unit = PlayerPrefs.GetString("unit", "");
        countryInformations.Name = PlayerPrefs.GetString("countryName", "");
        countryInformations.Digits = int.Parse(PlayerPrefs.GetString("digits", ""));
        countryInformations.Code = PlayerPrefs.GetString("code", "");

        person.CountryInformations = countryInformations;

        return person;
    }
    public static void SetPerson(Person person) {
        if (person != null) {
            if (person.Id != null)
                PlayerPrefs.SetString("personId", person.Id);
            if (person.FirstName != null)
                PlayerPrefs.SetString("firstName", person.FirstName);
            if (person.LastName != null)
                PlayerPrefs.SetString("lastName", person.LastName);
            if (person.Bio != null)
                PlayerPrefs.SetString("bio", person.Bio);
            if (person.Chattiness != null)
                PlayerPrefs.SetString("chattiness", person.Chattiness);

            if (person.Birthday != null)
                PlayerPrefs.SetString("birthday", Program.BirthdayToString(person.Birthday));

            PlayerPrefs.SetString("gender", person.Gender.ToString());
            PlayerPrefs.SetString("rateAverage", person.RateAverage.ToString());
            PlayerPrefs.SetString("acomplishedRides", person.AcomplishedRides.ToString());
            PlayerPrefs.SetString("canceledRides", person.CanceledRides.ToString());
            PlayerPrefs.SetString("rateCount", person.RateCount.ToString());

            if (person.ProfilePictureUrl != null)
                PlayerPrefs.SetString("profilePictureUrl", person.ProfilePictureUrl);
            if (person.CountryInformations != null) {
                if (person.CountryInformations.Id != null)
                    PlayerPrefs.SetString("countryInformationsId", person.CountryInformations.Id);
                if (person.CountryInformations.Unit != null)
                    PlayerPrefs.SetString("unit", person.CountryInformations.Unit);
                if (person.CountryInformations.Code != null)
                    PlayerPrefs.SetString("code", person.CountryInformations.Code);
                PlayerPrefs.SetString("digits", person.CountryInformations.Digits.ToString());
            }
        }
    }
    public static void NullifyPerson() {
        PlayerPrefs.SetString("personId", "");
        PlayerPrefs.SetString("firstName", "");
        PlayerPrefs.SetString("lastName", "");
        PlayerPrefs.SetString("bio", "");
        PlayerPrefs.SetString("chattiness", "");

        PlayerPrefs.SetString("birthday", "");

        PlayerPrefs.SetString("gender", "");
        PlayerPrefs.SetString("rateAverage", "");
        PlayerPrefs.SetString("acomplishedRides", "");
        PlayerPrefs.SetString("canceledRides", "");
        PlayerPrefs.SetString("rateCount", "");

        PlayerPrefs.SetString("profilePictureUrl", "");
        PlayerPrefs.SetString("countryInformationsId", "");
        PlayerPrefs.SetString("unit", "");
        PlayerPrefs.SetString("code", "");
        PlayerPrefs.SetString("digits", "");
    }
    //Driver
    public static void SetDriver(Driver d) {
        PlayerPrefs.SetString("driverId", d.Id);
        SetRegions(d.Regions);
        SetCars(d.Cars);
    }
    public static Driver GetDriver() {
        string driverId = PlayerPrefs.GetString("driverId", "");
        if (string.IsNullOrEmpty(driverId)) {
            return null;
        }
        Driver d = new Driver(driverId, GetCars(), GetRegions());
        return d;
    }
    //Driver's regions
    public static void SetRegions(List<Location> location) {
        PlayerPrefs.SetString("regionsCount", location.Count.ToString());
        for (int i = 0; i < location.Count; i++) {
            PlayerPrefs.SetString("regionName" + i, location[i].Name);
            PlayerPrefs.SetString("regionPlaceId" + i, location[i].PlaceId);
            PlayerPrefs.SetString("regionLatitude" + i, location[i].Latitude.ToString());
            PlayerPrefs.SetString("regionLongitude" + i, location[i].Longitude.ToString());
        }
    }
    public static List<Location> GetRegions() {
        List<Location> locations = new List<Location>();
        int regionsCount = int.Parse(PlayerPrefs.GetString("regionsCount", "0"));
        for (int i = 0; i < regionsCount; i++) {
            var name = PlayerPrefs.GetString("regionName" + i, "");
            var placeId = PlayerPrefs.GetString("regionPlaceId" + i, "");
            var latit = PlayerPrefs.GetString("regionLatitude" + i, "");
            var longi = PlayerPrefs.GetString("regionLongitude" + i, "");
            locations.Add(new Location(placeId, name, double.Parse(latit), double.Parse(longi)));
        }
        return locations;
    }
    //Driver's cars
    public static void SetCars(List<Car> cars) {
        PlayerPrefs.SetString("carsCount", cars.Count.ToString());
        for (int i = 0; i < cars.Count; i++) {
            PlayerPrefs.SetString("carId" + i, cars[i].Id);
            PlayerPrefs.SetString("carYear" + i, cars[i].Year.ToString());
            PlayerPrefs.SetString("carMaxLuggage" + i, cars[i].MaxLuggage.ToString());
            PlayerPrefs.SetString("carMaxSeats" + i, cars[i].MaxSeats.ToString());
            PlayerPrefs.SetString("carName" + i, cars[i].Name);
            PlayerPrefs.SetString("carColor" + i, cars[i].Color);
            PlayerPrefs.SetString("carBrand" + i, cars[i].Brand);
            PlayerPrefs.SetString("carPictureUrl" + i, cars[i].CarPictureUrl);
        }
    }
    public static List<Car> GetCars() {
        List<Car> cars = new List<Car>();
        int carsCount = int.Parse(PlayerPrefs.GetString("carsCount", "0"));
        for (int i = 0; i < carsCount; i++) {
            var id = PlayerPrefs.GetString("carId" + i, "");
            var year = int.Parse(PlayerPrefs.GetString("carYear" + i, ""));
            var maxLuggage = int.Parse(PlayerPrefs.GetString("carMaxLuggage" + i, ""));
            var maxSeats = int.Parse(PlayerPrefs.GetString("carMaxSeats" + i, ""));
            var name = PlayerPrefs.GetString("carName" + i, "");
            var color = PlayerPrefs.GetString("carColor" + i, "");
            var brand = PlayerPrefs.GetString("carBrand" + i, "");
            var picture = PlayerPrefs.GetString("carPictureUrl" + i, "");
            cars.Add(new Car(id, name, year, maxLuggage, maxSeats, brand, color, picture));
        }
        return cars;
    }
    public static void NullifyDriver() {
        PlayerPrefs.SetString("driverId", "");
        int regionsCount = int.Parse(PlayerPrefs.GetString("regionsCount", "0"));
        for (int i = 0; i < regionsCount; i++) {
            PlayerPrefs.SetString("regionName" + i, "");
            PlayerPrefs.SetString("regionPlaceId" + i, "");
            PlayerPrefs.SetString("regionLatitude" + i, "");
            PlayerPrefs.SetString("regionLongitude" + i, "");
        }
        PlayerPrefs.SetString("regionsCount", "");
        int carsCount = int.Parse(PlayerPrefs.GetString("carsCount", "0"));
        for (int i = 0; i < carsCount; i++) {
            PlayerPrefs.SetString("carId" + i, "");
            PlayerPrefs.SetString("carYear" + i, "");
            PlayerPrefs.SetString("carMaxLuggage" + i, "");
            PlayerPrefs.SetString("carMaxSeats" + i, "");
            PlayerPrefs.SetString("carName" + i, "");
            PlayerPrefs.SetString("carColor" + i, "");
            PlayerPrefs.SetString("carBrand" + i, "");
            PlayerPrefs.SetString("carPictureUrl" + i, "");
        }
        PlayerPrefs.SetString("carsCount", "");
    }
}
