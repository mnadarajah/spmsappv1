using SPMSCAV1.Services.Interface;
using System.Web;

namespace SPMSCAV1.ViewModels
{
    public class GenderDetailViewModel : BaseViewModel, IQueryAttributable
    {
        public const string ViewName = "GenderDetailPage";


        string description;
        string code;

        IGenderService _dataService;

        public GenderDetailViewModel(IGenderService dataService)
        {
            Title = "Detail";
            EditGenderCommand = new Command(EditGender);
            _dataService = dataService;
        }
        public double Id { get; set; }

        public Command EditGenderCommand { get; }

        public string Description
        {
            get => this.description;
            set => SetProperty(ref this.description, value);
        }

        public string Code
        {
            get => this.code;
            set => SetProperty(ref this.code, value);
        }

        public async Task LoadGenderId(long genderId)
        {
            try
            {
                var gender = await _dataService.GetByKeyAsync(genderId);
                Id = gender.GenderId;
                Description = gender.Description;
                Code = gender.Code;
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
            await LoadGenderId(id);
        }

        public async void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            string id = HttpUtility.UrlDecode(query["id"] as string);
            long genderId;
            long.TryParse(id, out genderId);
            await LoadGenderId(genderId);
        }

        public async void EditGender()
        {
            if (Id < 0)
                return;
            await Navigation.NavigateToAsync<EditGenderViewModel>(Id);
        }
    }
}