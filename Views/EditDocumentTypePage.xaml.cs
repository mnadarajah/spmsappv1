using SPMSCAV1.Services.Interface;
using SPMSCAV1.ViewModels;

namespace SPMSCAV1.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditDocumentTypePage : ContentPage
    {
        private readonly IDocumentTypeService _dataService;
        public EditDocumentTypePage(IDocumentTypeService dataService)
        {
            InitializeComponent();
            _dataService = dataService;
            BindingContext = new EditDocumentTypeViewModel(dataService);
        }

    }
}