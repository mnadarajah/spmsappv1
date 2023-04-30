using DevExpress.Maui.DataForm;
using SPMSCAV1.Models;
using SPMSCAV1.Services.Interface;
using SPMSCAV1.Services.Repository;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace SPMSCAV1.ViewModels
{
    public class NewCalendarViewModel : BaseViewModel, IQueryAttributable
    {
        public const string ViewName = "NewCalendarPage";

        int id;
        DateTime startTime;
        DateTime endTime;
        DateTime startDate;
        DateTime endDate;
        DateTime startingDateTime;
        DateTime endingDateTime;
        string subject;
        string labelId;
        string location;

        public NewCalendarViewModel()
        {
            Title = "New Calendar";
            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();


        }

        [DataFormTimeEditor]
        public DateTime StartTime
        {
            get => this.startTime;
            set => SetProperty(ref this.startTime, value);

        }

        [DataFormTimeEditor]
        public DateTime EndTime
        {
            get => this.endTime;
            set => SetProperty(ref this.endTime, value);

        }

        [DataFormDateEditor]
        public DateTime StartDate
        {
            get => this.startDate;
            set => SetProperty(ref this.startDate, value);

        }

        [DataFormDateEditor]
        public DateTime EndDate
        {
            get => this.endDate;
            set => SetProperty(ref this.endDate, value);

        }

        public string Subject
        {
            get => this.subject;
            set => SetProperty(ref this.subject, value);
        }

        public string Location
        {
            get => this.location;
            set => SetProperty(ref this.location, value);
        }

        [DataFormComboBoxEditor]
        public string LabelId
        {
            get => this.labelId;
            set => SetProperty(ref this.labelId, value);
        }




        [DataFormDisplayOptions(IsVisible = false)]
        public Command SaveCommand { get; }

        [DataFormDisplayOptions(IsVisible = false)]
        public Command CancelCommand { get; }


        bool ValidateSave()
        {
            DateOnly startingDate = DateOnly.FromDateTime(StartDate);
            TimeOnly startingTime = TimeOnly.FromDateTime(StartTime);
            DateOnly endingDate = DateOnly.FromDateTime(EndDate);
            TimeOnly endingTime = TimeOnly.FromDateTime(EndTime);
            startingDateTime = startingDate.ToDateTime(startingTime);
            endingDateTime = endingDate.ToDateTime(endingTime);
            return !String.IsNullOrWhiteSpace(this.subject)
                && startingDateTime.CompareTo(endingDateTime) < 0 ;
        }

        async void OnCancel()
        {
            await Shell.Current.GoToAsync($"///SPMSCAV1.Views.CalendarPage");
        }

        async void OnSave()
        {
            Random rand = new Random();
            id = rand.Next(500, 1000);
            var navigationParameter = new Dictionary<string, object>()
            {
                {"crud", "post" },
                { "id", id.ToString() },
                { "startTime", startingDateTime.ToString() },
                {"endTime", endingDateTime.ToString() },
                {"subject", subject },
                {"labelId", labelId },
                {"location", location }
            };
            await Shell.Current.GoToAsync($"..", navigationParameter);
        }

        public void OnAppearing()
        {
            Subject = String.Empty;
            LabelId = String.Empty;
            Location = String.Empty;
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            DateTime date;
            string start = HttpUtility.UrlDecode(query["id"] as string);
            DateTime.TryParse(start, out date);
            startDate = endDate = startTime = endTime = date;
            Subject = String.Empty;
            LabelId = String.Empty;
            Location = String.Empty;

        }
    }
}