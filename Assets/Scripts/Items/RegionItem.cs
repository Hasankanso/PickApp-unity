using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RegionItem : Panel
{
    public Image deleteButton;
    private Location regionL;
    public InputFieldScript region;
    private BecomeDriver becomeDriver;
    public void DeleteRegion()
    {
        BecomeDriver.regionCounter -= 1;
        Destroy(gameObject);
    }
    public void EditRegion(Dropdown regionDropdown)
    {
        //    this.regionDropdown = regionDropdown;
    }
    public void Init(BecomeDriver becomeDriver)
    {
        this.becomeDriver = becomeDriver;

        if (BecomeDriver.regionCounter == 0)
        {
            deleteButton.gameObject.SetActive(false);
        }
    }
    public void Init(string region,BecomeDriver becomeDriver)
    {
        this.becomeDriver = becomeDriver;
        if (BecomeDriver.regionCounter == 0)
        {
            deleteButton.gameObject.SetActive(false);
        }
    }

    public void OnLocationPicked(Location loc)
    {
        regionL = loc;
        region.GetComponent<InputField>().text = regionL.Name;
        region.PlaceHolder();
    }
    public void OpenLocationPicker()
    {
        becomeDriver.OpenLocationFinder(region.text.text, OnLocationPicked);
    }
    public bool Validate()
    {
        bool valid = true;
        if (region.text.text.Equals(""))
        {
            valid = false;
        }
        return valid;
    }
    public Location getRegion()
    {
        return regionL;
    }
    internal override void Clear()
    {
        throw new NotImplementedException();
    }
}
