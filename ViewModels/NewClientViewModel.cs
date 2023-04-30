using DevExpress.Maui.DataForm;
using SPMSCAV1.Models;
using SPMSCAV1.Services.Interface;
using SPMSCAV1.Services.Repository;
using System.ComponentModel.DataAnnotations;

namespace SPMSCAV1.ViewModels
{
    public class NewClientViewModel : BaseViewModel
    {
        public const string ViewName = "NewClientPage";

        LookupService lookupService;

        string prefixId;
        string firstName;
        string middleName;
        string lastName;
        string suffix;
        string genderId;
        DateTime dateofBirth = DateTime.Today;
        string address1;
        string address2;
        string city;
        string provinceOrStateId;
        string postalCodeOrZipCode;
        string countryId;
        string homePhone;
        string cellPhone;
        string persanalFax;
        string personalEmail;
        string maritalStatusId;
        string socialInsuranceNumber;
        string healthCardNumber;
        string driversLicenseNumber;
        int weight;
        int height;
        string occupation;
        string workPhone;
        string workPhoneExtension;
        string workFax;
        string workEmail;


        IClientService _dataService;
        public NewClientViewModel(IClientService dataService)
        {
            Title = "New Client";
            lookupService = LookupService.Getinstance();
            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
            _dataService = dataService;


        }

        [DataFormComboBoxEditor]
        public string PrefixId
        {
            get => this.prefixId;
            set => SetProperty(ref this.prefixId, value);
        }
        public string FirstName
        {
            get => this.firstName;
            set => SetProperty(ref this.firstName, value);
        }
        public string MiddleName
        {
            get => this.middleName;
            set => SetProperty(ref this.middleName, value);
        }
        public string LastName
        {
            get => this.lastName;
            set => SetProperty(ref this.lastName, value);
        }
        public string Suffix
        {
            get => this.suffix;
            set => SetProperty(ref this.suffix, value);
        }
        [DataFormComboBoxEditor]
        [Required(ErrorMessage = "Required")]
        public string GenderId
        {
            get => this.genderId;
            set => SetProperty(ref this.genderId, value);
        }
        [DataFormDateEditor]
        public DateTime DateofBirth
        {
            get => this.dateofBirth;
            set => SetProperty(ref this.dateofBirth, value);
        }
        public string Address1
        {
            get => this.address1;
            set => SetProperty(ref this.address1, value);
        }
        public string Address2
        {
            get => this.address2;
            set => SetProperty(ref this.address2, value);
        }
        public string City
        {
            get => this.city;
            set => SetProperty(ref this.city, value);
        }
        [DataFormComboBoxEditor]
        [Required(ErrorMessage = "Required")]
        public string ProvinceOrStateId
        {
            get => this.provinceOrStateId;
            set => SetProperty(ref this.provinceOrStateId, value);
        }
        public string PostalCodeOrZipCode
        {
            get => this.postalCodeOrZipCode;
            set => SetProperty(ref this.postalCodeOrZipCode, value);
        }
        [DataFormComboBoxEditor]
        [Required(ErrorMessage = "Required")]
        public string CountryId
        {
            get => this.countryId;
            set => SetProperty(ref this.countryId, value);
        }
        public string HomePhone
        {
            get => this.homePhone;
            set => SetProperty(ref this.homePhone, value);
        }
        public string CellPhone
        {
            get => this.cellPhone;
            set => SetProperty(ref this.cellPhone, value);
        }
        public string PersonalFax
        {
            get => this.persanalFax;
            set => SetProperty(ref this.persanalFax, value);
        }
        public string PersonalEmail
        {
            get => this.personalEmail;
            set => SetProperty(ref this.personalEmail, value);
        }
        [DataFormComboBoxEditor]
        [Required(ErrorMessage = "Required")]
        public string MaritalStatusId
        {
            get => this.maritalStatusId;
            set => SetProperty(ref this.maritalStatusId, value);
        }
        public string SocialInsuranceNumber
        {
            get => this.socialInsuranceNumber;
            set => SetProperty(ref this.socialInsuranceNumber, value);
        }
        public string HealthCardNumber
        {
            get => this.healthCardNumber;
            set => SetProperty(ref this.healthCardNumber, value);
        }
        public string DriversLicenseNumber
        {
            get => this.driversLicenseNumber;
            set => SetProperty(ref this.driversLicenseNumber, value);
        }
        public int Weight
        {
            get => this.weight;
            set => SetProperty(ref this.weight, value);
        }
        public int Height
        {
            get => this.height;
            set => SetProperty(ref this.height, value);
        }
        public string Occupation
        {
            get => this.occupation;
            set => SetProperty(ref this.occupation, value);
        }
        public string WorkPhone
        {
            get => this.workPhone;
            set => SetProperty(ref this.workPhone, value);
        }
        public string WorkPhoneExtension
        {
            get => this.workPhoneExtension;
            set => SetProperty(ref this.workPhoneExtension, value);
        }
        public string WorkFax
        {
            get => this.workFax;
            set => SetProperty(ref this.workFax, value);
        }
        public string WorkEmail
        {
            get => this.workEmail;
            set => SetProperty(ref this.workEmail, value);
        }


