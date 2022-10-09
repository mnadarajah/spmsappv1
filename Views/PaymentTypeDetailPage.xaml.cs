using SPMSCAV1.Services.Interface;
using SPMSCAV1.ViewModels;

namespace SPMSCAV1.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PaymentTypeDetailPage : ContentPage
    {
        private readonly IPaymentTypeService _dataService;
        public PaymentTypeDetailPage(IPaymentTypeService dataService)
        {
            InitializeComponent();
            _dataService = dataService;
            BindingContext = new PaymentTypeDetailViewModel(dataService);

        }
    }
}