using Requests;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.UI;
using static Panel;

public class PanelsFactory : MonoBehaviour {
    private static PanelsFactory defaultPanelsFactory;
    public NotificationsPanel notificationsPanel;
    public LocationsFinderPanel locationResultsPanel;
    public RideResultsPanel rideResultsPanel;
    public BookingHistoryPanel bookingHistory;
    public HowItWorksPanel howItWorks;
    public PrivacyPolicyPanel privacyPolicy;
    public TermsConditionsPanel termsConditions;
    public ContactUsPanel contactUs;
    public LicensesPanel licenses;
    public UserRatings userRatings;
    public LoginPanel loginPanel;
    public AlertPanel alertPanel;
    public ChatPanel chatPanel;
    public SearchPanel searchPanel;
    public BecomeDriver becomeDriver;
    public ProfilePanel profilePanel;
    public SchedulePanel schedulePanel;
    public RegisterPanel registerPanel;
    public ReportUserPanel reportUserPanel;
    public SettingsPanel settingsPanel;
    public UserDetails userDetails;
    public AddCarPanel addCar;
    public BioPanel bioPanel;
    public ChattinessPanel chattinessPanel;
    public AccountPanel accountPanel;
    public DialogBox dialogBox;
    public CarDetails carDetails;
    public DirectionsFinderPanel directionsFinderPanel;
    public ChangePasswordPanel changePassword;
    public ImageViewPanel imageViewer;


    public RideDetails rideDetails;
    public CarsListPanel carsListPanel;
    public PhonePanel phonePanel;


    private void Start() {
        defaultPanelsFactory = this;
    }

    private void hellobiatch(Ride arg1, HttpStatusCode arg2, string arg3) {
        throw new NotImplementedException();
    }

    internal static Panel CreateImageViewer(Texture image) {
        ImageViewPanel panel = Instantiate(defaultPanelsFactory.imageViewer);
        panel.Init(image);
        return panel;
    }

    public static PhonePanel createPhonePanel() {
        PhonePanel phonePanel = Instantiate(defaultPanelsFactory.phonePanel);
        phonePanel.Init();
        return phonePanel;
    }
    public static PhonePanel CreatePhonePanel(User user) {
        PhonePanel phonePanel = Instantiate(defaultPanelsFactory.phonePanel);
        phonePanel.Init(user);
        return phonePanel;
    }
    public static LoginPanel createLogin(bool isFromFooter) {
        LoginPanel loginPanel = Instantiate(defaultPanelsFactory.loginPanel);
        loginPanel.Init(isFromFooter);
        return loginPanel;
    }
    public static CarsListPanel CreateCarsListPanel(Action<Car> OnCarPicked, Car chosenCar) {
        CarsListPanel clPanel = Instantiate(defaultPanelsFactory.carsListPanel);
        clPanel.Init(OnCarPicked, chosenCar);
        return clPanel;
    }
    public static BecomeDriver CreateBecomeDriver(Person person) {
        BecomeDriver becomeDriver = Instantiate(defaultPanelsFactory.becomeDriver);
        becomeDriver.Init(person);
        return becomeDriver;
    }
    public static BecomeDriver CreateBecomeDriver() {
        BecomeDriver becomeDriver = Instantiate(defaultPanelsFactory.becomeDriver);
        becomeDriver.Init();
        return becomeDriver;
    }
    public static CarDetails CreateCarDetails(Car car) {
        CarDetails carDetails = Instantiate(defaultPanelsFactory.carDetails);
        carDetails.Init(car);
        return carDetails;
    }
    public static NotificationsPanel createNotificationPanel() {
        NotificationsPanel notificationsPanel = Instantiate(defaultPanelsFactory.notificationsPanel);
        notificationsPanel.Init();
        return notificationsPanel;
    }
    public static UserRatings CreateUserRatings(User user) {
        UserRatings userRatings = Instantiate(defaultPanelsFactory.userRatings);
        userRatings.Init(user);
        return userRatings;
    }
    public static ChatPanel createChat(Chat chat) {
        ChatPanel chatPanel = Instantiate(defaultPanelsFactory.chatPanel);
        chatPanel.Init(chat);
        return chatPanel;
    }
    public static ChatPanel createChat(Person person) {
        ChatPanel chatPanel = Instantiate(defaultPanelsFactory.chatPanel);
        chatPanel.Init(person);
        return chatPanel;
    }
    public static RideResultsPanel CreateRideResults(List<Ride> rides, SearchInfo searchInfo) {
        RideResultsPanel ride = Instantiate(defaultPanelsFactory.rideResultsPanel);
        ride.Init(rides, searchInfo);
        return ride;
    }
    public static BookingHistoryPanel CreateBookingHistory() {
        BookingHistoryPanel bookingHistoryPanel = Instantiate(defaultPanelsFactory.bookingHistory);
        bookingHistoryPanel.Init();
        return bookingHistoryPanel;
    }

    public static AlertPanel createAlert(SearchInfo searchInfo) {
        AlertPanel alertPanel = Instantiate(defaultPanelsFactory.alertPanel);
        alertPanel.init(searchInfo);
        return alertPanel;
    }

    public static LocationsFinderPanel CreateLocationsFinderPanel(string initText, Action<Location> OnLocationPicked) {
        LocationsFinderPanel locResultsPanel = Instantiate(defaultPanelsFactory.locationResultsPanel);
        locResultsPanel.Init(initText, OnLocationPicked);
        return locResultsPanel;
    }

