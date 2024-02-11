using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace Better.Locators.Runtime.Installers
{
    [Serializable]
    public class MonoLocatorInstaller : MonoLocatorInstaller<MonoLocator, MonoBehaviour>
    {
        [SerializeField] private MonoBehaviour[] _items;

        protected override MonoBehaviour[] Items => _items;
    }

    [Serializable]
    public abstract class MonoLocatorInstaller<TLocator, TDerived> : Installer where TDerived : class where TLocator : MonoLocator<TDerived>
    {
        [SerializeField] private TLocator _locator;
        
        protected abstract TDerived[] Items { get; }
        
        public override Task InstallBindingsAsync(CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                Debug.LogError($"[{nameof(MonoLocatorInstaller<TLocator, TDerived>)}] {nameof(InstallBindingsAsync)}: operation was cancelled");
                return Task.CompletedTask;
            }

            for (int i = 0; i < Items.Length; i++)
            {
                _locator.Register(Items[i]);
            }

            return Task.CompletedTask;
        }

        public override void UninstallBindings()
        {
            for (int i = 0; i < Items.Length; i++)
            {
                _locator.Unregister(Items[i]);
            }
        }
    }
}