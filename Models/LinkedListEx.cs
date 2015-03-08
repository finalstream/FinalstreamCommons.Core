using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalstreamCommons.Models
{
    public class LinkedListEx<T> : LinkedList<T>
    {

        public void Reset(IEnumerable<T> items)
        {
            Clear();
            foreach (var item in items)
            {
                AddLast(item);
            }
        }

    }
}
