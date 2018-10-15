using Microsoft.UpdateServices.Administration;
using Microsoft.UpdateServices.Internal.BaseApi;
using MG.UpdateManagement.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace MG.UpdateManagement.Objects
{
    public class UMUpdateCollection : IList<UMUpdate>, IUMCollection
    {
        private readonly List<UMUpdate> _list;
        private readonly Type[] AcceptedTypes = new Type[2] 
        {
            typeof(UMUpdate),
            typeof(Update)
        };

        public int Count => _list.Count;

        public object SyncRoot => throw new NotImplementedException();

        public bool IsSynchronized => false;

        public bool IsReadOnly => false;

        public bool IsFixedSize => false;

        UMUpdate IList<UMUpdate>.this[int index]
        {
            get => _list[index];
            set => _list[index] = value;
        }
        public UMUpdate this[int index]
        {
            get => _list.ElementAt(index);
            set => _list[index] = value;
        }
        public int this[Guid id]
        {
            get
            {
                int result = -1;
                for (int i = 0; i < _list.Count; i++)
                {
                    var up = _list[i];
                    if (up.ObjectId.Equals(id))
                    {
                        result = i;
                        break;
                    }
                }
                return result;
            }
        }

        #region Special Remove
        internal void Delete(UMUpdate ump)
        {
            ump.ShowOriginal().UpdateServer.DeleteUpdate(ump.Id.UpdateId);
            Remove(ump);
        }
        internal void Delete(Guid updateId)
        {
            UMContext.Context.DeleteUpdate(updateId);
            RemoveAt(this[updateId]);
        }

        #endregion

        #region Constructors
        public UMUpdateCollection() => _list = new List<UMUpdate>();
        object IUMCollection.this[int i] => 
            _list.ElementAt(i);

        public object this[string key] => 
            _list.Single(x => x.ObjectName == key);

        #endregion

        public static implicit operator UMUpdateCollection(UpdateCollection upCol)
        {
            var col = new UMUpdateCollection();
            for (int i = 0; i < upCol.Count; i++)
            {
                col.Add((Update)upCol[i]);
            }
            return col;
        }

        #region IEnumerable<IUpdate>
        public IEnumerable Enumerate() => _list.ToArray();

        IEnumerator<UMUpdate> IEnumerable<UMUpdate>.GetEnumerator() => _list.GetEnumerator();

        #endregion

        #region IList<UMUpdate> Methods
        public IEnumerator GetEnumerator() => _list.GetEnumerator();
        public void RemoveAt(int index) => _list.RemoveAt(index);
        public int IndexOf(UMUpdate item) => _list.IndexOf(item);
        public void Insert(int index, UMUpdate item) => _list.Insert(index, item);
        public void Add(UMUpdate item) => _list.Add(item);
        public bool Contains(UMUpdate item) => _list.Contains(item);
        public void CopyTo(UMUpdate[] array, int arrayIndex) => _list.CopyTo(array, arrayIndex);
        public bool Remove(UMUpdate item) => _list.Remove(item);
        public void Clear() => _list.Clear();

        #endregion
    }
}
