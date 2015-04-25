using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalstreamCommons.Collections
{
    public class ObservableCollectionEx<T> : ObservableCollection<T>
    {
        public void Reset(IEnumerable<T> enumerable)
        {
            this.Clear();
            foreach (var item in enumerable)
            {
                Add(item);
            }
        }
    }
}
