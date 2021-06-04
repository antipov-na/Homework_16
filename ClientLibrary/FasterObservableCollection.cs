using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace MyLibrary
{
    public class FasterObservableCollection<T> : ObservableCollection<T>
    {
        bool notification = true;

        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            if (notification)
            {
                base.OnCollectionChanged(e);
            }
        }

        public void AddMany(IReadOnlyCollection<T> collection)
        {
            this.notification = false;
            foreach (var item in collection)
            {
                Add(item);
            }
            this.notification = true;
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }
    }
}
