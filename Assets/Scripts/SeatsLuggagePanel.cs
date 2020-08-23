using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SeatsLuggagePanel : Panel {
    public Dropdown numberOfSeats;
    public Dropdown numberOfLuggage;
    private int carMaxSeats, carMaxLuggage;
    private Action<int, int> SeatsLuggagePickedCallBack;
    private List<string> seatsList = new List<string>();
    private List<string> luggageList = new List<string>();

    public void Init(Action<int, int> SeatsLuggagePickedCallBack, int rideAvailableSeats, int rideAvailableLuggage, int carMaxSeats, int carMaxLuggage) {
        Clear();
        this.SeatsLuggagePickedCallBack = SeatsLuggagePickedCallBack;
        this.carMaxLuggage = carMaxLuggage;
        this.carMaxSeats = carMaxSeats;
        for (int i = 1; i <= carMaxSeats; i++) {
            seatsList.Add(i.ToString());
        }
        for (int i = 1; i <= carMaxLuggage; i++) {
            luggageList.Add(i.ToString());
        }
        numberOfSeats.AddOptions(seatsList);
        numberOfLuggage.AddOptions(luggageList);
        if (rideAvailableSeats != 0 && rideAvailableLuggage != 0) {
            numberOfSeats.value = rideAvailableSeats - 1;
            numberOfLuggage.value = rideAvailableLuggage - 1;
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
