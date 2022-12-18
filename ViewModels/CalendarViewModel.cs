using SPMSCAV1.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPMSCAV1.ViewModels
{
    public class CalendarViewModel : INotifyPropertyChanged
    {
        readonly CalendarModel data;

        public event PropertyChangedEventHandler PropertyChanged;
        public DateTime StartDate { get { return CalendarModel.BaseDate; } }
        public IReadOnlyList<MedicalAppointment> MedicalAppointments
        { get => data.MedicalAppointments; }

        public IReadOnlyList<MedicalAppointmentType> AppointmentTypes 
        { get => data.Labels; }

        public CalendarViewModel()
        {
            data = new CalendarModel();
        }

        protected void RaisePropertyChanged(string name)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
    }
}
