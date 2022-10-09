using SPMSCAV1.Models;
using SPMSCAV1.Services.Interface;
using System.Collections.ObjectModel;

namespace SPMSCAV1.ViewModels
{
    public class GoodAndServiceTypeViewModel : BaseViewModel
    {
        IGoodAndServiceTypeService _dataService;
        GoodAndServiceTypeModel _selectedGoodAndServiceType;

        public GoodAndServiceTypeViewModel(IGoodAndServiceTypeService dataService)

        {
            Title = "Browse";
            GoodAndServiceTypes = new ObservableCollection<GoodAndServiceTypeModel>();
            LoadGoodAndServiceTypesCommand = new Command(() => ExecuteLoadGoodAndServiceTypesCommand());
            GoodAndServiceTypeTapped = new Command<GoodAndServiceTypeModel>(OnGoodAndServiceTypeSelected);
            AddGoodAndServiceTypeCommand = new Command(OnAddGoodAndServiceType);
            _dataService = dataService;
        }


        public ObservableCollection<GoodAndServiceTypeModel> GoodAndServiceTypes { get; }

        public Command LoadGoodAndServiceTypesCommand { get; }

        public Command AddGoodAndServiceTypeCommand { get; }

        public Command<GoodAndServiceTypeModel> GoodAndServiceTypeTapped { get; }

        public GoodAndServiceTypeModel SelectedGoodAndServiceType
        {
            get => this._selectedGoodAndServiceType;
            set
            {
                SetProperty(ref this._selectedGoodAndServiceType, value);
                OnGoodAndServiceTypeSelected(value);
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
                var injuryCodeCategorys = await _dataService.GetListAsync();
                foreach (var injuryCodeCategory in injuryCodeCategorys)
                {
                    injuryCodeCategory.Description = injuryCodeCategory.Description;
                    GoodAndServiceTypes.Add(injuryCodeCategory);
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

        async void OnAddGoodAndServiceType(object obj)
        {
            await Navigation.NavigateToAsync<NewGoodAndServiceTypeViewModel>(null);
        }

        async void OnGoodAndServiceTypeSelected(GoodAndServiceTypeModel injuryCodeCategory)
        {
            if (injuryCodeCategory == null)
                return;
            await Navigation.NavigateToAsync<GoodAndServiceTypeDetailViewModel>(injuryCodeCategory.GoodAndServiceTypeId);
        }
    }
}