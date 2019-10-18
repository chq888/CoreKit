using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CoreKit.XF.Infrastructure
{

    public enum AppPage
    {
        EditPage,
        DetailPage,
    }

    public partial class NavigationService
    {
        public async Task GotoPageAsync(AppPage page)
        {
            MasterDetailPage masterDetailPage = (MasterDetailPage)Application.Current.MainPage;

            switch (page)
            {
                case AppPage.EditPage:
                    await masterDetailPage.Detail.Navigation.PushAsync(new Page());
                    break;
                case AppPage.DetailPage:
                    if (Device.Idiom == TargetIdiom.Phone)
                        masterDetailPage.IsPresented = false;
                    break;
                default:
                    throw new ArgumentOutOfRangeException("page");
            }
        }

    }

    public partial class NavigationService : INavigationService
    {

        private static readonly Task TaskCompleted = Task.FromResult(0);
        readonly Dictionary<string, Func<Page>> registeredPages = new Dictionary<string, Func<Page>>();

        public void RegisterPage(string pageKey, Func<Page> creator)
        {
            if (string.IsNullOrEmpty(pageKey))
                throw new ArgumentNullException("pageKey");
            if (creator == null)
                throw new ArgumentNullException("creator");
            registeredPages.Add(pageKey, creator);
        }

        public void UnregisterPage(string pageKey)
        {
            if (string.IsNullOrEmpty(pageKey))
                throw new ArgumentNullException("pageKey");
            registeredPages.Remove(pageKey);
        }

        Page GetPageByKey(string pageKey)
        {
            if (string.IsNullOrEmpty(pageKey))
                throw new ArgumentNullException("key");

            Func<Page> creator;
            return registeredPages.TryGetValue(pageKey, out creator) ? creator.Invoke() : null;
        }

        private INavigation navigation;
        public INavigation Navigation
        {
            get
            {
                if (navigation == null)
                {
                    // Most of the time this is good.
                    var main = Application.Current.MainPage;
                    if (main is NavigationPage)
                        navigation = main.Navigation;

                    //TODO will remove
                    // Special case for Master/Detail page.
                    var masterDetailPage = main as MasterDetailPage;
                    if (masterDetailPage != null)
                    {
                        if (masterDetailPage.Master is NavigationPage)
                            navigation = masterDetailPage.Master.Navigation;
                        if (masterDetailPage.Detail is NavigationPage)
                            navigation = masterDetailPage.Detail.Navigation;
                    }
                    else
                    {
                        throw new Exception("Failed to locate navigation");
                    }
                }

                return navigation;
            }
        }

        public Task NavigateAsync(string pageKey, object viewModel = null)
        {
            if (Device.Idiom == TargetIdiom.Phone)
            {
                var mdPage = Application.Current.MainPage as MasterDetailPage;
                if (mdPage != null)
                {
                    mdPage.IsPresented = false;
                }
            }

            var page = GetPageByKey(pageKey);
            if (page == null)
                return TaskCompleted;

            if (viewModel != null)
                page.BindingContext = viewModel;

            return Navigation.PushAsync(page);
        }

        public bool CanGoBack
        {
            get
            {
                return Navigation.NavigationStack.Count > 1;
            }
        }

        public Task GoBackAsync()
        {
            return !CanGoBack ? TaskCompleted : Navigation.PopAsync();
        }

        public Task PushModalAsync(string pageKey, object viewModel = null)
        {
            var page = GetPageByKey(pageKey);
            if (page == null)
                throw new ArgumentException("Cannot navigate to unregistered page", "pageKey");

            if (viewModel != null)
                page.BindingContext = viewModel;

            return Navigation.PushModalAsync(page);
        }

        public Task PopModalAsync()
        {
            return Navigation.PopModalAsync();
        }

    }

}


