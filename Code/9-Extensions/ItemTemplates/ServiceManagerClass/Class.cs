using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace $rootnamespace$
{
	public static class $safeitemrootname$
    {
        private static IServiceProvider __serviceProvider;

        /// <summary>
        /// Configure default and custom services.
        /// </summary>
        /// <param name="customServices">custom service action, set null if not custom services defined.</param>
        public static void ConfigureServices(Action<IServiceCollection> customServices = null)
        {
            IServiceCollection services = new ServiceCollection();

            AddConfigServices(services);
            AddLoggerServices(services);

            if (customServices != null)
            {
                customServices(services);
            }

            __serviceProvider = services.BuildServiceProvider();
        }

        private static void AddConfigServices(IServiceCollection services)
        {
            IConfigurationRoot config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                .Build();
            services.AddSingleton<IConfigurationRoot>(config);
        }

        private static void AddLoggerServices(IServiceCollection services)
        {
            LoggerFactory loggerFactory = new LoggerFactory();
            string categoryName = Assembly.GetEntryAssembly().GetName().Name;
            ILogger logger = loggerFactory.AddConsole(true).CreateLogger(categoryName);
            services.AddSingleton<ILogger>(logger);
        }

        /// <summary>
        /// Creates a new Microsoft.Extensions.DependencyInjection.IServiceScope that can be used to resolve scoped services.
        /// </summary>
        /// <param name="provider">The System.IServiceProvider to create the scope from.</param>
        /// <returns>A Microsoft.Extensions.DependencyInjection.IServiceScope that can be used to resolve scoped services.</returns>
        public static IServiceScope CreateScope()
        {
            return __serviceProvider.CreateScope();
        }
        
        /// <summary>
        /// Get service of type serviceType from the System.IServiceProvider.
        /// </summary>
        /// <param name="provider">The System.IServiceProvider to retrieve the service object from.</param>
        /// <param name="serviceType">An object that specifies the type of service object to get.</param>
        /// <returns>A service object of type serviceType.</returns>
        public static object GetRequiredService(Type serviceType)
        {
            return __serviceProvider.GetRequiredService(serviceType);
        }

        /// <summary>
        /// Get service of type T from the System.IServiceProvider.
        /// </summary>
        /// <typeparam name="T">The type of service object to get.</typeparam>
        /// <param name="provider">The System.IServiceProvider to retrieve the service object from.</param>
        /// <returns>A service object of type T.</returns>
        public static T GetRequiredService<T>()
        {
            return __serviceProvider.GetRequiredService<T>();
        }

        /// <summary>
        /// Get service of type T from the System.IServiceProvider.
        /// </summary>
        /// <typeparam name="T">The type of service object to get.</typeparam>
        /// <param name="provider">The System.IServiceProvider to retrieve the service object from.</param>
        /// <returns>A service object of type T or null if there is no such service.</returns>
        public static T GetService<T>()
        {
            return __serviceProvider.GetService<T>();
        }

        /// <summary>
        /// Get an enumeration of services of type T from the System.IServiceProvider.
        /// </summary>
        /// <typeparam name="T">The type of service object to get.</typeparam>
        /// <param name="provider">The System.IServiceProvider to retrieve the services from.</param>
        /// <returns>An enumeration of services of type T.</returns>
        public static IEnumerable<T> GetServices<T>()
        {
            return __serviceProvider.GetServices<T>();
        }

        /// <summary>
        /// Get an enumeration of services of type serviceType from the System.IServiceProvider.
        /// </summary>
        /// <param name="provider">The System.IServiceProvider to retrieve the services from.</param>
        /// <param name="serviceType">An object that specifies the type of service object to get.</param>
        /// <returns>An enumeration of services of type serviceType.</returns>
        public static IEnumerable<object> GetServices(Type serviceType)
        {
            return __serviceProvider.GetServices(serviceType);
        }
    }
}
