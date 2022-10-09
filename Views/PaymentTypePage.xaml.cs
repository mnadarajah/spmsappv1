using SPMSCAV1.Services.Interface;
using SPMSCAV1.ViewModels;

namespace SPMSCAV1.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PaymentTypePage : ContentPage
    {
        private readonly IPaymentTypeService _dataService;
        public PaymentTypePage(IPaymentTypeService dataService)
        {
            InitializeComponent();
            _dataService = dataService;
            BindingContext = ViewModel = new PaymentTypeViewModel(_dataService);
            //BindingContext = ViewModel = new PaymentTypesViewModel();
        }

        PaymentTypeViewModel ViewModel { get; }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ViewModel.OnAppearing();
        }
    }
}