using UnityEngine;
using System.Collections;
using System;


public class NativeDialog {
    private string v1;
    private string v2;

    public NativeDialog(string v1, string v2) {
        this.v1 = v1;
        this.v2 = v2;
    }

    public static void OpenDialog(string title, string message, string ok = "Ok", Action okAction = null) {
        MobileDialogInfo.Create(title, message, ok, okAction);
    }
    public static void OpenDialog(string title, string message, string yes, string no, Action yesAction = null, Action noAction = null) {
        MobileDialogConfirm.Create(title, message, yes, no, yesAction, noAction);
    }

    public void SetUrlString(string v) {
        throw new NotImplementedException();
    }

    public void init() {
        throw new NotImplementedException();
    }

    public static void OpenDialog(string title, string message, string accept, string neutral, string decline, Action acceptAction = null, Action neutralAction = null, Action declineAction = null) {
        MobileDialogNeutral.Create(title, message, accept, neutral, decline, acceptAction, neutralAction, declineAction);
    }
    public static void OpenDatePicker(int year, int month, int day, Action<DateTime> onChange = null, Action<DateTime> onClose = null) {
        MobileDateTimePicker.CreateDate(year, month, day, onChange, onClose);
    }
    public static void OpenTimePicker(Action<DateTime> onChange = null, Action<DateTime> onClose = null) {
        MobileDateTimePicker.CreateTime(onChange, onClose);
    }
}
