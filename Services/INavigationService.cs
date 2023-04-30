using SPMSCAV1.ViewModels;

namespace SPMSCAV1.Services
{
    public interface INavigationService
    {
        Task NavigateToAsync<TViewModel>() where TViewModel : BaseViewModel;

        Task NavigateToAsync<TViewModel>(bool isAbsoluteRoute) where TViewModel : BaseViewModel;

        Task NavigateToAsync<TViewModel>(object parameter) where TViewModel : BaseViewModel;

        Task NavigateToAsyncMultiparm<TViewModel>(Dictionary<string, object> parameter) where TViewModel : BaseViewModel;

        Task GoBackAsync();
    }
}