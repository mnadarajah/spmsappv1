using DevExpress.Maui.Scheduler;
using SPMSCAV1.Services.Interface;
using SPMSCAV1.ViewModels;

namespace SPMSCAV1.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CalendarPage : ContentPage
    {
        public CalendarPage()
        {
            InitializeComponent();
        }

        private void WorkWeekView_Tap(object sender, SchedulerGestureEventArgs e)
        {
            if (e.AppointmentInfo == null)
            {
                ShowNewAppointmentEditPage(e.IntervalInfo);
                return;
            }
            AppointmentItem appointment = e.AppointmentInfo.Appointment;
            ShowAppointmentEditPage(appointment);
        }

        private void ShowAppointmentEditPage(AppointmentItem appointment)
        {
            AppointmentEditPage appEditPage = new AppointmentEditPage(appointment, this.storage);
            Navigation.PushAsync(appEditPage);
        }

        private void ShowNewAppointmentEditPage(IntervalInfo info)
        {
            AppointmentEditPage appEditPage = new AppointmentEditPage(info.Start, info.End,
                                                                     info.AllDay, this.storage);
            Navigation.PushAsync(appEditPage);
        }
    }
}