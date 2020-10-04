using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpDownPicker : MonoBehaviour {
    public Text title, counter;
    public Image up, down;
    public Sprite minusOn, minusOff, plusOn, plusOff;
    int min, max, counterInt;


    public void Init(string title, int min, int max, int defaultCounter = 0) {
        Clear();
        this.title.text = title;
        this.counterInt = defaultCounter;
        this.counter.text = "" + counterInt;
        this.min = min;
        this.max = max;
    }
    public void CounterUp() {
        down.sprite = minusOn;
        if (!(Value >= max)) {
            counterInt++;
            this.counter.text = "" + Value;
        }
        if (counterInt == max) {
            up.sprite = plusOff;
        }
    }
    public void CounterDown() {
        up.sprite = plusOn;
        if (!(Value <= min)) {
            counterInt--;
            this.counter.text = "" + Value;
        }
        if (counterInt == min) {
            down.sprite = minusOff;
        }
    }
    public int Value { get => counterInt; }
    public void SetValue(int value) {
        counterInt = value;
        this.counter.text = "" + Value;
    }
    internal void Clear() {
        counterInt = 0;
        counter.text = "" + Value;
        down.sprite = minusOff;
    }
}
