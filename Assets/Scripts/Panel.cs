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
  public enum Status { ADD, UPDATE, VIEW };

  private Status status = Status.VIEW;
  public int Id { get => id; }

  protected Panel Previous { get => previous; }
  private Panel Next { get => next; }
  public Status StatusProperty { get => status; set => status = value; }

  private static DialogBox dialogBox;
    private static LocationsFinderPanel locationFinderPanel;
  void Update()
  {
        
    if (Input.GetKeyDown(KeyCode.Escape)
        && TouchScreenKeyboard.visible == false
        && !FooterMenu.IsStaticPanel(this))
    {
      back();
    }
  }

    public void back()
  {
    if (Previous != null)
    {
      gameObject.SetActive(false);
      previous.gameObject.SetActive(true);
    }
  }

  protected void OpenNext()
  {
    next.transform.SetParent(transform.parent, false);
    next.status = status;
    openExisted(next);
  }

  public void OpenDialog(Panel newPanel)
  {
    newPanel.transform.SetParent(transform.parent, false);
    newPanel.gameObject.SetActive(true);
  }

  public void CloseDialog()
  {
    destroy();
  }

  public void openExisted(Panel panel)
  {
    gameObject.SetActive(false);
    panel.previous = this;
    panel.gameObject.SetActive(true);
  }

  public void openCreated(Panel newPanel)
  {
    if (newPanel.Equals(next))
    {
      newPanel.next = next.next;
      next.next = null;
    }

    if (next != null) next.destroy();

    next = newPanel;
    OpenNext();
  }
  internal abstract void Clear();

  public void closeBack()
  {
    previous.gameObject.SetActive(true);
    destroy();
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

  protected void destroy()
  {
    if (next != null) next.destroy();
    Destroy(gameObject);
  }

  public override int GetHashCode()
  {
    var hashCode = 1550466422;
    hashCode = hashCode * -1521134295 + id.GetHashCode();
    return hashCode;
  }
}