using System.Threading;
using System.Threading.Tasks;
using Better.Services.Runtime;

namespace Samples.TestSamples
{
    public class TestService : MonoService
    {
        protected override Task OnInitializeAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        protected override Task OnPostInitializeAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}