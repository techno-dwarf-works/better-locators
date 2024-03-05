using System;
using System.Collections.Generic;
using UnityEngine;

namespace Better.Locators.Runtime
{
    public class Locator<TItem>
    {
        private readonly Dictionary<Type, TItem> _services = new();

        public virtual void Register<T>(T item) where T : TItem
        {
            var type = item.GetType();
            if (HasRegistered<T>())
            {
                var message = $"Service of type {type} is already registered. Operation cancelled";
                Debug.LogError(message);
                return;
            }

            _services[type] = item;
        }

        public virtual bool HasRegistered<T>() where T : TItem
        {
            var type = typeof(T);
            return _services.ContainsKey(type);
        }

        public virtual void Unregister<T>(T service) where T : TItem
        {
            var type = service.GetType();
            if (!HasRegistered<T>())
            {
                var message = $"Item of type {type} is not registered. Operation cancelled";
                Debug.LogError(message);
                return;
            }

            _services.Remove(type);
        }

        public virtual T Get<T>() where T : TItem
        {
            var type = typeof(T);

            if (_services.TryGetValue(type, out var item))
            {
                return (T)item;
            }

            var message = $"Item of type {type} is not registered.";
            throw new InvalidOperationException(message);
        }
    }

    public class Locator : Locator<object>
    {
    }
}