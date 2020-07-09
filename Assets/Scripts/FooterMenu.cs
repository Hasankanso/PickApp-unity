using Newtonsoft.Json.Linq;
using Requests;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.UI;

public class FooterMenu : MonoBehaviour
{

  Panel currPanel;

  [SerializeField] private AddRidePanel addRidePanel;
  [SerializeField] private SearchPanel searchPanel;
  [SerializeField] private MyRidePanel yourRidesPanel;
  [SerializeField] private InboxPanel inboxPanel;
  [SerializeField] private ProfilePanel profilePanel;

  public static FooterMenu dFooterMenu;

  public Button searchButton, addRideButton, myRidesButton, messagesButton, profileButton;

  Dictionary<string, Panel> panels;

  void Start()
  {
    if (dFooterMenu == null)
    {
      dFooterMenu = this;
    }
    searchPanel.gameObject.SetActive(true);
    addRidePanel.gameObject.SetActive(false);
    yourRidesPanel.gameObject.SetActive(false);
    inboxPanel.gameObject.SetActive(false);
    profilePanel.gameObject.SetActive(false);
    currPanel = searchPanel;
    searchButton.image.sprite = searchButton.spriteState.selectedSprite;
    string cacheToken = Cache.GetToken();
    if (!string.IsNullOrEmpty(cacheToken))
    {
      User user = new User();
      user.Id = Cache.GetUserId();
      user.Token = cacheToken;
      user.Phone = "+" + Cache.GetPhoneCode() + "" + Cache.GetPhone();
      user.Email = Cache.GetEmail();
      Program.User = user;
      Debug.Log(Program.UserToken);
      Request<Person> request = new GetLoggedInUser(user);
      request.Send(Response);
    }
  }

  public static void Open(string panelName)
  {
    Panel p = dFooterMenu.panels[panelName];
    if (p != null)
    {
      dFooterMenu.Open(p);
    }
  }

  public static void Open(string panelName, string dialogMessage)
  {
    Panel p = dFooterMenu.panels[panelName];
    if (p != null) {
      dFooterMenu.Open(p);
      p.OpenDialog(dialogMessage, true);
    }
  }

  private void Open(Panel newPanel)
  {
    currPanel.gameObject.SetActive(false);
    newPanel.gameObject.SetActive(true);
    newPanel.transform.SetAsLastSibling();
    currPanel = newPanel;
    transform.SetAsLastSibling();
  }

  private void Response(Person u, int code, string message)
  {
    if (!code.Equals((int)HttpStatusCode.OK))
    {
      Cache.SetToken("");
      Program.User = null;
    }
    else
    {
      Program.User.Person = u;
      Program.IsLoggedIn = true;
    }
  }
  public void OpenSearchPanel()
  {
    ResetButtons();
    Open(searchPanel);
    searchButton.image.sprite = searchButton.spriteState.selectedSprite;
  }

  public void OpenAddRidePanel()
  {
    ResetButtons();
    currPanel.openExisted(addRidePanel);
    addRidePanel.Init();
    currPanel = addRidePanel;
    addRideButton.image.sprite = addRideButton.spriteState.selectedSprite;
  }

  public void OpenAddRidePanel(Panel srcPanel, Ride ride)
  {
    ResetButtons();

    srcPanel.Next = addRidePanel;
    addRidePanel.Init(ride);
    srcPanel.OpenNext();
    currPanel = addRidePanel;
    addRideButton.image.sprite = addRideButton.spriteState.selectedSprite;

  }

  public void OpenAddRidePanel(Panel srcPanel, Alert alert)
  {
    ResetButtons();

    srcPanel.Next = addRidePanel;
    addRidePanel.Init(alert);
    srcPanel.OpenNext();
    currPanel = addRidePanel;
    addRideButton.image.sprite = addRideButton.spriteState.selectedSprite;
  }

  public void OpenAddRidePanel(Panel srcPanel, ScheduleRide schedule)
  {
    ResetButtons();

    srcPanel.Next = addRidePanel;
    addRidePanel.Init(schedule);
    srcPanel.OpenNext();
    currPanel = addRidePanel;
    addRideButton.image.sprite = addRideButton.spriteState.selectedSprite;
  }

  public void OpenProfilePanel()
  {
    ResetButtons();
    profileButton.image.sprite = profileButton.spriteState.selectedSprite;


    if (!Program.IsLoggedIn)
    {
      Panel loginPanel = PanelsFactory.createLogin(true);
      currPanel.Open(loginPanel, null);
    }
    else
    {
      Open(profilePanel);
      profilePanel.Init();
    }
  }

  public void OpenInboxPanel(Person personToChat, Panel srcPanel)
  {
    ResetButtons();

    srcPanel.Next = inboxPanel;
    inboxPanel.Init(personToChat);
    srcPanel.OpenNext();
    currPanel = inboxPanel;

    messagesButton.image.sprite = messagesButton.spriteState.selectedSprite;
  }


  public void OpenYourRidesPanel()
  {
    ResetButtons();
    Debug.Log(Program.IsLoggedIn);


    if (!Program.IsLoggedIn)
    {
      Panel loginPanel = PanelsFactory.createLogin(true);
      currPanel.Open(loginPanel, null);
    }
    else
    {
      Open(yourRidesPanel);
      myRidesButton.image.sprite = myRidesButton.spriteState.selectedSprite;
      yourRidesPanel.Init();
    }
  }

  public static bool IsStaticPanel(Panel p)
  {
    return p.Equals(dFooterMenu.searchPanel) || p.Equals(dFooterMenu.addRidePanel) || p.Equals(dFooterMenu.yourRidesPanel) || p.Equals(dFooterMenu.inboxPanel) || p.Equals(dFooterMenu.profilePanel);
  }

  void ResetButtons()
  {
    searchButton.image.sprite = searchButton.spriteState.disabledSprite;
    addRideButton.image.sprite = addRideButton.spriteState.disabledSprite;
    myRidesButton.image.sprite = myRidesButton.spriteState.disabledSprite;
    messagesButton.image.sprite = messagesButton.spriteState.disabledSprite;
    profileButton.image.sprite = profileButton.spriteState.disabledSprite;
  }
}
