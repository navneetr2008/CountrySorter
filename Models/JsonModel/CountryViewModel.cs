using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CountrySorter
{// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class Currency
    {
        public string name { get; set; }
    }

    public class Language
    {
        public string name { get; set; }
    }

    public class CountryData
    {
        public string name { get; set; }
        public string capital { get; set; }
        public string region { get; set; }
        public string subregion { get; set; }
        public List<Currency> currencies { get; set; }
        public List<Language> languages { get; set; }
    }


}
