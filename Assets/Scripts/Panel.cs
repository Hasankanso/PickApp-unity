﻿using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using UnityEngine.UI;
using Requests;
using Newtonsoft.Json.Linq;
using UnityEngine;
using BackendlessAPI;
using BackendlessAPI.Engine;
using System.Collections.Generic;
using BackendlessAPI.Utils;
using System;
using BackendlessAPI.RT;

public abstract class Panel : MonoBehaviour, IEquatable<Panel>
{
  public int id;
  public string panelName;
  private Panel previous, next;
  private bool opened = false;
  public bool permanent = false; //for the moment this will only affect the destroy process, if permanent this panel will not be destroyed by calling Panel.Destroy()


  private static DialogBox dialogBox;
  private YesNoDialog yesNoDialog;

  private static LocationsFinderPanel locationFinderPanel;
  private Spinner spinner;

  public enum StatusE { ADD, UPDATE, VIEW };

  private StatusE status = StatusE.VIEW;

  public virtual void Init()
  {

  }

  public virtual void ConvertToArabicUI(){
    
  }

  public virtual void Init(StatusE statusE)
  {

  }

  private void Initialize(Panel nextPanel) // anything you want to set before the panel opens, will be here
  {
    nextPanel.Status = Status;
    nextPanel.opened = true;
    if(Program.language.Arabic){
      ConvertToArabicUI();
    }

  }

  public int Id { get => id; }

  protected Panel Previous { get => previous; }

  public Panel Next
  {
    get => next; set
    {
      if (Next != null && !ReferenceEquals(value, Next))
      {

        Next.ForwardDestroyImmediate();
      }
      next = value;
      next.transform.SetParent(transform.parent, false);
      Initialize(value);
      next.previous = this;
    }
  }

  public StatusE Status
  {
    get => status; set
    {
      status = value;
      if (next != null)
      {
        next.status = value;
      }
    }
  }

  void Update()
  {
    if (Input.GetKeyDown(KeyCode.Escape)
        && TouchScreenKeyboard.visible == false)
    {
      Back();
    }
  }

  public void Back()
  {
    if (Previous != null)
    {
      Hide();
      Previous.Show();
      AdMob.DestroyBanner();
    }
  }

  public void BackClose()
  {
    Back();
    ForwardDestroyImmediate();
  }

  public void Hide()
  {
    gameObject.SetActive(false);
    opened = false;
  }

  public void Show()
  {
    gameObject.SetActive(true);
    opened = true;
  }

  protected void ForwardDestroyImmediate()
  {
    if (Next != null) Next.ForwardDestroyImmediate();
    if (!permanent)
    {
      DestroyImmediate(gameObject);
    }
  }

  protected void BackwardDestroyImmediate()
  {
    if (Previous != null) Previous.BackwardDestroyImmediate();
    if (!permanent)
    {
      DestroyImmediate(gameObject);
    }
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

    Next.status = status;
    Hide();
    Next.Show();
    Next.transform.SetAsLastSibling();

    return Next;
  }

  public void CloseSpinner()
  {
    Destroy(spinner.gameObject);
  }
  public void OpenDialog(Panel newPanel)
  {
    newPanel.transform.SetParent(transform.parent, false);
    newPanel.Show();
    newPanel.transform.SetAsLastSibling();
  }

  public void OpenSpinner()
  {
    spinner = PanelsFactory.CreateSpinner();
    spinner.transform.SetParent(transform, false);
    spinner.Show();
    spinner.transform.SetAsLastSibling();
  }
  public void CloseDialog()
  {
    ForwardDestroyImmediate();
  }

  public void OpenTail()
  {
    if (Next == null) return;

    OpenNext();
    next.OpenTail();
  }

  public void Open(Panel nextPanel)
  {
    Next = nextPanel; // inside of this there's underlying code (check Next Property's code)
    nextPanel.Init();
    if (opened)
    {
      OpenNext();
    }
  }


  public void Open(Panel nextPanel, Action Init)
  {
    if (!opened)
    {
      nextPanel.ForwardDestroyImmediate();
      return;
    }
    Next = nextPanel; // inside of this there's underlying code (check Next Property's code)
    Init?.Invoke(); //this line is equal to if(Init !=null) { Init();}
    OpenNext();
  }

  public void DestroyImediateForwardBackward()
  {
    if (Next != null) Next.ForwardDestroyImmediate(); //destroy all next panels that are not permanent
    BackwardDestroyImmediate(); //this will destroy this panel and all previous panels that are not permanent
  }
  public void DestroyForwardBackward()
  {
    if (Next != null) Next.ForwardDestroy(); //destroy all next panels that are not permanent
    BackwardDestroy(); //this will destroy this panel and all previous panels that are not permanent
  }
  public void MissionCompleted(string panelName)
  {
    MissionCompleted(panelName, null, true);
  }

  public void MissionCompleted(string panelName, bool initialize)
  {
    MissionCompleted(panelName, null, initialize);
  }

  public void MissionCompleted(string panelName, string dialogMessage)
  {
    MissionCompleted(panelName, dialogMessage, true);
  }
  public void MissionCompleted(string panelName, string dialogMessage, bool initialize)
  {
    FooterMenu.Open(panelName, dialogMessage, initialize);
    AdMob.DestroyBanner();
    DestroyImediateForwardBackward();
  }
  internal abstract void Clear();


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
      dialogBox.Init(message, success);
    }
    OpenDialog(dialogBox);
  }

  public void OpenYesNoDialog(string question, Action<bool> OnDecisionTaken)
  {
    if (yesNoDialog == null)
      yesNoDialog = PanelsFactory.CreateYesNoDialog();

    yesNoDialog.Init(question, OnDecisionTaken);
    OpenDialog(yesNoDialog);
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