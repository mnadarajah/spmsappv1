using DevExpress.Maui.DataForm;
using SPMSCAV1.Models;
using SPMSCAV1.Services.Interface;

namespace SPMSCAV1.ViewModels
{
    public class NewInjuryCodeSeriesTypeViewModel : BaseViewModel
    {
        public const string ViewName = "NewInjuryCodeSeriesTypePage";


        string description;
        string code;


        IInjuryCodeSeriesTypeService _dataService;
        public NewInjuryCodeSeriesTypeViewModel(IInjuryCodeSeriesTypeService dataService)
        {
            Title = "New InjuryCodeSeriesType";
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
            InjuryCodeSeriesTypeModel newInjuryCodeSeriesType = new InjuryCodeSeriesTypeModel()
            {
                //InjuryCodeSeriesTypeId = (long)_dataService.GetListAsync().Result.Count + 1,
                Description = description,
                Code = code,
                CreatedBy = Environment.UserName,
                Created = DateTime.Now,
                UpdatedBy = Environment.UserName,
                Updated = DateTime.Now,
                Version = 1,
                Active = true
            };

            await _dataService.AddAndSaveAsync(newInjuryCodeSeriesType);

            // This will pop the current page off the navigation stack
            await Navigation.GoBackAsync();
        }

       /* public override async Task InitializeAsync(object parameter)
        {
        }*/

    }
}