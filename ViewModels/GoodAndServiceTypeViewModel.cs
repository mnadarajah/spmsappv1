using SPMSCAV1.Models;
using SPMSCAV1.Services.Interface;
using System.Collections.ObjectModel;
using DevExpress.Maui.DataForm;

namespace SPMSCAV1.ViewModels
{
    public class GoodAndServiceTypeViewModel : BaseViewModel
    {
        IGoodAndServiceTypeService _dataService;
        GoodAndServiceTypeModel _selectedGoodAndServiceType;
        string searchValue;
        bool init = false;

        public GoodAndServiceTypeViewModel(IGoodAndServiceTypeService dataService)

        {
            Title = "Browse";
            GoodAndServiceTypes = new ObservableCollection<GoodAndServiceTypeModel>();
            GoodAndServiceTypesOriginal = new ObservableCollection<GoodAndServiceTypeModel>();
            LoadGoodAndServiceTypesCommand = new Command(() => ExecuteLoadGoodAndServiceTypesCommand());
            GoodAndServiceTypeTapped = new Command<GoodAndServiceTypeModel>(OnGoodAndServiceTypeSelected);
            AddGoodAndServiceTypeCommand = new Command(OnAddGoodAndServiceType);
            SearchGoodAndServiceTypeCommand = new Command(SearchGoodAndServiceType);
            _dataService = dataService;
        }

        [DataFormDisplayOptions(IsVisible = false)]
        public ObservableCollection<GoodAndServiceTypeModel> GoodAndServiceTypes { get; }

        [DataFormDisplayOptions(IsVisible = false)]
        public ObservableCollection<GoodAndServiceTypeModel> GoodAndServiceTypesOriginal { get; }


        [DataFormDisplayOptions(IsVisible = false)]
        public Command LoadGoodAndServiceTypesCommand { get; }

        [DataFormDisplayOptions(IsVisible = false)]
        public Command AddGoodAndServiceTypeCommand { get; }

        [DataFormDisplayOptions(IsVisible = false)]
        public Command SearchGoodAndServiceTypeCommand { get; }

        [DataFormDisplayOptions(IsVisible = false)]
        public Command<GoodAndServiceTypeModel> GoodAndServiceTypeTapped { get; }

        [DataFormDisplayOptions(IsVisible = false)]
        public GoodAndServiceTypeModel SelectedGoodAndServiceType
        {
            get => this._selectedGoodAndServiceType;
            set
            {
                SetProperty(ref this._selectedGoodAndServiceType, value);
                OnGoodAndServiceTypeSelected(value);
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
            SelectedGoodAndServiceType = null;
            ExecuteLoadGoodAndServiceTypesCommand();
        }

        async void ExecuteLoadGoodAndServiceTypesCommand()
        {
            IsBusy = true;
            try
            {
                GoodAndServiceTypes.Clear();
                var genders = await _dataService.GetListAsync();
                foreach (var gender in genders)
                {
                    gender.Description = gender.Description;
                    GoodAndServiceTypes.Add(gender);
                    GoodAndServiceTypesOriginal.Add(gender);
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


        public void SearchGoodAndServiceType()
        {
            IsBusy = true;
            if (!init)
            {

            }
            else
            {
                GoodAndServiceTypes.Clear();
                if (SearchValue.Equals(null))
                {
                    foreach (var gender in GoodAndServiceTypesOriginal)
                    {
                        GoodAndServiceTypes.Add(gender);
                    }
                }
                else
                {
                    //if (SearchValue.Length > 1)
                    //{

                    foreach (var gender in GoodAndServiceTypesOriginal)
                    {
                        gender.Description = gender.Description;
                        if (gender.Description.ToLower().Contains(SearchValue.ToLower()))
                        {
                            GoodAndServiceTypes.Add(gender);
                        }
                    }
                    //}
                }
            }
            IsBusy = false;
        }


        async void OnAddGoodAndServiceType(object obj)
        {
            await Navigation.NavigateToAsync<NewGoodAndServiceTypeViewModel>(null);
        }

        async void OnGoodAndServiceTypeSelected(GoodAndServiceTypeModel gender)
        {
            if (gender == null)
                return;
            await Navigation.NavigateToAsync<GoodAndServiceTypeDetailViewModel>(gender.GoodAndServiceTypeId);
        }

    }
}