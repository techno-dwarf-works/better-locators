#if BETTER_SERVICES
using System;
using Better.Services.Runtime;

namespace Better.Locators.Runtime.Installers
{
    [Serializable]
    public class PocoServicesInstaller : ServicesInstaller<PocoService>
    {
    }
}
#endif