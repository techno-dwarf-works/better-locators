#if BETTER_SERVICES
using Better.Services.Runtime.Interfaces;

namespace Better.Locators.Runtime
{
    public sealed class ServiceProperty<T> where T : IService
    {
        private T _cachedService;

        public T CachedService
        {
            get
            {
                if (_cachedService == null)
                {
                    _cachedService = ServiceLocator.Get<T>();
                }

                return _cachedService;
            }
        }

        public bool IsRegistered => ServiceLocator.HasRegistered<T>();
    }
}
#endif