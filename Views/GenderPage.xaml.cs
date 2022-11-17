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
            BindingContext = ViewModel = new GenderViewModel(_dataService);
            //BindingContext = ViewModel = new GendersViewModel();
        }

        GenderViewModel ViewModel { get; }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ViewModel.OnAppearing();
        }

        private void dataForm_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (!ViewModel.Genders.Equals(null))
            {
                ViewModel.SearchGender();
            }

        }

    }
}