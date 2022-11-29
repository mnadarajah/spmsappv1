using SPMSCAV1.Services.Interface;
using SPMSCAV1.ViewModels;

namespace SPMSCAV1.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditPaymentTypePage : ContentPage
    {
        private readonly IPaymentTypeService _dataService;
        public EditPaymentTypePage(IPaymentTypeService dataService)
        {
            InitializeComponent();
            _dataService = dataService;
            BindingContext = new EditPaymentTypeViewModel(dataService);
        }

    }
}