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
            Routing.RegisterRoute(typeof(GenderDetailPage).FullName, typeof(GenderDetailPage));
            Routing.RegisterRoute(typeof(PaymentTypeDetailPage).FullName, typeof(PaymentTypeDetailPage));
            Routing.RegisterRoute(typeof(InjuryCodeSeriesTypeDetailPage).FullName, typeof(InjuryCodeSeriesTypeDetailPage));
            Routing.RegisterRoute(typeof(InjuryCodeCategoryTypeDetailPage).FullName, typeof(InjuryCodeCategoryTypeDetailPage));
            Routing.RegisterRoute(typeof(GoodAndServiceTypeDetailPage).FullName, typeof(GoodAndServiceTypeDetailPage));
            Routing.RegisterRoute(typeof(DocumentTypeDetailPage).FullName, typeof(DocumentTypeDetailPage));
            Routing.RegisterRoute(typeof(NewGenderPage).FullName, typeof(NewGenderPage));
            Routing.RegisterRoute(typeof(NewPaymentTypePage).FullName, typeof(NewPaymentTypePage));
            Routing.RegisterRoute(typeof(NewInjuryCodeSeriesTypePage).FullName, typeof(NewInjuryCodeSeriesTypePage));
            Routing.RegisterRoute(typeof(NewInjuryCodeCategoryTypePage).FullName, typeof(NewInjuryCodeCategoryTypePage));
            Routing.RegisterRoute(typeof(NewGoodAndServiceTypePage).FullName, typeof(NewGoodAndServiceTypePage));
            Routing.RegisterRoute(typeof(NewDocumentTypePage).FullName, typeof(NewDocumentTypePage));
            Routing.RegisterRoute(typeof(EditGenderPage).FullName, typeof(EditGenderPage));
            Routing.RegisterRoute(typeof(EditPaymentTypePage).FullName, typeof(EditPaymentTypePage));
            Routing.RegisterRoute(typeof(EditInjuryCodeSeriesTypePage).FullName, typeof(EditInjuryCodeSeriesTypePage));
            Routing.RegisterRoute(typeof(EditInjuryCodeCategoryTypePage).FullName, typeof(EditInjuryCodeCategoryTypePage));
            Routing.RegisterRoute(typeof(EditGoodAndServiceTypePage).FullName, typeof(EditGoodAndServiceTypePage));
            Routing.RegisterRoute(typeof(EditDocumentTypePage).FullName, typeof(EditDocumentTypePage));
            Routing.RegisterRoute(typeof(EditClientPage).FullName, typeof(EditClientPage));
            Routing.RegisterRoute(typeof(NewClientPage).FullName, typeof(NewClientPage));
            Routing.RegisterRoute(typeof(ClientDetailPage).FullName, typeof(ClientDetailPage));
            Routing.RegisterRoute(typeof(CalendarPage).FullName, typeof(CalendarPage));
            Routing.RegisterRoute(typeof(NewCalendarPage).FullName, typeof(NewCalendarPage));
            Routing.RegisterRoute(typeof(EditCalendarPage).FullName, typeof(EditCalendarPage));

            MainPage = new MainPage();
            //MainPage = new NavigationPage(new MainPage());

        }
    }
}