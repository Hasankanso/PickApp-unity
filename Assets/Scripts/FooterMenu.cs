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
  Dictionary<string, Button> buttons;

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

    panels = new Dictionary<string, Panel>(5);
    buttons = new Dictionary<string, Button>(5);

    panels.Add(AddRidePanel.PANELNAME, addRidePanel);
    panels.Add(SearchPanel.PANELNAME, searchPanel);
    panels.Add(MyRidePanel.PANELNAME, yourRidesPanel);
    panels.Add(InboxPanel.PANELNAME, inboxPanel);
    panels.Add(ProfilePanel.PANELNAME, profilePanel);

    buttons.Add(AddRidePanel.PANELNAME, addRideButton);
    buttons.Add(SearchPanel.PANELNAME, searchButton);
    buttons.Add(MyRidePanel.PANELNAME, myRidesButton);
    buttons.Add(InboxPanel.PANELNAME, messagesButton);
    buttons.Add(ProfilePanel.PANELNAME, profileButton);

    User user = new User();
    user.Id = Cache.GetUserId();
    string token = Cache.GetToken();
    user.Phone = "+" + Cache.GetPhoneCode() + "" + Cache.GetPhone();
    user.password = Cache.GetPassword();
    user.Email = Cache.GetEmail();
    Debug.Log("cache" + token);
    if (!string.IsNullOrEmpty(token))
    {
      Debug.Log("auto login");
      Program.User = user;
      user.Token = token;
      Request<Person> request = new GetLoggedInUser(user);
      request.Send(ResponseOfAutoLogin);
    }
  }
  private void ResponseOfLogin(User u, int code, string message)
  {
    if (!code.Equals((int)HttpStatusCode.OK))
    {
      Debug.Log(5);
      Program.User = null;
    }
    else
    {
      Cache.SetToken(u.Token);
      Program.User.Person = u.Person;
      Program.User.Driver = u.Driver;
      Program.IsLoggedIn = true;
    }
  }
  private void ResponseOfAutoLogin(Person u, int code, string message)
  {
    if (!code.Equals((int)HttpStatusCode.OK))
    {
      Cache.SetToken("");
      Program.User.Token = null;
      if (!string.IsNullOrEmpty(Program.User.password) && !string.IsNullOrEmpty(Program.User.phone))
      {
        Debug.Log("auto login faild(cache is outdated), trying to login from cache credentials" + Program.User.phone + " " + Program.User.password);
        Request<User> request = new Login(Program.User);
        request.Send(ResponseOfLogin);
      }
      else
        Program.User = null;
    }
    else
    {
      Program.User.Person = u;
      Program.IsLoggedIn = true;
    }
  }
  public static void Open(string panelName)
  {
    Open(panelName, null);
  }

  public static void Open(string panelName, string dialogMessage)
  {
    Panel p = dFooterMenu.panels[panelName];
    Button b = dFooterMenu.buttons[panelName];

    if (p != null)
    {
      dFooterMenu.ResetButtons();
      b.image.sprite = b.spriteState.selectedSprite;
      dFooterMenu.Open(p);
      if (dialogMessage != null)
      {
        p.OpenDialog(dialogMessage, true);
      }
    }
  }

  private void Open(Panel newPanel)
  {
    currPanel.gameObject.SetActive(false);
    newPanel.gameObject.SetActive(true);
    newPanel.Init();
    newPanel.transform.SetAsLastSibling();
    currPanel = newPanel;

    transform.SetAsLastSibling();
    newPanel.OpenTail();
  }


  public void OpenSearchPanel()
  {
    ResetButtons();
    Open(searchPanel);
  }

  public void OpenAddRidePanel()
  {
    ResetButtons();
    Open(addRidePanel);
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
      LoginPanel loginPanel = PanelsFactory.CreateLogin();
      currPanel.Open(loginPanel, () => { loginPanel.Init(true); });
      currPanel = loginPanel;
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
      LoginPanel loginPanel = PanelsFactory.CreateLogin();
      currPanel.Open(loginPanel, () => { loginPanel.Init(true); });
      currPanel = loginPanel;
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
