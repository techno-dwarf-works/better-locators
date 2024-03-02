#if BETTER_SERVICES
using Better.Services.Runtime.Interfaces;
using UnityEngine;

namespace Better.Locators.Runtime
{
    public static class ServiceLocator
    {
        private static readonly Locator<IService> _locator;

        static ServiceLocator()
        {
            _locator = new Locator<IService>();
        }

        public static void RegisterService<T>(T item) where T : IService
        {
            var type = item.GetType();
            if (!item.Initialized)
            {
                var message = $"Service of type {type} not {nameof(IService.Initialized)}";
                Debug.LogWarning(message);
            }

            _locator.Register(item);
        }

        public static void UnregisterService<T>(T item) where T : IService
        {
            _locator.Unregister(item);
        }

        public static T GetService<T>() where T : IService
        {
            return _locator.Get<T>();
        }
    }
}
#endif