using SPMSCAV1.Models;
using SPMSCAV1.Services.Interface;
using System.Collections.ObjectModel;

namespace SPMSCAV1.ViewModels
{
    public class DocumentTypeViewModel : BaseViewModel
    {
        IDocumentTypeService _dataService;
        DocumentTypeModel _selectedDocumentType;

        public DocumentTypeViewModel(IDocumentTypeService dataService)

        {
            Title = "Browse";
            DocumentTypes = new ObservableCollection<DocumentTypeModel>();
            LoadDocumentTypesCommand = new Command(() => ExecuteLoadDocumentTypesCommand());
            DocumentTypeTapped = new Command<DocumentTypeModel>(OnDocumentTypeSelected);
            AddDocumentTypeCommand = new Command(OnAddDocumentType);
            _dataService = dataService;
        }


        public ObservableCollection<DocumentTypeModel> DocumentTypes { get; }

        public Command LoadDocumentTypesCommand { get; }

        public Command AddDocumentTypeCommand { get; }

        public Command<DocumentTypeModel> DocumentTypeTapped { get; }

        public DocumentTypeModel SelectedDocumentType
        {
            get => this._selectedDocumentType;
            set
            {
                SetProperty(ref this._selectedDocumentType, value);
                OnDocumentTypeSelected(value);
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
                var injuryCodeCategorys = await _dataService.GetListAsync();
                foreach (var injuryCodeCategory in injuryCodeCategorys)
                {
                    injuryCodeCategory.Description = injuryCodeCategory.Description;
                    DocumentTypes.Add(injuryCodeCategory);
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

        async void OnAddDocumentType(object obj)
        {
            await Navigation.NavigateToAsync<NewDocumentTypeViewModel>(null);
        }

        async void OnDocumentTypeSelected(DocumentTypeModel injuryCodeCategory)
        {
            if (injuryCodeCategory == null)
                return;
            await Navigation.NavigateToAsync<DocumentTypeDetailViewModel>(injuryCodeCategory.DocumentTypeId);
        }
    }
}