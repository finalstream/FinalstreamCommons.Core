using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace FinalstreamCommons.Collections
{
    public class ObservableCollectionEx<T> : ObservableCollection<T>
    {
        public void Reset(IEnumerable<T> enumerable)
        {
            Clear();
            foreach (var item in enumerable)
            {
                Add(item);
            }
        }
    }
}