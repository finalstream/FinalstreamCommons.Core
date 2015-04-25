using System;
using System.Collections.Generic;

namespace FinalstreamCommons.Collections
{
    public class LinkedListEx<T> : LinkedList<T>
    {
        public LinkedListEx(IEnumerable<T> enumerable):base(enumerable)
        {
        }

        public LinkedListEx()
        {
        }

        public void Reset(IEnumerable<T> items)
        {
            Clear();
            foreach (var item in items)
            {
                AddLast(item);
            }
        }

        public LinkedListNode<T> this[int index]
        {
            get { return this[index]; }
            set { this[index] = value; }
        }
    }
}
