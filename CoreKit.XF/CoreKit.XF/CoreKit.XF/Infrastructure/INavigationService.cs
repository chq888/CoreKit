using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CoreKit.XF.Infrastructure
{

    /// <summary>
    /// An interface defining how navigation between pages should be performed in various frameworks such as Windows, Windows Phone, Android, iOS etc.
    /// </summary>
    public interface INavigationService
    {

        ////
        //// Summary:
        ////     The key corresponding to the currently displayed page.
        //string CurrentPageKey { get; }

        void RegisterPage(string pageKey, Func<Page> creator);
        void UnregisterPage(string pageKey);

        //
        // Summary:
        //     Instructs the navigation service to display a new page corresponding to the given
        //     key, and passes a parameter to the new page. Depending on the platforms, the
        //     navigation service might have to be Configure with a key/page list.
        //
        // Parameters:
        //   pageKey:
        //     The key corresponding to the page that should be displayed.
        //
        //   parameter: viewModel
        //     The parameter that should be passed to the new page.
        Task NavigateAsync(string pageKey, object parameter = null);

        bool CanGoBack { get; }

        //
        // Summary:
        //     If possible, instructs the navigation service to discard the current page and
        //     display the previous page on the navigation stack.
        Task GoBackAsync();

        Task PushModalAsync(string pageKey, object parameter = null);
        Task PopModalAsync();


        //void NavigateTo(Type viewModelType, IPayload parameter = null, Action<Guid> callback = null, bool clearHistory = false);
        //void NavigateTo<T>(IPayload parameter = null, Action<Guid> callback = null, bool clearHistory = false) where T : IBaseViewModel;

        //void NavigateToSubView(Type viewModelType, IPayload parameter = null, Action<Guid> callback = null, bool clearHistory = false);
        //void NavigateToSubView<T>(IPayload parameter = null, Action<Guid> callback = null, bool clearHistory = false) where T : IBaseViewModel;

        //void NavigateBack(Action done = null, BackBehaviour behaviour = BackBehaviour.CloseLastSubView);
        //void NavigateBack(Action<Guid> callbackAction, Guid payloadId, Action done = null, BackBehaviour behaviour = BackBehaviour.CloseLastSubView);

        //Task NavigateBack<T>() where T : IBaseViewModel;
        //Task NavigateBack<T>(Action<Guid> callbackAction, Guid payloadId) where T : IBaseViewModel;

        //Task NavigateBack(Type viewModelInterfaceType);
        //Task NavigateBack(Type viewModelInterfaceType, Action<Guid> callbackAction, Guid payloadId);

    }


    ///// <summary>
    ///// An interface defining how navigation between pages should be performed in various frameworks such as Windows, Windows Phone, Android, iOS etc.
    ///// </summary>
    //public interface INavigationService
    //{

    //    //
    //    // Summary:
    //    //     The key corresponding to the currently displayed page.
    //    string CurrentPageKey { get; }

    //    void RegisterPage(string pageKey, Func<Page> creator);
    //    void UnregisterPage(string pageKey);

    //    //
    //    // Summary:
    //    //     Instructs the navigation service to display a new page corresponding to the given
    //    //     key, and passes a parameter to the new page. Depending on the platforms, the
    //    //     navigation service might have to be Configure with a key/page list.
    //    //
    //    // Parameters:
    //    //   pageKey:
    //    //     The key corresponding to the page that should be displayed.
    //    //
    //    //   parameter: viewModel
    //    //     The parameter that should be passed to the new page.
    //    Task NavigateAsync(string pageKey, object parameter = null);

    //    bool CanGoBack { get; }

    //    //
    //    // Summary:
    //    //     If possible, instructs the navigation service to discard the current page and
    //    //     display the previous page on the navigation stack.
    //    Task GoBackAsync();

    //    Task PushModalAsync(string pageKey, object parameter = null);
    //    Task PopModalAsync();


    //    //void NavigateTo(Type viewModelType, IPayload parameter = null, Action<Guid> callback = null, bool clearHistory = false);
    //    //void NavigateTo<T>(IPayload parameter = null, Action<Guid> callback = null, bool clearHistory = false) where T : IBaseViewModel;

    //    //void NavigateToSubView(Type viewModelType, IPayload parameter = null, Action<Guid> callback = null, bool clearHistory = false);
    //    //void NavigateToSubView<T>(IPayload parameter = null, Action<Guid> callback = null, bool clearHistory = false) where T : IBaseViewModel;

    //    //void NavigateBack(Action done = null, BackBehaviour behaviour = BackBehaviour.CloseLastSubView);
    //    //void NavigateBack(Action<Guid> callbackAction, Guid payloadId, Action done = null, BackBehaviour behaviour = BackBehaviour.CloseLastSubView);

    //    //Task NavigateBack<T>() where T : IBaseViewModel;
    //    //Task NavigateBack<T>(Action<Guid> callbackAction, Guid payloadId) where T : IBaseViewModel;

    //    //Task NavigateBack(Type viewModelInterfaceType);
    //    //Task NavigateBack(Type viewModelInterfaceType, Action<Guid> callbackAction, Guid payloadId);

    //}


    //public interface INavigationService
    //{
    //    Page CurrentPage { get; }
    //    Task InitializeAsync();
    //    Task NavigateToAsync(Type viewModelType);

    //    Task NavigateToAsync(Type viewModelType, object parameter);
    //    Task NavigateToModallyAsync(Type viewModelType);
    //    Task NavigateToModallyAsync(Type viewModelType, object parameter);
    //    Task NavigateBackModallyAsync();
    //    Task NavigateToPageModallyAsync(Page page);
    //    Task NavigateToTabAsync(Type viewModelType);
    //    Task NavigateToTabAsync(Type viewModelType, object parameter);

    //    Task ClearBackStack();
    //    Task RemoveLastFromBackStackAsync();

    //    Task PopToRootAsync();
    //}




    //public interface INavigationService
    //{
    //    void NavigateTo(Type type, string parameterName, string parameterValue, bool replaceView = false);

    //    void NavigateBack();
    //}

    //public interface INavigationService
    //{
    //    ViewModel PreviousPageViewModel { get; }
    //    Task InitializeAsync();
    //    Task NavigateTo<TViewModel>(bool animated = true) where TViewModel : ViewModel;
    //    Task NavigateTo<TViewModel>(object parameter, bool animated = true) where TViewModel : ViewModel;
    //    Task RemoveLastFromBackStack();
    //    Task RemoveBackStackAsync();
    //    Task GoBackAsync(bool animated = true);
    //}


    ////
    //// Summary:
    ////     
    ////     
    //public interface INavigationService
    //{
    //    //
    //    // Summary:
    //    //     The key corresponding to the currently displayed page.
    //    string CurrentPageKey { get; }

    //    //
    //    // Summary:
    //    //     If possible, instructs the navigation service to discard the current page and
    //    //     display the previous page on the navigation stack.
    //    void GoBack();
    //    //
    //    // Summary:
    //    //     Instructs the navigation service to display a new page corresponding to the given
    //    //     key. Depending on the platforms, the navigation service might have to be configured
    //    //     with a key/page list.
    //    //
    //    // Parameters:
    //    //   pageKey:
    //    //     The key corresponding to the page that should be displayed.
    //    void NavigateTo(string pageKey);
    //    //
    //    // Summary:
    //    //     Instructs the navigation service to display a new page corresponding to the given
    //    //     key, and passes a parameter to the new page. Depending on the platforms, the
    //    //     navigation service might have to be Configure with a key/page list.
    //    //
    //    // Parameters:
    //    //   pageKey:
    //    //     The key corresponding to the page that should be displayed.
    //    //
    //    //   parameter:
    //    //     The parameter that should be passed to the new page.
    //    void NavigateTo(string pageKey, object parameter);
    //}


    //public interface INavigationService
    //{
    //    void Initialize(NavigableElement navigationRootPage);

    //    Task NavigateToAsync(string navigationRoute, Dictionary<string, string> args = null, NavigationOptions options = null);

    //    Task GoBackAsync(bool fromModal = false);
    //}


    //public interface INavigationService
    //{
    //    ViewModelBase PreviousPageViewModel { get; }
    //    Task InitializeAsync();
    //    Task NavigateToAsync<TViewModel>() where TViewModel : ViewModelBase;
    //    Task NavigateToAsync<TViewModel>(object parameter) where TViewModel : ViewModelBase;
    //    Task RemoveLastFromBackStackAsync();
    //    Task RemoveBackStackAsync();
    //}




    //public class NavigationService : INavigationService
    //{

    //    public string CurrentPageKey
    //    {
    //    }

    //    public void GoBack()
    //    {
    //        Navigation.PopAsync();
    //    }

    //    public void NavigateTo(string pageKey)
    //    {
    //        NavigateTo(pageKey, null);
    //    }

    //    public void NavigateTo(string pageKey, object parameter)
    //    {

    //    }

    //    public static void Configure(string pageKey, Type pageType)
    //    {
    //    }

    //    public static void Initialize(INavigation navigation)
    //    {
    //        Navigation = navigation;
    //    }
    //}




    //public interface INavigationService
    //{
    //    Task InitializeAsync();

    //    Task NavigateToAsync<TViewModel>() where TViewModel : ViewModelBase;

    //    Task NavigateToAsync<TViewModel>(object parameter) where TViewModel : ViewModelBase;

    //    Task NavigateToAsync(Type viewModelType);

    //    Task NavigateToAsync(Type viewModelType, object parameter);

    //    Task NavigateBackAsync();

    //    Task RemoveLastFromBackStackAsync();

    //    Task NavigateToPopupAsync<TViewModel>(bool animate) where TViewModel : ViewModelBase;

    //    Task NavigateToPopupAsync<TViewModel>(object parameter, bool animate) where TViewModel : ViewModelBase;
    //}

}