//public interface INavigationService
//{
//    Page CurrentPage { get; }
//    Task InitializeAsync();
//    Task NavigateToAsync(Type viewModelType);
//    Task ClearBackStack();
//    Task NavigateToAsync(Type viewModelType, object parameter);
//    Task NavigateToModallyAsync(Type viewModelType);
//    Task NavigateToModallyAsync(Type viewModelType, object parameter);
//    Task NavigateBackModallyAsync();
//    Task NavigateToPageModallyAsync(Page page);
//    Task NavigateToTabAsync(Type viewModelType);
//    Task NavigateToTabAsync(Type viewModelType, object parameter);
//    Task RemoveLastFromBackStackAsync();
//    Task PopToRootAsync();
//}
//public class NavigationService : INavigationService
//{
//    private readonly Dictionary<Type, Type> _mappings = new Dictionary<Type, Type>();
//    private readonly Dictionary<Type, string> _mappingsShell = new Dictionary<Type, string>();
//    private ISettingsService _settingsService;
//    public static Application CurrentApplication => Application.Current;
//    public static Shell CurrentShell => Shell.Current;
//    public Page CurrentPage { get; set; }

//    public NavigationService(ISettingsService settingsService)
//    {
//        _settingsService = settingsService;

//        RegisterRoutes();
//        CreatePageViewModelMappings();
//    }

//    public async Task InitializeAsync()
//    {
//        string isRegistered = Xamarin.Essentials.SecureStorage.GetAsync(SettingsKeysConstants.TeacherIsRegistered).Result ?? string.Empty;
//        _settingsService.TeacherIsLoggedIn = bool.FalseString;

//        if (!isRegistered.Equals(bool.TrueString) || !_settingsService.TeacherIsLoggedIn.Equals(bool.TrueString))
//        {
//            await NavigateToTabAsync(typeof(AccountTabbedViewModel));
//        }
//    }

//    public async Task NavigateToTabAsync(Type viewModelType)
//    {
//        await InternalNavigateToTabAsync(viewModelType, null);
//    }

//    public async Task NavigateToTabAsync(Type viewModelType, object parameter)
//    {
//        await InternalNavigateToTabAsync(viewModelType, parameter);
//    }

//    protected async Task InternalNavigateToTabAsync(Type viewModelType, object parameter)
//    {
//        string navigationRoute = GetPageRouteForViewModel(viewModelType);

//        await CurrentShell.GoToAsync(navigationRoute, true);
//    }

//    public Task NavigateToAsync(Type viewModelType)
//    {
//        return InternalNavigateToAsync(viewModelType, null);
//    }

//    public Task NavigateToAsync(Type viewModelType, object parameter)
//    {
//        return InternalNavigateToAsync(viewModelType, parameter);
//    }

//    public Task NavigateToModallyAsync(Type viewModelType)
//    {
//        return InternalNavigateToModallyAsync(viewModelType, null);
//    }

//    public Task NavigateToModallyAsync(Type viewModelType, object parameter)
//    {
//        return InternalNavigateToModallyAsync(viewModelType, parameter);
//    }

//    public async Task ClearBackStack()
//    {
//        await CurrentShell.Navigation.PopToRootAsync();
//    }

//    public async Task NavigateBackAsync()
//    {
//        await CurrentShell.Navigation.PopModalAsync();
//    }

//    public virtual Task RemoveLastFromBackStackAsync()
//    {
//        return Task.FromResult(true);
//    }

//    public Task PopToRootAsync()
//    {
//        throw new NotImplementedException();
//    }

//    protected virtual async Task InternalNavigateToModallyAsync(Type viewModelType, object parameter)
//    {
//        CurrentPage = CreateAndBindPage(viewModelType);

//        await CurrentShell.Navigation.PushModalAsync(new NavigationPage(CurrentPage));

//        if (CurrentPage.BindingContext is ViewModelBase viewModel) await viewModel.InitializeAsync(parameter);
//    }

//    protected virtual async Task InternalNavigateToAsync(Type viewModelType, object parameter)
//    {
//        CurrentPage = CreateAndBindPage(viewModelType);

//        await CurrentShell.Navigation.PushAsync(CurrentPage);

//        if (CurrentPage.BindingContext is ViewModelBase viewModel) await viewModel.InitializeAsync(parameter);
//    }

//    protected Type GetPageTypeForViewModel(Type viewModelType)
//    {
//        if (!_mappings.ContainsKey(viewModelType))
//        {
//            throw new KeyNotFoundException($"No map for ${viewModelType} was found on navigation mappings");
//        }

//        return _mappings[viewModelType];
//    }

//    protected string GetPageRouteForViewModel(Type viewModelType)
//    {
//        if (!_mappingsShell.ContainsKey(viewModelType))
//        {
//            throw new KeyNotFoundException($"No route for ${viewModelType} was found on navigation mappings");
//        }

//        return _mappingsShell[viewModelType];
//    }

