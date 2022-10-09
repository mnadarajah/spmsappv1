using SPMSCAV1.Models;
using SPMSCAV1.Services.Interface;
using System.Collections.ObjectModel;

namespace SPMSCAV1.ViewModels
{
    public class PaymentTypeViewModel : BaseViewModel
    {
        IPaymentTypeService _dataService;
        PaymentTypeModel _selectedPaymentType;

        public PaymentTypeViewModel(IPaymentTypeService dataService)

        {
            Title = "Browse";
            PaymentTypes = new ObservableCollection<PaymentTypeModel>();
            LoadPaymentTypesCommand = new Command(() => ExecuteLoadPaymentTypesCommand());
            PaymentTypeTapped = new Command<PaymentTypeModel>(OnPaymentTypeSelected);
            AddPaymentTypeCommand = new Command(OnAddPaymentType);
            _dataService = dataService;
        }


        public ObservableCollection<PaymentTypeModel> PaymentTypes { get; }

        public Command LoadPaymentTypesCommand { get; }

        public Command AddPaymentTypeCommand { get; }

        public Command<PaymentTypeModel> PaymentTypeTapped { get; }

        public PaymentTypeModel SelectedPaymentType
        {
            get => this._selectedPaymentType;
            set
            {
                SetProperty(ref this._selectedPaymentType, value);
                OnPaymentTypeSelected(value);
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