using Firebase;
using Firebase.Auth;
using Requests;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
public class PhonePanel : Panel {
    public InputFieldScript phone, code;
    public Text phoneLabel, resendCodeLabel;
    public Image codeImage;
    public Text countryCode, alreadyRegistredPhone;
    public GameObject firstView, secondView, UserAlreadyExistView;
    private User user = null;
    private static string idToken;
    private static uint timeout = 120000;
    private IEnumerator timer;
    ForceResendingToken token = null;
    string verificationId = "";
    private bool isForceRegister = false;

    public void SendCode() {
        FirebaseAuth firebaseAuth = FirebaseAuth.GetAuth(Program.FirebaseApp);
        PhoneAuthProvider provider = PhoneAuthProvider.GetInstance(firebaseAuth);

        timer = ResendTimer();
        StartCoroutine(timer);
        provider.VerifyPhoneNumber(countryCode.text + phone.text.text, timeout, token,
          verificationCompleted: (credential) => {
              print(credential.ToString());
              Debug.Log("Phone number has been validated!");
          },
          verificationFailed: (error) => {
              OpenDialog(error, false);
              if (timer != null) {
                  StopCoroutine(timer);
                  resendCodeLabel.GetComponent<Button>().interactable = true;
                  resendCodeLabel.text = "Resend code";
                  resendCodeLabel.color = new Color(255f / 255f, 188f / 255f, 66f / 255f);
                  codeImage.color = new Color(255f / 255f, 188f / 255f, 66f / 255f);
              }
          },
          codeSent: (id, token) => {
              OpenDialog("Sms has been sent!", true);
              verificationId = id;
              this.token = token;
          },
            codeAutoRetrievalTimeOut: (id) => {
                verificationId = id;
                OpenDialog("Make sure your phone number is correct!", false);
            });
    }

    void FirebaseVerification() {
        FirebaseAuth firebaseAuth = FirebaseAuth.GetAuth(Program.FirebaseApp);
        PhoneAuthProvider provider = PhoneAuthProvider.GetInstance(firebaseAuth);
        Credential credential = provider.GetCredential(verificationId, code.text.text);

        firebaseAuth.SignInWithCredentialAsync(credential).ContinueWith(task => {
            if (task.IsFaulted)
                OpenDialog(task.Exception.ToString(), false);

            FirebaseUser newUser = task.Result;
            newUser.TokenAsync(true).ContinueWith(task2 => {
                if (task2.IsCanceled) {
                    OpenDialog("TokenAsync was canceled.", false);
                    Debug.LogError("TokenAsync was canceled.");
                    return;
                } else if (task2.IsFaulted) {
                    OpenDialog("TokenAsync encountered an error: ", false);
                    Debug.LogError("TokenAsync encountered an error: " + task.Exception);
                    return;
                }
                idToken = task2.Result;
                if (!isForceRegister) {
                    Request<User> registerRequest = new RegisterPerson(user, idToken);
                    registerRequest.AddSendListener(OpenSpinner);
                    registerRequest.AddReceiveListener(CloseSpinner);
                    registerRequest.Send(RegistrationResponse);
                    return;
                } else {
                    Request<User> registerRequest = new ForceRegisterPerson(user, idToken);
                    registerRequest.AddSendListener(OpenSpinner);
                    registerRequest.AddReceiveListener(CloseSpinner);
                    registerRequest.Send(RegistrationResponse);
                    return;
                }
            });
            return;
        });
    }
    public void OpenLoginPanel() {
        LoginPanel p = PanelsFactory.CreateLogin();
        Open(p, () => { p.Init(false); });
    }

    private void RegistrationResponse(User user, int statusCode, string message) {
        if (statusCode != (int)HttpStatusCode.OK) {
            OpenDialog(message, false);
        } else {
            Program.User = user;
            Cache.User(user);
            Program.IsLoggedIn = true;
            FooterMenu.dFooterMenu.OpenSearchPanel();
            OpenDialog("Welcome to PickApp!", true);
            DestroyForwardBackward();
        }
    }

