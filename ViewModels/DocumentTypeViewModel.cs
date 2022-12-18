using DevExpress.Maui.DataForm;
using SPMSCAV1.Models;
using SPMSCAV1.Services.Interface;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace SPMSCAV1.ViewModels
{
    public class DocumentTypeViewModel : BaseViewModel, INotifyPropertyChanged
    {
        IDocumentTypeService _dataService;
        DocumentTypeModel _selectedDocumentType;
        string searchValue;
        bool init = false;
        bool isRefreshing = false;
        int skip = 0;
        int take = 10;

        public DocumentTypeViewModel(IDocumentTypeService dataService)

        {
            Title = "Browse";
            DocumentTypes = new ObservableCollection<DocumentTypeModel>();
            DocumentTypesOriginal = new ObservableCollection<DocumentTypeModel>();
            LoadDocumentTypesCommand = new Command(() => ExecuteLoadDocumentTypesCommand());
            DocumentTypeTapped = new Command<DocumentTypeModel>(OnDocumentTypeSelected);
            AddDocumentTypeCommand = new Command(OnAddDocumentType);
            SearchDocumentTypeCommand = new Command(SearchDocumentType);
            LoadMoreCommand = new Command(ExecuteLoadMoreCommand);
            _dataService = dataService;
        }

        [DataFormDisplayOptions(IsVisible = false)]
        public ObservableCollection<DocumentTypeModel> DocumentTypes { get; }

        [DataFormDisplayOptions(IsVisible = false)]
        public ObservableCollection<DocumentTypeModel> DocumentTypesOriginal { get; }

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
            skip = 0;
            ExecuteLoadDocumentTypesCommand();
        }

        async void ExecuteLoadDocumentTypesCommand()
        {
            IsBusy = true;
            try
            {
                DocumentTypes.Clear();
                var documentTypes = await _dataService.Search("*", skip, take);
                foreach (var documentType in documentTypes)
                {
                    documentType.Description = documentType.Description;
                    DocumentTypes.Add(documentType);
                    DocumentTypesOriginal.Add(documentType);
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
                    var documentTypes = await _dataService.Search("*", skip, take);
                    foreach (var documentType in documentTypes)
                    {
                        documentType.Description = documentType.Description;
                        DocumentTypes.Add(documentType);
                        DocumentTypesOriginal.Add(documentType);
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
                    foreach (var documentType in DocumentTypesOriginal)
                    {
                        DocumentTypes.Add(documentType);
                    }
                }
                else
                {
                    //if (SearchValue.Length > 1)
                    //{

                    foreach (var documentType in DocumentTypesOriginal)
                    {
                        documentType.Description = documentType.Description;
                        if (documentType.Description.ToLower().Contains(SearchValue.ToLower()))
                        {
                            DocumentTypes.Add(documentType);
                        }
                    }
                    //}
                }
            }
            IsBusy = false;
        }

        public async void SearchDocumentType(string searchValue)
        {
            IsBusy = true;
            if (!init)
            {

            }
            else
            {
                IEnumerable<DocumentTypeModel> documentTypes;
                DocumentTypes.Clear();
                if (!string.IsNullOrEmpty(searchValue))
                {
                    documentTypes = await _dataService.Search(searchValue);
                }
                else
                {
                    documentTypes = await _dataService.Search("*", skip, take);
                }

                foreach (var documentType in documentTypes)
                {
                    documentType.Description = documentType.Description;
                    DocumentTypes.Add(documentType);
                    DocumentTypesOriginal.Add(documentType);
                }
            }
            IsBusy = false;
        }


        async void OnAddDocumentType(object obj)
        {
            await Navigation.NavigateToAsync<NewDocumentTypeViewModel>(null);
        }

        async void OnDocumentTypeSelected(DocumentTypeModel documentType)
        {
            if (documentType == null)
                return;
            await Navigation.NavigateToAsync<DocumentTypeDetailViewModel>(documentType.DocumentTypeId);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}