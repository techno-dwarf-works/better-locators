using System;
using System.Threading;
using System.Threading.Tasks;

namespace Better.Locators.Runtime.Installers
{
    [Serializable]
    public abstract class Installer
    {
        public abstract Task InstallBindingsAsync(CancellationToken cancellationToken);
        public abstract void UninstallBindings();
    }
}