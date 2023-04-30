using DevExpress.Maui.DataForm;
using SPMSCAV1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPMSCAV1.Services.Repository
{
    public class ComboBoxDataProviderService : IPickerSourceProvider
    {

        List<GenderModel> genderModels = new List<GenderModel>();
        List<CountryModel> countryModels = new List<CountryModel>();
        List<ProvinceOrStateModel> provinceOrStateModels = new List<ProvinceOrStateModel>();
        List<MaritalStateModel> maritalStateModels = new List<MaritalStateModel>();
        List<PrefixModel> prefixModels = new List<PrefixModel>();
        List<string> labelIds = CalendarModel.AppointmentTypes.ToList();
        LookupService _lookupService = LookupService.Getinstance();
        public System.Collections.IEnumerable GetSource(string propertyName)
        {
            if (genderModels.Count() == 0)
            {
                genderModels = _lookupService.GetGenderModels;
            }

            if (countryModels.Count() == 0)
            {
                countryModels = _lookupService.GetCountryModels;
            }
            if (provinceOrStateModels.Count() == 0)
            {
                provinceOrStateModels = _lookupService.GetProvinceOrStateModels;
            }
            if (maritalStateModels.Count() == 0)
            {
                maritalStateModels = _lookupService.GetMaritalStateModels;
            }
            if (prefixModels.Count() == 0)
            {
                prefixModels = _lookupService.GetPrefixModels;
            }

            if (propertyName == "CountryId")
            {
                List<String> countryDescriptions = new List<string>();
                foreach (var country in countryModels)
                {
                    countryDescriptions.Add(country.Description);
                }
                return countryDescriptions;
            }
            if (propertyName == "ProvinceOrStateId")
            {
                List<String> provinceOrStateDescriptions = new List<string>();
                foreach (var provinceOrState in provinceOrStateModels)
                {
                    provinceOrStateDescriptions.Add(provinceOrState.Description);
                }
                return provinceOrStateDescriptions;
            }
            if (propertyName == "GenderId")
            {
                List<String> genderDescriptions = new List<string>();
                foreach (var gender in genderModels)
                {
                    genderDescriptions.Add(gender.Description);
                }
                return genderDescriptions;
            }
            if (propertyName == "MaritalStatusId")
            {
                List<String> maritalStatusDescriptions = new List<string>();
                foreach (var maritalState in maritalStateModels)
                {
                    maritalStatusDescriptions.Add(maritalState.Description);
                }
                return maritalStatusDescriptions;
            }
            if (propertyName == "PrefixId")
            {
                List<String> prefixDescriptions = new List<string>();
                foreach (var prefixState in prefixModels)
                {
                    prefixDescriptions.Add(prefixState.Description);
                }
                return prefixDescriptions;
            }
            if (propertyName == "LabelId")
            {
                return labelIds;
            }
            return null;

        }

    }
}
