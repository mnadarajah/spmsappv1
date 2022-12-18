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
        private static ObservableCollection<GenderModel> _GenderList = new ObservableCollection<GenderModel>();
        private static ObservableCollection<CountryModel> _CountryList = new ObservableCollection<CountryModel>();
        private static ObservableCollection<ProvinceOrStateModel> _ProvinceOrStateList = new ObservableCollection<ProvinceOrStateModel>();
        private static ObservableCollection<MaritalStateModel> _MaritalStateList = new ObservableCollection<MaritalStateModel>();

        private LookupService()
        {
            LoadGenderModels();
            LoadProvinceOrStateModels();
            LoadCountryModels();
            LoadMaritalStateModels();
        }

        public static LookupService  Getinstance()
        {
            if (_lookupService == null)
            {
                _lookupService= new LookupService();
            }
            return _lookupService;
        }

        public ObservableCollection<GenderModel> GetGenderModels
        {
            get
            {
                return _GenderList;
            }
        }

        public ObservableCollection<CountryModel> GetCountryModels
        {
            get
            {
                return _CountryList;
            }
        }

        public ObservableCollection<MaritalStateModel> GetMaritalStateModels
        {
            get
            {
                return _MaritalStateList;
            }
        }

        public ObservableCollection<ProvinceOrStateModel> GetProvinceOrStateModels
        {
            get
            {
                return _ProvinceOrStateList;
            }
        }

        public void RefreshModels()
        {
            LoadGenderModels();
            LoadProvinceOrStateModels();
            LoadCountryModels();
            LoadMaritalStateModels();
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
    }
}
