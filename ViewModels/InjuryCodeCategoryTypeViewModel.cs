using DevExpress.Maui.DataForm;
using SPMSCAV1.Models;
using SPMSCAV1.Services.Interface;
using System.Collections.ObjectModel;

namespace SPMSCAV1.ViewModels
{
    public class InjuryCodeCategoryTypeViewModel : BaseViewModel
    {
        IInjuryCodeCategoryTypeService _dataService;
        InjuryCodeCategoryTypeModel _selectedInjuryCodeCategoryType;
        string searchValue;
        bool init = false;

        public InjuryCodeCategoryTypeViewModel(IInjuryCodeCategoryTypeService dataService)

        {
            Title = "Browse";
            InjuryCodeCategoryTypes = new ObservableCollection<InjuryCodeCategoryTypeModel>();
            InjuryCodeCategoryTypesOriginal = new ObservableCollection<InjuryCodeCategoryTypeModel>();
            LoadInjuryCodeCategoryTypesCommand = new Command(() => ExecuteLoadInjuryCodeCategoryTypesCommand());
            InjuryCodeCategoryTypeTapped = new Command<InjuryCodeCategoryTypeModel>(OnInjuryCodeCategoryTypeSelected);
            AddInjuryCodeCategoryTypeCommand = new Command(OnAddInjuryCodeCategoryType);
            SearchInjuryCodeCategoryTypeCommand = new Command(SearchInjuryCodeCategoryType);
            _dataService = dataService;
        }

        [DataFormDisplayOptions(IsVisible = false)]
        public ObservableCollection<InjuryCodeCategoryTypeModel> InjuryCodeCategoryTypes { get; }

        [DataFormDisplayOptions(IsVisible = false)]
        public ObservableCollection<InjuryCodeCategoryTypeModel> InjuryCodeCategoryTypesOriginal { get; }


        [DataFormDisplayOptions(IsVisible = false)]
        public Command LoadInjuryCodeCategoryTypesCommand { get; }

        [DataFormDisplayOptions(IsVisible = false)]
        public Command AddInjuryCodeCategoryTypeCommand { get; }

        [DataFormDisplayOptions(IsVisible = false)]
        public Command SearchInjuryCodeCategoryTypeCommand { get; }

        [DataFormDisplayOptions(IsVisible = false)]
        public Command<InjuryCodeCategoryTypeModel> InjuryCodeCategoryTypeTapped { get; }

        [DataFormDisplayOptions(IsVisible = false)]
        public InjuryCodeCategoryTypeModel SelectedInjuryCodeCategoryType
        {
            get => this._selectedInjuryCodeCategoryType;
            set
            {
                SetProperty(ref this._selectedInjuryCodeCategoryType, value);
                OnInjuryCodeCategoryTypeSelected(value);
            }
        }

        public string SearchValue
        {
            get => this.searchValue;
            set
            {
                SetProperty(ref this.searchValue, value);
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
                var genders = await _dataService.GetListAsync();
                foreach (var gender in genders)
                {
                    gender.Description = gender.Description;
                    InjuryCodeCategoryTypes.Add(gender);
                    InjuryCodeCategoryTypesOriginal.Add(gender);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }
            finally
            {
                init = true;
                IsBusy = false;
            }
        }


        public void SearchInjuryCodeCategoryType()
        {
            IsBusy = true;
            if (!init)
            {

            }
            else
            {
                InjuryCodeCategoryTypes.Clear();
                if (SearchValue.Equals(null))
                {
                    foreach (var gender in InjuryCodeCategoryTypesOriginal)
                    {
                        InjuryCodeCategoryTypes.Add(gender);
                    }
                }
                else
                {
                    //if (SearchValue.Length > 1)
                    //{

                    foreach (var gender in InjuryCodeCategoryTypesOriginal)
                    {
                        gender.Description = gender.Description;
                        if (gender.Description.ToLower().Contains(SearchValue.ToLower()))
                        {
                            InjuryCodeCategoryTypes.Add(gender);
                        }
                    }
                    //}
                }
            }
            IsBusy = false;
        }


        async void OnAddInjuryCodeCategoryType(object obj)
        {
            await Navigation.NavigateToAsync<NewInjuryCodeCategoryTypeViewModel>(null);
        }

        async void OnInjuryCodeCategoryTypeSelected(InjuryCodeCategoryTypeModel gender)
        {
            if (gender == null)
                return;
            await Navigation.NavigateToAsync<InjuryCodeCategoryTypeDetailViewModel>(gender.InjuryCodeCategoryTypeId);
        }

    }
}