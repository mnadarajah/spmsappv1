using SPMSCAV1.Services.Interface;
using SPMSCAV1.ViewModels;

namespace SPMSCAV1.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ClientDetailPage : ContentPage
    {
        private readonly IClientService _dataService;
        public ClientDetailPage(IClientService dataService)
        {
            InitializeComponent();
            _dataService = dataService;
            BindingContext = new ClientDetailViewModel(dataService);

        }
    }
}