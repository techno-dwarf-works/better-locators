#if BETTER_SERVICES
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Better.Commons.Runtime.Extensions;
using Better.Services.Runtime.Interfaces;
using UnityEngine;

namespace Better.Locators.Runtime.Installers
{
    [Serializable]
    public abstract class ServicesInstaller<TDerivedService> : Installer
        where TDerivedService : class, IService
    {
        [SerializeReference] private TDerivedService[] _services;

        public override async Task InstallBindingsAsync(CancellationToken cancellationToken)
        {
            await _services.Select(x => x.InitializeAsync(cancellationToken)).WhenAll();
            
            if (cancellationToken.IsCancellationRequested)
            {
                Debug.LogError("Operation was cancelled");
                return;
            }

            for (int i = 0; i < _services.Length; i++)
            {
                ServiceLocator.Register(_services[i]);
            }

            await _services.Select(x => x.PostInitializeAsync(cancellationToken)).WhenAll();
        }

        public override void UninstallBindings()
        {
            for (int i = 0; i < _services.Length; i++)
            {
                ServiceLocator.Unregister<IService>(_services[i]);
            }
        }
    }
}
#endif