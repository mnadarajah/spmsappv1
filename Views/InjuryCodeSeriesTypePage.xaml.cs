using SPMSCAV1.Services.Interface;
using SPMSCAV1.ViewModels;

namespace SPMSCAV1.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InjuryCodeSeriesTypePage : ContentPage
    {
        private readonly IInjuryCodeSeriesTypeService _dataService;
        public InjuryCodeSeriesTypePage(IInjuryCodeSeriesTypeService dataService)
        {
            InitializeComponent();
            _dataService = dataService;
            BindingContext = ViewModel = new InjuryCodeSeriesTypeViewModel(_dataService);
            //BindingContext = ViewModel = new InjuryCodeSeriesTypesViewModel();
        }

        InjuryCodeSeriesTypeViewModel ViewModel { get; }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ViewModel.OnAppearing();
        }
    }
}