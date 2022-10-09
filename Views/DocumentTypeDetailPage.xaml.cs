using SPMSCAV1.Services.Interface;
using SPMSCAV1.ViewModels;

namespace SPMSCAV1.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DocumentTypeDetailPage : ContentPage
    {
        private readonly IDocumentTypeService _dataService;
        public DocumentTypeDetailPage(IDocumentTypeService dataService)
        {
            InitializeComponent();
            _dataService = dataService;
            BindingContext = new DocumentTypeDetailViewModel(dataService);

        }
    }
}