using Microsoft.UpdateServices.Administration;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace MG.UpdateManagement.Objects
{
    public class UMTargetCollection : IList<IComputerTarget>, IUMCollection
    {
        private List<IComputerTarget> _list;

        public UMTargetCollection() => _list = new List<IComputerTarget>();

        public IComputerTarget this[int index]
        {
            get => _list[index];
            set => _list[index] = value;
        }
        public object this[string key] => _list.Find(x => x.FullDomainName == key);
        object IUMCollection.this[int i] => _list[i];

        public int Count => _list.Count;

        public bool IsReadOnly => false;

        public static implicit operator UMTargetCollection(ComputerTargetCollection cCol)
        {
            var col = new UMTargetCollection();
            for (int i = 0; i < cCol.Count; i++)
            {
                col.Add(cCol[i]);
            }
            return col;
        }

        public IEnumerable Enumerate() => _list.ToArray();

        public void Add(IComputerTarget item) => _list.Add(item);
        public void Clear() => _list.Clear();
        public bool Contains(IComputerTarget item) => _list.Contains(item);
        public void CopyTo(IComputerTarget[] array, int arrayIndex) => _list.CopyTo(array, arrayIndex);
        public IEnumerator<IComputerTarget> GetEnumerator() => _list.GetEnumerator();
        public int IndexOf(IComputerTarget item) => _list.IndexOf(item);
        public void Insert(int index, IComputerTarget item) => _list.Insert(index, item);
        public bool Remove(IComputerTarget item) => _list.Remove(item);
        public void RemoveAt(int index) => _list.RemoveAt(index);
        IEnumerator IEnumerable.GetEnumerator() => ((IList<IComputerTarget>)_list).GetEnumerator();
    }
}
