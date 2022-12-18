using SPMSCAV1.Services.Interface;
using System.Web;

namespace SPMSCAV1.ViewModels
{
    public class GoodAndServiceTypeDetailViewModel : BaseViewModel, IQueryAttributable
    {
        public const string ViewName = "GoodAndServiceTypeDetailPage";


        string description;
        string code;

        IGoodAndServiceTypeService _dataService;

        public GoodAndServiceTypeDetailViewModel(IGoodAndServiceTypeService dataService)
        {
            _dataService = dataService;
            EditGoodAndServiceTypeCommand = new Command(EditGoodAndServiceType);
        }
        public double Id { get; set; }

        public Command EditGoodAndServiceTypeCommand { get; }

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

        public async Task LoadGoodAndServiceTypeId(long genderId)
        {
            try
            {
                var gender = await _dataService.GetByKeyAsync(genderId);
                Id = gender.GoodAndServiceTypeId;
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
            await LoadGoodAndServiceTypeId(id);
        }

        public async void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            string id = HttpUtility.UrlDecode(query["id"] as string);
            long genderId;
            long.TryParse(id, out genderId);
            await LoadGoodAndServiceTypeId(genderId);
        }

        public async void EditGoodAndServiceType()
        {
            if (Id < 0)
                return;
            //await Navigation.NavigateToAsync<EditGoodAndServiceTypeViewModel>(Id);
            await Navigation.NavigateToAsync<EditGoodAndServiceTypeViewModel>(Id);
        }
    }
}