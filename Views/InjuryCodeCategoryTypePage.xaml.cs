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

        private void dataForm_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (!ViewModel.InjuryCodeCategoryTypes.Equals(null))
            {
                ViewModel.SearchInjuryCodeCategoryType();
            }

        }

        void OnEntryTextChanged(object sender, TextChangedEventArgs e)
        {
            string oldText = e.OldTextValue;
            string newText = e.NewTextValue;
            string myText = searchValue.Text;
            if (newText.Length != 1)
            {
                ViewModel.SearchInjuryCodeCategoryType(newText);
            }
        }

        void OnEntryCompleted(object sender, EventArgs e)
        {
            string text = ((Entry)sender).Text;
        }
    }
}