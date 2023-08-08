using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Models
{
    public class YearMasterResponse
    {
        public int iYearId { get; set; }
        public string sYear { get; set; }
        public DateTime? dFromDate { get; set; }
        public DateTime? dToDate { get; set; }
    }

    public class CountryMasterResponse
    {
        public int iCountryId { get; set; }
        public string sCountryCode { get; set; }
        public string sCountryName { get; set; }
    }


    public class CityListAutocompleteResquest
    {
        public string sSearch { get; set; }
        public int CountryId { get; set; }

    }

    public class CityListAutocompleteResponse
    {
        public int iCityId { get; set; }
        public string sCityName { get; set; }
        public int iCountryId { get; set; }
        public int iStateId { get; set; }
        public int iSTDCode { get; set; }
        public string sStateName { get; set; }
        public int iTaxCode { get; set; }
        public string sCountryName { get; set; }
        public string sISDCode { get; set; }

    }
}