//    protected Page CreateAndBindPage(Type viewModelType)
//    {
//        Type pageType = GetPageTypeForViewModel(viewModelType);

//        if (pageType == null)
//        {
//            throw new Exception($"Mapping type for {viewModelType} is not a page");
//        }

//        Page page = Activator.CreateInstance(pageType) as Page;
//        ViewModelBase viewModel = AppContainer.Resolve(viewModelType) as ViewModelBase;
//        page.BindingContext = viewModel;

//        return page;
//    }

//    private void RegisterRoutes()
//    {
//        //            Routing.RegisterRoute(NavigationRoutesConstants.LoginPage, typeof(LoginPage));
//        //             Routing.RegisterRoute(NavigationRoutesConstants.AccountTabbedPage, typeof(AccountTabbedViewModel));
//        //            Routing.RegisterRoute(NavigationRoutesConstants.SchoolTripDetails, typeof(SchoolTripDetailsPage));
//        //            Routing.RegisterRoute(NavigationRoutesConstants.NewSchoolTripScan, typeof(NewSchoolTripScanPage));
//    }

//    private void CreatePageViewModelMappings()
//    {
//        _mappings.Add(typeof(NewSchoolTripScanViewModel), typeof(NewSchoolTripScanPage));
//        _mappings.Add(typeof(ScanToddlerDetailsViewModel), typeof(ScanToddlerDetailsPage));
//        _mappings.Add(typeof(SchoolTripDetailsViewModel), typeof(SchoolTripDetailsPage));
//        //            _mappingsShell.Add(typeof(TeacherViewModel), NavigationRoutesConstants.Teacher);
//        _mappingsShell.Add(typeof(AccountTabbedViewModel), NavigationRoutesConstants.AccountTabbedPage);
//        _mappingsShell.Add(typeof(SchoolTripsViewModel), NavigationRoutesConstants.SchoolTripsPage);
//        //            _mappingsShell.Add(typeof(LoginViewModel), NavigationRoutesConstants.LoginPage);
//        //            _mappings.Add(typeof(SchoolTripsViewModel), typeof(SchoolTripsPage));
//    }
//}








//public interface INavigationService
//{
//    void NavigateTo(Type type, string parameterName, string parameterValue, bool replaceView = false);

//    void NavigateBack();
//}
//public class NavigationService : INavigationService
//{
//    protected readonly Dictionary<Type, Type> MappingPageAndViewModel;

//    protected Application CurrentApplication
//    {
//        get { return Application.Current; }
//    }

//    public NavigationService()
//    {
//        MappingPageAndViewModel = new Dictionary<Type, Type>();

//        SetPageViewModelMappings();
//    }

//    public async void NavigateBack()
//    {
//        await CurrentApplication.MainPage.Navigation.PopAsync(true);
//    }

//    public async void NavigateTo(Type type, string parameterName, string parameterValue, bool replaceView = false)
//    {
//        if (type == typeof(CategoryPageViewModel) && string.IsNullOrEmpty(parameterValue))
//        {
//            CurrentApplication.MainPage = new AppShell();
//            return;
//        }
//        if (!replaceView)
//        {
//            await CurrentApplication.MainPage.Navigation.PushAsync(GetPageWithBindingContext(type, parameterName, parameterValue), true);
//        }
//        else
//        {
//            CurrentApplication.MainPage = new NavigationPage(GetPageWithBindingContext(type, parameterName, parameterValue));
//        }
//    }

//    private void SetPageViewModelMappings()
//    {
//        MappingPageAndViewModel.Add(typeof(LoginPageViewModel), typeof(SimpleLoginPage));
//        MappingPageAndViewModel.Add(typeof(SignUpPageViewModel), typeof(SimpleSignUpPage));
//        MappingPageAndViewModel.Add(typeof(ForgotPasswordViewModel), typeof(SimpleForgotPasswordPage));
//        MappingPageAndViewModel.Add(typeof(ResetPasswordViewModel), typeof(SimpleResetPasswordPage));

//        MappingPageAndViewModel.Add(typeof(OnBoardingGradientViewModel), typeof(OnBoardingGradientPage));
//        MappingPageAndViewModel.Add(typeof(CategoryPageViewModel), typeof(CategoryTilePage));
//        MappingPageAndViewModel.Add(typeof(CatalogPageViewModel), typeof(CatalogListPage));
//        MappingPageAndViewModel.Add(typeof(DetailPageViewModel), typeof(DetailPage));
//        MappingPageAndViewModel.Add(typeof(CartPageViewModel), typeof(CartPage));
//        MappingPageAndViewModel.Add(typeof(CheckoutPageViewModel), typeof(CheckoutPage));

