using Requests;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class SchedulePanel : Panel
{
    public Text startDate, endDate;

  public Text[] daysText;
  private bool[] daysBool = new bool[7];

  public Sprite errorImage;


  public override void Init()
  {
    Clear();
  }

  public void Init(ScheduleRide sr)
  {
    Clear();
    Fill(sr);
  }

  public void Fill(ScheduleRide sr)
  {
    startDate.text = Program.DateToString(sr.StartDate);
    endDate.text = Program.DateToString(sr.EndDate);
    SetWeekDays(sr.IsMonday, sr.IsTuesday, sr.IsWednesday, sr.IsThursday, sr.IsFriday, sr.IsSaturday, sr.IsSunday);
  }

  public void OpenAddRidePanel()
  {
    if (validate())
    {
      ScheduleRide scheduleRide = new ScheduleRide(Program.StringToDate(startDate.text), Program.StringToDate(endDate.text), daysBool[0], daysBool[1], daysBool[2], daysBool[3], daysBool[4], daysBool[5], daysBool[6]);
      FooterMenu.dFooterMenu.OpenAddRidePanel(this, scheduleRide);
    }
  }

  public void OnDatePicked(DateTime d, Text text)
  {
    text.text = Program.DateToString(d);
  }

  public void OpenDatePicker(Text text)
  {
    DateTime now = DateTime.Now;
    MobileDateTimePicker.CreateDate(now.Year, now.Month, now.Day, null, delegate (DateTime dt) { OnDatePicked(dt, text); });
  }

  public void dayOnOff(int dayPosition)
  {
    if (dayPosition >= 7 || dayPosition < 0) return;

    if (daysBool[dayPosition])
    {
      daysBool[dayPosition] = false;
      daysText[dayPosition].color = new Color(186f / 255f, 186f / 255f, 186f / 255f, 128f / 255f);
    }
    else
    {
      daysBool[dayPosition] = true;
      daysText[dayPosition].color = new Color(255f / 255f, 188f / 255f, 66f / 255f);
    }
  }


  public void SetWeekDays(bool isMonday, bool isTuesday, bool isWednesday, bool isThursday, bool isFriday, bool isSaturday, bool isSunday)
  {
    daysBool[0] = isMonday;
    daysBool[1] = isTuesday;
    daysBool[2] = isWednesday;
    daysBool[3] = isThursday;
    daysBool[4] = isFriday;
    daysBool[5] = isSaturday;
    daysBool[6] = isSunday;

    for (int i = 0; i < 7; i++)
    {
      if (daysBool[i])
      {
        daysText[i].color = new Color(255f / 255f, 188f / 255f, 66f / 255f);
      }
      else
      {
        daysText[i].color = new Color(186f / 255f, 186f / 255f, 186f / 255f, 128f / 255f);

      }
    }
  }

  //TODO validate Date
  public bool validate()
  {
        bool valid = true;
       /* if (Program.StringToDate(startDate.text) < DateTime.Now)
        {
            OpenDialog("The max period of schedule is six months", false);
            valid = false;
        }
        if (DateTime.Compare(Program.StringToDate(endDate.text) , Program.MaxScheduleDate)>0)
        {
            OpenDialog("The max period of schedule is six months", false);
            valid = false;
        }*/
        return valid;
    }
 

  internal override void Clear()
  {
    //time.text = Program.TimeToString(DateTime.Now);
    startDate.text = Program.DateToString(DateTime.Now);
    endDate.text = Program.DateToString(DateTime.Now);

    for (int i = 0; i < 7; i++)
    {
      daysBool[i] = false;
      daysText[i].color = new Color(186f / 255f, 186f / 255f, 186f / 255f, 128f / 255f);
    }
  }
}
