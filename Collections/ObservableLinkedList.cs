using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace FinalstreamCommons.Collections
{
    /// <summary>
    ///     This class is a LinkedList that can be used in a WPF MVVM scenario. Composition was used instead of inheritance,
    ///     because inheriting from LinkedList does not allow overriding its methods.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ObservableLinkedList<T> : INotifyCollectionChanged, IList<T>
    {
        private readonly LinkedListEx<T> _linkedList;

        public int IndexOf(T item)
        {
            var count = 0;
            for (var node = _linkedList.First; node != null; node = node.Next, count++)
            {
                if (item.Equals(node.Value))
                    return count;
            }
            return -1;
        }

        public void Insert(int index, T item)
        {
            _linkedList.AddAfter(_linkedList[index], item);
        }

        public void RemoveAt(int index)
        {
            _linkedList.Remove(_linkedList[index]);
        }

        public T this[int index]
        {
            get { return _linkedList[index].Value; }
            set { _linkedList[index] = new LinkedListNode<T>(value); }
        }

        public void Reset(IEnumerable<T> enumerable)
        {
            _linkedList.Reset(enumerable);
            OnNotifyCollectionChanged(NotifyCollectionChangedAction.Add);
        }

        #region Variables accessors

        public int Count
        {
            get { return _linkedList.Count; }
        }

        public bool IsReadOnly { get; private set; }

        public LinkedListNode<T> First
        {
            get { return _linkedList.First; }
        }

        public LinkedListNode<T> Last
        {
            get { return _linkedList.Last; }
        }

        #endregion

        #region Constructors

        public ObservableLinkedList()
        {
            _linkedList = new LinkedListEx<T>();
        }

        public ObservableLinkedList(IEnumerable<T> collection)
        {
            _linkedList = new LinkedListEx<T>(collection);
        }

        #endregion

        #region LinkedList<T> Composition

        public LinkedListNode<T> AddAfter(LinkedListNode<T> prevNode, T value)
        {
            var ret = _linkedList.AddAfter(prevNode, value);
            OnNotifyCollectionChanged(NotifyCollectionChangedAction.Add);
            return ret;
        }

        public void AddAfter(LinkedListNode<T> node, LinkedListNode<T> newNode)
        {
            _linkedList.AddAfter(node, newNode);
            OnNotifyCollectionChanged(NotifyCollectionChangedAction.Add);
        }

        public LinkedListNode<T> AddBefore(LinkedListNode<T> node, T value)
        {
            var ret = _linkedList.AddBefore(node, value);
            OnNotifyCollectionChanged(NotifyCollectionChangedAction.Add);
            return ret;
        }

        public void AddBefore(LinkedListNode<T> node, LinkedListNode<T> newNode)
        {
            _linkedList.AddBefore(node, newNode);
            OnNotifyCollectionChanged(NotifyCollectionChangedAction.Add);
        }

        public LinkedListNode<T> AddFirst(T value)
        {
            var ret = _linkedList.AddFirst(value);
            OnNotifyCollectionChanged(NotifyCollectionChangedAction.Add);
            return ret;
        }

        public void AddFirst(LinkedListNode<T> node)
        {
            _linkedList.AddFirst(node);
            OnNotifyCollectionChanged(NotifyCollectionChangedAction.Add);
        }

        public LinkedListNode<T> AddLast(T value)
        {
            var ret = _linkedList.AddLast(value);
            OnNotifyCollectionChanged(NotifyCollectionChangedAction.Add);
            return ret;
        }

        public void AddLast(LinkedListNode<T> node)
        {
            _linkedList.AddLast(node);
            OnNotifyCollectionChanged(NotifyCollectionChangedAction.Add);
        }

        public void Add(T item)
        {
            AddLast(new LinkedListNode<T>(item));
        }

        public void Clear()
        {
            _linkedList.Clear();
            OnNotifyCollectionChanged(NotifyCollectionChangedAction.Reset);
        }

        public bool Contains(T value)
        {
            return _linkedList.Contains(value);
        }

        public void CopyTo(T[] array, int index)
        {
            _linkedList.CopyTo(array, index);
        }

        public bool LinkedListEquals(object obj)
        {
            return _linkedList.Equals(obj);
        }

        public LinkedListNode<T> Find(T value)
        {
            return _linkedList.Find(value);
        }

        public LinkedListNode<T> FindLast(T value)
        {
            return _linkedList.FindLast(value);
        }

        public Type GetLinkedListType()
        {
            return _linkedList.GetType();
        }

        public bool Remove(T value)
        {
            var ret = _linkedList.Remove(value);
            OnNotifyCollectionChanged(NotifyCollectionChangedAction.Remove);
            return ret;
        }

        public void Remove(LinkedListNode<T> node)
        {
            _linkedList.Remove(node);
            OnNotifyCollectionChanged(NotifyCollectionChangedAction.Remove);
        }

        public void RemoveFirst()
        {
            _linkedList.RemoveFirst();
            OnNotifyCollectionChanged(NotifyCollectionChangedAction.Remove);
        }

        public void RemoveLast()
        {
            _linkedList.RemoveLast();
            OnNotifyCollectionChanged(NotifyCollectionChangedAction.Remove);
        }

        #endregion

        #region INotifyCollectionChanged Members

        public event NotifyCollectionChangedEventHandler CollectionChanged;

        public void OnNotifyCollectionChanged(NotifyCollectionChangedAction action)
        {
            if (CollectionChanged != null)
            {
                CollectionChanged(this, new NotifyCollectionChangedEventArgs(action));
            }
        }

        #endregion

        #region IEnumerable Members

        public IEnumerator<T> GetEnumerator()
        {
            return _linkedList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _linkedList.GetEnumerator();
        }

        #endregion
    }
}