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

        private void dataForm_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (!ViewModel.InjuryCodeSeriesTypes.Equals(null))
            {
                ViewModel.SearchInjuryCodeSeriesType();
            }

        }

        void OnEntryTextChanged(object sender, TextChangedEventArgs e)
        {
            string oldText = e.OldTextValue;
            string newText = e.NewTextValue;
            string myText = searchValue.Text;
            if (newText.Length != 1)
            {
                ViewModel.SearchInjuryCodeSeriesType(newText);
            }
        }

        void OnEntryCompleted(object sender, EventArgs e)
        {
            string text = ((Entry)sender).Text;
        }
    }
}