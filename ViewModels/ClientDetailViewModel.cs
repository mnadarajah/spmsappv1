using SPMSCAV1.Models;
using SPMSCAV1.Services.Interface;
using SPMSCAV1.Services.Repository;
using System.Collections.ObjectModel;
using System.Web;

namespace SPMSCAV1.ViewModels
{
    public class ClientDetailViewModel : BaseViewModel, IQueryAttributable
    {
        public const string ViewName = "ClientDetailPage";

        List<GenderModel> genderModels= new List<GenderModel>();
        List<CountryModel> countryModels= new List<CountryModel>();
        List<ProvinceOrStateModel> provinceOrStateModels= new List<ProvinceOrStateModel>();
        List<MaritalStateModel> maritalStateModels= new List<MaritalStateModel>();

        long prefixId;
        string firstName;
        string middleName;
        string lastName;
        string suffix;
        string genderId;
        DateOnly dateofBirth;
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

        public ClientDetailViewModel(IClientService dataService)
        {
            Title = "Detail";
            EditClientCommand = new Command(EditClient);
            _dataService = dataService;
        }
        public double Id { get; set; }

        public Command EditClientCommand { get; }

        public long PrefixId 
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
        public string GenderId 
        { 
            get => this.genderId; 
            set => SetProperty(ref this.genderId, value); 
        }
        public DateOnly DateofBirth 
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
        public string PersanalFax 
        { 
            get => this.persanalFax; 
            set => SetProperty(ref this.persanalFax, value); 
        }
        public string PersonalEmail 
        { 
            get => this.personalEmail; 
            set => SetProperty(ref this.personalEmail, value); 
        }
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

        public async Task LoadClientId(long clientId)
        {
            try
            {
                LookupService lookupService = LookupService.Getinstance();
                var client = await _dataService.GetByKeyAsync(clientId);
                Id = client.ClientId;
                FirstName = client.FirstName;
                LastName = client.LastName;
                MiddleName = client.MiddleName;
                Suffix = client.Suffix;
                DateofBirth = DateOnly.FromDateTime(client.DateofBirth);
                Address1 = client.Address1;
                Address2 = client.Address2;
                City= client.City;
                PostalCodeOrZipCode = client.PostalCodeOrZipCode;
                SocialInsuranceNumber= client.SocialInsuranceNumber;
                HealthCardNumber = client.HealthCardNumber;
                DriversLicenseNumber= client.DriversLicenseNumber;
                Weight = client.Weight ?? 0;
                Height = client.Height ?? 0;
                Occupation = client.Occupation;
                WorkPhone = client.WorkPhone;
                WorkPhoneExtension= client.WorkPhoneExtension;
                WorkFax = client.WorkFax;
                WorkEmail= client.WorkEmail;

                //CountryId = lookupService.GetCountryModels.Find(i => i.CountryId == client.CountryId).Code;
                //provinceOrStateModels = lookupService.GetProvinceOrStateModels;
                genderModels = lookupService.GetGenderModels.ToList();
                provinceOrStateModels= lookupService.GetProvinceOrStateModels.ToList();
                countryModels= lookupService.GetCountryModels.ToList();
                maritalStateModels= lookupService.GetMaritalStateModels.ToList();
                //ProvinceOrStateId = provinceOrStateModels.Find(i => i.ProvinceOrStateId == client.ProvinceOrStateId).Code;
                GenderId = genderModels.Find(i => i.GenderId == client.GenderId).Description;
                ProvinceOrStateId = provinceOrStateModels.Find(i => i.ProvinceOrStateId == client.ProvinceOrStateId).Description;
                CountryId = countryModels.Find(i => i.CountryId== client.CountryId).Description;
                MaritalStatusId = maritalStateModels.Find(i => i.MaritalStatusId==client.MaritalStatusId).Description;
            }
            catch (Exception)
            {
                System.Diagnostics.Debug.WriteLine("Failed to Load Item");
            }
        }

        public override async Task InitializeAsync(object parameter)
        {
            long id;
            long.TryParse(parameter as string, out id);
            await LoadClientId(id);
        }

        public async void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            string id = HttpUtility.UrlDecode(query["id"] as string);
            long clientId;
            long.TryParse(id, out clientId);
            await LoadClientId(clientId);
        }

        public async void EditClient()
        {
            if (Id < 0)
                return;
            //await Navigation.NavigateToAsync<EditClientViewModel>(Id);
            await Navigation.NavigateToAsync<EditClientViewModel>(Id);
        }
    }
}