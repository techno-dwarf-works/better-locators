namespace Better.Locators.Runtime
{
    public interface ILocator<in TItem>
    {
        void Register<T>(T item) where T : TItem;
        bool HasRegistered<T>() where T : TItem;
        void Unregister<T>(T item) where T : TItem;
        T Get<T>() where T : TItem;
    }
}