//        MappingPageAndViewModel.Add(typeof(AboutUsViewModel), typeof(AboutUsSimplePage));
//    }

//    public Page GetPageWithBindingContext(Type type, string parameterName, string parameterValue)
//    {
//        Type pageType = GetPageForViewModel(type);

//        if (pageType == null)
//        {
//            throw new Exception($"Mapping type for {type} is not a page");
//        }

//        Page page = Activator.CreateInstance(pageType) as Page;

//        if (string.IsNullOrEmpty(parameterName))
//        {
//            page.BindingContext = TypeLocator.Instance.Resolve(type) as ViewModelBase;
//        }
//        else
//        {
//            page.BindingContext = TypeLocator.Instance.Resolve(type, new Autofac.NamedParameter(parameterName, parameterValue)) as ViewModelBase;
//        }

//        return page;
//    }

//    private Type GetPageForViewModel(Type viewModelType)
//    {
//        if (!MappingPageAndViewModel.ContainsKey(viewModelType))
//        {
//            throw new KeyNotFoundException($"No map for ${viewModelType} was found on navigation mappings");
//        }

//        return MappingPageAndViewModel[viewModelType];
//    }
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
//public class NavigationService : INavigationService
//{
//    public ViewModel PreviousPageViewModel
//    {
//        get
//        {
//            var navigationStack = Shell.Current.Navigation.NavigationStack;
//            if (navigationStack.Count <= 1)
//                throw new InvalidOperationException("There is no previous page in navigation stack.");
//            var previousPage = navigationStack[navigationStack.Count - 2];
//            return previousPage.BindingContext as ViewModel;
//        }
//    }

//    public Task InitializeAsync() => Task.CompletedTask;

//    public Task NavigateTo<TViewModel>(bool animated = true) where TViewModel : ViewModel =>
//        InternalNavigateToAsync(typeof(TViewModel), null, animated);

//    public Task NavigateTo<TViewModel>(object parameter, bool animated = true) where TViewModel : ViewModel =>
//        InternalNavigateToAsync(typeof(TViewModel), parameter, animated);

//    public Task RemoveLastFromBackStack()
//    {
//        var lastPage = Shell.Current.Navigation.NavigationStack.Last();
//        Shell.Current.Navigation.RemovePage(lastPage);
//        return Task.CompletedTask;
//    }

//    public Task RemoveBackStackAsync()
//    {
//        for (int i = 0; i < Shell.Current.Navigation.NavigationStack.Count - 1; i++)
//        {
//            var page = Shell.Current.Navigation.NavigationStack[i];
//            Shell.Current.Navigation.RemovePage(page);
//        }

//        return Task.FromResult(true);
//    }

//    public async Task GoBackAsync(bool animated = true) => await Shell.Current.Navigation.PopAsync(animated);

//    private async Task InternalNavigateToAsync(Type viewModelType, object parameter, bool animated)
//    {
//        var page = CreatePage(viewModelType);
//        await Shell.Current.Navigation.PushAsync(page, animated);
//        if (page.BindingContext is ViewModel viewModel)
//        {
//            await viewModel.Initialize(parameter);
//        }
//    }

//    private Page CreatePage(Type viewModelType)
//    {
//        var pageType = GetPageTypeFor(viewModelType);
//        if (pageType == null)
//            throw new NotSupportedException($"Cannot locate page type for {viewModelType}");
//        return Activator.CreateInstance(pageType) as Page;
//    }

//    private Type GetPageTypeFor(Type viewModelType)
//    {
//        var viewName = viewModelType.FullName?.Replace("Model", string.Empty);
//        var viewModelAssemblyName = viewModelType.GetTypeInfo().Assembly.FullName;
//        var viewAssemblyName = $"{viewName}, {viewModelAssemblyName}";
//        return Type.GetType(viewAssemblyName);
//    }
//}








////
//// Summary:
////     An interface defining how navigation between pages should be performed in various
////     frameworks such as Windows, Windows Phone, Android, iOS etc.
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
//public class NavigationService : INavigationService
//{
//    private static readonly Dictionary<string, Type> PagesByKey = new Dictionary<string, Type>();
//    private static INavigation Navigation;

