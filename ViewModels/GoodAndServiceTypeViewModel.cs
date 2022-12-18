using SPMSCAV1.Models;
using SPMSCAV1.Services.Interface;
using System.Collections.ObjectModel;
using DevExpress.Maui.DataForm;
using System.ComponentModel;
using System.Windows.Input;
using System.Runtime.CompilerServices;

namespace SPMSCAV1.ViewModels
{
    public class GoodAndServiceTypeViewModel : BaseViewModel, INotifyPropertyChanged
    {
        IGoodAndServiceTypeService _dataService;
        GoodAndServiceTypeModel _selectedGoodAndServiceType;
        string searchValue;
        bool init = false;
        bool isRefreshing = false;
        int skip = 0;
        int take = 10;

        public GoodAndServiceTypeViewModel(IGoodAndServiceTypeService dataService)

        {
            Title = "Browse";
            GoodAndServiceTypes = new ObservableCollection<GoodAndServiceTypeModel>();
            GoodAndServiceTypesOriginal = new ObservableCollection<GoodAndServiceTypeModel>();
            LoadGoodAndServiceTypesCommand = new Command(() => ExecuteLoadGoodAndServiceTypesCommand());
            GoodAndServiceTypeTapped = new Command<GoodAndServiceTypeModel>(OnGoodAndServiceTypeSelected);
            AddGoodAndServiceTypeCommand = new Command(OnAddGoodAndServiceType);
            SearchGoodAndServiceTypeCommand = new Command(SearchGoodAndServiceType);
            LoadMoreCommand = new Command(ExecuteLoadMoreCommand);
            _dataService = dataService;
        }

        [DataFormDisplayOptions(IsVisible = false)]
        public ObservableCollection<GoodAndServiceTypeModel> GoodAndServiceTypes { get; }

        [DataFormDisplayOptions(IsVisible = false)]
        public ObservableCollection<GoodAndServiceTypeModel> GoodAndServiceTypesOriginal { get; }


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
            skip = 0;
            ExecuteLoadGoodAndServiceTypesCommand();
        }

        async void ExecuteLoadGoodAndServiceTypesCommand()
        {
            IsBusy = true;
            try
            {
                GoodAndServiceTypes.Clear();
                var goodAndServiceTypes = await _dataService.Search("*", skip, take);
                foreach (var goodAndServiceType in goodAndServiceTypes)
                {
                    goodAndServiceType.Description = goodAndServiceType.Description;
                    GoodAndServiceTypes.Add(goodAndServiceType);
                    GoodAndServiceTypesOriginal.Add(goodAndServiceType);
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
                    var goodAndServiceTypes = await _dataService.Search("*", skip, take);
                    foreach (var goodAndServiceType in goodAndServiceTypes)
                    {
                        goodAndServiceType.Description = goodAndServiceType.Description;
                        GoodAndServiceTypes.Add(goodAndServiceType);
                        GoodAndServiceTypesOriginal.Add(goodAndServiceType);
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
                    foreach (var goodAndServiceType in GoodAndServiceTypesOriginal)
                    {
                        GoodAndServiceTypes.Add(goodAndServiceType);
                    }
                }
                else
                {
                    //if (SearchValue.Length > 1)
                    //{

                    foreach (var goodAndServiceType in GoodAndServiceTypesOriginal)
                    {
                        goodAndServiceType.Description = goodAndServiceType.Description;
                        if (goodAndServiceType.Description.ToLower().Contains(SearchValue.ToLower()))
                        {
                            GoodAndServiceTypes.Add(goodAndServiceType);
                        }
                    }
                    //}
                }
            }
            IsBusy = false;
        }

        public async void SearchGoodAndServiceType(string searchValue)
        {
            IsBusy = true;
            if (!init)
            {

            }
            else
            {
                IEnumerable<GoodAndServiceTypeModel> goodAndServiceTypes;
                GoodAndServiceTypes.Clear();
                if (!string.IsNullOrEmpty(searchValue))
                {
                    goodAndServiceTypes = await _dataService.Search(searchValue);
                }
                else
                {
                    goodAndServiceTypes = await _dataService.Search("*", skip, take);
                }

                foreach (var goodAndServiceType in goodAndServiceTypes)
                {
                    goodAndServiceType.Description = goodAndServiceType.Description;
                    GoodAndServiceTypes.Add(goodAndServiceType);
                    GoodAndServiceTypesOriginal.Add(goodAndServiceType);
                }
            }
            IsBusy = false;
        }


        async void OnAddGoodAndServiceType(object obj)
        {
            await Navigation.NavigateToAsync<NewGoodAndServiceTypeViewModel>(null);
        }

        async void OnGoodAndServiceTypeSelected(GoodAndServiceTypeModel goodAndServiceType)
        {
            if (goodAndServiceType == null)
                return;
            await Navigation.NavigateToAsync<GoodAndServiceTypeDetailViewModel>(goodAndServiceType.GoodAndServiceTypeId);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}