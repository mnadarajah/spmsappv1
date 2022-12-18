using SPMSCAV1.Services.Interface;
using SPMSCAV1.ViewModels;

namespace SPMSCAV1.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DocumentTypePage : ContentPage
    {
        private readonly IDocumentTypeService _dataService;
        public DocumentTypePage(IDocumentTypeService dataService)
        {
            InitializeComponent();
            _dataService = dataService;
            BindingContext = ViewModel = new DocumentTypeViewModel(_dataService);
            //BindingContext = ViewModel = new DocumentTypesViewModel();
        }

        DocumentTypeViewModel ViewModel { get; }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ViewModel.OnAppearing();
        }

        private void dataForm_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (!ViewModel.DocumentTypes.Equals(null))
            {
                ViewModel.SearchDocumentType();
            }

        }

        void OnEntryTextChanged(object sender, TextChangedEventArgs e)
        {
            string oldText = e.OldTextValue;
            string newText = e.NewTextValue;
            string myText = searchValue.Text;
            if (newText.Length != 1)
            {
                ViewModel.SearchDocumentType(newText);
            }
        }

        void OnEntryCompleted(object sender, EventArgs e)
        {
            string text = ((Entry)sender).Text;
        }
    }
}