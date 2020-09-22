using Newtonsoft.Json.Linq;
using Requests;
using System;
using System.Collections;
using System.Collections.Generic;

public class CountryInformations {
    private string id;
    private string unit;
    private string name;
    private string countryComponent;
    private int digits;
    private string code;
    private DateTime updated;

    public CountryInformations(string id, string unit, string name, int digits, string code, string countryComponent) {
        this.id = id;
        this.unit = unit;
        this.name = name;
        this.digits = digits;
        this.code = code;
        this.countryComponent = countryComponent;
    }

    public CountryInformations() {
    }
    public CountryInformations(string unit) {
        this.Unit = unit;
    }

    public string Unit { get => unit; set => unit = value; }
    public DateTime Updated { get => updated; set => updated = value; }
    public string Name { get => name; set => name = value; }
    public int Digits { get => digits; set => digits = value; }
    public string Code { get => code; set => code = value; }
    public string Id { get => id; set => id = value; }
    public string CountryComponent { get => countryComponent; set => countryComponent = value; }

    public JObject ToJson() {
        JObject countryInformationsJ = new JObject();
        countryInformationsJ[nameof(this.id)] = this.Id;
        return countryInformationsJ;
    }
    public static CountryInformations ToObject(JObject json) {
        string id = "";
        var oId = json["objectId"];
        if (oId != null)
            id = oId.ToString();
        string name = "";
        var nm = json["name"];
        if (nm != null)
            name = nm.ToString();
        string code = "";
        var cd = json["code"];
        if (cd != null)
            code = cd.ToString();
        string unit = "";
        var un = json["unit"];
        if (un != null)
            unit = un.ToString();
        string countryComponent = "";
        var cc = json["countryComponent"];
        if (cc != null)
            countryComponent = cc.ToString();
        int digits = -1;
        var dj = json[nameof(CountryInformations.digits)];
        if (dj != null)
            int.TryParse(dj.ToString(), out digits);
        return new CountryInformations(id, unit, name, digits, code, countryComponent);
    }
    public static bool Equal(string countriesKeys, string countryName) {
        if (countriesKeys.Equals(countryName))
            return true;
        return false;
    }
    public override string ToString() {
        return Unit;
    }
}
