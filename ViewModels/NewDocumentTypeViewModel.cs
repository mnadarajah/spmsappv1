using DevExpress.Maui.DataForm;
using SPMSCAV1.Models;
using SPMSCAV1.Services.Interface;

namespace SPMSCAV1.ViewModels
{
    public class NewDocumentTypeViewModel : BaseViewModel
    {
        public const string ViewName = "NewDocumentTypePage";


        string description;
        string code;


        IDocumentTypeService _dataService;
        public NewDocumentTypeViewModel(IDocumentTypeService dataService)
        {
            Title = "New DocumentType";
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
            DocumentTypeModel newDocumentType = new DocumentTypeModel()
            {
                //DocumentTypeId = (long)_dataService.GetListAsync().Result.Count + 1,
                Description = description,
                Code = code,
                CreatedBy = Environment.UserName,
                Created = DateTime.Now,
                UpdatedBy = Environment.UserName,
                Updated = DateTime.Now,
                Version = 1,
                Active = true
            };

            await _dataService.AddAndSaveAsync(newDocumentType);

            // This will pop the current page off the navigation stack
            await Navigation.GoBackAsync();
        }

       /* public override async Task InitializeAsync(object parameter)
        {
        }*/

    }
}