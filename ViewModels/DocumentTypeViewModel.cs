using DevExpress.Maui.DataForm;
using SPMSCAV1.Models;
using SPMSCAV1.Services.Interface;
using System.Collections.ObjectModel;

namespace SPMSCAV1.ViewModels
{
    public class DocumentTypeViewModel : BaseViewModel
    {
        IDocumentTypeService _dataService;
        DocumentTypeModel _selectedDocumentType;
        string searchValue;
        bool init = false;

        public DocumentTypeViewModel(IDocumentTypeService dataService)

        {
            Title = "Browse";
            DocumentTypes = new ObservableCollection<DocumentTypeModel>();
            DocumentTypesOriginal = new ObservableCollection<DocumentTypeModel>();
            LoadDocumentTypesCommand = new Command(() => ExecuteLoadDocumentTypesCommand());
            DocumentTypeTapped = new Command<DocumentTypeModel>(OnDocumentTypeSelected);
            AddDocumentTypeCommand = new Command(OnAddDocumentType);
            SearchDocumentTypeCommand = new Command(SearchDocumentType);
            _dataService = dataService;
        }

        [DataFormDisplayOptions(IsVisible = false)]
        public ObservableCollection<DocumentTypeModel> DocumentTypes { get; }

        [DataFormDisplayOptions(IsVisible = false)]
        public ObservableCollection<DocumentTypeModel> DocumentTypesOriginal { get; }


        [DataFormDisplayOptions(IsVisible = false)]
        public Command LoadDocumentTypesCommand { get; }

        [DataFormDisplayOptions(IsVisible = false)]
        public Command AddDocumentTypeCommand { get; }

        [DataFormDisplayOptions(IsVisible = false)]
        public Command SearchDocumentTypeCommand { get; }

        [DataFormDisplayOptions(IsVisible = false)]
        public Command<DocumentTypeModel> DocumentTypeTapped { get; }

        [DataFormDisplayOptions(IsVisible = false)]
        public DocumentTypeModel SelectedDocumentType
        {
            get => this._selectedDocumentType;
            set
            {
                SetProperty(ref this._selectedDocumentType, value);
                OnDocumentTypeSelected(value);
            }
        }

        public string SearchValue
        {
            get => this.searchValue;
            set
            {
                SetProperty(ref this.searchValue, value);
            }


        }

        public void OnAppearing()
        {
            IsBusy = true;
            SelectedDocumentType = null;
            ExecuteLoadDocumentTypesCommand();
        }

        async void ExecuteLoadDocumentTypesCommand()
        {
            IsBusy = true;
            try
            {
                DocumentTypes.Clear();
                var genders = await _dataService.GetListAsync();
                foreach (var gender in genders)
                {
                    gender.Description = gender.Description;
                    DocumentTypes.Add(gender);
                    DocumentTypesOriginal.Add(gender);
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


        public void SearchDocumentType()
        {
            IsBusy = true;
            if (!init)
            {

            }
            else
            {
                DocumentTypes.Clear();
                if (SearchValue.Equals(null))
                {
                    foreach (var gender in DocumentTypesOriginal)
                    {
                        DocumentTypes.Add(gender);
                    }
                }
                else
                {
                    //if (SearchValue.Length > 1)
                    //{

                    foreach (var gender in DocumentTypesOriginal)
                    {
                        gender.Description = gender.Description;
                        if (gender.Description.ToLower().Contains(SearchValue.ToLower()))
                        {
                            DocumentTypes.Add(gender);
                        }
                    }
                    //}
                }
            }
            IsBusy = false;
        }


        async void OnAddDocumentType(object obj)
        {
            await Navigation.NavigateToAsync<NewDocumentTypeViewModel>(null);
        }

        async void OnDocumentTypeSelected(DocumentTypeModel gender)
        {
            if (gender == null)
                return;
            await Navigation.NavigateToAsync<DocumentTypeDetailViewModel>(gender.DocumentTypeId);
        }

    }
}