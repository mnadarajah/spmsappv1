using SPMSCAV1.Services.Interface;
using SPMSCAV1.ViewModels;

namespace SPMSCAV1.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InjuryCodeSeriesTypeDetailPage : ContentPage
    {
        private readonly IInjuryCodeSeriesTypeService _dataService;
        public InjuryCodeSeriesTypeDetailPage(IInjuryCodeSeriesTypeService dataService)
        {
            InitializeComponent();
            _dataService = dataService;
            BindingContext = new InjuryCodeSeriesTypeDetailViewModel(dataService);

        }
    }
}