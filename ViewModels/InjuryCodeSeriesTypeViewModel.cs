using SPMSCAV1.Models;
using SPMSCAV1.Services.Interface;
using System.Collections.ObjectModel;
using DevExpress.Maui.DataForm;

namespace SPMSCAV1.ViewModels
{
    public class InjuryCodeSeriesTypeViewModel : BaseViewModel
    {
        IInjuryCodeSeriesTypeService _dataService;
        InjuryCodeSeriesTypeModel _selectedInjuryCodeSeriesType;
        string searchValue;
        bool init = false;

        public InjuryCodeSeriesTypeViewModel(IInjuryCodeSeriesTypeService dataService)

        {
            Title = "Browse";
            InjuryCodeSeriesTypes = new ObservableCollection<InjuryCodeSeriesTypeModel>();
            InjuryCodeSeriesTypesOriginal = new ObservableCollection<InjuryCodeSeriesTypeModel>();
            LoadInjuryCodeSeriesTypesCommand = new Command(() => ExecuteLoadInjuryCodeSeriesTypesCommand());
            InjuryCodeSeriesTypeTapped = new Command<InjuryCodeSeriesTypeModel>(OnInjuryCodeSeriesTypeSelected);
            AddInjuryCodeSeriesTypeCommand = new Command(OnAddInjuryCodeSeriesType);
            SearchInjuryCodeSeriesTypeCommand = new Command(SearchInjuryCodeSeriesType);
            _dataService = dataService;
        }

        [DataFormDisplayOptions(IsVisible = false)]
        public ObservableCollection<InjuryCodeSeriesTypeModel> InjuryCodeSeriesTypes { get; }

        [DataFormDisplayOptions(IsVisible = false)]
        public ObservableCollection<InjuryCodeSeriesTypeModel> InjuryCodeSeriesTypesOriginal { get; }


        [DataFormDisplayOptions(IsVisible = false)]
        public Command LoadInjuryCodeSeriesTypesCommand { get; }

        [DataFormDisplayOptions(IsVisible = false)]
        public Command AddInjuryCodeSeriesTypeCommand { get; }

        [DataFormDisplayOptions(IsVisible = false)]
        public Command SearchInjuryCodeSeriesTypeCommand { get; }

        [DataFormDisplayOptions(IsVisible = false)]
        public Command<InjuryCodeSeriesTypeModel> InjuryCodeSeriesTypeTapped { get; }

        [DataFormDisplayOptions(IsVisible = false)]
        public InjuryCodeSeriesTypeModel SelectedInjuryCodeSeriesType
        {
            get => this._selectedInjuryCodeSeriesType;
            set
            {
                SetProperty(ref this._selectedInjuryCodeSeriesType, value);
                OnInjuryCodeSeriesTypeSelected(value);
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
            SelectedInjuryCodeSeriesType = null;
            ExecuteLoadInjuryCodeSeriesTypesCommand();
        }

        async void ExecuteLoadInjuryCodeSeriesTypesCommand()
        {
            IsBusy = true;
            try
            {
                InjuryCodeSeriesTypes.Clear();
                var genders = await _dataService.GetListAsync();
                foreach (var gender in genders)
                {
                    gender.Description = gender.Description;
                    InjuryCodeSeriesTypes.Add(gender);
                    InjuryCodeSeriesTypesOriginal.Add(gender);
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


        public void SearchInjuryCodeSeriesType()
        {
            IsBusy = true;
            if (!init)
            {

            }
            else
            {
                InjuryCodeSeriesTypes.Clear();
                if (SearchValue.Equals(null))
                {
                    foreach (var gender in InjuryCodeSeriesTypesOriginal)
                    {
                        InjuryCodeSeriesTypes.Add(gender);
                    }
                }
                else
                {
                    //if (SearchValue.Length > 1)
                    //{

                    foreach (var gender in InjuryCodeSeriesTypesOriginal)
                    {
                        gender.Description = gender.Description;
                        if (gender.Description.ToLower().Contains(SearchValue.ToLower()))
                        {
                            InjuryCodeSeriesTypes.Add(gender);
                        }
                    }
                    //}
                }
            }
            IsBusy = false;
        }


        async void OnAddInjuryCodeSeriesType(object obj)
        {
            await Navigation.NavigateToAsync<NewInjuryCodeSeriesTypeViewModel>(null);
        }

        async void OnInjuryCodeSeriesTypeSelected(InjuryCodeSeriesTypeModel gender)
        {
            if (gender == null)
                return;
            await Navigation.NavigateToAsync<InjuryCodeSeriesTypeDetailViewModel>(gender.InjuryCodeSeriesTypeId);
        }

    }
}