using SPMSCAV1.Models;
using SPMSCAV1.Services.Interface;
using System.Collections.ObjectModel;
using DevExpress.Maui.DataForm;
using System.ComponentModel;
using System.Windows.Input;
using System.Runtime.CompilerServices;

namespace SPMSCAV1.ViewModels
{
    public class InjuryCodeSeriesTypeViewModel : BaseViewModel, INotifyPropertyChanged
    {
        IInjuryCodeSeriesTypeService _dataService;
        InjuryCodeSeriesTypeModel _selectedInjuryCodeSeriesType;
        string searchValue;
        bool init = false;
        bool isRefreshing = false;
        int skip = 0;
        int take = 10;

        public InjuryCodeSeriesTypeViewModel(IInjuryCodeSeriesTypeService dataService)

        {
            Title = "Browse";
            InjuryCodeSeriesTypes = new ObservableCollection<InjuryCodeSeriesTypeModel>();
            InjuryCodeSeriesTypesOriginal = new ObservableCollection<InjuryCodeSeriesTypeModel>();
            LoadInjuryCodeSeriesTypesCommand = new Command(() => ExecuteLoadInjuryCodeSeriesTypesCommand());
            InjuryCodeSeriesTypeTapped = new Command<InjuryCodeSeriesTypeModel>(OnInjuryCodeSeriesTypeSelected);
            AddInjuryCodeSeriesTypeCommand = new Command(OnAddInjuryCodeSeriesType);
            SearchInjuryCodeSeriesTypeCommand = new Command(SearchInjuryCodeSeriesType);
            LoadMoreCommand = new Command(ExecuteLoadMoreCommand);
            _dataService = dataService;
        }

        [DataFormDisplayOptions(IsVisible = false)]
        public ObservableCollection<InjuryCodeSeriesTypeModel> InjuryCodeSeriesTypes { get; }

        [DataFormDisplayOptions(IsVisible = false)]
        public ObservableCollection<InjuryCodeSeriesTypeModel> InjuryCodeSeriesTypesOriginal { get; }

        [DataFormDisplayOptions(IsVisible = false)]

        ICommand loadMoreCommand = null;

        [DataFormDisplayOptions(IsVisible = false)]
        public ICommand LoadMoreCommand
        {
            get { return loadMoreCommand; }
            set
            {
                if (loadMoreCommand != value)
                {
                    loadMoreCommand = value;
                    OnPropertyChanged("LoadMoreCommand");
                }
            }
        }


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
            skip = 0;
            ExecuteLoadInjuryCodeSeriesTypesCommand();
        }

        async void ExecuteLoadInjuryCodeSeriesTypesCommand()
        {
            IsBusy = true;
            try
            {
                InjuryCodeSeriesTypes.Clear();
                var injuryCodeSeriesTypes = await _dataService.Search("*", skip, take);
                foreach (var injuryCodeSeriesType in injuryCodeSeriesTypes)
                {
                    injuryCodeSeriesType.Description = injuryCodeSeriesType.Description;
                    InjuryCodeSeriesTypes.Add(injuryCodeSeriesType);
                    InjuryCodeSeriesTypesOriginal.Add(injuryCodeSeriesType);
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

        public bool IsRefreshing
        {
            get { return isRefreshing; }
            set
            {
                if (isRefreshing != value)
                {
                    isRefreshing = value;
                    OnPropertyChanged("IsRefreshing");
                }
            }
        }

        async void ExecuteLoadMoreCommand()
        {
            await Task.Delay(2000);
            if (!IsBusy && init)
            {
                try
                {
                    IsBusy = true;
                    skip += 1;
                    var injuryCodeSeriesTypes = await _dataService.Search("*", skip, take);
                    foreach (var injuryCodeSeriesType in injuryCodeSeriesTypes)
                    {
                        injuryCodeSeriesType.Description = injuryCodeSeriesType.Description;
                        InjuryCodeSeriesTypes.Add(injuryCodeSeriesType);
                        InjuryCodeSeriesTypesOriginal.Add(injuryCodeSeriesType);
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex);
                }
                finally
                {
                    IsBusy = false;
                    IsRefreshing = false;
                }
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
                    foreach (var injuryCodeSeriesType in InjuryCodeSeriesTypesOriginal)
                    {
                        InjuryCodeSeriesTypes.Add(injuryCodeSeriesType);
                    }
                }
                else
                {
                    //if (SearchValue.Length > 1)
                    //{

                    foreach (var injuryCodeSeriesType in InjuryCodeSeriesTypesOriginal)
                    {
                        injuryCodeSeriesType.Description = injuryCodeSeriesType.Description;
                        if (injuryCodeSeriesType.Description.ToLower().Contains(SearchValue.ToLower()))
                        {
                            InjuryCodeSeriesTypes.Add(injuryCodeSeriesType);
                        }
                    }
                    //}
                }
            }
            IsBusy = false;
        }

        public async void SearchInjuryCodeSeriesType(string searchValue)
        {
            IsBusy = true;
            if (!init)
            {

            }
            else
            {
                IEnumerable<InjuryCodeSeriesTypeModel> injuryCodeSeriesTypes;
                InjuryCodeSeriesTypes.Clear();
                if (!string.IsNullOrEmpty(searchValue))
                {
                    injuryCodeSeriesTypes = await _dataService.Search(searchValue);
                }
                else
                {
                    injuryCodeSeriesTypes = await _dataService.Search("*", skip, take);
                }

                foreach (var injuryCodeSeriesType in injuryCodeSeriesTypes)
                {
                    injuryCodeSeriesType.Description = injuryCodeSeriesType.Description;
                    InjuryCodeSeriesTypes.Add(injuryCodeSeriesType);
                    InjuryCodeSeriesTypesOriginal.Add(injuryCodeSeriesType);
                }
            }
            IsBusy = false;
        }


        async void OnAddInjuryCodeSeriesType(object obj)
        {
            await Navigation.NavigateToAsync<NewInjuryCodeSeriesTypeViewModel>(null);
        }

        async void OnInjuryCodeSeriesTypeSelected(InjuryCodeSeriesTypeModel injuryCodeSeriesType)
        {
            if (injuryCodeSeriesType == null)
                return;
            await Navigation.NavigateToAsync<InjuryCodeSeriesTypeDetailViewModel>(injuryCodeSeriesType.InjuryCodeSeriesTypeId);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}