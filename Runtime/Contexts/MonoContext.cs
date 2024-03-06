using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Better.Attributes.Runtime.Select;
using Better.Extensions.Runtime;
using Better.Locators.Runtime.Installers;
using UnityEngine;

namespace Better.Locators.Runtime.Contexts
{
    public class MonoContext : MonoBehaviour
    {
        [Select]
        [SerializeReference] private Installer[] _installers;

        private CancellationTokenSource _tokenSource;

        private async void Awake()
        {
            _tokenSource = new CancellationTokenSource();
            await EnterAsync(_tokenSource.Token);
        }

        private async Task EnterAsync(CancellationToken cancellationToken)
        {
            await _installers.Select(x => x.InstallBindingsAsync(cancellationToken)).WhenAll();
        }

        private void Exit()
        {
            for (int i = 0; i < _installers.Length; i++)
            {
                _installers[i].UninstallBindings();
            }
        }

        private void OnDestroy()
        {
            _tokenSource?.Cancel();
            Exit();
        }
    }
}