using System.Threading;
using System.Threading.Tasks;
using Better.Services.Runtime.Interfaces;

namespace Better.Locators.Runtime
{
    public interface ILocator<TItem>
    {
        void Register<T>(T item) where T : TItem;
        bool HasRegistered<T>() where T : TItem;
        void Unregister<T>(T item) where T : TItem;
        T Get<T>() where T : TItem;
        Task<T> GetAsync<T>(CancellationToken token) where T : TItem;
    }
}