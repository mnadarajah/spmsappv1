using SPMSCAV1.Services.Interface;
using System.Web;

namespace SPMSCAV1.ViewModels
{
    public class PaymentTypeDetailViewModel : BaseViewModel, IQueryAttributable
    {
        //public const string ViewName = "PaymentTypeDetailPage";


        //string description;
        //string code;

        //IPaymentTypeService _dataService;

        //public PaymentTypeDetailViewModel(IPaymentTypeService dataService)
        //{
        //    _dataService = dataService;
        //}
        //public double Id { get; set; }

        //public string Description
        //{
        //    get => this.description;
        //    set => SetProperty(ref this.description, value);
        //}

        //public string Code
        //{
        //    get => this.code;
        //    set => SetProperty(ref this.code, value);
        //}

        //public async Task LoadPaymentTypeId(long paymentTypeId)
        //{
        //    try
        //    {
        //        var paymentType = await _dataService.GetByKeyAsync(paymentTypeId);
        //        Id = paymentType.PaymentTypeId;
        //        Description = paymentType.Description;
        //        Code = paymentType.Code;
        //    }
        //    catch (Exception)
        //    {
        //        System.Diagnostics.Debug.WriteLine("Failed to Load Item");
        //    }
        //}

        //public override async Task InitializeAsync(object parameter)
        //{
        //    long id;
        //    long.TryParse(parameter as string, out id);
        //    await LoadPaymentTypeId(id);
        //}

        //public async void ApplyQueryAttributes(IDictionary<string, object> query)
        //{
        //    string id = HttpUtility.UrlDecode(query["id"] as string);
        //    long paymentTypeId;
        //    long.TryParse(id, out paymentTypeId);
        //    await LoadPaymentTypeId(paymentTypeId);
        //}
        public const string ViewName = "PaymentTypeDetailPage";


        string description;
        string code;

        IPaymentTypeService _dataService;

        public PaymentTypeDetailViewModel(IPaymentTypeService dataService)
        {
            Title = "Detail";
            EditPaymentTypeCommand = new Command(EditPaymentType);
            _dataService = dataService;
        }
        public double Id { get; set; }

        public Command EditPaymentTypeCommand { get; }

        public string Description
        {
            get => this.description;
            set => SetProperty(ref this.description, value);
        }

        public string Code
        {
            get => this.code;
            set => SetProperty(ref this.code, value);
        }

        public async Task LoadPaymentTypeId(long paymentTypeId)
        {
            try
            {
                var paymentType = await _dataService.GetByKeyAsync(paymentTypeId);
                Id = paymentType.PaymentTypeId;
                Description = paymentType.Description;
                Code = paymentType.Code;
            }
            catch (Exception)
            {
                System.Diagnostics.Debug.WriteLine("Failed to Load Item");
            }
        }

        public override async Task InitializeAsync(object parameter)
        {
            long id;
            long.TryParse(parameter as string, out id);
            await LoadPaymentTypeId(id);
        }

        public async void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            string id = HttpUtility.UrlDecode(query["id"] as string);
            long paymentTypeId;
            long.TryParse(id, out paymentTypeId);
            await LoadPaymentTypeId(paymentTypeId);
        }

        public async void EditPaymentType()
        {
            if (Id < 0)
                return;
            //await Navigation.NavigateToAsync<EditPaymentTypeViewModel>(Id);
            await Navigation.NavigateToAsync<EditPaymentTypeViewModel>(Id);
        }
    }
}