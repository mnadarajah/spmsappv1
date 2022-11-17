using DevExpress.Maui.DataForm;
using SPMSCAV1.Models;
using SPMSCAV1.Services.Interface;
using System.Collections.ObjectModel;

namespace SPMSCAV1.ViewModels
{
    //public class PaymentTypeViewModel : BaseViewModel
    //{
    //    IPaymentTypeService _dataService;
    //    PaymentTypeModel _selectedPaymentType;

    //    public PaymentTypeViewModel(IPaymentTypeService dataService)

    //    {
    //        Title = "Browse";
    //        PaymentTypes = new ObservableCollection<PaymentTypeModel>();
    //        LoadPaymentTypesCommand = new Command(() => ExecuteLoadPaymentTypesCommand());
    //        PaymentTypeTapped = new Command<PaymentTypeModel>(OnPaymentTypeSelected);
    //        AddPaymentTypeCommand = new Command(OnAddPaymentType);
    //        _dataService = dataService;
    //    }


    //    public ObservableCollection<PaymentTypeModel> PaymentTypes { get; }

    //    public Command LoadPaymentTypesCommand { get; }

    //    public Command AddPaymentTypeCommand { get; }

    //    public Command<PaymentTypeModel> PaymentTypeTapped { get; }

    //    public PaymentTypeModel SelectedPaymentType
    //    {
    //        get => this._selectedPaymentType;
    //        set
    //        {
    //            SetProperty(ref this._selectedPaymentType, value);
    //            OnPaymentTypeSelected(value);
    //        }
    //    }

    //    public void OnAppearing()
    //    {
    //        IsBusy = true;
    //        SelectedPaymentType = null;
    //        ExecuteLoadPaymentTypesCommand();
    //    }

    //    async void ExecuteLoadPaymentTypesCommand()
    //    {
    //        IsBusy = true;
    //        try
    //        {
    //            PaymentTypes.Clear();
    //            var paymentTypes = await _dataService.GetListAsync();
    //            foreach (var paymentType in paymentTypes)
    //            {
    //                paymentType.Description = paymentType.Description;
    //                PaymentTypes.Add(paymentType);
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            System.Diagnostics.Debug.WriteLine(ex);
    //        }
    //        finally
    //        {
    //            IsBusy = false;
    //        }
    //    }

    //    async void OnAddPaymentType(object obj)
    //    {
    //        await Navigation.NavigateToAsync<NewPaymentTypeViewModel>(null);
    //    }

    //    async void OnPaymentTypeSelected(PaymentTypeModel paymentType)
    //    {
    //        if (paymentType == null)
    //            return;
    //        await Navigation.NavigateToAsync<PaymentTypeDetailViewModel>(paymentType.PaymentTypeId);
    //    }
    //}

    public class PaymentTypeViewModel : BaseViewModel
    {
        IPaymentTypeService _dataService;
        PaymentTypeModel _selectedPaymentType;
        string searchValue;
        bool init = false;

        public PaymentTypeViewModel(IPaymentTypeService dataService)

        {
            Title = "Browse";
            PaymentTypes = new ObservableCollection<PaymentTypeModel>();
            PaymentTypesOriginal = new ObservableCollection<PaymentTypeModel>();
            LoadPaymentTypesCommand = new Command(() => ExecuteLoadPaymentTypesCommand());
            PaymentTypeTapped = new Command<PaymentTypeModel>(OnPaymentTypeSelected);
            AddPaymentTypeCommand = new Command(OnAddPaymentType);
            SearchPaymentTypeCommand = new Command(SearchPaymentType);
            _dataService = dataService;
        }

        [DataFormDisplayOptions(IsVisible = false)]
        public ObservableCollection<PaymentTypeModel> PaymentTypes { get; }

        [DataFormDisplayOptions(IsVisible = false)]
        public ObservableCollection<PaymentTypeModel> PaymentTypesOriginal { get; }


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
            ExecuteLoadPaymentTypesCommand();
        }

        async void ExecuteLoadPaymentTypesCommand()
        {
            IsBusy = true;
            try
            {
                PaymentTypes.Clear();
                var paymentTypes = await _dataService.GetListAsync();
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

    }
}