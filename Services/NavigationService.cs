using SPMSCAV1.ViewModels;
using SPMSCAV1.Views;
using System.Globalization;
using System.Reflection;
using System.Web;

namespace SPMSCAV1.Services
{
    public class NavigationService : INavigationService
    {

        public async Task NavigateToAsync<TViewModel>() where TViewModel : BaseViewModel
        {
            await InternalNavigateToAsync(typeof(TViewModel), null, false);
        }

        public async Task NavigateToAsync<TViewModel>(bool isAbsoluteRoute) where TViewModel : BaseViewModel
        {
            await InternalNavigateToAsync(typeof(TViewModel), null, isAbsoluteRoute);
        }

        public async Task NavigateToAsync<TViewModel>(object parameter) where TViewModel : BaseViewModel
        {
            await InternalNavigateToAsync(typeof(TViewModel), parameter, false);
        }

        public async Task NavigateToAsyncMultiparm<TViewModel>(Dictionary<string, object> parameter) where TViewModel : BaseViewModel
        {
            await InternalNavigateToAsyncMultiparm(typeof(TViewModel), parameter, false);
        }

        public async Task GoBackAsync()
        {
            await Shell.Current.GoToAsync("..");
        }

        async Task InternalNavigateToAsync(Type viewModelType, object parameter, bool isAbsoluteRoute = false)
        {
            var viewName = viewModelType.FullName.Replace("ViewModels", "Views").Replace("ViewModel", "Page");
            string absolutePrefix = isAbsoluteRoute ? "///" : String.Empty;
            if (parameter != null)
            {
                string finalPath = $"{absolutePrefix}{viewName}?id={HttpUtility.UrlEncode(parameter.ToString())}";
                await Shell.Current.GoToAsync(finalPath);
            }
            else
            {
                await Shell.Current.GoToAsync($"{absolutePrefix}{viewName}");
            }
        }

        async Task InternalNavigateToAsyncMultiparm(Type viewModelType, Dictionary<string, object> parameter, bool isAbsoluteRoute = false)
        {
            var viewName = viewModelType.FullName.Replace("ViewModels", "Views").Replace("ViewModel", "Page");
            string absolutePrefix = isAbsoluteRoute ? "///" : String.Empty;
            if (parameter != null)
            {
                string finalPath = $"{absolutePrefix}{viewName}";
                await Shell.Current.GoToAsync(finalPath, parameter);
            }
            else
            {
                await Shell.Current.GoToAsync($"{absolutePrefix}{viewName}");
            }
        }
    }
}