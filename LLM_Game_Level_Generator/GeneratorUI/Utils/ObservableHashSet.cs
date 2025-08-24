namespace GeneratorUI.Utils
{
    using System.Collections.Specialized;

    public class ObservableHashSet<T> : HashSet<T>, ISet<T>, INotifyCollectionChanged
    {
        private readonly HashSet<T> internalSet = new HashSet<T>();

        public event NotifyCollectionChangedEventHandler? CollectionChanged;

        // Implement ISet<T> methods (Add, Remove, etc.) and raise CollectionChanged
        public new bool Add(T item)
        {
            if (this.internalSet.Add(item))
            {
                this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item));
                return true;
            }

            return false;
        }

        public new bool Remove(T item)
        {
            if (this.internalSet.Remove(item))
            {
                this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, item));
                return true;
            }

            return false;
        }

        protected virtual void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            CollectionChanged?.Invoke(this, e);
        }
    }
}
