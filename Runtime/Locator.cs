﻿using System;
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

            if (_services.ContainsKey(type))
            {
                var message = $"[{nameof(Locator<TItem>)}] {nameof(Register)}: Service of type {type} is already registered. Operation cancelled";
                Debug.LogError(message);
            }

            _services[type] = item;
        }

        public virtual void Unregister<T>(T service) where T : TItem
        {
            var type = service.GetType();

            if (!_services.ContainsKey(type))
            {
                var message = $"[{nameof(Locator<TItem>)}] {nameof(Unregister)}: Item of type {type} is not registered. Operation cancelled";
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

            var message = $"[{nameof(Locator<TItem>)}] {nameof(Get)}: Item of type {type} is not registered.";
            throw new InvalidOperationException(message);
        }
    }

    public class Locator : Locator<object>
    {
    }
}