﻿using DevExpress.Maui.DataForm;
using SPMSCAV1.Models;
using SPMSCAV1.Services.Interface;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace SPMSCAV1.ViewModels
{
    public class GenderViewModel : BaseViewModel, INotifyPropertyChanged
    {
        IGenderService _dataService;
        GenderModel _selectedGender;
        List<string> _Queue = new List<string>();
        string searchValue;
        bool init = false;
        bool isRefreshing = false;
        int skip = 0;
        int take = 10;

        public GenderViewModel(IGenderService dataService)

        {
            Title = "Browse";
            Genders = new ObservableCollection<GenderModel>();
            GendersOriginal = new ObservableCollection<GenderModel>();
            LoadGendersCommand = new Command(() => ExecuteLoadGendersCommand());
            GenderTapped = new Command<GenderModel>(OnGenderSelected);
            AddGenderCommand = new Command(OnAddGender);
            SearchGenderCommand = new Command(SearchGender);
            LoadMoreCommand = new Command(ExecuteLoadMoreCommand);
            _dataService = dataService;
        }

        [DataFormDisplayOptions(IsVisible = false)]
        public ObservableCollection<GenderModel> Genders { get; }

        [DataFormDisplayOptions(IsVisible = false)]
        public ObservableCollection<GenderModel> GendersOriginal { get; }

        [DataFormDisplayOptions(IsVisible = false)]

        ICommand loadMoreCommand = null;

        [DataFormDisplayOptions(IsVisible = false)]
        public ICommand LoadMoreCommand
        {
            get { return loadMoreCommand; }
            set
            {
                if (loadMoreCommand != value)
                {
                    loadMoreCommand = value;
                    OnPropertyChanged("LoadMoreCommand");
                }
            }
        }


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
            skip = 0;
            ExecuteLoadGendersCommand();
        }

        async void ExecuteLoadGendersCommand()
        {
            IsBusy = true;
            try
            {
                Genders.Clear();
                var genders = await _dataService.Search("*", skip, take);
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

        public bool IsRefreshing
        {
            get { return isRefreshing; }
            set
            {
                if (isRefreshing != value)
                {
                    isRefreshing = value;
                    OnPropertyChanged("IsRefreshing");
                }
            }
        }

        async void ExecuteLoadMoreCommand()
        {
            await Task.Delay(2000);
            if (!IsBusy && init)
            {
                try
                {
                    IsBusy = true;
                    skip += 1;
                    var genderTypes = await _dataService.Search("*", skip, take);
                    foreach (var genderType in genderTypes)
                    {
                        genderType.Description = genderType.Description;
                        Genders.Add(genderType);
                        GendersOriginal.Add(genderType);
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex);
                }
                finally
                {
                    IsBusy = false;
                    IsRefreshing = false;
                }
            }
        }


        public async void SearchGender()
        {
            IsBusy = true;
            if (!init){

            }
            else
            {
                IEnumerable<GenderModel> genders;
                Genders.Clear();
                if (!string.IsNullOrEmpty(SearchValue))
                {
                     genders = await _dataService.Search(SearchValue);
                }
                else
                {
                     genders = await _dataService.Search("*", skip, take);
                }

                foreach (var gender in genders)
                {
                    gender.Description = gender.Description;
                    Genders.Add(gender);
                    GendersOriginal.Add(gender);
                }
            }
            IsBusy = false;
        }

        public async void SearchGender(string searchValue)
        {
            IsBusy = true;
            if (!init)
            {

            }
            else
            {
                IEnumerable<GenderModel> genders;
                Genders.Clear();
                if (!string.IsNullOrEmpty(searchValue))
                {
                    genders = await _dataService.Search(searchValue);
                }
                else
                {
                    genders = await _dataService.Search("*", skip, take);
                }

                foreach (var gender in genders)
                {
                    gender.Description = gender.Description;
                    Genders.Add(gender);
                    GendersOriginal.Add(gender);
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

        public void AddTextToQueue(string text)
        {
            //lock(_Queue)
            _Queue.Add(text);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}