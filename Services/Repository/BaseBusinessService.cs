using SPMSCAV1.Services.Interface;
using System.Text;
using System.Text.Json;

namespace SPMSCAV1.Services.Repository
{
    public abstract class BaseBusinessService<T> : IBaseBusinessService<T> where T : class
    {
       
        protected readonly HttpClient _httpClient;
        protected readonly string _baseAddress;
        protected readonly string _url;
        protected string _route;
        protected readonly JsonSerializerOptions _jsonSerializerOptions;
        private bool disposedValue;

        public BaseBusinessService()
        {
            _httpClient = new HttpClient();
            //_baseAddress = DeviceInfo.Platform == DevicePlatform.Android ? "http://127.0.0.1:7157" : "https://localhost:7157";
            _baseAddress = DeviceInfo.Platform == DevicePlatform.Android ? "https://10.0.2.2:7157" : "https://localhost:7157";
            //_baseAddress =   "http://127.0.0.1:7157";
            _url = $"{_baseAddress}/api/";
            _jsonSerializerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
        }

        public async Task AddAndSaveAsync(T entity)
        {
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                //Debug.WriteLine("");
                return;
            }

            try
            {
                string json = JsonSerializer.Serialize<T>(entity, _jsonSerializerOptions);
                StringContent stringContent = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage httpResponseMessage = await _httpClient.PostAsync($"{_url + _route}", stringContent);

                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    string content = await httpResponseMessage.Content.ReadAsStringAsync();
                  
                }
                else
                {
                    //Debug.WriteLine("");
                }

            }
            catch (Exception)
            {

                throw;
            }

            return;
        }

        public async Task AttachAndSaveAsync(T entity, long id)
        {
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                //Debug.WriteLine("");
                return;
            }

            try
            {
                string json = JsonSerializer.Serialize<T>(entity, _jsonSerializerOptions);
                StringContent stringContent = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage httpResponseMessage = await _httpClient.PutAsync($"{_url + _route}/{id}", stringContent);

                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    string content = await httpResponseMessage.Content.ReadAsStringAsync();

                }
                else
                {
                    //Debug.WriteLine("");
                }

            }
            catch (Exception)
            {

                throw;
            }

            return;
        }

        public async Task DeleteAndSaveAsync(long id)
        {
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                //Debug.WriteLine("");
                return ;
            }

            try
            {
              

                HttpResponseMessage httpResponseMessage = await _httpClient.DeleteAsync($"{_url + _route}{id}");

                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    string content = await httpResponseMessage.Content.ReadAsStringAsync();

                }
                else
                {
                    //Debug.WriteLine("");
                }

            }
            catch (Exception)
            {

                throw;
            }
            return;
        }

        public async Task<T> GetByKeyAsync(long? id)
        {
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                //Debug.WriteLine("");
                return null;
            }

            try
            {
                HttpResponseMessage httpResponseMessage = await _httpClient.GetAsync($"{_url + _route}/{id}");

                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    string content = await httpResponseMessage.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<T>(content, _jsonSerializerOptions);
                }
                else
                {
                    //Debug.WriteLine("");
                }
            }
            catch (Exception)
            {
                //Debug.WriteLine("");
                throw;
            }
            return null;
        }

        public async Task<IEnumerable<T>> Search(string searchValue)
        {
            List<T> list = new List<T>();
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                //Debug.WriteLine("");
                return list;
            }

            try
            {
                HttpResponseMessage httpResponseMessage = await _httpClient.GetAsync($"{_url + _route}/search/{searchValue}");

                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    string content = await httpResponseMessage.Content.ReadAsStringAsync();
                    list = JsonSerializer.Deserialize<List<T>>(content, _jsonSerializerOptions);
                }
                else
                {
                    //Debug.WriteLine("");
                }
            }
            catch (Exception)
            {
                //Debug.WriteLine("");
                throw;
            }

            return list;
        }

        public async Task<List<T>> GetListAsync()
        {
            List<T> list = new List<T>();
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                //Debug.WriteLine("");
                return list;
            }

            try
            {
                HttpResponseMessage httpResponseMessage = await _httpClient.GetAsync($"{_url + _route}");

                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    string content = await httpResponseMessage.Content.ReadAsStringAsync();
                    list = JsonSerializer.Deserialize<List<T>>(content, _jsonSerializerOptions);
                }
                else
                {
                    //Debug.WriteLine("");
                }
            }
            catch (Exception)
            {
                //Debug.WriteLine("");
                throw;
            }

            return list;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~BaseBusinessService()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
