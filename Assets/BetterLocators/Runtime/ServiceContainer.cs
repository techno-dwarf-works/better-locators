#if BETTER_SERVICES
using Better.Services.Runtime.Interfaces;

namespace Better.Locators.Runtime
{
    public sealed class ServiceContainer<T> where T : IService
    {
        private T _service;

        public T Service
        {
            get
            {
                if (_service == null)
                {
                    _service = ServiceLocator.GetService<T>();
                }

                return _service;
            }
        }
    }
}
#endif