//    public string CurrentPageKey
//    {
//        get
//        {
//            lock (PagesByKey)
//            {
//                if (!Navigation.NavigationStack.Any())
//                {
//                    return null;
//                }

//                var pageType = Navigation.NavigationStack.First().GetType();

//                return PagesByKey.ContainsValue(pageType)
//                    ? PagesByKey.First(p => p.Value == pageType).Key
//                    : null;
//            }
//        }
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
//        lock (PagesByKey)
//        {
//            if (PagesByKey.ContainsKey(pageKey))
//            {
//                var type = PagesByKey[pageKey];
//                ConstructorInfo constructor;
//                object[] parameters;

//                if (parameter == null)
//                {
//                    constructor = type.GetTypeInfo()
//                        .DeclaredConstructors
//                        .FirstOrDefault(c => !c.GetParameters().Any());

//                    parameters = new object[]
//                    {
//                    };
//                }
//                else
//                {
//                    constructor = type.GetTypeInfo()
//                        .DeclaredConstructors
//                        .FirstOrDefault(
//                            c =>
//                            {
//                                var p = c.GetParameters();
//                                return p.Count() == 1
//                                       && p[0].ParameterType == parameter.GetType();
//                            });

//                    parameters = new[]
//                    {
//                        parameter
//                    };
//                }

//                if (constructor == null)
//                {
//                    throw new InvalidOperationException(
//                        "No suitable constructor found for page " + pageKey);
//                }

//                var page = constructor.Invoke(parameters) as Page;
//                Navigation.PushAsync(page);
//            }
//            else
//            {
//                throw new ArgumentException(
//                    string.Format(
//                        "No such page: {0}. Did you forget to call NavigationService.Configure?",
//                        pageKey),
//                    "pageKey");
//            }
//        }
//    }

//    public static void Configure(string pageKey, Type pageType)
//    {
//        lock (PagesByKey)
//        {
//            if (PagesByKey.ContainsKey(pageKey))
//            {
//                PagesByKey[pageKey] = pageType;
//            }
//            else
//            {
//                PagesByKey.Add(pageKey, pageType);
//            }
//        }
//    }

//    public static void Initialize(INavigation navigation)
//    {
//        Navigation = navigation;
//    }
//}











//public interface INavigationService
//{
//    void Initialize(NavigableElement navigationRootPage);

//    Task NavigateToAsync(string navigationRoute, Dictionary<string, string> args = null, NavigationOptions options = null);

//    Task GoBackAsync(bool fromModal = false);
//}

//public class NavigationService : INavigationService
//{
//    private bool _initialized;

//    private NavigableElement _navigationRoot;

//    private AppShell _shell => App.Current.MainPage as AppShell;

//    private NavigableElement NavigationRoot
//    {
//        get => GetShellSection(_navigationRoot) ?? _navigationRoot;
//        set => _navigationRoot = value;
//    }

//    private void Shell_Navigated(object sender, ShellNavigatedEventArgs e)
//    {
//        Debug.WriteLine($"Navigated to {e.Current.Location} from {e.Previous?.Location} navigation type{e.Source.ToString()}");
//    }

//    private void Shell_Navigating(object sender, ShellNavigatingEventArgs e)
//    {
//        //TODO: Hook e.Cancel into viewmodel			
//    }

//    private async Task NavigateShellAsync(string navigationRoute, Dictionary<string, string> args, bool animated = true)
//    {
//        var queryString = args.AsQueryString();
//        navigationRoute = navigationRoute + queryString;
//        Debug.WriteLine($"Shell Navigating to {navigationRoute}");
//        await _shell.GoToAsync(navigationRoute, true);
//    }

//    public async Task GoBackAsync(bool fromModal = false)
//    {
//        if (!fromModal)
//        {
//            await NavigationRoot.Navigation.PopAsync();
//        }
//        else
//        {
//            await NavigationRoot.Navigation.PopModalAsync();
//        }
//    }

//    public async Task NavigateToAsync(string navigationRoute, Dictionary<string, string> args = null, NavigationOptions options = null)
//    {

//        IView view = App.IoC.Resolve<IView>(navigationRoute);
//        var page = view as Page;

