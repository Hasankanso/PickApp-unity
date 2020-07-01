using Newtonsoft.Json.Linq;
using Requests;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.UI;

public class FooterMenu : MonoBehaviour {

    Panel currPanel;

    [SerializeField] private AddRidePanel addRidePanel;
    [SerializeField] private SearchPanel searchPanel;
    [SerializeField] private MyRidePanel yourRidesPanel;
    [SerializeField] private InboxPanel inboxPanel;
    [SerializeField] private ProfilePanel profilePanel;

    public static FooterMenu dFooterMenu;

    public Button searchButton, addRideButton, myRidesButton, messagesButton, profileButton;

    void Start() {
        if (dFooterMenu == null) {
            dFooterMenu = this;
        }
        searchPanel.gameObject.SetActive(true);
        addRidePanel.gameObject.SetActive(false);
        yourRidesPanel.gameObject.SetActive(false);
        inboxPanel.gameObject.SetActive(false);
        profilePanel.gameObject.SetActive(false);
        currPanel = searchPanel;
        searchButton.image.sprite = searchButton.spriteState.selectedSprite;
        if (!string.IsNullOrEmpty(Cache.GetToken())) {
            User user = new User();
            user.Id = Cache.GetUserId();
            user.Token = Cache.GetToken();
            Program.User = user;
            Debug.Log(Program.UserToken);
            Request<User> request = new GetUser(user);
            request.Send(Response);
        }
  }

private void Response(User u, HttpStatusCode code, string message) {
    JObject j = null;
    if (string.IsNullOrEmpty(message)) {
        j = JObject.Parse(message);
        if (!string.IsNullOrEmpty(message) && int.Parse(j["code"].ToString()) == 3003) {
        }
    } else if (!code.Equals(HttpStatusCode.OK)) {
    } else {
        Program.User = u;
        Program.IsLoggedIn = true;
    }
}
public void OpenSearchPanel() {
    ResetButtons();
    currPanel.openExisted(searchPanel);
    currPanel = searchPanel;
    searchButton.image.sprite = searchButton.spriteState.selectedSprite;
}

public void OpenAddRidePanel() {
    ResetButtons();
    currPanel.openExisted(addRidePanel);
    addRidePanel.Init();
    currPanel = addRidePanel;
    addRideButton.image.sprite = addRideButton.spriteState.selectedSprite;

}
public void OpenAddRidePanel(Panel srcPanel, Ride ride) {
    ResetButtons();
    srcPanel.openExisted(addRidePanel);
    currPanel = addRidePanel;
    addRideButton.image.sprite = addRideButton.spriteState.selectedSprite;
    addRidePanel.Init(ride);
}

public void OpenAddRidePanel(Panel srcPanel, Alert alert) {
    ResetButtons();
    srcPanel.openExisted(addRidePanel);
    currPanel = addRidePanel;
    addRideButton.image.sprite = addRideButton.spriteState.selectedSprite;
    addRidePanel.Init(alert);
}

public void OpenAddRidePanel(Panel srcPanel, ScheduleRide schedule) {
    ResetButtons();
    srcPanel.openExisted(addRidePanel);
    currPanel = addRidePanel;
    addRideButton.image.sprite = addRideButton.spriteState.selectedSprite;
    addRidePanel.Init(schedule);
}

public void OpenProfilePanel() {
    ResetButtons();
    profileButton.image.sprite = profileButton.spriteState.selectedSprite;
    if (!Program.IsLoggedIn) {
        Panel notLoginPanel = PanelsFactory.createLogin(true);
        currPanel.openCreated(notLoginPanel);
        currPanel = notLoginPanel;
    } else {
        currPanel.openExisted(profilePanel);
        currPanel = profilePanel;
        profilePanel.Init();
    }
}

public void OpenInboxPanel(Person personToChat, Panel srcPanel) {
    ResetButtons();
    srcPanel.openExisted(inboxPanel);
    currPanel = inboxPanel;
    messagesButton.image.sprite = messagesButton.spriteState.selectedSprite;
    inboxPanel.Init(personToChat);
}


    public void OpenYourRidesPanel()
    {
        ResetButtons();
        Debug.Log(Program.IsLoggedIn);

        if (!Program.IsLoggedIn)
        {
            Panel notLoginPanel = PanelsFactory.createLogin(true);
            currPanel.openCreated(notLoginPanel);
            currPanel = notLoginPanel;
        }
        else
        {
            currPanel.openExisted(yourRidesPanel);
            currPanel = yourRidesPanel;
            myRidesButton.image.sprite = myRidesButton.spriteState.selectedSprite;
            yourRidesPanel.Init();
        }
    }

public static bool IsStaticPanel(Panel p) {
    return p.Equals(dFooterMenu.searchPanel) || p.Equals(dFooterMenu.addRidePanel) || p.Equals(dFooterMenu.yourRidesPanel) || p.Equals(dFooterMenu.inboxPanel) || p.Equals(dFooterMenu.profilePanel);
}

void ResetButtons() {
    searchButton.image.sprite = searchButton.spriteState.disabledSprite;
    addRideButton.image.sprite = addRideButton.spriteState.disabledSprite;
    myRidesButton.image.sprite = myRidesButton.spriteState.disabledSprite;
    messagesButton.image.sprite = messagesButton.spriteState.disabledSprite;
    profileButton.image.sprite = profileButton.spriteState.disabledSprite;
}
}
