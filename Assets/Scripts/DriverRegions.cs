using Requests;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class DriverRegions : Panel
{
    public Person person ;
    public Driver driver ;
    public Text title;
    public Button update;
    public ListView listView;
    public List<RegionItem> regionItems = new List<RegionItem>();
    private List<Location> regions = new List<Location>();
    public  int regionCounter = 0;
    public void submit()
    {
        if (Validate())
        {
            regions.Clear();
            foreach (var item in regionItems)
            {
                regions.Add(item.getRegion());
            }
            driver = new Driver(regions);
        }
    }


    public void AddItemToList()
    {
        if (regionCounter <= 2)
        {
            var obj = ItemsFactory.CreateRegionItem(listView.scrollContainer, this);
            listView.Add(obj.gameObject);
            regionItems.Add(obj);
            regionCounter += 1;
            Debug.Log(1);
            if (regionCounter == 1)
            {
                regionItems[0].deleteButton.gameObject.SetActive(false);
            }
            else
            {
                regionItems[0].deleteButton.gameObject.SetActive(true);
            }
        }
        else OpenDialog("You have added the maximum number of regions", false);
    }

    public void AddRegion()
    {

        if (ValidateAddingRegions())
        {
            AddItemToList();
        }
    }
    public override void Init()
    {
        Clear();
        this.person = Program.Person;
        AddItemToList();
        title.text = "Regions";
    }

    internal override void Clear()
    {
        listView.Clear();
        regionCounter = 0;
        title.text = "My Regions";
    }
    public bool Validate()
    {
        bool valid = true;
        for (int i = 0; i < regionItems.Count; i++)
        {
            if (!regionItems[i].Validate())
            {
                OpenDialog("Insert the region then click next", false);
                valid = false;
            }

        }
        if (regionItems.Count == 4)
        {
            OpenDialog("You have Add The Maximum Number Of Regions", false);
            valid = false;

        }


        return valid;
    }
    public bool ValidateAddingRegions()
    {
        bool valid = true;
        print(regionItems.Count + " regionItems");
        for (int i = 0; i < regionItems.Count; i++)
        {
            if (!regionItems[i].Validate())
            {
                OpenDialog("Please add the previous region first", false);
                valid = false;
            }
        }
        return valid;
    }
    public bool ValidateRegionName(string region)
    {
        bool valid = true;
        for (int i = 0; i < regionItems.Count; i++)
        {
            if (regionItems[i].ToString().Equals(region))
            {
                OpenDialog("This region is already exist", false);
                valid = false;
            }
        }
        return valid;
    }
    public void ValidateDeleteRegion()
    {
        for (int i = 0; i < regionCounter; i++)
        {
            if (i > 0)
            {
                regionItems[i].deleteButton.gameObject.SetActive(true);
            }
            else regionItems[i].deleteButton.gameObject.SetActive(false);
        }
    }
}