//        options = options ?? NavigationOptions.Default();

//        if (page == null)
//        {
//            Debug.WriteLine($"Could not resolve view for {navigationRoute}: Assuming this is a shell route...");
//            await NavigateShellAsync(navigationRoute, args, options.Animated);
//            return;
//        }

//        if (page is MvvmContentPage mvvmPage)
//        {
//            mvvmPage.NavigationArgs = args;
//        }

//        if (options.CloseFlyout)
//        {
//            await _shell.CloseFlyoutAsync();
//        }

//        if (!options.Modal)
//        {
//            await NavigationRoot.Navigation.PushAsync(page, options.Animated).ConfigureAwait(false);
//        }
//        else
//        {
//            await NavigationRoot.Navigation.PushModalAsync(page, options.Animated).ConfigureAwait(false);
//        }
//    }

//    public void Initialize(NavigableElement navigationRootPage)
//    {
//        if (_initialized)
//        {
//            return;
//        }

//        _initialized = true;
//        NavigationRoot = navigationRootPage;
//        _shell.Navigating += Shell_Navigating;
//        _shell.Navigated += Shell_Navigated;
//    }

//    // Provides a navigatable section for elements which aren't explicitly defined within the Shell. For example,
//    // if it's accessed from the fly-out through a MenuItem but it doesn't belong to any section
//    internal static ShellSection GetShellSection(Element element)
//    {
//        if (element == null)
//        {
//            return null;
//        }

//        var parent = element;
//        var parentSection = parent as ShellSection;

//        while (parentSection == null && parent != null)
//        {
//            parent = parent.Parent;
//            parentSection = parent as ShellSection;
//        }

//        return parentSection;
//    }
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
//    public NavigationService()
//    {
//    }

//    public ViewModelBase PreviousPageViewModel => throw new NotImplementedException();

//    public Task InitializeAsync()
//    {
//        if (string.IsNullOrEmpty(Settings.AuthAccessToken))
//        {
//            return NavigateToAsync<LoginViewModel>();
//        }
//        return NavigateToAsync<MainViewModel>();
//    }

//    public Task NavigateToAsync<TViewModel>() where TViewModel : ViewModelBase
//    {
//        return InternalNavigateToAsync(typeof(TViewModel), null);
//    }

//    public Task NavigateToAsync<TViewModel>(object parameter) where TViewModel : ViewModelBase
//    {
//        return InternalNavigateToAsync(typeof(TViewModel), parameter);
//    }

//    public Task RemoveBackStackAsync() => throw new NotImplementedException();
//    public Task RemoveLastFromBackStackAsync() => throw new NotImplementedException();

//    private async Task InternalNavigateToAsync(Type viewModelType, object parameter)
//    {
//        Page page = CreatePage(viewModelType, parameter);

//        if (page is LoginView)
//        {
//            Application.Current.MainPage = new CustomNavigationView(page);
//        }
//        else
//        {
//            if (Application.Current.MainPage is CustomNavigationView navigationPage)
//            {
//                await navigationPage.PushAsync(page);
//            }
//            else
//            {
//                Application.Current.MainPage = new CustomNavigationView(page);
//            }
//        }

//        await (page.BindingContext as ViewModelBase).InitializeAsync(parameter);
//    }

//    private Type GetPageTypeForViewModel(Type viewModelType)
//    {
//        var viewName = viewModelType.FullName.Replace("Model", string.Empty);
//        var viewModelAssemblyName = viewModelType.GetTypeInfo().Assembly.FullName;
//        var viewAssemblyName = string.Format(CultureInfo.InvariantCulture, "{0}, {1}", viewName, viewModelAssemblyName);
//        var viewType = Type.GetType(viewAssemblyName);
//        return viewType;
//    }

//    private Page CreatePage(Type viewModelType, object parameter)
//    {
//        Type pageType = GetPageTypeForViewModel(viewModelType);
//        if (pageType == null)
//        {
//            throw new Exception($"Cannot locate page type for {viewModelType}");
//        }

//        var page = Activator.CreateInstance(pageType) as Page;
//        return page;
//    }
//}










//public class NavigationService : INavigationService
//{
//    private static readonly Dictionary<string, Type> PagesByKey = new Dictionary<string, Type>();
//    private static INavigation Navigation;

