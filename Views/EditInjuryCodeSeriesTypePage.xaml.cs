using SPMSCAV1.Services.Interface;
using SPMSCAV1.ViewModels;

namespace SPMSCAV1.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditInjuryCodeSeriesTypePage : ContentPage
    {
        private readonly IInjuryCodeSeriesTypeService _dataService;
        public EditInjuryCodeSeriesTypePage(IInjuryCodeSeriesTypeService dataService)
        {
            InitializeComponent();
            _dataService = dataService;
            BindingContext = new EditInjuryCodeSeriesTypeViewModel(dataService);
        }

    }
}