    IEnumerator ResendTimer() {
        float counter = timeout / 1000f; //convert from milliseconds to seconds.

        resendCodeLabel.GetComponent<Button>().interactable = false;
        resendCodeLabel.color = new Color(186f / 255f, 186f / 255f, 186f / 255f, 128f / 255f);
        codeImage.color = new Color(168f / 255f, 168f / 255f, 168f / 255f, 128f / 255f);
        int minutes = 0;
        int seconds = 0;

        while (counter >= 0) {
            counter -= Time.deltaTime;
            minutes = Mathf.FloorToInt(counter / 60f);
            seconds = Mathf.RoundToInt(counter % 60f);
            resendCodeLabel.text = "Resend code in " + minutes + ":" + seconds;
            yield return null;
        }

        resendCodeLabel.GetComponent<Button>().interactable = true;
        resendCodeLabel.text = "Resend code";
        resendCodeLabel.color = new Color(255f / 255f, 188f / 255f, 66f / 255f);
        codeImage.color = new Color(255f / 255f, 188f / 255f, 66f / 255f);
    }
    public void CheckIfUserExist() {
        if (vadilateFirstView()) {
            User checkUser = new User {
                Phone = countryCode.text + phone.text.text
            };
            Request<bool> registerRequest = new CheckUserExist(checkUser);
            registerRequest.AddSendListener(OpenSpinner);
            registerRequest.AddReceiveListener(CloseSpinner);
            registerRequest.Send(CheckUserExistResponse);
        }
    }

    private void CheckUserExistResponse(bool userExist, int statusCode, string message) {
        if (statusCode != (int)HttpStatusCode.OK) {
            OpenDialog(message, false);
        } else {
            firstView.SetActive(false);
            if (userExist == true) {
                UserAlreadyExistView.SetActive(true);
                alreadyRegistredPhone.text = "User " + countryCode.text + phone.text.text + " already registred, " +
                "if it's not your account you can skip it.";
            } else {
                openView(1);
            }
        }
    }
    public void Skip() {
        isForceRegister = true;
        openView(1);
    }
    public void Register() {
        if (vadilateSecondView()) {
            user.Phone = phone.text.text;
            FirebaseVerification();
        }
    }
    public void openView(int index) {
        firstView.SetActive(false);
        secondView.SetActive(false);
        UserAlreadyExistView.SetActive(false);
        if (index == 0) {
            firstView.SetActive(true);
        } else if (index == 1 && vadilateFirstView()) {
            SendCode();
            secondView.SetActive(true);
            phoneLabel.text = phone.text.text;
        }
    }
    public void closeView(int index) {
        firstView.SetActive(false);
        secondView.SetActive(false);
        phone.PlaceHolder();
        code.PlaceHolder();
        if (index == 0) {
            firstView.SetActive(true);
        } else if (index == 1)
            secondView.SetActive(true);
    }

    bool vadilateFirstView() {
        if (phone.text.text.Equals("")) {
            phone.Error();
            OpenDialog("Please enter your phone number", false);
            return false;
        }
        if (Regex.Matches(phone.text.text, @"[a-zA-Z]").Count > 0) {
            phone.Error();
            OpenDialog("Your phone number is invalid", false);
            return false;
        }
        if (phone.text.text.Length != user.Person.CountryInformations.Digits) {
            phone.Error();
            OpenDialog("Your phone number is invalid", false);
            return false;
        }
        return true;
    }
    bool vadilateSecondView() {
        string givenCode = code.text.text;
        print(givenCode);
        if (givenCode.Equals("") || givenCode.Length != 6) {
            code.Error();
            return false;
        }
        return true;
    }
    public void Init(User user) {
        Clear();
        this.user = user;
        CountryInformations country = user.Person.CountryInformations;
        phone.GetComponentInParent<InputField>().characterLimit = country.Digits;
        countryCode.text = country.Code;
    }
    internal override void Clear() {
        phone.Reset();
        isForceRegister = false;
        if (timer != null) {
            StopCoroutine(timer);
        }
        UserAlreadyExistView.SetActive(false);
        openView(0);
    }
}