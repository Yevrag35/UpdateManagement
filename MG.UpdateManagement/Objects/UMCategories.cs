using Microsoft.UpdateServices.Administration;
using Microsoft.UpdateServices.Internal.BaseApi;
using MG.UpdateManagement.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace MG.UpdateManagement.Objects
{
    public class UMCategories : IList<IUpdateCategory>, IUMCollection
    {
        private static readonly Guid win7 = Guid.Parse("bfe5b177-a086-47a0-b102-097e4fa1f807");
        private static readonly Guid win81 = Guid.Parse("6407468e-edc7-4ecd-8c32-521f64cee65e");
        private static readonly Guid win10 = Guid.Parse("a3c2375d-0c8a-42f9-bce0-28333e198407");
        private static readonly Guid s2k8r2 = Guid.Parse("fdfe8200-9d98-44ba-a12a-772282bf60ef");
        private static readonly Guid s2k12 = Guid.Parse("a105a108-7c9b-4518-bbbe-73f0fe30012b");
        private static readonly Guid s2k12r2 = Guid.Parse("d31bd4c3-d872-41c9-a2e7-231f372588cb");
        private static readonly Guid s2k16 = Guid.Parse("569e8e8f-c6cd-42c8-92a3-efbb20a0f6f5");
        private static readonly Guid o10 = Guid.Parse("84f5f325-30d7-41c4-81d1-87a0e6535b66");
        private static readonly Guid o13 = Guid.Parse("704a0a4a-518f-4d69-9e03-10ba44198bd5");
        private static readonly Guid o16 = Guid.Parse("25aed893-7c2d-4a31-ae22-28ff8ac150ed");

        private static readonly Guid[] onlyThese = new Guid[10]
        {
            win7, win81, win10, s2k8r2, s2k12, s2k12r2, s2k16,
            o10, o13, o16
        };

        private List<IUpdateCategory> _list;

        public static implicit operator UMCategories(UpdateCategoryCollection catCol)
        {
            var col = new UMCategories();
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
    }
}