        [DataFormDisplayOptions(IsVisible = false)]
        public Command SaveCommand { get; }

        [DataFormDisplayOptions(IsVisible = false)]
        public Command CancelCommand { get; }


        bool ValidateSave()
        {
            return !String.IsNullOrWhiteSpace(this.firstName)
                && !String.IsNullOrWhiteSpace(this.lastName);
        }

        async void OnCancel()
        {
            // This will pop the current page off the navigation stack
            await Navigation.GoBackAsync();
        }

        async void OnSave()
        {

            long _countryId;
            long _genderId;
            long _maritalStatusId;
            long _provinceOrStateId;
            long _prefixId;

            DateTime _dateOfBirth = DateofBirth;
            if (CountryId!= null)
            {
                _countryId = lookupService.GetCountryModels.Find(i => i.Description == CountryId).CountryId;
            }
            else
            {
                _countryId = 0;
            }
            if (GenderId != null)
            {
                _genderId = lookupService.GetGenderModels.Find(i => i.Description == GenderId).GenderId;
            }
            else
            {
                _genderId = 0;
            }


            if (MaritalStatusId != null)
            {
                _maritalStatusId = lookupService.GetMaritalStateModels.Find(i => i.Description == MaritalStatusId).MaritalStatusId;
            }
            else
            {
                _maritalStatusId = 0;
            }

            if (ProvinceOrStateId != null)
            {
                _provinceOrStateId = lookupService.GetProvinceOrStateModels.Find(i => i.Description == ProvinceOrStateId).ProvinceOrStateId;
            }
            else
            {
                _provinceOrStateId = 0;
            }

            if (PrefixId != null)
            {
                _prefixId = lookupService.GetPrefixModels.Find(i => i.Description == PrefixId).PrefixId;
            }
            else
            {
                _prefixId = 0;
            }

            ClientModel newClient = new ClientModel()
            {
                //ClientId = (long)_dataService.GetListAsync().Result.Count + 1,
                PrefixId = _prefixId,
                FirstName = firstName,
                MiddleName = middleName,
                LastName = lastName,
                Suffix = suffix,
                GenderId = _genderId,
                DateofBirth = _dateOfBirth,
                Address1 = address1,
                Address2 = address2,
                City = city,
                ProvinceOrStateId = _provinceOrStateId,
                PostalCodeOrZipCode = postalCodeOrZipCode,
                CountryId = _countryId,
                HomePhone = homePhone,
                CellPhone = cellPhone,
                PersonalFax = persanalFax,
                PersonalEmail = personalEmail,
                MaritalStatusId = _maritalStatusId,
                SocialInsuranceNumber = socialInsuranceNumber,
                HealthCardNumber = healthCardNumber,
                DriversLicenseNumber = driversLicenseNumber,
                Weight = weight,
                Height = height,
                Occupation = occupation,
                WorkPhone = workPhone,
                WorkPhoneExtension = workPhoneExtension,
                WorkFax = workFax,
                WorkEmail = workEmail,
                CreatedBy = Environment.UserName,
                Created = DateTime.Now,
                UpdatedBy = Environment.UserName,
                Updated = DateTime.Now,
                Version = 1,
                Active = true
            };

            await _dataService.AddAndSaveAsync(newClient);

            // This will pop the current page off the navigation stack
            await Navigation.GoBackAsync();
        }
    }
}