    public static DirectionsFinderPanel CreateDirectionFinderPanel(Texture2D map, string origin, string destination, bool alternatives, Action<Texture2D> OnItemPicked) {
        DirectionsFinderPanel dirResultsPanel = Instantiate(defaultPanelsFactory.directionsFinderPanel);
        dirResultsPanel.Init(map, origin, destination, alternatives, OnItemPicked);
        return dirResultsPanel;
    }


    public static AlertPanel createAlert() {
        AlertPanel alertPanel = Instantiate(defaultPanelsFactory.alertPanel);
        return alertPanel;
    }
    public static ReportUserPanel createReportUser(Person person) {
        ReportUserPanel reportUserPanel = Instantiate(defaultPanelsFactory.reportUserPanel);
        reportUserPanel.Init(person);
        return reportUserPanel;
    }
    public static RegisterPanel createRegister() {
        RegisterPanel register = Instantiate(defaultPanelsFactory.registerPanel);
        register.Init();
        return register;
    }

    public static SearchPanel createSearch() {
        SearchPanel searchPanel = Instantiate(defaultPanelsFactory.searchPanel);
        return searchPanel;
    }

    public static SettingsPanel createSettings() {
        SettingsPanel settingsPanel = Instantiate(defaultPanelsFactory.settingsPanel);
        return settingsPanel;
    }
    public static ProfilePanel createProfile() {
        ProfilePanel profilePanel = Instantiate(defaultPanelsFactory.profilePanel);
        return profilePanel;
    }
    public static HowItWorksPanel createHowItWorks() {
        HowItWorksPanel howItWorks = Instantiate(defaultPanelsFactory.howItWorks);
        return howItWorks;
    }
    public static SchedulePanel CreateAddSchedule() {
        SchedulePanel schedulePanel = Instantiate(defaultPanelsFactory.schedulePanel);
        return schedulePanel;
    }
    public static SchedulePanel CreateEditSchedule(ScheduleRide scheduleRide) {
        SchedulePanel schedulePanel = Instantiate(defaultPanelsFactory.schedulePanel);
        schedulePanel.Init(scheduleRide);
        return schedulePanel;
    }

    public static TermsConditionsPanel createTermsConditions() {
        TermsConditionsPanel termsConditions = Instantiate(defaultPanelsFactory.termsConditions);
        return termsConditions;
    }

    public static PrivacyPolicyPanel CreatePrivacyPolicy() {
        PrivacyPolicyPanel privacyPolicy = Instantiate(defaultPanelsFactory.privacyPolicy);
        return privacyPolicy;
    }

    public static UserDetails CreateUserDetails(User person) {
        UserDetails userDetails = Instantiate(defaultPanelsFactory.userDetails);
        userDetails.Init(person);
        return userDetails;
    }

    public static AccountPanel CreateAccountPanel() {
        AccountPanel accountPanel = Instantiate(defaultPanelsFactory.accountPanel);
        accountPanel.Init();
        return accountPanel;
    }

    public static BioPanel CreateBioPanel() {
        BioPanel bioPanel = Instantiate(defaultPanelsFactory.bioPanel);
        bioPanel.Init();
        return bioPanel;
    }

    public static ChattinessPanel CreateChattinessPanel() {
        ChattinessPanel chattinessPanel = Instantiate(defaultPanelsFactory.chattinessPanel);
        return chattinessPanel;
    }

    public static AddCarPanel createEditCar(Car car) {
        AddCarPanel addCarPanel = Instantiate(defaultPanelsFactory.addCar);
        addCarPanel.init(car);
        return addCarPanel;
    }

    public static AddCarPanel createAddCar() {
        AddCarPanel addCarPanel = Instantiate(defaultPanelsFactory.addCar);
        addCarPanel.init();
        return addCarPanel;
    }

    public static LicensesPanel createLicenses() {
        LicensesPanel licenses = Instantiate(defaultPanelsFactory.licenses);
        return licenses;
    }

    public static ContactUsPanel createContactUs() {
        ContactUsPanel contactUs = Instantiate(defaultPanelsFactory.contactUs);
        return contactUs;
    }


    //to add, view, edit and remove Ride
    public static RideDetails CreateRideDetails(Ride ride, bool owner, Status status) {
        Debug.Log(
   status);
        RideDetails rideDetails = Instantiate(defaultPanelsFactory.rideDetails);
        rideDetails.Init(ride, owner, status);
        return rideDetails;
    }

    //to add, edit and remove Schedule
    public static RideDetails CreateScheduleDetails(ScheduleRide schedule, Status status) {
        RideDetails rideDetails = Instantiate(defaultPanelsFactory.rideDetails);
        rideDetails.Init(schedule, status);
        return rideDetails;
    }

    public static DialogBox CreateDialogBox(string message, bool isSuccess) {
        DialogBox dialogBox = Instantiate(defaultPanelsFactory.dialogBox);
        dialogBox.init(message, isSuccess);
        return dialogBox;
    }

    public static ChangePasswordPanel ChangePassword(User user) {
        ChangePasswordPanel changePassword = Instantiate(defaultPanelsFactory.changePassword);
        changePassword.Init(user);
        return changePassword;
    }
    public static ChangePasswordPanel ChangePassword(bool isForgetPassword, User user) {
        ChangePasswordPanel changePassword = Instantiate(defaultPanelsFactory.changePassword);
        changePassword.Init(isForgetPassword, user);
        return changePassword;
    }
}
