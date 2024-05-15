using System;
using System.Threading;
using System.Threading.Tasks;
using Better.Commons.Runtime.Utility;
using Better.Locators.Runtime.Awaiters;

namespace Better.Locators.Runtime
{
    public static class ILocatorExtension
    {
        public static bool TryGet<TBase, TItem>(this ILocator<TBase> self, out TItem item)
            where TItem : TBase
        {
            if (self == null)
            {
                DebugUtility.LogException<ArgumentNullException>(nameof(self));
                item = default;
                return false;
            }

            if (self.HasRegistered<TItem>())
            {
                item = self.Get<TItem>();
                return true;
            }

            item = default;
            return false;
        }

        public static Task<TItem> GetAsync<TBase, TItem>(this ILocator<TBase> self, CancellationToken token = default)
            where TItem : TBase
        {
            if (self == null)
            {
                DebugUtility.LogException<ArgumentNullException>(nameof(self));
                return default;
            }

            var awaiter = new LocatorGetAwaiter<TBase, TItem>(self, token);
            return awaiter.Task;
        }
    }
}