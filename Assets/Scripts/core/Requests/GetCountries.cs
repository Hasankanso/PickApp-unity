using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using UnityEngine;
using System.Net;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace Requests {
    class GetCountries : Request<Dictionary<string, CountryInformations>> {
        private Dictionary<string ,CountryInformations> countryInformations;

        public GetCountries() {
            HttpPath = "/PersonBusiness/GetCountries";
        }

        public override async Task<Dictionary<string, CountryInformations>> BuildResponse(JToken response, int statusCode) {
            var countries = (JArray) response;
            this.countryInformations=new Dictionary<string, CountryInformations>();
            Program.CountriesInformationsNames = new List<string>();
            foreach (var countryJ in countries) {
                CountryInformations country = CountryInformations.ToObject((JObject)countryJ);
                this.countryInformations.Add(country.Name, country);
                Program.CountriesInformationsNames.Add(country.Name);
            }
            return countryInformations;
        }

        public override string ToJson() {
            return "";
        }

        protected override string IsValid() {
            //throw new System.NotImplementedException();
            return null;
        }
    }
}
