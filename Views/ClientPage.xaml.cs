using SPMSCAV1.Services.Interface;
using SPMSCAV1.ViewModels;

namespace SPMSCAV1.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ClientPage : ContentPage
    {
        private readonly IClientService _dataService;
        public ClientPage(IClientService dataService)
        {
            InitializeComponent();
            _dataService = dataService;
            BindingContext = ViewModel = new ClientViewModel(_dataService);
            //BindingContext = ViewModel = new ClientsViewModel();
        }

        ClientViewModel ViewModel { get; }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ViewModel.OnAppearing();
        }

        void OnEntryTextChanged(object sender, TextChangedEventArgs e)
        {
            string oldText = e.OldTextValue;
            string newText = e.NewTextValue;
            string myText = searchValue.Text;
            if(newText.Length != 1)
            {
                ViewModel.SearchClient(newText);
            }
        }

        void OnEntryCompleted(object sender, EventArgs e)
        {
            string text = ((Entry)sender).Text;
        }

    }
}