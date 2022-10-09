using SPMSCAV1.Services.Interface;
using SPMSCAV1.ViewModels;

namespace SPMSCAV1.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GoodAndServiceTypeDetailPage : ContentPage
    {
        private readonly IGoodAndServiceTypeService _dataService;
        public GoodAndServiceTypeDetailPage(IGoodAndServiceTypeService dataService)
        {
            InitializeComponent();
            _dataService = dataService;
            BindingContext = new GoodAndServiceTypeDetailViewModel(dataService);

        }
    }
}