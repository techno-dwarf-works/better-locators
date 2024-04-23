using System.Threading;
using Better.Commons.Runtime.Helpers.CompletionAwaiters;
using ThreadingTask = System.Threading.Tasks.Task;

namespace Better.Locators.Runtime.Awaiters
{
    public class LocatorGetAwaiter<TItem, TValue> : CompletionAwaiter<ILocator<TItem>, TValue> where TValue : TItem
    {
        public LocatorGetAwaiter(ILocator<TItem> source, CancellationToken cancellationToken) : base(source, cancellationToken)
        {
            ProcessAsync(cancellationToken);
        }

        private async void ProcessAsync(CancellationToken cancellationToken)
        {
            while (!Source.HasRegistered<TValue>() && !cancellationToken.IsCancellationRequested)
            {
                await ThreadingTask.Yield();
            }

            if (cancellationToken.IsCancellationRequested) return;
            var service = Source.Get<TValue>();
            SetResult(service);
        }

        protected override void OnCompleted(TValue result)
        {

        }
    }
}