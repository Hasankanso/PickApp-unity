using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public abstract class Panel : MonoBehaviour, IEquatable<Panel>
{

  public int id;
  public string panelName;
  private Panel previous, next;
  public bool permanent = false; //for the moment this will only affect the destroy process, if permanent this panel will not be destroyed by calling Panel.Destroy()
  public enum StatusE { ADD, UPDATE, VIEW };

  private StatusE status = StatusE.VIEW;
  private void Initialize(Panel nextPanel) // anything you want to set before the panel opens, will be here
  {
    nextPanel.Status = Status;
  }
  public int Id { get => id; }

  protected Panel Previous { get => previous; }

  public Panel Next
  {
    get => next; set
    {
      if (Next != null && !ReferenceEquals(value, Next))
      {

        Next.ForwardDestroy();
      }

      next = value;
      Initialize(value);
    }
  }

  public StatusE Status
  {
    get => status; set
    {
      status = value;
      if (next != null)
      {
        next.Status = value;
      }
    }
  }

  public StatusE StatusProperty //deprecated
  {
    get => status; set
    {
      status = value;
      if (Next != null)
      {
        Next.StatusProperty = value;
      }
    }
  }

  private static DialogBox dialogBox;
  private static LocationsFinderPanel locationFinderPanel;
  void Update()
  {

    if (Input.GetKeyDown(KeyCode.Escape)
        && TouchScreenKeyboard.visible == false
        && !FooterMenu.IsStaticPanel(this))
    {
      Back();
    }
  }

  public void back() //deprecated
  {
    if (Previous != null)
    {
      gameObject.SetActive(false);
      previous.gameObject.SetActive(true);
    }
  }

  public void Back()
  {
    if (Previous != null)
    {
      gameObject.SetActive(false);
      Previous.gameObject.SetActive(true);
    }
    else
    {
      Debug.LogError("called Back or BackClose although there's no previous Panel");
    }
  }

  public void BackClose()
  {
    Back();
    ForwardDestroy();
  }


  protected void ForwardDestroy()
  {
    if (Next != null) Next.ForwardDestroy();
    if (!permanent)
    {
      Destroy(gameObject);
    }
  }

  protected void BackwardDestroy()
  {
    if (Previous != null) Previous.BackwardDestroy();
    if (!permanent)
    {
      Destroy(gameObject);
    }
  }

  public Panel OpenNext()
  {
    if (Next == null) return null;

    Next.transform.SetParent(transform.parent, false);
    Next.status = status;
    gameObject.SetActive(false);
    Next.previous = this;
    Next.gameObject.SetActive(true);
    Next.transform.SetAsLastSibling();
    return Next;
  }

  public void OpenDialog(Panel newPanel)
  {
    newPanel.transform.SetParent(transform.parent, false);
    newPanel.gameObject.SetActive(true);
  }

  public void CloseDialog()
  {
    ForwardDestroy();
  }

  public void openExisted(Panel newPanel) //deprecated
  {
    Next = newPanel;
    gameObject.SetActive(false);
    newPanel.previous = this;
    newPanel.gameObject.SetActive(true);
  }

  public void openCreated(Panel newPanel) //deprecated
  {
    Next = newPanel;
    OpenNext();
  }

  public void Open(Panel newPanel) // use this only if you don't have Init(...) based on Status in your new panel
  {
    Next = newPanel; // inside of this there's underlying code (check Next Property's code)
    OpenNext();
  }

  public void destroy()
  {
    ForwardDestroy(); //destroy all next panels that are not permanent
  }

  public void DestroyForwardBackward()
  {
    if (Next != null) Next.ForwardDestroy(); //destroy all next panels that are not permanent
    BackwardDestroy(); //this will destroy this panel and all previous panels that are not permanent
  }
  public void MissionCompleted(Panel toOpen)
  {

    if (toOpen.permanent == false)
    {
      Debug.LogWarning("maybe you should set \"permanent\" in " + toOpen.panelName + " to true?");
    }

    toOpen.gameObject.SetActive(true);
    toOpen.transform.SetAsLastSibling(); // move the panel to the fron of user screen

    DestroyForwardBackward();
  }

  internal abstract void Clear();

  public void closeBack() //deprecated
  {
    previous.gameObject.SetActive(true);
    ForwardDestroy();
  }

  public override bool Equals(object obj)
  {
    return Equals(obj as Panel);
  }

  public bool Equals(Panel other)
  {
    return other != null &&
           id == other.id;
  }

  public void OpenDialog(string message, bool success)
  {
    if (dialogBox == null)
    {
      dialogBox = PanelsFactory.CreateDialogBox(message, success);
    }
    else
    {
      dialogBox.init(message, success);
    }
    OpenDialog(dialogBox);
  }

  public void OpenLocationFinder(string text, Action<Location> OnFromLocationPicked)
  {
    if (locationFinderPanel == null)
    {
      locationFinderPanel = PanelsFactory.CreateLocationsFinderPanel(text, OnFromLocationPicked);
    }

    OpenDialog(locationFinderPanel);
  }

  public void OpenDateTimePicker(DateTime startDate, Action<DateTime> OnDatePicked)
  {
    MobileDateTimePicker.CreateDate(startDate.Year, startDate.Month, startDate.Day, null, (dt) => OpenTimePicker(dt, OnDatePicked));
  }

  private void OpenTimePicker(DateTime date, Action<DateTime> OnDatePicked)
  {
    MobileDateTimePicker.CreateTime(null, (time) => { OnDatePicked(Program.CombineDateTime(date, time)); });
  }

  public void OpenDateTimePicker(Action<DateTime> OnDatePicked)
  {
    OpenDateTimePicker(DateTime.Now, OnDatePicked);
  }

  public override int GetHashCode()
  {
    var hashCode = 1550466422;
    hashCode = hashCode * -1521134295 + id.GetHashCode();
    return hashCode;
  }
}