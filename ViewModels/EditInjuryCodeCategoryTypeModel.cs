using DevExpress.Maui.DataForm;
using SPMSCAV1.Models;
using SPMSCAV1.Services.Interface;
using System.Web;

namespace SPMSCAV1.ViewModels
{
    public class EditInjuryCodeCategoryTypeViewModel : BaseViewModel, IQueryAttributable
    {
        public const string ViewName = "EditInjuryCodeCategoryTypePage";

        string description;
        string code;


        IInjuryCodeCategoryTypeService _dataService;
        public EditInjuryCodeCategoryTypeViewModel(IInjuryCodeCategoryTypeService dataService)
        {
            Title = "Edit InjuryCodeCategoryType";
            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
            _dataService = dataService;
        }


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


        [DataFormDisplayOptions(IsVisible = false)]
        public Command SaveCommand { get; }

        [DataFormDisplayOptions(IsVisible = false)]
        public Command CancelCommand { get; }

        [DataFormDisplayOptions(IsVisible = false)]
        public long Id { get; set; }


        bool ValidateSave()
        {
            return !String.IsNullOrWhiteSpace(this.description)
                && !String.IsNullOrWhiteSpace(this.code);
        }

        async void OnCancel()
        {
            // This will pop the current page off the navigation stack
            await Navigation.GoBackAsync();
        }

        async void OnSave()
        {
            InjuryCodeCategoryTypeModel editInjuryCodeCategoryType = new InjuryCodeCategoryTypeModel()
            {
                InjuryCodeCategoryTypeId = Id,
                Description = description,
                Code = code,
                CreatedBy = Environment.UserName,
                Created = DateTime.Now,
                UpdatedBy = Environment.UserName,
                Updated = DateTime.Now,
                Version = 1,
                Active = true
            };

            await _dataService.AttachAndSaveAsync(editInjuryCodeCategoryType, Id);

            // This will pop the current page off the navigation stack
            await Navigation.GoBackAsync();
        }

        public async Task LoadInjuryCodeCategoryTypeId(long injuryCodeCategoryTypeId)
        {
            try
            {
                var injuryCodeCategoryType = await _dataService.GetByKeyAsync(injuryCodeCategoryTypeId);
                Id = injuryCodeCategoryType.InjuryCodeCategoryTypeId;
                Description = injuryCodeCategoryType.Description;
                Code = injuryCodeCategoryType.Code;
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
            await LoadInjuryCodeCategoryTypeId(id);
        }

        public async void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            string id = HttpUtility.UrlDecode(query["id"] as string);
            long injuryCodeCategoryTypeId;
            long.TryParse(id, out injuryCodeCategoryTypeId);
            await LoadInjuryCodeCategoryTypeId(injuryCodeCategoryTypeId);
        }
    }
}