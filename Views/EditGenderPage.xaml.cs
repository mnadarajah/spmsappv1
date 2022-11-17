using SPMSCAV1.Services.Interface;
using SPMSCAV1.ViewModels;

namespace SPMSCAV1.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditGenderPage : ContentPage
    {
        private readonly IGenderService _dataService;
        public EditGenderPage(IGenderService dataService)
        {
            InitializeComponent();
            _dataService = dataService;
            BindingContext = new EditGenderViewModel(dataService);
        }

    }
}