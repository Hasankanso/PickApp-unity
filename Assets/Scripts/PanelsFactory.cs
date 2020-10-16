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
    public LoginRegisterPanel loginRegisterPanel;
    public NotificationsPanel notificationsPanel;
    public LocationsFinderPanel locationResultsPanel;
    public RideResultsPanel rideResultsPanel;
    public MyRidesHistoryPanel myRidesHistory;
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
    public Spinner spinner;
    public SeatsLuggagePanel seatsLuggagePanel;
    public DialogBox dialogBox;
    public CarDetails carDetails;
    public DirectionsFinderPanel directionsFinderPanel;
    public ImageViewPanel imageViewer;
    public NotificationScript notificationScript;
    public RideDetails rideDetails;
    public CarsListPanel carsListPanel;
    public PhonePanel phonePanel;
    public YesNoDialog yesNoDialog;

    private void Start() {
        defaultPanelsFactory = this;
    }

    internal static Panel CreateImageViewer(Texture image) {
        ImageViewPanel panel = Instantiate(defaultPanelsFactory.imageViewer);
        panel.Init(image);
        return panel;
    }
    public static NotificationScript CreateNotificationScript() {
        NotificationScript panel = Instantiate(defaultPanelsFactory.notificationScript);
        return panel;
    }

    public static PhonePanel CreatePhonePanel() {
        PhonePanel phonePanel = Instantiate(defaultPanelsFactory.phonePanel);
        return phonePanel;
    }
    public static LoginRegisterPanel CreateLoginRegisterPanel() {
        LoginRegisterPanel loginRegister = Instantiate(defaultPanelsFactory.loginRegisterPanel);
        return loginRegister;
    }
    public static SeatsLuggagePanel CreateSeatsLuggagePanel() {
        SeatsLuggagePanel seatsLuggagePanel = Instantiate(defaultPanelsFactory.seatsLuggagePanel);
        return seatsLuggagePanel;
    }
    public static LoginPanel CreateLogin() {
        LoginPanel loginPanel = Instantiate(defaultPanelsFactory.loginPanel);
        return loginPanel;
    }
    public static CarsListPanel CreateCarsListPanel() {
        CarsListPanel clPanel = Instantiate(defaultPanelsFactory.carsListPanel);
        return clPanel;
    }
    public static BecomeDriver CreateBecomeDriver() {
        BecomeDriver becomeDriver = Instantiate(defaultPanelsFactory.becomeDriver);
        return becomeDriver;
    }
    public static CarDetails CreateCarDetails() {
        CarDetails carDetails = Instantiate(defaultPanelsFactory.carDetails);
        return carDetails;
    }
    public static NotificationsPanel CreateNotificationPanel() {
        NotificationsPanel notificationsPanel = Instantiate(defaultPanelsFactory.notificationsPanel);
        return notificationsPanel;
    }
    public static UserRatings CreateUserRatings() {
        UserRatings userRatings = Instantiate(defaultPanelsFactory.userRatings);
        return userRatings;
    }
    public static ChatPanel CreateChat() {
        ChatPanel chatPanel = Instantiate(defaultPanelsFactory.chatPanel);
        return chatPanel;
    }
    public static Spinner CreateSpinner() {
        Spinner spinner = Instantiate(defaultPanelsFactory.spinner);
        return spinner;
    }
    public static RideResultsPanel CreateRideResults() {
        RideResultsPanel ride = Instantiate(defaultPanelsFactory.rideResultsPanel);
        return ride;
    }
    public static MyRidesHistoryPanel CreateMyRidesHistory() {
        MyRidesHistoryPanel myRidesHistoryPanel = Instantiate(defaultPanelsFactory.myRidesHistory);
        return myRidesHistoryPanel;
    }
    public static LocationsFinderPanel CreateLocationsFinderPanel(string initText, Action<Location> OnLocationPicked) {
        LocationsFinderPanel locResultsPanel = Instantiate(defaultPanelsFactory.locationResultsPanel);
        locResultsPanel.Init(initText, OnLocationPicked);
        return locResultsPanel;
    }
    public static DirectionsFinderPanel CreateDirectionFinderPanel() {
        DirectionsFinderPanel dirResultsPanel = Instantiate(defaultPanelsFactory.directionsFinderPanel);
        return dirResultsPanel;
    }
    public static AlertPanel CreateAlert() {
        AlertPanel alertPanel = Instantiate(defaultPanelsFactory.alertPanel);
        return alertPanel;
    }
    public static ReportUserPanel CreateReportUser() {
        ReportUserPanel reportUserPanel = Instantiate(defaultPanelsFactory.reportUserPanel);
        return reportUserPanel;
    }
    public static RegisterPanel CreateRegister() {
        RegisterPanel register = Instantiate(defaultPanelsFactory.registerPanel);
        return register;
    }
    public static SettingsPanel CreateSettings() {
        SettingsPanel settingsPanel = Instantiate(defaultPanelsFactory.settingsPanel);
        return settingsPanel;
    }
    public static HowItWorksPanel CreateHowItWorks() {
        HowItWorksPanel howItWorks = Instantiate(defaultPanelsFactory.howItWorks);
        return howItWorks;
    }
    public static SchedulePanel CreateAddSchedule() {
        SchedulePanel schedulePanel = Instantiate(defaultPanelsFactory.schedulePanel);
        return schedulePanel;
    }

    internal static YesNoDialog CreateYesNoDialog() {
        YesNoDialog yesNoDialog = Instantiate(defaultPanelsFactory.yesNoDialog);
        return yesNoDialog;
    }

    public static TermsConditionsPanel CreateTermsConditions() {
        TermsConditionsPanel termsConditions = Instantiate(defaultPanelsFactory.termsConditions);
        return termsConditions;
    }

    public static PrivacyPolicyPanel CreatePrivacyPolicy() {
        PrivacyPolicyPanel privacyPolicy = Instantiate(defaultPanelsFactory.privacyPolicy);
        return privacyPolicy;
    }
    public static UserDetails CreateUserDetails() {
        UserDetails userDetails = Instantiate(defaultPanelsFactory.userDetails);
        return userDetails;
    }
    public static AccountPanel CreateAccountPanel() {
        AccountPanel accountPanel = Instantiate(defaultPanelsFactory.accountPanel);
        return accountPanel;
    }

    public static BioPanel CreateBioPanel() {
        BioPanel bioPanel = Instantiate(defaultPanelsFactory.bioPanel);
        return bioPanel;
    }

    public static ChattinessPanel CreateChattinessPanel() {
        ChattinessPanel chattinessPanel = Instantiate(defaultPanelsFactory.chattinessPanel);
        return chattinessPanel;
    }
    public static AddCarPanel CreateAddCar() {
        AddCarPanel addCarPanel = Instantiate(defaultPanelsFactory.addCar);
        return addCarPanel;
    }
    public static LicensesPanel CreateLicenses() {
        LicensesPanel licenses = Instantiate(defaultPanelsFactory.licenses);
        return licenses;
    }
    public static ContactUsPanel CreateContactUs() {
        ContactUsPanel contactUs = Instantiate(defaultPanelsFactory.contactUs);
        return contactUs;
    }

    //to add, view, edit and remove Ride
    public static RideDetails CreateRideDetails() {
        RideDetails rideDetails = Instantiate(defaultPanelsFactory.rideDetails);
        return rideDetails;
    }
    public static DialogBox CreateDialogBox(string message, bool isSuccess) {
        DialogBox dialogBox = Instantiate(defaultPanelsFactory.dialogBox);
        dialogBox.Init(message, isSuccess);
        return dialogBox;
    }

}
