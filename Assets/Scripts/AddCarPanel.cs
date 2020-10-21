using Requests;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
public class AddCarPanel : Panel {
    public InputFieldScript carName,
    brand,
    year;
    public Image carImage,
    largeCarImage,
    BlackCheckMark,
    WhiteCheckMark,
    GreyCheckMark,
    DarkGreyCheckMark,
    RedCheckMark,
    BlueCheckMark,
    DarkBlueCheckMark,
    YellowCheckMark,
    PinkCheckMark,
    PurpleCheckMark,
    BrownCheckMark,
    OrangeCheckMark,
    GreenCheckMark;
    public GameObject firstView,
    secondView,
    viewImageModel;
    public Button add,
    update,
    becomeDriverBtn;
    public UpDownPicker maxSeats, maxLuggage;
    private Car car = null;
    private Driver driver = null;
    private string color;

    public void Submit() {
        if (VadilateSecondView()) {
            car = new Car(carName.text.text, int.Parse(year.text.text), maxLuggage.Value, maxSeats.Value, brand.text.text, color, carImage.sprite.texture);
            Request<List<Car>> request = new AddCar(car, Program.User);
            request.AddSendListener(OpenSpinner);
            request.AddReceiveListener(CloseSpinner);
            request.Send(response);
        }
    }
    public void BecomeDriver() {
        if (VadilateSecondView()) {
            car = new Car(carName.text.text, int.Parse(year.text.text),  maxLuggage.Value, maxSeats.Value, brand.text.text, color, carImage.sprite.texture);
            driver.Cars = new List<Car>();
            driver.Cars.Add(car);
            Request<Driver> request = new BecomeDriverRequest(Program.User, driver);
            request.AddSendListener(OpenSpinner);
            request.AddReceiveListener(CloseSpinner);
            request.Send(BecomeDriverResponse);
        }
    }

    private void BecomeDriverResponse(Driver result, int code, string message) {
        if (!code.Equals((int)HttpStatusCode.OK)) {
            OpenDialog(message, false);
        } else {
            Program.User.Driver = result;
            MissionCompleted(AddRidePanel.PANELNAME, "Now you are a driver", false);
        }
    }

    public void ViewChoosenImage() {
        viewImageModel.gameObject.SetActive(true);
    }

    public void CloseViewChoosenImage() {
        viewImageModel.gameObject.SetActive(false);
    }
    public void submitUpdate() {
        if (VadilateSecondView()) {
            car.Name = carName.text.text;
            car.Year = int.Parse(year.text.text);
            car.MaxSeats = maxSeats.Value;
            car.MaxLuggage = maxLuggage.Value;
            car.Brand = brand.text.text;
            car.Color = color;
            car.Picture = carImage.sprite.texture;
            Request<List<Car>> request = new EditCar(car, Program.User);
            request.AddSendListener(OpenSpinner);
            request.AddReceiveListener(CloseSpinner);
            request.Send(response);
        }
    }

    private void response(List<Car> result, int code, string message) {
        if (!code.Equals((int)HttpStatusCode.OK)) {
            OpenDialog(message, false);

        } else {
            Program.Driver.Cars = result;
            MissionCompleted(ProfilePanel.PANELNAME, "Car has been edited");
        }
    }

    public void Init(Car car) {
        Clear();
        this.car = car;
        update.gameObject.SetActive(true);
        carName.SetText(car.Name);
        year.SetText(car.Year.ToString());
        maxSeats.Init("Max seats", 1, 8, car.MaxSeats);
        maxLuggage.Init("Max luggage", 1, 8, car.MaxLuggage);
        brand.SetText(car.Brand);
        color = car.Color;
        GetColor(car.Color);
        carImage.sprite = Program.GetImage(car.Picture);
        largeCarImage.sprite = Program.GetImage(car.Picture);
    }
    public override void Init() {
        Clear();
        maxSeats.Init("Max seats", 1, 8, 3);
        maxLuggage.Init("Max luggage", 1, 8, 2);
        add.gameObject.SetActive(true);
    }
    public void Init(Driver driver) {
        Clear();
        maxSeats.Init("Max seats", 1, 8, 3);
        maxLuggage.Init("Max luggage", 1, 8, 2);
        this.driver = driver;
        becomeDriverBtn.gameObject.SetActive(true);
    }
    private bool VadilateFirstView() {
        if (carName.text.text.Equals("")) {
            carName.Error();
            OpenDialog("Insert your car name", false);
            return false;
        }
        if (brand.text.text.Equals("")) {
            brand.Error();
            OpenDialog("Insert your car brand", false);
            return false;
        }
        if (year.text.text.Equals("")) {
            year.Error();
            OpenDialog("Insert your car year", false);
            return false;
        } else {
            if (int.Parse(year.text.text) < 1960 || int.Parse(year.text.text) > DateTime.Now.Year) {
                year.Error();
                OpenDialog("Invalid year", false);
                return false;
            }
        }
        return true;
    }

