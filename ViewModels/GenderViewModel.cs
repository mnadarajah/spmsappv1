using DevExpress.Maui.DataForm;
using SPMSCAV1.Models;
using SPMSCAV1.Services.Interface;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace SPMSCAV1.ViewModels
{
    public class GenderViewModel : BaseViewModel
    {
        IGenderService _dataService;
        GenderModel _selectedGender;
        string searchValue;
        bool init = false;

        public GenderViewModel(IGenderService dataService)

        {
            Title = "Browse";
            Genders = new ObservableCollection<GenderModel>();
            GendersOriginal = new ObservableCollection<GenderModel>();
            LoadGendersCommand = new Command(() => ExecuteLoadGendersCommand());
            GenderTapped = new Command<GenderModel>(OnGenderSelected);
            AddGenderCommand = new Command(OnAddGender);
            SearchGenderCommand = new Command(SearchGender);
            _dataService = dataService;
        }

        [DataFormDisplayOptions(IsVisible = false)]
        public ObservableCollection<GenderModel> Genders { get; }

        [DataFormDisplayOptions(IsVisible = false)]
        public ObservableCollection<GenderModel> GendersOriginal { get; }


        [DataFormDisplayOptions(IsVisible = false)]
        public Command LoadGendersCommand { get; }

        [DataFormDisplayOptions(IsVisible = false)]
        public Command AddGenderCommand { get; }

        [DataFormDisplayOptions(IsVisible = false)]
        public Command SearchGenderCommand { get; }

        [DataFormDisplayOptions(IsVisible = false)]
        public Command<GenderModel> GenderTapped { get; }

        [DataFormDisplayOptions(IsVisible = false)]
        public GenderModel SelectedGender
        {
            get => this._selectedGender;
            set
            {
                SetProperty(ref this._selectedGender, value);
                OnGenderSelected(value);
            }
        }

        public string SearchValue
        {
            get => this.searchValue;
            set  {
                SetProperty(ref this.searchValue, value); 
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
                    GendersOriginal.Add(gender);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }
            finally
            {
                init = true;
                IsBusy = false;
            }
        }


        public void SearchGender()
        {
            IsBusy = true;
            if (!init){

            }
            else
            {
                Genders.Clear();
                if (SearchValue.Equals(null))
                {
                    foreach (var gender in GendersOriginal)
                    {
                        Genders.Add(gender);
                    }
                }
                else
                {
                    foreach (var gender in GendersOriginal)
                    {
                        gender.Description = gender.Description;
                        if (gender.Description.ToLower().Contains(SearchValue.ToLower()))
                        {
                            Genders.Add(gender);
                        }
                    }
                }
            }
            IsBusy = false;
        }


        async void OnAddGender(object obj)
        {
            await Navigation.NavigateToAsync<NewGenderViewModel>(null);
        }

        async void OnGenderSelected(GenderModel gender)
        {
            if (gender == null)
                return;
            await Navigation.NavigateToAsync<GenderDetailViewModel>(gender.GenderId);
        }

    }
}