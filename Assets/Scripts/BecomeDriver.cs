using Requests;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class BecomeDriver : Panel {
    public Driver driver = null;
    public Text title;
    public Button becomeDriver, editRegions;
    public ListView listView;
    public List<RegionItem> regionItems = new List<RegionItem>();
    private List<Location> regions = new List<Location>();
    public static int regionCounter = 0;
    public void submit() {
        if (Validate()) {
            regions.Clear();
            foreach (var item in regionItems) {
                regions.Add(item.getRegion());
            }
            driver = new Driver(regions);
            AddCarPanel p = PanelsFactory.CreateAddCar();
            Open(p, () => {
                p.Init(driver);
            });
        }
    }

    public void EditRegions() {
        if (Validate()) {
            foreach (var item in regionItems) {
                regions.Add(item.getRegion());
            }
            driver = new Driver(regions);
            Request<Driver> request = new EditRegions(Program.User, driver);
            request.AddSendListener(OpenSpinner);
            request.AddReceiveListener(CloseSpinner);
            request.Send(EditResponse);
        }
    }
    private void EditResponse(Driver driver, int code, string message) {
        if (!code.Equals((int)HttpStatusCode.OK)) {
            OpenDialog(message, false);
        } else {
            Program.Driver.regions.Clear();
            Program.Driver.regions = driver.regions;
            MissionCompleted(ProfilePanel.PANELNAME, "Regions Updated", true);
        }
    }
    public void AddItemToList() {
        if (regionCounter <= 2) {
            var obj = ItemsFactory.CreateRegionItem(listView.scrollContainer, this);
            listView.Add(obj.gameObject);
            regionItems.Add(obj);
            regionCounter += 1;
            if (regionCounter == 1) {
                regionItems[0].deleteButton.gameObject.SetActive(false);
            } else {
                regionItems[0].deleteButton.gameObject.SetActive(true);
            }
        } else OpenDialog("You have added the maximum number of regions", false);
    }
    public void AddItemToList(List<Location> regions) {
        foreach (var region in regions) {
            var obj = ItemsFactory.CreateRegionItem(listView.scrollContainer, region, this);
            listView.Add(obj.gameObject);
            regionItems.Add(obj);
            regionCounter += 1;
            ValidateDeleteRegion();
        }
    }
    public void AddRegion() {

        if (ValidateAddingRegions()) {
            AddItemToList();
        }
    }
    public override void Init() {

        if (Status == StatusE.ADD) {
            Clear();
            AddItemToList();
            title.text = "Become A Driver";
            editRegions.gameObject.SetActive(false);
            becomeDriver.gameObject.SetActive(true);

        } else if (Status == StatusE.VIEW) {
            Clear();
            AdMob.InitializeBannerView();
            AddItemToList(Program.Driver.regions);
            title.text = "Regions";
            editRegions.gameObject.SetActive(true);
            becomeDriver.gameObject.SetActive(false);
        }

    }

    internal override void Clear() {
        listView.Clear();
        regionCounter = 0;
        editRegions.gameObject.SetActive(false);
        becomeDriver.gameObject.SetActive(true);
        title.text = "Become a driver";
    }
    public bool Validate() {
        for (int i = 0; i < regionItems.Count; i++) {
            if (!regionItems[i].Validate()) {
                OpenDialog("Insert the region then click next", false);
                return false;
            }
        }
        if (regionItems.Count == 4) {
            OpenDialog("You have Add The Maximum Number Of Regions", false);
            return false;
        }
        return true;
    }
    public bool ValidateAddingRegions() {
        for (int i = 0; i < regionItems.Count; i++) {
            if (!regionItems[i].Validate()) {
                OpenDialog("Please add the previous region first", false);
                return false;
            }
        }
        return true;
    }
    public bool ValidateRegionName(string region) {
        for (int i = 0; i < regionItems.Count; i++) {
            if (regionItems[i].ToString().Equals(region)) {
                OpenDialog("This region is already exist", false);
                return false;
            }
        }
        return true;
    }
    public void ValidateDeleteRegion() {
        for (int i = 0; i < regionCounter; i++) {
            if (i > 0) {
                regionItems[i].deleteButton.gameObject.SetActive(true);
            } else regionItems[i].deleteButton.gameObject.SetActive(false);
        }
    }
}