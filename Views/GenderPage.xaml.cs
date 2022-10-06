using SPMSCAV1.Services.Interface;
using SPMSCAV1.ViewModels;

namespace SPMSCAV1.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GenderPage : ContentPage
    {
        private readonly IGenderService _dataService;
        public GenderPage(IGenderService dataService)
        {
            InitializeComponent();
            _dataService = dataService;
            BindingContext = ViewModel = new GendersViewModel(_dataService);
            //BindingContext = ViewModel = new GendersViewModel();
        }

        GendersViewModel ViewModel { get; }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ViewModel.OnAppearing();
        }
    }
}