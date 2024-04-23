using System.Threading;
using System.Threading.Tasks;

namespace Better.Locators.Runtime
{
    public abstract class PocoLocator<TItem> : ILocator<TItem>
    {
        protected ILocator<TItem> _internalLocator;

        protected PocoLocator()
        {
            _internalLocator = new InternalLocator<TItem>();
        }

        #region ILocator<TItem>

        public virtual void Register<T>(T item) where T : TItem => _internalLocator.Register(item);
        public virtual bool HasRegistered<T>() where T : TItem => _internalLocator.HasRegistered<T>();
        public virtual void Unregister<T>(T item) where T : TItem => _internalLocator.Unregister(item);
        public virtual T Get<T>() where T : TItem => _internalLocator.Get<T>();
        public Task<T> GetAsync<T>(CancellationToken token) where T : TItem => _internalLocator.GetAsync<T>(token);

        #endregion
    }

    public class PocoLocator : PocoLocator<object>
    {
    }
}