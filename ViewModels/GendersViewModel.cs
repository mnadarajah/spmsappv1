using SPMSCAV1.Models;
using SPMSCAV1.Services.Interface;
using System.Collections.ObjectModel;

namespace SPMSCAV1.ViewModels
{
    public class GendersViewModel : BaseViewModel
    {
        IGenderService _dataService;
        GenderModel _selectedGender;

        public GendersViewModel(IGenderService dataService)

        {
            Title = "Browse";
            Genders = new ObservableCollection<GenderModel>();
            LoadGendersCommand = new Command(() => ExecuteLoadGendersCommand());
            GenderTapped = new Command<GenderModel>(OnGenderSelected);
            AddGenderCommand = new Command(OnAddGender);
            _dataService = dataService;
        }


        public ObservableCollection<GenderModel> Genders { get; }

        public Command LoadGendersCommand { get; }

        public Command AddGenderCommand { get; }

        public Command<GenderModel> GenderTapped { get; }

        public GenderModel SelectedGender
        {
            get => this._selectedGender;
            set
            {
                SetProperty(ref this._selectedGender, value);
                OnGenderSelected(value);
            }
        }

        public void OnAppearing()
        {
            IsBusy = true;
            SelectedGender = null;
            ExecuteLoadGendersCommand();
        }

        async void ExecuteLoadGendersCommand()
        {
            IsBusy = true;
            try
            {
                Genders.Clear();
                var genders = await _dataService.GetListAsync();
                foreach (var gender in genders)
                {
                    gender.Description = gender.Description;
                    Genders.Add(gender);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        async void OnAddGender(object obj)
        {
            //await Navigation.NavigateToAsync<NewGenderViewModel>(null);
        }

        async void OnGenderSelected(GenderModel gender)
        {
            if (gender == null)
                return;
            //await Navigation.NavigateToAsync<GenderDetailViewModel>(gender.GenderId);
        }
    }
}