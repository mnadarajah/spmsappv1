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

        void OnEntryTextChanged(object sender, TextChangedEventArgs e)
        {
            string oldText = e.OldTextValue;
            string newText = e.NewTextValue;
            string myText = searchValue.Text;
            if (newText.Length != 1)
            {
                ViewModel.SearchGoodAndServiceType(newText);
            }
        }

        void OnEntryCompleted(object sender, EventArgs e)
        {
            string text = ((Entry)sender).Text;
        }
    }
}