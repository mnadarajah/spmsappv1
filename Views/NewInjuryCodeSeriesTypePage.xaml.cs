using SPMSCAV1.Services.Interface;
using SPMSCAV1.ViewModels;

namespace SPMSCAV1.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewInjuryCodeSeriesTypePage : ContentPage
    {
        private readonly IInjuryCodeSeriesTypeService _dataService;
        public NewInjuryCodeSeriesTypePage(IInjuryCodeSeriesTypeService dataService)
        {
            InitializeComponent();
            _dataService = dataService;
            BindingContext = new NewInjuryCodeSeriesTypeViewModel(dataService);
        }

    }
}