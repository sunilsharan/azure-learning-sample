using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace User.Profile.Model
{
    public class Address
    {

        [JsonProperty("addressLine1")]
        public string AddressLine1 { get; set; }

        [JsonProperty("addressLine2")]
        public string AddressLine2 { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }


        [JsonProperty("state")]
        public string State { get; set; }


        [JsonProperty("postalcode")]
        public string PostalCode { get; set; }

    }
}
