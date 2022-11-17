using SPMSCAV1.Services;
using SPMSCAV1.Views;
using Application = Microsoft.Maui.Controls.Application;

namespace SPMSCAV1
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            DependencyService.Register<NavigationService>();
            Routing.RegisterRoute(typeof(ItemDetailPage).FullName, typeof(ItemDetailPage));
            Routing.RegisterRoute(typeof(GenderDetailPage).FullName, typeof(GenderDetailPage));
            Routing.RegisterRoute(typeof(PaymentTypeDetailPage).FullName, typeof(PaymentTypeDetailPage));
            Routing.RegisterRoute(typeof(InjuryCodeSeriesTypeDetailPage).FullName, typeof(InjuryCodeSeriesTypeDetailPage));
            Routing.RegisterRoute(typeof(InjuryCodeCategoryTypeDetailPage).FullName, typeof(InjuryCodeCategoryTypeDetailPage));
            Routing.RegisterRoute(typeof(GoodAndServiceTypeDetailPage).FullName, typeof(GoodAndServiceTypeDetailPage));
            Routing.RegisterRoute(typeof(DocumentTypeDetailPage).FullName, typeof(DocumentTypeDetailPage));
            Routing.RegisterRoute(typeof(NewItemPage).FullName, typeof(NewItemPage));
            Routing.RegisterRoute(typeof(NewGenderPage).FullName, typeof(NewGenderPage));
            Routing.RegisterRoute(typeof(NewPaymentTypePage).FullName, typeof(NewPaymentTypePage));
            Routing.RegisterRoute(typeof(NewInjuryCodeSeriesTypePage).FullName, typeof(NewInjuryCodeSeriesTypePage));
            Routing.RegisterRoute(typeof(NewInjuryCodeCategoryTypePage).FullName, typeof(NewInjuryCodeCategoryTypePage));
            Routing.RegisterRoute(typeof(NewGoodAndServiceTypePage).FullName, typeof(NewGoodAndServiceTypePage));
            Routing.RegisterRoute(typeof(NewDocumentTypePage).FullName, typeof(NewDocumentTypePage));
            Routing.RegisterRoute(typeof(EditGenderPage).FullName, typeof(EditGenderPage));
            MainPage = new MainPage();
        }
    }
}