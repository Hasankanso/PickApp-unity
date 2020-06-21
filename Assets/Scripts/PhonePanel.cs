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
    public Button nextReset, nextRegister, reset, register;
    public Text countryCode;
    public GameObject firstView, secondView;
    private Person person = null;
    private string secretCode = "";
    private static uint timeout = 120000;
    private IEnumerator timer;
    FirebaseAuth firebaseAuth;
    PhoneAuthProvider provider;

    public void SendCode() {
        firebaseAuth = FirebaseAuth.GetAuth(Program.FirebaseApp);
        provider = PhoneAuthProvider.GetInstance(firebaseAuth);
        timer = ResendTimer();
        StartCoroutine(timer);
        provider.VerifyPhoneNumber(countryCode.text + phone.text.text, timeout, null,
          verificationCompleted: (credential) => {
              OpenDialog("Phone number has been validated!", true);
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
              secretCode = id;
          },
            codeAutoRetrievalTimeOut: (id) => {
                secretCode = id;
                OpenDialog("time out, make sure phone number is correct", false);
                print("time out, make sure phone number is correct");
            });
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


    public void Register() {
        if (vadilateSecondView()) {
            Credential credential = provider.GetCredential(secretCode, code.text.text);
            firebaseAuth.SignInWithCredentialAsync(credential).ContinueWith(task => {
                if (task.IsFaulted) {
                    Debug.LogError("SignInWithCredentialAsync encountered an error: " +
                                   task.Exception);
                    return;
                }
                FirebaseUser newUser = task.Result;
                //person.Phone = phone.text.text;
                //Request<Person> request = new RegisterPerson(person);
                //request.Send(RegisterResponse);
            });
        }
    }
    private void RegisterResponse(Person result, HttpStatusCode code, string message) {
        if (!code.Equals(HttpStatusCode.OK))
            OpenDialog("Error while register", false);
        else {
            OpenDialog("Welcome to PickApp!", true);
            FooterMenu.dFooterMenu.OpenSearchPanel();
        }
    }
    public void ResetPassword() {
        if (vadilateSecondView()) {
            Request<Person> request = new VerifySmsCode(person, code.text.text);
            request.Send(ResetPasswordResponse);
        }
    }

    private void ResetPasswordResponse(Person result, HttpStatusCode code, string message) {
        if (!code.Equals(HttpStatusCode.OK))
            OpenDialog("Wrong Code", false);
        else {
            OpenDialog("Please add a new password!", true);
            Panel panel = PanelsFactory.ChangePassword(true, result);
            openCreated(panel);
        }
    }
    public void openView(int index) {
        firstView.SetActive(false);
        secondView.SetActive(false);
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
        bool valid = true;
        if (phone.text.text.Equals("")) {
            phone.Error();
            OpenDialog("Please enter your phone number", false);
            valid = false;
        }
        if (Regex.Matches(phone.text.text, @"[a-zA-Z]").Count > 0) {
            phone.Error();
            OpenDialog("Your phone number is invalid", false);
            valid = false;
        }
        if (phone.text.text.Length != Program.CountryInformations.Digits) {
            phone.Error();
            OpenDialog("Your phone number is invalid", false);
            valid = false;
        }
        return valid;
    }
    bool vadilateSecondView() {
        bool valid = true;
        string givenCode = code.text.text;
        print(givenCode);
        if (givenCode.Equals("")) {
            code.Error();
            OpenDialog("Invalid code", false);
            valid = false;
        }
        return valid;
    }
    public void Init(Person person) {
        Clear();
        register.gameObject.SetActive(true);
        nextRegister.gameObject.SetActive(true);
        this.person = person;
        phone.GetComponentInParent<InputField>().characterLimit = Program.CountryInformations.Digits;
        countryCode.text = Program.CountryInformations.Code;
    }
    public void Init() {
        Clear();
        phone.GetComponentInParent<InputField>().characterLimit = Program.CountryInformations.Digits;
        countryCode.text = Program.CountryInformations.Code;
        reset.gameObject.SetActive(true);
        nextReset.gameObject.SetActive(true);
    }
    internal override void Clear() {
        phone.Reset();
        if (timer != null) {
            StopCoroutine(timer);
        }
        reset.gameObject.SetActive(false);
        register.gameObject.SetActive(false);
        nextReset.gameObject.SetActive(false);
        nextRegister.gameObject.SetActive(false);
        openView(0);
    }
}
