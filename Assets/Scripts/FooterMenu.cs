using Newtonsoft.Json.Linq;
using Requests;
using System;
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
  private LoginRegisterPanel loginRegister = null;

  public static FooterMenu dFooterMenu;

  public Button searchButton, addRideButton, myRidesButton, messagesButton, profileButton;

  Dictionary<string, Panel> panels;
  Dictionary<string, Button> buttons;

  void Start() //ToDo verification code
  {
    if (dFooterMenu == null)
    {
      dFooterMenu = this;
    }
    searchPanel.Show();
    addRidePanel.Hide();
    yourRidesPanel.Hide();
    inboxPanel.Hide();
    profilePanel.Hide();
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
    user.Phone = "+" + Cache.GetPhoneCode() + "" + Cache.GetPhone();
    user.Email = Cache.GetEmail();
    Program.User = user;
    Request<Person> request = new GetLoggedInUser(user);
    request.Send(ResponseOfAutoLogin);
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
      Cache.SetUserId(u.id);
      Program.User.Person = u.Person;
      Program.User.Driver = u.Driver;
      Program.IsLoggedIn = true;
    }
  }
  private void ResponseOfAutoLogin(Person u, int code, string message) //ToDo we have to check here i fsomething is missed
  {
    if (!code.Equals((int)HttpStatusCode.OK))
    {
      Cache.SetUserId("");
      Program.User.id = null;

      if (!string.IsNullOrEmpty(Program.User.phone))
      {
        Debug.Log("auto login faild(cache is outdated), trying to login from cache credentials" + Program.User.phone + " ");
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

  public static void Open(string panelName) //default case: no message, with initialize.
  {
    Open(panelName, null, true);
  }

  public static void Open(string panelName, bool initialize)
  {
    Open(panelName, null, initialize);
  }

  public static void Open(string panelName, string dialogMessage)
  {
    Open(panelName, dialogMessage, true);
  }

  public static void Open(string panelName, string dialogMessage, bool initialize)
  {
    Panel p = dFooterMenu.panels[panelName];
    Button b = dFooterMenu.buttons[panelName];

    if (p != null)
    {
      dFooterMenu.ResetButtons();
      b.image.sprite = b.spriteState.selectedSprite;
      dFooterMenu.Open(p, initialize);
      if (dialogMessage != null)
      {
        p.OpenDialog(dialogMessage, true);
      }
    }
  }

  private void Open(Panel newPanel){
    Open(newPanel, true);
  }

  private void Open(Panel newPanel, bool initialize)
  {
    currPanel.Hide();
    newPanel.Show();
    if (initialize)
    {
      newPanel.Init();
    }
    newPanel.transform.SetAsLastSibling();
    currPanel = newPanel;
    transform.SetAsLastSibling();
  }

  private void Open(Panel newPanel, Action Init)
  {
    currPanel.Hide();
    newPanel.Show();
    Init?.Invoke();
    newPanel.transform.SetAsLastSibling();
    currPanel = newPanel;
    transform.SetAsLastSibling();
  }


  public void OpenSearchPanel()
  {
    ResetButtons();
    searchButton.image.sprite = searchButton.spriteState.selectedSprite;
    Open(searchPanel);
  }

  public void OpenAddRidePanel()
  {
    ResetButtons();
    addRideButton.image.sprite = addRideButton.spriteState.selectedSprite;
    Open(addRidePanel);
  }

  public void OpenAddRidePanel(Panel srcPanel, Ride ride)
  {
    ResetButtons();
    addRideButton.image.sprite = addRideButton.spriteState.selectedSprite;
    srcPanel.Open(addRidePanel, () => addRidePanel.Init(ride));
    currPanel = addRidePanel;
  }

  public void OpenAddRidePanel(Panel srcPanel, Alert alert)
  {
    ResetButtons();
    addRideButton.image.sprite = addRideButton.spriteState.selectedSprite;
    srcPanel.Open(addRidePanel, () => addRidePanel.Init(alert));
    currPanel = addRidePanel;
  }

  public void OpenAddRidePanel(Panel srcPanel, ScheduleRide schedule)
  {
    ResetButtons();
    addRideButton.image.sprite = addRideButton.spriteState.selectedSprite;
    srcPanel.Open(addRidePanel, () => addRidePanel.Init(schedule));
    currPanel = addRidePanel;
  }

  public void OpenProfilePanel()
  {
    ResetButtons();
    profileButton.image.sprite = profileButton.spriteState.selectedSprite;


    if (!Program.IsLoggedIn)
    {
      if (loginRegister == null) loginRegister = PanelsFactory.CreateLoginRegisterPanel();
      loginRegister.transform.SetParent(transform.parent, false);
      Open(loginRegister, () => { loginRegister.Init(true); });
    }
    else
    {
      Open(profilePanel);
    }
  }

  public void OpenInboxPanel(Person personToChat, Panel srcPanel)
  {
    ResetButtons();
    messagesButton.image.sprite = messagesButton.spriteState.selectedSprite;
    srcPanel.Open(inboxPanel, () => inboxPanel.Init(personToChat));
    currPanel = inboxPanel;
  }


  public void OpenYourRidesPanel()
  {
    ResetButtons();
    myRidesButton.image.sprite = myRidesButton.spriteState.selectedSprite;
    if (!Program.IsLoggedIn)
    {
      if (loginRegister == null) loginRegister = PanelsFactory.CreateLoginRegisterPanel();
      loginRegister.transform.SetParent(transform.parent, false);
      Open(loginRegister, () => loginRegister.Init(false));
    }
    else
    {
      Open(yourRidesPanel);
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
