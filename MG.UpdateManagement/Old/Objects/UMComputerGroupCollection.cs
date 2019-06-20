using MG.UpdateManagement.Framework;
using Microsoft.UpdateServices.Administration;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace MG.UpdateManagement.Objects
{
    public class UMComputerGroupCollection : IList<IComputerTargetGroup>, IUMCollection
    {
        private List<IComputerTargetGroup> _list;

        public UMComputerGroupCollection() => _list = new List<IComputerTargetGroup>();

        public IComputerTargetGroup this[int index]
        {
            get => _list[index];
            set => _list[index] = value;
        }

        public object this[string key] => _list.Single(x => x.Name == key);

        object IUMCollection.this[int i] => _list[i];

        public static implicit operator UMComputerGroupCollection(ComputerTargetGroupCollection cgCol)
        {
            var col = new UMComputerGroupCollection();
            for (int i = 0; i < cgCol.Count; i++)
            {
                col.Add(cgCol[i]);
            }
            return col;
        }

        public int Count => _list.Count;

        public bool IsReadOnly => false;
        
        public IEnumerable Enumerate() => _list.ToArray();

        public void Add(IComputerTargetGroup item) => _list.Add(item);
        public void Clear() => _list.Clear();
        public bool Contains(IComputerTargetGroup item) => _list.Contains(item);
        public void CopyTo(IComputerTargetGroup[] array, int arrayIndex) => 
            _list.CopyTo(array, arrayIndex);
        public IEnumerator<IComputerTargetGroup> GetEnumerator() => 
            _list.GetEnumerator();
        public int IndexOf(IComputerTargetGroup item) => 
            _list.IndexOf(item);
        public void Insert(int index, IComputerTargetGroup item) => 
            _list.Insert(index, item);
        public bool Remove(IComputerTargetGroup item) => 
            _list.Remove(item);
        public void RemoveAt(int index) => 
            _list.RemoveAt(index);
        IEnumerator IEnumerable.GetEnumerator() => 
            _list.GetEnumerator();

        #region Cool Methods
        public static void GetGroups() => UMContext.AllComputerGroups = UMContext.Context.GetComputerTargetGroups();

        #endregion
    }
}
