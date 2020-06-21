using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RegionItem : Panel {
    public Dropdown regionDropdown;
    public Image deleteButton;
    public void DeleteRegion() {
        BecomeDriver.regionCounter -= 1;
        Destroy(gameObject);
    }
    public void EditRegion(Dropdown regionDropdown) {
        this.regionDropdown = regionDropdown;
    }
    public void Init() {
        if (BecomeDriver.regionCounter == 0) {
            deleteButton.gameObject.SetActive(false);
        }
    }
    public void Init(string region) {
        if (BecomeDriver.regionCounter == 0) {
            deleteButton.gameObject.SetActive(false);
        }
        SetRegion(region);
    }

    private void SetRegion(string region) {
        if (region.Equals("Akkar"))
            regionDropdown.value = 1;
        else if (region.Equals("Aley"))
            regionDropdown.value = 2;
        else if (region.Equals("Baalbek"))
            regionDropdown.value = 3;
        else if (region.Equals("Baabda")) 
            regionDropdown.value = 4;
        else if (region.Equals("Beirut"))
            regionDropdown.value = 5;
        else if (region.Equals("Chouf")) 
            regionDropdown.value = 6;
        else if (region.Equals("Hermel")) 
            regionDropdown.value = 7;
        else if (region.Equals("Keserwan"))
            regionDropdown.value = 8;
        else if (region.Equals("Matn")) 
            regionDropdown.value = 9;
        else if (region.Equals("Jbeil")) 
            regionDropdown.value = 10;
        else if (region.Equals("Rashaya")) 
            regionDropdown.value = 11;
        else if (region.Equals("Western Beqaa"))
            regionDropdown.value = 12;
        else if (region.Equals("Saida"))
            regionDropdown.value = 13;
        else if (region.Equals("Bintjbeil")) 
            regionDropdown.value = 14;
        else if (region.Equals("Hasbaya")) 
            regionDropdown.value = 15;
        else if (region.Equals("Marjeyoun"))
            regionDropdown.value = 16;
        else if (region.Equals("Batroun")) 
            regionDropdown.value = 17;
        else if (region.Equals("Bsharri")) 
            regionDropdown.value = 18;
        else if (region.Equals("Nabatieh"))
            regionDropdown.value = 19;
        else if (region.Equals("Jezzine")) 
            regionDropdown.value = 20;
        else if (region.Equals("Koura")) 
            regionDropdown.value = 21;
        else if (region.Equals("Minieh-Dannieh"))
            regionDropdown.value = 22;
        else if (region.Equals("Tripoli")) 
            regionDropdown.value = 23;
        else if (region.Equals("Tyre")) 
            regionDropdown.value = 24;
        else if (region.Equals("Zahle"))
            regionDropdown.value = 25;
        else if (region.Equals("Zgharta")) 
            regionDropdown.value = 26;
    }


    public bool Validate() {
        bool valid = true;
        if (regionDropdown.value == 0) {
            valid = false;
        }
        return valid;
    }
    public string getRegion() {
        return regionDropdown.options[regionDropdown.value].text;
    }
    internal override void Clear() {
        throw new NotImplementedException();
    }
}
