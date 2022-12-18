using DevExpress.Maui.DataForm;
using SPMSCAV1.Models;
using SPMSCAV1.Services.Interface;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace SPMSCAV1.ViewModels
{
    public class InjuryCodeCategoryTypeViewModel : BaseViewModel, INotifyPropertyChanged
    {
        IInjuryCodeCategoryTypeService _dataService;
        InjuryCodeCategoryTypeModel _selectedInjuryCodeCategoryType;
        string searchValue;
        bool init = false;
        bool isRefreshing = false;
        int skip = 0;
        int take = 10;

        public InjuryCodeCategoryTypeViewModel(IInjuryCodeCategoryTypeService dataService)

        {
            Title = "Browse";
            InjuryCodeCategoryTypes = new ObservableCollection<InjuryCodeCategoryTypeModel>();
            InjuryCodeCategoryTypesOriginal = new ObservableCollection<InjuryCodeCategoryTypeModel>();
            LoadInjuryCodeCategoryTypesCommand = new Command(() => ExecuteLoadInjuryCodeCategoryTypesCommand());
            InjuryCodeCategoryTypeTapped = new Command<InjuryCodeCategoryTypeModel>(OnInjuryCodeCategoryTypeSelected);
            AddInjuryCodeCategoryTypeCommand = new Command(OnAddInjuryCodeCategoryType);
            SearchInjuryCodeCategoryTypeCommand = new Command(SearchInjuryCodeCategoryType);
            LoadMoreCommand = new Command(ExecuteLoadMoreCommand);
            _dataService = dataService;
        }

        [DataFormDisplayOptions(IsVisible = false)]
        public ObservableCollection<InjuryCodeCategoryTypeModel> InjuryCodeCategoryTypes { get; }

        [DataFormDisplayOptions(IsVisible = false)]
        public ObservableCollection<InjuryCodeCategoryTypeModel> InjuryCodeCategoryTypesOriginal { get; }

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
            skip = 0;
            ExecuteLoadInjuryCodeCategoryTypesCommand();
        }

        async void ExecuteLoadInjuryCodeCategoryTypesCommand()
        {
            IsBusy = true;
            try
            {
                InjuryCodeCategoryTypes.Clear();
                var injuryCodeCategoryTypes = await _dataService.Search("*", skip, take);
                foreach (var injuryCodeCategoryType in injuryCodeCategoryTypes)
                {
                    injuryCodeCategoryType.Description = injuryCodeCategoryType.Description;
                    InjuryCodeCategoryTypes.Add(injuryCodeCategoryType);
                    InjuryCodeCategoryTypesOriginal.Add(injuryCodeCategoryType);
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
                    var injuryCodeCategoryTypes = await _dataService.Search("*", skip, take);
                    foreach (var injuryCodeCategoryType in injuryCodeCategoryTypes)
                    {
                        injuryCodeCategoryType.Description = injuryCodeCategoryType.Description;
                        InjuryCodeCategoryTypes.Add(injuryCodeCategoryType);
                        InjuryCodeCategoryTypesOriginal.Add(injuryCodeCategoryType);
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
                    foreach (var injuryCodeCategoryType in InjuryCodeCategoryTypesOriginal)
                    {
                        InjuryCodeCategoryTypes.Add(injuryCodeCategoryType);
                    }
                }
                else
                {
                    //if (SearchValue.Length > 1)
                    //{

                    foreach (var injuryCodeCategoryType in InjuryCodeCategoryTypesOriginal)
                    {
                        injuryCodeCategoryType.Description = injuryCodeCategoryType.Description;
                        if (injuryCodeCategoryType.Description.ToLower().Contains(SearchValue.ToLower()))
                        {
                            InjuryCodeCategoryTypes.Add(injuryCodeCategoryType);
                        }
                    }
                    //}
                }
            }
            IsBusy = false;
        }

        public async void SearchInjuryCodeCategoryType(string searchValue)
        {
            IsBusy = true;
            if (!init)
            {

            }
            else
            {
                IEnumerable<InjuryCodeCategoryTypeModel> injuryCodeCategoryTypes;
                InjuryCodeCategoryTypes.Clear();
                if (!string.IsNullOrEmpty(searchValue))
                {
                    injuryCodeCategoryTypes = await _dataService.Search(searchValue);
                }
                else
                {
                    injuryCodeCategoryTypes = await _dataService.Search("*", skip, take);
                }

                foreach (var injuryCodeCategoryType in injuryCodeCategoryTypes)
                {
                    injuryCodeCategoryType.Description = injuryCodeCategoryType.Description;
                    InjuryCodeCategoryTypes.Add(injuryCodeCategoryType);
                    InjuryCodeCategoryTypesOriginal.Add(injuryCodeCategoryType);
                }
            }
            IsBusy = false;
        }


        async void OnAddInjuryCodeCategoryType(object obj)
        {
            await Navigation.NavigateToAsync<NewInjuryCodeCategoryTypeViewModel>(null);
        }

        async void OnInjuryCodeCategoryTypeSelected(InjuryCodeCategoryTypeModel injuryCodeCategoryType)
        {
            if (injuryCodeCategoryType == null)
                return;
            await Navigation.NavigateToAsync<InjuryCodeCategoryTypeDetailViewModel>(injuryCodeCategoryType.InjuryCodeCategoryTypeId);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}