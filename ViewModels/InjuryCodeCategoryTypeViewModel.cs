using SPMSCAV1.Models;
using SPMSCAV1.Services.Interface;
using System.Collections.ObjectModel;

namespace SPMSCAV1.ViewModels
{
    public class InjuryCodeCategoryTypeViewModel : BaseViewModel
    {
        IInjuryCodeCategoryTypeService _dataService;
        InjuryCodeCategoryTypeModel _selectedInjuryCodeCategoryType;

        public InjuryCodeCategoryTypeViewModel(IInjuryCodeCategoryTypeService dataService)

        {
            Title = "Browse";
            InjuryCodeCategoryTypes = new ObservableCollection<InjuryCodeCategoryTypeModel>();
            LoadInjuryCodeCategoryTypesCommand = new Command(() => ExecuteLoadInjuryCodeCategoryTypesCommand());
            InjuryCodeCategoryTypeTapped = new Command<InjuryCodeCategoryTypeModel>(OnInjuryCodeCategoryTypeSelected);
            AddInjuryCodeCategoryTypeCommand = new Command(OnAddInjuryCodeCategoryType);
            _dataService = dataService;
        }


        public ObservableCollection<InjuryCodeCategoryTypeModel> InjuryCodeCategoryTypes { get; }

        public Command LoadInjuryCodeCategoryTypesCommand { get; }

        public Command AddInjuryCodeCategoryTypeCommand { get; }

        public Command<InjuryCodeCategoryTypeModel> InjuryCodeCategoryTypeTapped { get; }

        public InjuryCodeCategoryTypeModel SelectedInjuryCodeCategoryType
        {
            get => this._selectedInjuryCodeCategoryType;
            set
            {
                SetProperty(ref this._selectedInjuryCodeCategoryType, value);
                OnInjuryCodeCategoryTypeSelected(value);
            }
        }

        public void OnAppearing()
        {
            IsBusy = true;
            SelectedInjuryCodeCategoryType = null;
            ExecuteLoadInjuryCodeCategoryTypesCommand();
        }

        async void ExecuteLoadInjuryCodeCategoryTypesCommand()
        {
            IsBusy = true;
            try
            {
                InjuryCodeCategoryTypes.Clear();
                var injuryCodeCategorys = await _dataService.GetListAsync();
                foreach (var injuryCodeCategory in injuryCodeCategorys)
                {
                    injuryCodeCategory.Description = injuryCodeCategory.Description;
                    InjuryCodeCategoryTypes.Add(injuryCodeCategory);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        async void OnAddInjuryCodeCategoryType(object obj)
        {
            await Navigation.NavigateToAsync<NewInjuryCodeCategoryTypeViewModel>(null);
        }

        async void OnInjuryCodeCategoryTypeSelected(InjuryCodeCategoryTypeModel injuryCodeCategory)
        {
            if (injuryCodeCategory == null)
                return;
            await Navigation.NavigateToAsync<InjuryCodeCategoryTypeDetailViewModel>(injuryCodeCategory.InjuryCodeCategoryTypeId);
        }
    }
}