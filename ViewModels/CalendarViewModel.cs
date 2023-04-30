using DevExpress.Maui.Scheduler;
using DevExpress.Maui.Scheduler.Internal;
using SPMSCAV1.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SPMSCAV1.ViewModels
{
    public class CalendarViewModel : BaseViewModel, INotifyPropertyChanged, IQueryAttributable
    {
        public CalendarModel data;

        public event PropertyChangedEventHandler PropertyChanged;
        public DateTime StartDate { get { return CalendarModel.BaseDate; } }
        public ObservableCollection<MedicalAppointment> MedicalAppointments
        { get; set; }

        public List<MedicalAppointmentType> AppointmentTypes 
        { get => data.Labels.ToList(); }

        public CalendarViewModel()
        {
            data = new CalendarModel();
            MedicalAppointments = data.MedicalAppointments;
        }

        public async void createNewAppointment(DateTime start)
        {
            await Navigation.NavigateToAsync<NewCalendarViewModel>(start);
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.Count > 0)
            {
                if (HttpUtility.UrlDecode(query["crud"] as string).Equals("post"))
                {
                    string id = HttpUtility.UrlDecode(query["id"] as string);
                    string startTime = HttpUtility.UrlDecode(query["startTime"] as string);
                    string endTime = HttpUtility.UrlDecode(query["endTime"] as string);
                    string subject = HttpUtility.UrlDecode(query["subject"] as string);
                    string label = HttpUtility.UrlDecode(query["labelId"] as string);
                    string location = HttpUtility.UrlDecode(query["location"] as string);

                    int appointmentId = int.Parse(id);
                    string patientName = subject;
                    DateTime start = DateTime.Parse(startTime);
                    DateTime end = DateTime.Parse(endTime);
                    int labelId = AppointmentTypes.Find(i => i.Caption == label).Id;
                    string room = location;

                    var apt = data.CreateMedicAppointment(appointmentId, patientName, start, end, labelId, room);
                }
                if (HttpUtility.UrlDecode(query["crud"] as string).Equals("put"))
                {
                    string id = HttpUtility.UrlDecode(query["id"] as string);
                    string startTime = HttpUtility.UrlDecode(query["startTime"] as string);
                    string endTime = HttpUtility.UrlDecode(query["endTime"] as string);
                    string subject = HttpUtility.UrlDecode(query["subject"] as string);
                    string label = HttpUtility.UrlDecode(query["labelId"] as string);
                    string location = HttpUtility.UrlDecode(query["location"] as string);

                    int appointmentId = int.Parse(id);
                    string patientName = subject;
                    DateTime start = DateTime.Parse(startTime);
                    DateTime end = DateTime.Parse(endTime);
                    int labelId;
                    if (!int.TryParse(label, out labelId))
                    {
                        labelId = AppointmentTypes.Find(i => i.Caption == label).Id;
                    }
                    string room = location;
                    MedicalAppointment medApt = new MedicalAppointment()
                    {
                        Id = appointmentId,
                        Subject = patientName,
                        EndTime = end,
                        StartTime = start,
                        Location = room,
                        LabelId = labelId
                    };
                    MedicalAppointments.Remove(MedicalAppointments.Where(i => i.Id == appointmentId).Single());
                    MedicalAppointments.Add(medApt);
                }
                MedicalAppointments = data.MedicalAppointments;
            }         
        }

        public void OnAppearing()
        {
            MedicalAppointments = data.MedicalAppointments;
        }

        public async void editAppointment(AppointmentItem appointment)
        {
            var navigationParameter = new Dictionary<string, object>()
            {
                { "appointmentId", appointment.Id },
                { "startTime", appointment.Start },
                {"endTime", appointment.End },
                {"subject", appointment.Subject },
                {"labelId", appointment.LabelId },
                {"location", appointment.Location }
            };

            await Navigation.NavigateToAsyncMultiparm<EditCalendarViewModel>(navigationParameter);
        }
    }
}
