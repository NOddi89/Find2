using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using C5;
using SCG = System.Collections.Generic;

namespace Assets.Scripts
{
    class PriorityQueue<T> : IPriorityQueue<T>
    {
        public T this[IPriorityQueueHandle<T> handle]
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public EventTypeEnum ActiveEvents
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public bool AllowsDuplicates
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public IComparer<T> Comparer
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public int Count
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public Speed CountSpeed
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public bool DuplicatesByCounting
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public IEqualityComparer<T> EqualityComparer
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public bool IsEmpty
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public bool IsReadOnly
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public EventTypeEnum ListenableEvents
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public event CollectionChangedHandler<T> CollectionChanged;
        public event CollectionClearedHandler<T> CollectionCleared;
        public event ItemInsertedHandler<T> ItemInserted;
        public event ItemRemovedAtHandler<T> ItemRemovedAt;
        public event ItemsAddedHandler<T> ItemsAdded;
        public event ItemsRemovedHandler<T> ItemsRemoved;

        public bool Add(T item)
        {
            throw new NotImplementedException();
        }

        public bool Add(ref IPriorityQueueHandle<T> handle, T item)
        {
            throw new NotImplementedException();
        }

        public void AddAll(IEnumerable<T> items)
        {
            throw new NotImplementedException();
        }

        public bool All(Func<T, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public void Apply(Action<T> action)
        {
            throw new NotImplementedException();
        }

        public bool Check()
        {
            throw new NotImplementedException();
        }

        public T Choose()
        {
            throw new NotImplementedException();
        }

        public void CopyTo(T[] array, int index)
        {
            throw new NotImplementedException();
        }

        public T Delete(IPriorityQueueHandle<T> handle)
        {
            throw new NotImplementedException();
        }

        public T DeleteMax()
        {
            throw new NotImplementedException();
        }

        public T DeleteMax(out IPriorityQueueHandle<T> handle)
        {
            throw new NotImplementedException();
        }

        public T DeleteMin()
        {
            throw new NotImplementedException();
        }

        public T DeleteMin(out IPriorityQueueHandle<T> handle)
        {
            throw new NotImplementedException();
        }

        public bool Exists(Func<T, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> Filter(Func<T, bool> filter)
        {
            throw new NotImplementedException();
        }

        public bool Find(Func<T, bool> predicate, out T item)
        {
            throw new NotImplementedException();
        }

        public bool Find(IPriorityQueueHandle<T> handle, out T item)
        {
            throw new NotImplementedException();
        }

        public T FindMax()
        {
            throw new NotImplementedException();
        }

        public T FindMax(out IPriorityQueueHandle<T> handle)
        {
            throw new NotImplementedException();
        }

        public T FindMin()
        {
            throw new NotImplementedException();
        }

        public T FindMin(out IPriorityQueueHandle<T> handle)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<T> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public T Replace(IPriorityQueueHandle<T> handle, T item)
        {
            throw new NotImplementedException();
        }

        public bool Show(StringBuilder stringbuilder, ref int rest, IFormatProvider formatProvider)
        {
            throw new NotImplementedException();
        }

        public T[] ToArray()
        {
            throw new NotImplementedException();
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
