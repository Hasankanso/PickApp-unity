using Requests;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class BecomeDriver : Panel {
    public Person person = null;
    public Driver driver = null;
    public Text title;
    public Button becomeDriver, editRegions;
    public ListView listView;
    private List<RegionItem> regionItems = new List<RegionItem>();
    private List<string> regions = new List<string>();
    public static int regionCounter = 0;
    public void submit() {
        if (Validate()) {
            regions.Clear();
            foreach (var item in regionItems) {
                regions.Add(item.getRegion());
                //    regionItems.Add(item);
            }

            driver = new Driver(regions);
          //  Request<Driver> request = new BecomeDriverRequest(Program.User,driver);
          //  request.Send(response);
            Panel p = PanelsFactory.createAddCar(driver);
            openCreated(p);
        }
    }
    private void response(Driver result, int code, string message) {
        if (!code.Equals(HttpStatusCode.OK)) {
            Panel p = PanelsFactory.CreateDialogBox("Something went wrong", false);
            OpenDialog(p);
        } else {
            Panel p = PanelsFactory.CreateDialogBox("You become a driver", true);
            OpenDialog(p);
            Panel panel = PanelsFactory.createAddCar();
            openCreated(panel);
        }
    }
    public void EditRegions() {
        if (Validate()) {
            foreach (var item in regionItems) {
                driver.Regions.Add(item.getRegion());
            }
            //    Request<BecomeDriver> request = new BecomeDriver(becomeDriver);
            //     Task.Run(() => request.Send(EditResponse));
        }
    }
    private void EditResponse(Person result, HttpStatusCode code, string message) {
        if (!code.Equals(HttpStatusCode.OK)) {
            Panel p = PanelsFactory.CreateDialogBox("Something went wrong", false);
            OpenDialog(p);
        } else {
            Panel p = PanelsFactory.CreateDialogBox("Your regions ha been edited", true);
            OpenDialog(p);
            Panel panel = PanelsFactory.createAddCar();
            openCreated(panel);
        }
    }
    public void AddItemToList() {
        if (regionCounter <= 2) {
            var obj = ItemsFactory.CreateRegionItem(listView.scrollContainer,this);
            listView.Add(obj.gameObject);
            regionItems.Add(obj);
            regionCounter += 1;
        } else OpenDialog("You have added the maximum number of regions", false);
    }
    public void AddItemToList(List<string> regions) {
        foreach (var region in regions) {
            var obj = ItemsFactory.CreateRegionItem(listView.scrollContainer, region,this);
            listView.Add(obj.gameObject);
            regionItems.Add(obj);
            regionCounter += 1;
        }
    }
    public void AddRegion() {
        AddItemToList();
    }
    public void Init(Person person) {
        Clear();
        this.person = person;
        AddItemToList();
    }
    public void Init() {
        Clear();
        this.person = Program.Person;
        AddItemToList();
        title.text = "Regions";
        editRegions.gameObject.SetActive(false);
        becomeDriver.gameObject.SetActive(true);
    }
    internal override void Clear() {
        listView.Clear();
        regionCounter = 0;
        editRegions.gameObject.SetActive(false);
        becomeDriver.gameObject.SetActive(true);
        title.text = "Become a driver";
    }
    public bool Validate() {
        bool valid = true;
        for (int i = 0; i < regionItems.Count; i++) {
            if (!regionItems[i].Validate()) {
                OpenDialog("Please select region", false);
                valid = false;
            }
        }
        return valid;
    }
}