//    public string CurrentPageKey
//    {
//        get
//        {
//            lock (PagesByKey)
//            {
//                if (!Navigation.NavigationStack.Any())
//                {
//                    return null;
//                }

//                var pageType = Navigation.NavigationStack.First().GetType();

//                return PagesByKey.ContainsValue(pageType)
//                    ? PagesByKey.First(p => p.Value == pageType).Key
//                    : null;
//            }
//        }
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
//        lock (PagesByKey)
//        {
//            if (PagesByKey.ContainsKey(pageKey))
//            {
//                var type = PagesByKey[pageKey];
//                ConstructorInfo constructor;
//                object[] parameters;

//                if (parameter == null)
//                {
//                    constructor = type.GetTypeInfo()
//                        .DeclaredConstructors
//                        .FirstOrDefault(c => !c.GetParameters().Any());

//                    parameters = new object[]
//                    {
//                    };
//                }
//                else
//                {
//                    constructor = type.GetTypeInfo()
//                        .DeclaredConstructors
//                        .FirstOrDefault(
//                            c =>
//                            {
//                                var p = c.GetParameters();
//                                return p.Count() == 1
//                                       && p[0].ParameterType == parameter.GetType();
//                            });

//                    parameters = new[]
//                    {
//                        parameter
//                    };
//                }

//                if (constructor == null)
//                {
//                    throw new InvalidOperationException(
//                        "No suitable constructor found for page " + pageKey);
//                }

//                var page = constructor.Invoke(parameters) as Page;
//                Navigation.PushAsync(page);
//            }
//            else
//            {
//                throw new ArgumentException(
//                    string.Format(
//                        "No such page: {0}. Did you forget to call NavigationService.Configure?",
//                        pageKey),
//                    "pageKey");
//            }
//        }
//    }

//    public static void Configure(string pageKey, Type pageType)
//    {
//        lock (PagesByKey)
//        {
//            if (PagesByKey.ContainsKey(pageKey))
//            {
//                PagesByKey[pageKey] = pageType;
//            }
//            else
//            {
//                PagesByKey.Add(pageKey, pageType);
//            }
//        }
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

//public partial class NavigationService : INavigationService
//{
//    public Task NavigateToPopupAsync<TViewModel>(bool animate) where TViewModel : ViewModelBase => NavigateToPopupAsync<TViewModel>(null, animate);

//    public async Task NavigateToPopupAsync<TViewModel>(object parameter, bool animate) where TViewModel : ViewModelBase
//    {
//        var page = CreateAndBindPage(typeof(TViewModel), parameter);
//        await (page.BindingContext as ViewModelBase).InitializeAsync(parameter);

//        if (page is PopupPage)
//        {
//            await PopupNavigation.Instance.PushAsync(page as PopupPage, animate);
//        }
//        else
//        {
//            throw new ArgumentException($"The type ${typeof(TViewModel)} its not a PopupPage type");
//        }
//    }
//}
//public partial class NavigationService : INavigationService
//{
//    readonly IAuthenticationService authenticationService;
//    protected readonly Dictionary<Type, Type> mappings;

//    protected Application CurrentApplication => Application.Current;

//    public NavigationService(IAuthenticationService authenticationService)
//    {
//        this.authenticationService = authenticationService;
//        mappings = new Dictionary<Type, Type>();

//        CreatePageViewModelMappings();
//    }

//    public async Task InitializeAsync()
//    {
//        if (await authenticationService.UserIsAuthenticatedAndValidAsync())
//        {
//            await NavigateToAsync<MainViewModel>();
//        }
//        else
//        {
//            await NavigateToAsync<LoginViewModel>();
//        }
//    }

//    public Task NavigateToAsync<TViewModel>() where TViewModel : ViewModelBase => InternalNavigateToAsync(typeof(TViewModel), null);

//    public Task NavigateToAsync<TViewModel>(object parameter) where TViewModel : ViewModelBase => InternalNavigateToAsync(typeof(TViewModel), parameter);

//    public Task NavigateToAsync(Type viewModelType) => InternalNavigateToAsync(viewModelType, null);

//    public Task NavigateToAsync(Type viewModelType, object parameter) => InternalNavigateToAsync(viewModelType, parameter);

//    public async Task NavigateBackAsync()
//    {
//        if (CurrentApplication.MainPage is MainView)
//        {
//            var mainPage = CurrentApplication.MainPage as MainView;
//            await mainPage.Detail.Navigation.PopAsync();
//        }
//        else if (CurrentApplication.MainPage != null)
//        {
//            await CurrentApplication.MainPage.Navigation.PopAsync();
//        }
//    }

