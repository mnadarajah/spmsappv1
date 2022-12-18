using DevExpress.Maui.DataForm;
using SPMSCAV1.Models;
using SPMSCAV1.Services.Interface;
using SPMSCAV1.Services.Repository;
using SPMSCAV1.ViewModels;

namespace SPMSCAV1.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]


    public partial class EditClientPage : ContentPage
    {
        private readonly IClientService _dataService;
        public EditClientPage(IClientService dataService)
        {
            InitializeComponent();
            _dataService = dataService;

            BindingContext = new EditClientViewModel(dataService);
            dataForm.PickerSourceProvider = new ComboBoxDataProviderService();
        }

    }
}