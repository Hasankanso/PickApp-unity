using System;
using System.Collections.Generic;

public class CarsListPanel : Panel {
    public ListView carsList;
    private Action<Car> CarPickedCallBack;

    public void Init(Action<Car> CarPickedCallBack, Car chosenCar) {

        Clear();
        List<Car> cars = Program.Driver.Cars;
        this.CarPickedCallBack = CarPickedCallBack;

        foreach (Car c in cars) {
            CarItem cI = ItemsFactory.CreateCarItem(carsList.scrollContainer, c, OnItemClicked);
            cI.UnSelect();
            if (c.Equals(chosenCar)) {
                cI.Select();
            }
            carsList.Add(cI);
        }

    }

    public void Init(Action<Car> CarPickedCallBack) {

        List<Car> cars = Program.Driver.Cars;
        this.CarPickedCallBack = CarPickedCallBack;

        foreach (Car c in cars) {
            CarItem cI = ItemsFactory.CreateCarItem(carsList.scrollContainer, c, OnItemClicked);
            carsList.Add(cI);
        }
        Status = Previous.Status;
    }

    public void OnItemClicked(Car c, Item item) {
        if (carsList.selectedItem != null) {
            carsList.selectedItem.GetComponent<CarItem>().UnSelect();
        }

        item.Select();
        carsList.selectedItem = item.gameObject;
        CarPickedCallBack(c);
    }

    public void ProfileOnClick(Car c) {

    }


    internal override void Clear() {
        carsList.Clear();
    }
}
