using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SeatsLuggagePanel : Panel {
    public Dropdown numberOfSeats;
    public Dropdown numberOfLuggage;
    private int maxSeats, maxLuggage;
    private Action<int, int> SeatsLuggagePickedCallBack;
    private List<string> seatsList = new List<string>();
    private List<string> luggageList = new List<string>();

    public void Init(Action<int, int> SeatsLuggagePickedCallBack, int rideMaxSeats, int rideMaxLuggage, int maxSeats, int maxLuggage) {
        Clear();
        this.SeatsLuggagePickedCallBack = SeatsLuggagePickedCallBack;
        this.maxLuggage = maxLuggage;
        this.maxSeats = maxSeats;
        for (int i = 1; i <= maxSeats; i++) {
            seatsList.Add(i.ToString());
        }
        for (int i = 1; i <= maxLuggage; i++) {
            luggageList.Add(i.ToString());
        }
        numberOfSeats.AddOptions(seatsList);
        numberOfLuggage.AddOptions(luggageList);
        if (rideMaxSeats != 0 && rideMaxLuggage != 0) {
            numberOfSeats.value = rideMaxSeats - 1;
            numberOfLuggage.value = rideMaxLuggage - 1;
        }
    }
    public void SeatsLuggagePicked() {
        SeatsLuggagePickedCallBack(int.Parse(numberOfSeats.options[numberOfSeats.value].text), int.Parse(numberOfLuggage.options[numberOfLuggage.value].text));
    }

    internal override void Clear() {
        numberOfSeats.ClearOptions();
        numberOfLuggage.ClearOptions();
        seatsList.Clear();
        luggageList.Clear();
    }
}
