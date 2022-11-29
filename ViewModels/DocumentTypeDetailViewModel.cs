using SPMSCAV1.Services.Interface;
using System.Web;

namespace SPMSCAV1.ViewModels
{
    public class DocumentTypeDetailViewModel : BaseViewModel, IQueryAttributable
    {
        public const string ViewName = "DocumentTypeDetailPage";


        string description;
        string code;

        IDocumentTypeService _dataService;

        public DocumentTypeDetailViewModel(IDocumentTypeService dataService)
        {
            _dataService = dataService;
        }
        public double Id { get; set; }

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

        public async Task LoadDocumentTypeId(long genderId)
        {
            try
            {
                var gender = await _dataService.GetByKeyAsync(genderId);
                Id = gender.DocumentTypeId;
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
            await LoadDocumentTypeId(id);
        }

        public async void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            string id = HttpUtility.UrlDecode(query["id"] as string);
            long genderId;
            long.TryParse(id, out genderId);
            await LoadDocumentTypeId(genderId);
        }

        public async void EditDocumentType()
        {
            if (Id < 0)
                return;
            //await Navigation.NavigateToAsync<EditDocumentTypeViewModel>(Id);
            await Navigation.NavigateToAsync<EditDocumentTypeViewModel>(Id);
        }
    }
}