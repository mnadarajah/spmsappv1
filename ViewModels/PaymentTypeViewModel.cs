using DevExpress.Maui.DataForm;
using SPMSCAV1.Models;
using SPMSCAV1.Services.Interface;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace SPMSCAV1.ViewModels
{
    public class PaymentTypeViewModel : BaseViewModel, INotifyPropertyChanged
    {
        IPaymentTypeService _dataService;
        PaymentTypeModel _selectedPaymentType;
        string searchValue;
        bool init = false;
        bool isRefreshing = false;
        int skip = 0;
        int take = 10;

        public PaymentTypeViewModel(IPaymentTypeService dataService)

        {
            Title = "Browse";
            PaymentTypes = new ObservableCollection<PaymentTypeModel>();
            PaymentTypesOriginal = new ObservableCollection<PaymentTypeModel>();
            LoadPaymentTypesCommand = new Command(() => ExecuteLoadPaymentTypesCommand());
            PaymentTypeTapped = new Command<PaymentTypeModel>(OnPaymentTypeSelected);
            AddPaymentTypeCommand = new Command(OnAddPaymentType);
            SearchPaymentTypeCommand = new Command(SearchPaymentType);
            LoadMoreCommand = new Command(ExecuteLoadMoreCommand);
            _dataService = dataService;
        }

        [DataFormDisplayOptions(IsVisible = false)]
        public ObservableCollection<PaymentTypeModel> PaymentTypes { get; }

        [DataFormDisplayOptions(IsVisible = false)]
        public ObservableCollection<PaymentTypeModel> PaymentTypesOriginal { get; }

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
        public Command LoadPaymentTypesCommand { get; }

        [DataFormDisplayOptions(IsVisible = false)]
        public Command AddPaymentTypeCommand { get; }

        [DataFormDisplayOptions(IsVisible = false)]
        public Command SearchPaymentTypeCommand { get; }

        [DataFormDisplayOptions(IsVisible = false)]
        public Command<PaymentTypeModel> PaymentTypeTapped { get; }

        [DataFormDisplayOptions(IsVisible = false)]
        public PaymentTypeModel SelectedPaymentType
        {
            get => this._selectedPaymentType;
            set
            {
                SetProperty(ref this._selectedPaymentType, value);
                OnPaymentTypeSelected(value);
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
            SelectedPaymentType = null;
            skip = 0;
            ExecuteLoadPaymentTypesCommand();
        }

        async void ExecuteLoadPaymentTypesCommand()
        {
            IsBusy = true;
            try
            {
                PaymentTypes.Clear();
                var paymentTypes = await _dataService.Search("*", skip, take);
                foreach (var paymentType in paymentTypes)
                {
                    paymentType.Description = paymentType.Description;
                    PaymentTypes.Add(paymentType);
                    PaymentTypesOriginal.Add(paymentType);
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
                    var paymentTypes = await _dataService.Search("*", skip, take);
                    foreach (var paymentType in paymentTypes)
                    {
                        paymentType.Description = paymentType.Description;
                        PaymentTypes.Add(paymentType);
                        PaymentTypesOriginal.Add(paymentType);
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


        public void SearchPaymentType()
        {
            IsBusy = true;
            if (!init)
            {

            }
            else
            {
                PaymentTypes.Clear();
                if (SearchValue.Equals(null))
                {
                    foreach (var paymentType in PaymentTypesOriginal)
                    {
                        PaymentTypes.Add(paymentType);
                    }
                }
                else
                {
                    foreach (var paymentType in PaymentTypesOriginal)
                    {
                        paymentType.Description = paymentType.Description;
                        if (paymentType.Description.ToLower().Contains(SearchValue.ToLower()))
                        {
                            PaymentTypes.Add(paymentType);
                        }
                    }
                }
            }
            IsBusy = false;
        }

        public async void SearchPaymentType(string searchValue)
        {
            IsBusy = true;
            if (!init)
            {

            }
            else
            {
                IEnumerable<PaymentTypeModel> paymentTypes;
                PaymentTypes.Clear();
                if (!string.IsNullOrEmpty(searchValue))
                {
                    paymentTypes = await _dataService.Search(searchValue);
                }
                else
                {
                    paymentTypes = await _dataService.Search("*", skip, take);
                }

                foreach (var paymentType in paymentTypes)
                {
                    paymentType.Description = paymentType.Description;
                    PaymentTypes.Add(paymentType);
                    PaymentTypesOriginal.Add(paymentType);
                }
            }
            IsBusy = false;
        }


        async void OnAddPaymentType(object obj)
        {
            await Navigation.NavigateToAsync<NewPaymentTypeViewModel>(null);
        }

        async void OnPaymentTypeSelected(PaymentTypeModel paymentType)
        {
            if (paymentType == null)
                return;
            await Navigation.NavigateToAsync<PaymentTypeDetailViewModel>(paymentType.PaymentTypeId);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}