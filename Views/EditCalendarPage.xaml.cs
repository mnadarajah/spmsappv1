using DevExpress.Maui.DataForm;
using SPMSCAV1.Models;
using SPMSCAV1.Services.Interface;
using SPMSCAV1.Services.Repository;
using SPMSCAV1.ViewModels;

namespace SPMSCAV1.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditCalendarPage : ContentPage
    {
        public EditCalendarPage()
        {
            InitializeComponent();
            BindingContext = ViewModel = new EditCalendarViewModel();
            dataForm.PickerSourceProvider = new ComboBoxDataProviderService();
        }

        EditCalendarViewModel ViewModel { get; }
    }


}