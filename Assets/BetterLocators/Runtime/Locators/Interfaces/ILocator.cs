namespace Better.Locators.Runtime
{
    public interface ILocator<TItem>
    {
        public void Register<T>(T item) where T : TItem;
        public bool HasRegistered<T>() where T : TItem;
        public T Get<T>() where T : TItem;
        public void Unregister<T>(T item) where T : TItem;
    }
}