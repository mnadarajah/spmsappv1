using DevExpress.Maui.DataForm;
using SPMSCAV1.Models;
using SPMSCAV1.Services.Interface;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace SPMSCAV1.ViewModels
{
    public class ClientViewModel : BaseViewModel , INotifyPropertyChanged
    {
        IClientService _dataService;
        ClientModel _selectedClient;
        List<string> _Queue = new List<string>();
        string searchValue;
        bool init = false;
        bool isRefreshing = false;
        int skip = 0;
        int take = 15;
        public event PropertyChangedEventHandler PropertyChanged;

        public ClientViewModel(IClientService dataService)
        {
            Title = "Browse";
            Clients = new ObservableCollection<ClientModel>();
            ClientsOriginal = new ObservableCollection<ClientModel>();
            LoadClientsCommand = new Command(() => ExecuteLoadClientsCommand());
            ClientTapped = new Command<ClientModel>(OnClientSelected);
            AddClientCommand = new Command(OnAddClient);
            SearchClientCommand = new Command(SearchClient);
            LoadMoreCommand = new Command(ExecuteLoadMoreCommand);
            _dataService = dataService;
        }

        [DataFormDisplayOptions(IsVisible = false)]
        public ObservableCollection<ClientModel> Clients { get; }

        [DataFormDisplayOptions(IsVisible = false)]
        public ObservableCollection<ClientModel> ClientsOriginal { get; }

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
        public Command LoadClientsCommand { get; }

        [DataFormDisplayOptions(IsVisible = false)]
        public Command AddClientCommand { get; }

        [DataFormDisplayOptions(IsVisible = false)]
        public Command SearchClientCommand { get; }

        [DataFormDisplayOptions(IsVisible = false)]
        public Command<ClientModel> ClientTapped { get; }

        [DataFormDisplayOptions(IsVisible = false)]
        public ClientModel SelectedClient
        {
            get => this._selectedClient;
            set
            {
                SetProperty(ref this._selectedClient, value);
                OnClientSelected(value);
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
            SelectedClient = null;
            skip = 0;
            ExecuteLoadClientsCommand();
            init = true;
            IsRefreshing = false;
            
        }

        async void ExecuteLoadClientsCommand()
        {
            IsBusy = true;
            try
            {
                Clients.Clear();
                var clients = await _dataService.Search("*", skip, take);
                foreach (var client in clients)
                {
                    client.FirstName = client.FirstName;
                    Clients.Add(client);
                    ClientsOriginal.Add(client);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
                IsRefreshing= false;
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
                    var clients = await _dataService.Search("*", skip, take);
                    foreach (var client in clients)
                    {
                        client.FirstName = client.FirstName;
                        Clients.Add(client);
                        ClientsOriginal.Add(client);
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

        public async void SearchClient()
        {
            IsBusy = true;
            if (!init)
            {

            }
            else
            {
                IEnumerable<ClientModel> clients;
                Clients.Clear();
                if (!string.IsNullOrEmpty(SearchValue))
                {
                    clients = await _dataService.Search(SearchValue);
                }
                else
                {
                    clients = await _dataService.GetListAsync();
                }

                foreach (var client in clients)
                {
                    client.FirstName = client.FirstName;
                    Clients.Add(client);
                    ClientsOriginal.Add(client);
                }
            }
            IsBusy = false;
        }

        public async void SearchClient(string searchValue)
        {
            IsBusy = true;
            if (!init)
            {

            }
            else
            {
                IEnumerable<ClientModel> clients;
                Clients.Clear();
                if (!string.IsNullOrEmpty(searchValue))
                {
                    clients = await _dataService.Search(searchValue);
                }
                else
                {
                    clients = await _dataService.GetListAsync();
                }

                foreach (var client in clients)
                {
                    client.FirstName = client.FirstName;
                    Clients.Add(client);
                    ClientsOriginal.Add(client);
                }
            }
            IsBusy = false;
        }

        async void OnAddClient(object obj)
        {
            await Navigation.NavigateToAsync<NewClientViewModel>(null);
        }

        async void OnClientSelected(ClientModel client)
        {
            if (client == null)
                return;
            await Navigation.NavigateToAsync<ClientDetailViewModel>(client.ClientId);
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}