    private bool VadilateSecondView() {
        if (!BlackCheckMark.isActiveAndEnabled && !WhiteCheckMark.isActiveAndEnabled && !GreyCheckMark.isActiveAndEnabled && !DarkGreyCheckMark.isActiveAndEnabled && !RedCheckMark.isActiveAndEnabled && !BlueCheckMark.isActiveAndEnabled && !DarkBlueCheckMark.isActiveAndEnabled && !YellowCheckMark.isActiveAndEnabled && !PinkCheckMark.isActiveAndEnabled && !PurpleCheckMark.isActiveAndEnabled && !BrownCheckMark.isActiveAndEnabled && !OrangeCheckMark.isActiveAndEnabled && !GreenCheckMark.isActiveAndEnabled) {
            OpenDialog("Choose car color", false);
            return false;
        }
        return true;
    }
    private void GetColor(string color) {
        if (color.Equals("#000000")) {
            BlackCheckMark.enabled = true;
        } else if (color.Equals("#ffffff")) {
            WhiteCheckMark.enabled = true;
        } else if (color.Equals("#9F9F9F")) {
            GreyCheckMark.enabled = true;
        } else if (color.Equals("#767676")) {
            DarkGreyCheckMark.enabled = true;
        } else if (color.Equals("#FF0000")) {
            RedCheckMark.enabled = true;
        } else if (color.Equals("#004BFF")) {
            BlueCheckMark.enabled = true;
        } else if (color.Equals("#001196")) {
            DarkBlueCheckMark.enabled = true;
        } else if (color.Equals("#FFFC00")) {
            YellowCheckMark.enabled = true;
        } else if (color.Equals("#FFA5EA")) {
            PinkCheckMark.enabled = true;
        } else if (color.Equals("#6A0DAD")) {
            PurpleCheckMark.enabled = true;
        } else if (color.Equals("#964B00")) {
            BrownCheckMark.enabled = true;
        } else if (color.Equals("#FFA500")) {
            OrangeCheckMark.enabled = true;
        } else if (color.Equals("#00DB00")) {
            GreenCheckMark.enabled = true;
        }
    }
    public void ColorPicker(int colorIndex) {
        //disable all check box then enabled the choosen one
        BlackCheckMark.enabled = false;
        WhiteCheckMark.enabled = false;
        GreyCheckMark.enabled = false;
        DarkGreyCheckMark.enabled = false;
        RedCheckMark.enabled = false;
        BlueCheckMark.enabled = false;
        DarkBlueCheckMark.enabled = false;
        YellowCheckMark.enabled = false;
        PinkCheckMark.enabled = false;
        PurpleCheckMark.enabled = false;
        BrownCheckMark.enabled = false;
        OrangeCheckMark.enabled = false;
        GreenCheckMark.enabled = false;
        if (colorIndex == 0) {
            BlackCheckMark.enabled = true;
            color = "#000000";
        } else if (colorIndex == 1) {
            WhiteCheckMark.enabled = true;
            color = "#ffffff";
        } else if (colorIndex == 2) {
            GreyCheckMark.enabled = true;
            color = "#9F9F9F";
        } else if (colorIndex == 3) {
            DarkGreyCheckMark.enabled = true;
            color = "#767676";
        } else if (colorIndex == 4) {
            RedCheckMark.enabled = true;
            color = "#FF0000";
        } else if (colorIndex == 5) {
            BlueCheckMark.enabled = true;
            color = "#004BFF";
        } else if (colorIndex == 6) {
            DarkBlueCheckMark.enabled = true;
            color = "#001196";
        } else if (colorIndex == 7) {
            YellowCheckMark.enabled = true;
            color = "#FFFC00";
        } else if (colorIndex == 8) {
            PinkCheckMark.enabled = true;
            color = "#FFA5EA";
        } else if (colorIndex == 9) {
            PurpleCheckMark.enabled = true;
            color = "#6A0DAD";
        } else if (colorIndex == 10) {
            BrownCheckMark.enabled = true;
            color = "#964B00";
        } else if (colorIndex == 11) {
            OrangeCheckMark.enabled = true;
            color = "#FFA500";
        } else if (colorIndex == 12) {
            GreenCheckMark.enabled = true;
            color = "#00DB00";
        }
    }
    public void OpenView(int index) {
        if (index == 0) {
            firstView.SetActive(true);
            secondView.SetActive(false);
        } else if (index == 1 && VadilateFirstView()) {
            firstView.SetActive(false);
            secondView.SetActive(true);
        }
    }
    public void PickImage() {
        NativeGallery.Permission permission = NativeGallery.GetImageFromGallery((path) => {
            if (path != null) {
                // Create Texture from selected image
                Texture2D texture = NativeGallery.LoadImageAtPath(path, -1);
                if (texture == null) {
                    return;
                } else {
                    carImage.sprite = Program.GetImage(texture);
                    largeCarImage.sprite = Program.GetImage(texture);
                }
            }
        },
        "Select a PNG image", "image/png");
        Debug.Log("Permission result: " + permission);
    }
    public void CloseView(int index) {
        if (index == 0) {
            firstView.SetActive(true);
            secondView.SetActive(false);
        } else if (index == 1) {
            firstView.SetActive(false);
            secondView.SetActive(true);
        }
    }

    internal override void Clear() {
        //activate  view so we can clear input field content
        firstView.SetActive(true);
        secondView.SetActive(true);
        //clear content of all inptfield.
        carName.Reset();
        brand.Reset();
        year.Reset();
        maxLuggage.Clear();
        maxSeats.Clear();
        //disable all color checkmark
        BlackCheckMark.enabled = false;
        WhiteCheckMark.enabled = false;
        GreyCheckMark.enabled = false;
        DarkGreyCheckMark.enabled = false;
        RedCheckMark.enabled = false;
        BlueCheckMark.enabled = false;
        DarkBlueCheckMark.enabled = false;
        YellowCheckMark.enabled = false;
        PinkCheckMark.enabled = false;
        PurpleCheckMark.enabled = false;
        BrownCheckMark.enabled = false;
        OrangeCheckMark.enabled = false;
        GreenCheckMark.enabled = false;
        CloseViewChoosenImage();
        update.gameObject.SetActive(false);
        add.gameObject.SetActive(false);
        //open first view
        OpenView(0);
    }
}