#if BETTER_SERVICES
using System.Threading;
using System.Threading.Tasks;
using Better.Services.Runtime.Interfaces;
using UnityEngine;

namespace Better.Locators.Runtime
{
    public static class ServiceLocator
    {
        private static readonly ILocator<IService> _internalLocator;

        static ServiceLocator()
        {
            _internalLocator = new InternalLocator<IService>();
        }

        public static void Register<T>(T service) where T : IService
        {
            _internalLocator.Register(service);

            if (service is { Initialized: false })
            {
                var type = service.GetType();
                var message = $"Service of type {type} not initialized";
                Debug.LogWarning(message);
            }
        }

        public static bool HasRegistered<T>() where T : IService
        {
            return _internalLocator.HasRegistered<T>();
        }

        public static void Unregister<T>(T service) where T : IService
        {
            _internalLocator.Unregister(service);
        }

        public static T Get<T>() where T : IService
        {
            return _internalLocator.Get<T>();
        }
        
        public static Task<T> GetAsync<T>(CancellationToken token = default) where T : IService
        {
            return _internalLocator.GetAsync<T>(token);
        }
    }
}
#endif