using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MG.UpdateManagement.Objects
{
    public abstract class UmCollection<T> : IEnumerable<T> where T : IUmObject
    {
        #region FIELDS/CONSTANTS
        protected private List<T> _list;

        #endregion

        #region PROPERTIES
        public virtual int Count => _list.Count;

        #endregion

        #region CONSTRUCTORS
        public UmCollection() => _list = new List<T>();
        public UmCollection(int capacity) => _list = new List<T>(capacity);
        public UmCollection(IEnumerable<T> items) => _list = new List<T>(items);

        #endregion

        #region METHODS
        public virtual void Add(T item) => _list.Add(item);
        public virtual bool Contains(object value)
        {
            bool result = false;
            if (value is T item)
            {
                result = _list.Contains(item);
            }
            return result;
        }
        public virtual void Remove(T item) => _list.Remove(item);

        #endregion

        #region IENUMERABLE
        public IEnumerator<T> GetEnumerator() => _list.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => _list.GetEnumerator();

        #endregion
    }
}