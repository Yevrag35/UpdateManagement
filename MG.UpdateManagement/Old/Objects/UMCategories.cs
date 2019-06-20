using Microsoft.UpdateServices.Administration;
using Microsoft.UpdateServices.Internal.BaseApi;
using MG.Attributes;
using MG.UpdateManagement.Enumerations;
using MG.UpdateManagement.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace MG.UpdateManagement.Objects
{
    public class UMCategories : AttributeResolver, IList<IUpdateCategory>, IUMCollection
    {
        private List<IUpdateCategory> _list;

        public static implicit operator UMCategories(UpdateCategoryCollection catCol)
        {
            var col = new UMCategories();
            Guid[] onlyThese = col.GetQueriableProducts();
            for (int i = 0; i < catCol.Count; i++)
            {
                var c = catCol[i];
                if (onlyThese.Contains(c.Id))
                    col.Add(c);
            }
            return col;
        }
        public static implicit operator UpdateCategoryCollection(UMCategories umcats)
        {
            var col = new UpdateCategoryCollection();
            for (int i = 0; i < umcats.Count; i++)
            {
                var c = umcats[i];
                col.Add(c);
            }
            return col;
        }

        public UMCategories()
            : base() => _list = new List<IUpdateCategory>();

        public IUpdateCategory this[int index] { get => _list[index]; set => _list[index] = value; }

        public object this[string key] => throw new NotImplementedException();

        object IUMCollection.this[int i] => throw new NotImplementedException();

        public int Count => _list.Count;

        public bool IsReadOnly => false;

        public void Add(IUpdateCategory item) => _list.Add(item);
        public void Clear() => _list.Clear();
        public bool Contains(IUpdateCategory item) => _list.Contains(item);
        public void CopyTo(IUpdateCategory[] array, int arrayIndex) => _list.CopyTo(array, arrayIndex);
        public IEnumerable Enumerate() => _list.ToArray();
        public IEnumerator<IUpdateCategory> GetEnumerator() => _list.GetEnumerator();
        public int IndexOf(IUpdateCategory item) => _list.IndexOf(item);
        public void Insert(int index, IUpdateCategory item) => _list.Insert(index, item);
        public bool Remove(IUpdateCategory item) => _list.Remove(item);
        public void RemoveAt(int index) => _list.RemoveAt(index);
        IEnumerator IEnumerable.GetEnumerator() => _list.GetEnumerator();

        #region Special Filtering Method
        public Guid[] GetQueriableProducts()
        {
            UMProducts[] prods = typeof(UMProducts).GetEnumValues().Cast<UMProducts>().ToArray();
            var guids = new Guid[prods.Length];
            for (int i = 0; i < prods.Length; i++)
            {
                var p = prods[i];
                var id = Guid.Parse(GetAttributeValue<string>(p, typeof(IDAttribute)));
                guids[i] = id;
            }
            return guids.Distinct().ToArray();
        }

        #endregion
    }
}