//    public virtual Task RemoveLastFromBackStackAsync()
//    {
//        if (CurrentApplication.MainPage is MainView mainPage)
//        {
//            mainPage.Detail.Navigation.RemovePage(
//                mainPage.Detail.Navigation.NavigationStack[mainPage.Detail.Navigation.NavigationStack.Count - 2]);
//        }

//        return Task.FromResult(true);
//    }

//    protected virtual async Task InternalNavigateToAsync(Type viewModelType, object parameter)
//    {
//        var page = CreateAndBindPage(viewModelType, parameter);

//        if (page is MainView)
//        {
//            CurrentApplication.MainPage = page;
//        }
//        else if (page is LoginView)
//        {
//            CurrentApplication.MainPage = new CustomNavigationPage(page);
//        }
//        else if (CurrentApplication.MainPage is MainView)
//        {
//            var mainPage = CurrentApplication.MainPage as MainView;

//            if (mainPage.Detail is CustomNavigationPage navigationPage)
//            {
//                var currentPage = navigationPage.CurrentPage;

//                if (currentPage.GetType() != page.GetType())
//                {
//                    await navigationPage.PushAsync(page);
//                }
//            }
//            else
//            {
//                navigationPage = new CustomNavigationPage(page);
//                mainPage.Detail = navigationPage;
//            }

//            mainPage.IsPresented = false;
//        }
//        else
//        {
//            if (CurrentApplication.MainPage is CustomNavigationPage navigationPage)
//            {
//                await navigationPage.PushAsync(page);
//            }
//            else
//            {
//                CurrentApplication.MainPage = new CustomNavigationPage(page);
//            }
//        }

//        await (page.BindingContext as ViewModelBase).InitializeAsync(parameter);
//    }

//    protected Type GetPageTypeForViewModel(Type viewModelType)
//    {
//        if (!mappings.ContainsKey(viewModelType))
//        {
//            throw new KeyNotFoundException($"No map for ${viewModelType} was found on navigation mappings");
//        }

//        return mappings[viewModelType];
//    }

//    protected Page CreateAndBindPage(Type viewModelType, object parameter)
//    {
//        var pageType = GetPageTypeForViewModel(viewModelType);

//        if (pageType == null)
//        {
//            throw new Exception($"Mapping type for {viewModelType} is not a page");
//        }

//        var page = Activator.CreateInstance(pageType) as Page;
//        var viewModel = Locator.Instance.Resolve(viewModelType) as ViewModelBase;
//        page.BindingContext = viewModel;

//        return page;
//    }

//    void CreatePageViewModelMappings()
//    {
//        mappings.Add(typeof(BookingCalendarViewModel), typeof(BookingCalendarView));
//        mappings.Add(typeof(BookingHotelViewModel), typeof(BookingHotelView));
//        mappings.Add(typeof(BookingHotelsViewModel), typeof(BookingHotelsView));
//        mappings.Add(typeof(BookingViewModel), typeof(BookingView));
//        mappings.Add(typeof(CheckoutViewModel), typeof(CheckoutView));
//        mappings.Add(typeof(LoginViewModel), typeof(LoginView));
//        mappings.Add(typeof(MainViewModel), typeof(MainView));
//        mappings.Add(typeof(MyRoomViewModel), typeof(MyRoomView));
//        mappings.Add(typeof(NotificationsViewModel), typeof(NotificationsView));
//        mappings.Add(typeof(OpenDoorViewModel), typeof(OpenDoorView));
//        mappings.Add(typeof(SettingsViewModel<RemoteSettings>), typeof(SettingsView));
//        mappings.Add(typeof(ExtendedSplashViewModel), typeof(ExtendedSplashView));

//        if (Device.Idiom == TargetIdiom.Desktop)
//        {
//            mappings.Add(typeof(HomeViewModel), typeof(UwpHomeView));
//            mappings.Add(typeof(SuggestionsViewModel), typeof(UwpSuggestionsView));
//        }
//        else
//        {
//            mappings.Add(typeof(HomeViewModel), typeof(HomeView));
//            mappings.Add(typeof(SuggestionsViewModel), typeof(SuggestionsView));
//        }
//    }
//}