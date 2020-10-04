using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SeatsLuggagePanel : Panel {
    public UpDownPicker numberOfSeats, numberOfLuggage;
    private int carMaxSeats, carMaxLuggage;
    private Action<int, int> SeatsLuggagePickedCallBack;

    public void Init(Action<int, int> SeatsLuggagePickedCallBack, int rideAvailableSeats, int rideAvailableLuggage, int carMaxSeats, int carMaxLuggage) {
        Clear();
        this.SeatsLuggagePickedCallBack = SeatsLuggagePickedCallBack;
        this.carMaxLuggage = carMaxLuggage;
        this.carMaxSeats = carMaxSeats;
        if (rideAvailableSeats != 0)
            numberOfSeats.Init("Number of seats", 1, carMaxSeats, rideAvailableSeats);
        else
            numberOfSeats.Init("Number of seats", 1, carMaxSeats, 1);
        numberOfLuggage.Init("Number of luggage", 0, carMaxLuggage, rideAvailableLuggage);
    }
    public void SeatsLuggagePicked() {
        SeatsLuggagePickedCallBack(numberOfSeats.Value, numberOfLuggage.Value);
    }

    internal override void Clear() {
        numberOfSeats.Clear();
        numberOfLuggage.Clear();
    }
}
