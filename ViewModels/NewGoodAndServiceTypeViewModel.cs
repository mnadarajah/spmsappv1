﻿using DevExpress.Maui.DataForm;
using SPMSCAV1.Models;
using SPMSCAV1.Services.Interface;

namespace SPMSCAV1.ViewModels
{
    public class NewGoodAndServiceTypeViewModel : BaseViewModel
    {
        public const string ViewName = "NewGoodAndServiceTypePage";


        string description;
        string code;


        IGoodAndServiceTypeService _dataService;
        public NewGoodAndServiceTypeViewModel(IGoodAndServiceTypeService dataService)
        {
            Title = "New GoodAndServiceType";
            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
            _dataService = dataService;
        }


        public string Description
        {
            get => this.description;
            set => SetProperty(ref this.description, value);
        }

        public string Code
        {
            get => this.code;
            set => SetProperty(ref this.code, value);
        }


        [DataFormDisplayOptions(IsVisible = false)]
        public Command SaveCommand { get; }

        [DataFormDisplayOptions(IsVisible = false)]
        public Command CancelCommand { get; }


        bool ValidateSave()
        {
            return !String.IsNullOrWhiteSpace(this.description)
                && !String.IsNullOrWhiteSpace(this.code);
        }

        async void OnCancel()
        {
            // This will pop the current page off the navigation stack
            await Navigation.GoBackAsync();
        }

        async void OnSave()
        {
            GoodAndServiceTypeModel newGoodAndServiceType = new GoodAndServiceTypeModel()
            {
                //GoodAndServiceTypeId = (long)_dataService.GetListAsync().Result.Count + 1,
                Description = description,
                Code = code,
                CreatedBy = Environment.UserName,
                Created = DateTime.Now,
                UpdatedBy = Environment.UserName,
                Updated = DateTime.Now,
                Version = 1,
                Active = true
            };

            await _dataService.AddAndSaveAsync(newGoodAndServiceType);

            // This will pop the current page off the navigation stack
            await Navigation.GoBackAsync();
        }
    }
}