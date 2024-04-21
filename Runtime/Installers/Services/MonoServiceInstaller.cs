#if BETTER_SERVICES
using System;
using Better.Services.Runtime;
using UnityEngine;

namespace Better.Locators.Runtime.Installers
{
    [Serializable]
    public class MonoServicesInstaller : ServicesInstaller<MonoService>
    {
        [SerializeField] private MonoService[] _services;
        
        protected override MonoService[] Services => _services;
    }
}
#endif