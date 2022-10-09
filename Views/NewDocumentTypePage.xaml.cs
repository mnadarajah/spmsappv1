using SPMSCAV1.Services.Interface;
using SPMSCAV1.ViewModels;

namespace SPMSCAV1.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewDocumentTypePage : ContentPage
    {
        private readonly IDocumentTypeService _dataService;
        public NewDocumentTypePage(IDocumentTypeService dataService)
        {
            InitializeComponent();
            _dataService = dataService;
            BindingContext = new NewDocumentTypeViewModel(dataService);
        }

    }
}