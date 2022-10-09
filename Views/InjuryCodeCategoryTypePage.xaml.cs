using SPMSCAV1.Services.Interface;
using SPMSCAV1.ViewModels;

namespace SPMSCAV1.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InjuryCodeCategoryTypePage : ContentPage
    {
        private readonly IInjuryCodeCategoryTypeService _dataService;
        public InjuryCodeCategoryTypePage(IInjuryCodeCategoryTypeService dataService)
        {
            InitializeComponent();
            _dataService = dataService;
            BindingContext = ViewModel = new InjuryCodeCategoryTypeViewModel(_dataService);
            //BindingContext = ViewModel = new InjuryCodeCategoryTypesViewModel();
        }

        InjuryCodeCategoryTypeViewModel ViewModel { get; }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ViewModel.OnAppearing();
        }
    }
}