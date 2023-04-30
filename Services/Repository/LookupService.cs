using SPMSCAV1.Models;
using SPMSCAV1.Services.Interface;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPMSCAV1.Services.Repository
{
  
    public class LookupService
    {
        private static LookupService _lookupService;
        public static List<GenderModel> _GenderList = new List<GenderModel>();
        public static List<CountryModel> _CountryList = new List<CountryModel>();
        public static List<ProvinceOrStateModel> _ProvinceOrStateList = new List<ProvinceOrStateModel>();
        public static List<MaritalStateModel> _MaritalStateList = new List<MaritalStateModel>();
        public static List<PrefixModel> _PrefixList = new List<PrefixModel>();

        private LookupService()
        {
            LoadGenderModels();
            LoadProvinceOrStateModels();
            LoadCountryModels();
            LoadMaritalStateModels();
            LoadPrefixModels();
        }

        public static LookupService  Getinstance()
        {
            if (_lookupService == null)
            {
                _lookupService= new LookupService();
            }
            return _lookupService;
        }

        public List<GenderModel> GetGenderModels
        {
            get
            {
                return _GenderList;
            }
        }

        public List<CountryModel> GetCountryModels
        {
            get
            {
                return _CountryList;
            }
        }

        public List<MaritalStateModel> GetMaritalStateModels
        {
            get
            {
                return _MaritalStateList;
            }
        }

        public List<ProvinceOrStateModel> GetProvinceOrStateModels
        {
            get
            {
                return _ProvinceOrStateList;
            }
        }

        public List<PrefixModel> GetPrefixModels
        {
            get
            {
                return _PrefixList;
            }
        }

        public void RefreshModels()
        {
            LoadGenderModels();
            LoadProvinceOrStateModels();
            LoadCountryModels();
            LoadMaritalStateModels();
            LoadPrefixModels();
        }

        private async void LoadGenderModels()
        {
            IGenderService genderService = new GenderService();
            _GenderList.Clear();
            var genders = await genderService.GetListAsync();

            foreach (var gender in genders)
            {
                _GenderList.Add(gender);

            }
        }

        private async void LoadProvinceOrStateModels()
        {
            IProvinceOrStateService provinceOrStateService = new ProvinceOrStateService();
            _ProvinceOrStateList.Clear();
            var provinceOrStates = await provinceOrStateService.GetListAsync();

            foreach (var provinceOrState in provinceOrStates)
            {
                _ProvinceOrStateList.Add(provinceOrState);

            }
        }

        private async void LoadCountryModels()
        {
            ICountryService countryService = new CountryService();
            _CountryList.Clear();
            var countries = await countryService.GetListAsync();

            foreach (var country in countries)
            {
                _CountryList.Add(country);

            }
        }

        private async void LoadMaritalStateModels()
        {
            IMaritalStateService maritalStateService = new MaritalStateService();
            _MaritalStateList.Clear();
            var maritalStates = await maritalStateService.GetListAsync();

            foreach (var maritalState in maritalStates)
            {
                _MaritalStateList.Add(maritalState);

            }
        }

        private async void LoadPrefixModels()
        {
            IPrefixService prefixService = new PrefixService();
            _PrefixList.Clear();
            var prefixs = await prefixService.GetListAsync();

            foreach (var prefix in prefixs)
            {
                _PrefixList.Add(prefix);

            }
        }
    }
}
