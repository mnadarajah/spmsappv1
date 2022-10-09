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
    }
}