namespace GeneratorUI.Utils
{
    using GeneratorViewModel;

    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.Specialized;

    public class ObservableDictionary<ValueType> : IDictionary<string, ValueType>, INotifyCollectionChanged
        where ValueType : MapTile
    {
        private readonly Dictionary<string, ValueType> internalDict = new Dictionary<string, ValueType>();

        public event NotifyCollectionChangedEventHandler? CollectionChanged;

        public ValueType this[string key]
        {
            get => this.internalDict[key];
            set
            {
                if (this.internalDict.ContainsKey(key))
                {
                    var oldValue = this.internalDict[key];
                    this.internalDict[key] = value;
                    this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(
                        NotifyCollectionChangedAction.Replace,
                        new KeyValuePair<string, ValueType>(key, value),
                        new KeyValuePair<string, ValueType>(key, oldValue)));
                }
                else
                {
                    this.internalDict[key] = value;
                    this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(
                        NotifyCollectionChangedAction.Add,
                        new KeyValuePair<string, ValueType>(key, value)));
                }
            }
        }

        public ICollection<string> Keys => this.internalDict.Keys;
        public ICollection<ValueType> Values => this.internalDict.Values;
        public int Count => this.internalDict.Count;
        public bool IsReadOnly => false;

        public bool Add(ValueType item)
        {
            if (this.internalDict.TryAdd(item.TileCharacter, item))
            {
                this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(
                    NotifyCollectionChangedAction.Add,
                    new KeyValuePair<string, ValueType>(item.TileCharacter, item)));
                return true;
            }

            return false;
        }

        public void Add(string key, ValueType value)
        {
            if (this.internalDict.TryAdd(key, value))
            {
                this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(
                    NotifyCollectionChangedAction.Add,
                    new KeyValuePair<string, ValueType>(key, value)));
            }
        }

        public bool Remove(string key)
        {
            if (this.internalDict.TryGetValue(key, out var item) && this.internalDict.Remove(key))
            {
                this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(
                    NotifyCollectionChangedAction.Remove,
                    new KeyValuePair<string, ValueType>(key, item), -1));
                return true;
            }

            return false;
        }

        public bool ReplaceKey(string oldKey, string newKey)
        {
            if (string.IsNullOrEmpty(oldKey) || string.IsNullOrEmpty(newKey) || this.internalDict.ContainsKey(newKey))
                return false;

            if (this.internalDict.TryGetValue(oldKey, out var tempValue))
            {
                this.internalDict.Remove(oldKey);
                this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(
                    NotifyCollectionChangedAction.Remove,
                    new KeyValuePair<string, ValueType>(oldKey, tempValue)));

                tempValue.TileCharacter = newKey;
                this.internalDict.Add(newKey, tempValue);

                this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(
                    NotifyCollectionChangedAction.Add,
                    new KeyValuePair<string, ValueType>(newKey, tempValue)));

                return true;
            }

            return false;
        }

        public void UpdateKeys()
        {
            foreach (var key in new List<string>(this.internalDict.Keys))
            {
                if (this.internalDict.TryGetValue(key, out var value) && value.TileCharacter != key)
                {
                    this.ReplaceKey(key, value.TileCharacter);
                }
            }
        }

        public bool ContainsKey(string key) => this.internalDict.ContainsKey(key);
        public bool TryGetValue(string key, out ValueType value) => this.internalDict.TryGetValue(key, out value);

        public void Clear()
        {
            this.internalDict.Clear();
            this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }

        public IEnumerator<KeyValuePair<string, ValueType>> GetEnumerator() => this.internalDict.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => this.internalDict.GetEnumerator();

        public void Add(KeyValuePair<string, ValueType> item) => this.Add(item.Key, item.Value);
        public bool Contains(KeyValuePair<string, ValueType> item) => this.internalDict.Contains(item);
        public void CopyTo(KeyValuePair<string, ValueType>[] array, int arrayIndex) => ((ICollection<KeyValuePair<string, ValueType>>)this.internalDict).CopyTo(array, arrayIndex);
        public bool Remove(KeyValuePair<string, ValueType> item) => this.Remove(item.Key);

        protected virtual void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            CollectionChanged?.Invoke(this, e);
        }
    }
}
