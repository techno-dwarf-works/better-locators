using UnityEngine;

namespace Better.Locators.Runtime
{
    public abstract class MonoLocator<TItem> : MonoBehaviour
    {
        protected Locator<TItem> _locator;

        protected virtual void Awake()
        {
            _locator = new Locator<TItem>();
        }

        public virtual void Register<T>(T item) where T : TItem
        {
            _locator.Register(item);
        }

        public virtual void Unregister<T>(T item) where T : TItem
        {
            _locator.Unregister(item);
        }

        public virtual T Get<T>() where T : TItem
        {
            return _locator.Get<T>();
        }
    }
    
    public class MonoLocator : MonoLocator<MonoBehaviour>
    {
    }
}