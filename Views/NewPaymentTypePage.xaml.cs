using SPMSCAV1.Services.Interface;
using SPMSCAV1.ViewModels;

namespace SPMSCAV1.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewPaymentTypePage : ContentPage
    {
        private readonly IPaymentTypeService _dataService;
        public NewPaymentTypePage(IPaymentTypeService dataService)
        {
            InitializeComponent();
            _dataService = dataService;
            BindingContext = new NewPaymentTypeViewModel(dataService);
        }

    }
}