using SPMSCAV1.Models;
using SPMSCAV1.Services.Interface;
using System.Collections.ObjectModel;

namespace SPMSCAV1.ViewModels
{
    public class InjuryCodeSeriesTypeViewModel : BaseViewModel
    {
        IInjuryCodeSeriesTypeService _dataService;
        InjuryCodeSeriesTypeModel _selectedInjuryCodeSeriesType;

        public InjuryCodeSeriesTypeViewModel(IInjuryCodeSeriesTypeService dataService)

        {
            Title = "Browse";
            InjuryCodeSeriesTypes = new ObservableCollection<InjuryCodeSeriesTypeModel>();
            LoadInjuryCodeSeriesTypesCommand = new Command(() => ExecuteLoadInjuryCodeSeriesTypesCommand());
            InjuryCodeSeriesTypeTapped = new Command<InjuryCodeSeriesTypeModel>(OnInjuryCodeSeriesTypeSelected);
            AddInjuryCodeSeriesTypeCommand = new Command(OnAddInjuryCodeSeriesType);
            _dataService = dataService;
        }


        public ObservableCollection<InjuryCodeSeriesTypeModel> InjuryCodeSeriesTypes { get; }

        public Command LoadInjuryCodeSeriesTypesCommand { get; }

        public Command AddInjuryCodeSeriesTypeCommand { get; }

        public Command<InjuryCodeSeriesTypeModel> InjuryCodeSeriesTypeTapped { get; }

        public InjuryCodeSeriesTypeModel SelectedInjuryCodeSeriesType
        {
            get => this._selectedInjuryCodeSeriesType;
            set
            {
                SetProperty(ref this._selectedInjuryCodeSeriesType, value);
                OnInjuryCodeSeriesTypeSelected(value);
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
                var injuryCodeSeriess = await _dataService.GetListAsync();
                foreach (var injuryCodeSeries in injuryCodeSeriess)
                {
                    injuryCodeSeries.Description = injuryCodeSeries.Description;
                    InjuryCodeSeriesTypes.Add(injuryCodeSeries);
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

        async void OnAddInjuryCodeSeriesType(object obj)
        {
            await Navigation.NavigateToAsync<NewInjuryCodeSeriesTypeViewModel>(null);
        }

        async void OnInjuryCodeSeriesTypeSelected(InjuryCodeSeriesTypeModel injuryCodeSeries)
        {
            if (injuryCodeSeries == null)
                return;
            await Navigation.NavigateToAsync<InjuryCodeSeriesTypeDetailViewModel>(injuryCodeSeries.InjuryCodeSeriesTypeId);
        }
    }
}