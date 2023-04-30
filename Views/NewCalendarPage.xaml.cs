using DevExpress.Maui.DataForm;
using SPMSCAV1.Models;
using SPMSCAV1.Services.Interface;
using SPMSCAV1.Services.Repository;
using SPMSCAV1.ViewModels;

namespace SPMSCAV1.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewCalendarPage : ContentPage
    {
        public NewCalendarPage()
        {
            InitializeComponent();
            BindingContext = ViewModel = new NewCalendarViewModel();
            dataForm.PickerSourceProvider = new ComboBoxDataProviderService();
        }

        NewCalendarViewModel ViewModel { get; }


        protected override void OnAppearing()
        {
            base.OnAppearing();
            ViewModel.OnAppearing();
        }
    }


}