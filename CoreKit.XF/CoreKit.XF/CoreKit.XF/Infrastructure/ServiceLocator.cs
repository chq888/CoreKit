using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreKit.XF.Infrastructure
{

    /// <summary>
    /// Simple ServiceLocator implementation.
    /// </summary>
    public sealed class LazyServiceLocator
    {
        static readonly Lazy<LazyServiceLocator> instance = new Lazy<LazyServiceLocator>(() => new LazyServiceLocator());
        readonly Dictionary<Type, Lazy<object>> registeredServices = new Dictionary<Type, Lazy<object>>();

        /// <summary>
        /// Singleton instance for default service locator
        /// </summary>
        public static LazyServiceLocator Current => instance.Value;

        /// <summary>
        ///     Add a new contract + service implementation
        /// </summary>
        /// <typeparam name="TContract">Contract type</typeparam>
        /// <typeparam name="TService">Service type</typeparam>
        public void Register<TContract, TService>() where TService : new()
        {
            registeredServices[typeof(TContract)] =
                new Lazy<object>(() => Activator.CreateInstance(typeof(TService)));
        }

        /// <summary>
        ///     This resolves a service type and returns the implementation. Note that this
        ///     assumes the key used to register the object is of the appropriate type or
        ///     this method will throw an InvalidCastException!
        /// </summary>
        /// <typeparam name="T">Type to resolve</typeparam>
        /// <returns>Implementation</returns>
        public T Resolve<T>() where T : class
        {
            Lazy<object> service;
            if (registeredServices.TryGetValue(typeof(T), out service))
            {
                return (T)service.Value;
            }

            return null;
        }
    }


    // Define a service locator class in your common project
    public class ServiceLocator
    {
        // A dictionary to map common interfaces to native implementations
        private Dictionary<Type, object> _services;

        // A static instance of our locator (this guy is a singleton) 
        private static ServiceLocator _instance;
        public static ServiceLocator Current => _instance ?? (_instance = new ServiceLocator());

        // A private constructor to enforce the singleton
        private ServiceLocator()
        {
            _services = new Dictionary<Type, object>();
        }

        // A method for native projects to register their native implementations against the common interfaces
        public void Register(Type type, object implementation)
        {
            _services?.Add(type, implementation);
        }

        public void Register<T>(T service)
        {
            var key = typeof(T);
            if (_services.ContainsKey(key))
            {
                _services[key] = service;
            }
            else
            {
                _services.Add(key, service);
            }
        }

        // A method to get the implementation for a given interface
        public T Resolve<T>()
        {
            try
            {
                //var key = typeof(T);
                //if (_services.ContainsKey(key))
                //{
                //    return (T)_services[key];
                //}

                return (T)_services[typeof(T)];
            }
            catch
            {
                throw new ApplicationException($"Failed to resolve type: {typeof(T).FullName}");
            }
        }

    }


    public sealed class ServiceLocator<T> where T : class
    {
        static readonly Dictionary<Type, Lazy<T>> registeredServices = new Dictionary<Type, Lazy<T>>();
        static T current;

        //static readonly Lazy<ServiceLocator<T>> instance = new Lazy<ServiceLocator<T>>(() => new ServiceLocator<T>());
        ///// <summary>
        /////     Singleton instance for default service locator
        ///// </summary>
        //public static ServiceLocator<T> Instance => instance.Value;

        public static T Current
        {
            get { return current; }
            set
            {
                current = value;
                registeredServices[typeof(T)] = new Lazy<T>(() => current);
            }
        }


        /// <summary>
        ///     Add a new contract + service implementation
        /// </summary>
        /// <typeparam name="TContract">Contract type</typeparam>
        /// <typeparam name="TService">Service type</typeparam>
        public void Register()
        {
            registeredServices[typeof(T)] = new Lazy<T>(() => Activator.CreateInstance<T>());
        }

        /// <summary>
        ///     Add a new contract + service implementation
        /// </summary>
        /// <typeparam name="TContract">Contract type</typeparam>
        /// <typeparam name="TService">Service type</typeparam>
        public void Register(T val)
        {
            registeredServices[typeof(T)] = new Lazy<T>(() => val);
        }

        /// <summary>
        ///     This resolves a service type and returns the implementation. Note that this
        ///     assumes the key used to register the object is of the appropriate type or
        ///     this method will throw an InvalidCastException!
        /// </summary>
        /// <typeparam name="T">Type to resolve</typeparam>
        /// <returns>Implementation</returns>
        public T Resolve() // where T : class
        {
            Lazy<T> service;
            if (registeredServices.TryGetValue(typeof(T), out service))
            {
                return (T)service.Value;
            }

            return default(T);
        }
    }


    /// <summary>
    /// ProviderServiceLocator.SetLocatorProvider(services.BuildServiceProvider());
    /// </summary>
    public class ProviderServiceLocator
    {
        private ServiceProvider _currentServiceProvider;
        private static ServiceProvider _serviceProvider;

        public ProviderServiceLocator(ServiceProvider currentServiceProvider)
        {
            _currentServiceProvider = currentServiceProvider;
        }

        public static ProviderServiceLocator Current
        {
            get
            {
                return new ProviderServiceLocator(_serviceProvider);
            }
        }

        public static void SetLocatorProvider(ServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public object Resolve(Type serviceType)
        {
            return _currentServiceProvider.GetService(serviceType);
        }

        public TService Resolve<TService>()
        {
            return _currentServiceProvider.GetService<TService>();
        }
    }


    //private static Locator _locator;

    //public static Locator Locator
    //{
    //    get { return _locator = _locator ?? new Locator(); }
    //}

    //public class Locator
    //{
    //    private readonly IContainer _container;
    //    private ContainerBuilder _builder;

    //    public Locator()
    //    {
    //        _builder = new ContainerBuilder();

    //        _builder.RegisterType<ItemsViewModel>();

    //        _container = _builder.Build();
    //    }

    //    public ItemsViewModel ItemsViewModel
    //    {
    //        get { return _container.Resolve<ItemsViewModel>(); }
    //    }

    //}

}
