using SPMSCAV1.Services.Interface;
using SPMSCAV1.ViewModels;

namespace SPMSCAV1.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GoodAndServiceTypePage : ContentPage
    {
        private readonly IGoodAndServiceTypeService _dataService;
        public GoodAndServiceTypePage(IGoodAndServiceTypeService dataService)
        {
            InitializeComponent();
            _dataService = dataService;
            BindingContext = ViewModel = new GoodAndServiceTypeViewModel(_dataService);
            //BindingContext = ViewModel = new GoodAndServiceTypesViewModel();
        }

        GoodAndServiceTypeViewModel ViewModel { get; }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ViewModel.OnAppearing();
        }

        private void dataForm_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (!ViewModel.GoodAndServiceTypes.Equals(null))
            {
                ViewModel.SearchGoodAndServiceType();
            }

        }
    }
}