using SPMSCAV1.Services.Interface;
using SPMSCAV1.ViewModels;

namespace SPMSCAV1.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewInjuryCodeCategoryTypePage : ContentPage
    {
        private readonly IInjuryCodeCategoryTypeService _dataService;
        public NewInjuryCodeCategoryTypePage(IInjuryCodeCategoryTypeService dataService)
        {
            InitializeComponent();
            _dataService = dataService;
            BindingContext = new NewInjuryCodeCategoryTypeViewModel(dataService);
        }

    }
}