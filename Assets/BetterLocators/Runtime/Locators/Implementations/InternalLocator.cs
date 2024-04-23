using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Better.Commons.Runtime.Utility;
using Better.Locators.Runtime.Awaiters;

namespace Better.Locators.Runtime
{
    internal class InternalLocator<TItem> : ILocator<TItem>
    {
        private readonly Dictionary<Type, TItem> _typeItemsMap;

        public InternalLocator()
        {
            _typeItemsMap = new();
        }

        public void Register<T>(T item) where T : TItem
        {
            if (item == null)
            {
                DebugUtility.LogException<ArgumentNullException>(nameof(item));
                return;
            }

            var type = item.GetType();
            if (HasRegistered(type))
            {
                var message = $"{nameof(item)} of type {type} is already registered. Operation cancelled";
                DebugUtility.LogException<InvalidOperationException>(message);
                return;
            }

            _typeItemsMap[type] = item;
        }

        public bool HasRegistered<T>() where T : TItem
        {
            var type = typeof(T);
            return HasRegistered(type);
        }

        private bool HasRegistered(Type type)
        {
            return _typeItemsMap.ContainsKey(type);
        }

        public void Unregister<T>(T item) where T : TItem
        {
            if (item == null)
            {
                DebugUtility.LogException<ArgumentNullException>(nameof(item));
                return;
            }

            var type = item.GetType();
            if (!HasRegistered(type))
            {
                var message = $"{nameof(item)} of type {type} is not registered. Operation cancelled";
                DebugUtility.LogException<InvalidOperationException>(message);
                return;
            }

            _typeItemsMap.Remove(type);
        }

        public T Get<T>() where T : TItem
        {
            var type = typeof(T);
            if (_typeItemsMap.TryGetValue(type, out var item))
            {
                return (T)item;
            }

            var message = $"Element type {type} is not registered";
            DebugUtility.LogException<InvalidOperationException>(message);
            return default;
        }

        public Task<T> GetAsync<T>(CancellationToken token) where T : TItem
        {
            return new LocatorGetAwaiter<TItem, T>(this, token).Task;
        }
    }
}