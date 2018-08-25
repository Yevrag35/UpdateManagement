using Microsoft.UpdateServices.Administration;
using Microsoft.UpdateServices.Internal.BaseApi;
using MG.UpdateManagement.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace MG.UpdateManagement.Objects
{
    public class UMClassifications : IList<IUpdateClassification>, IUMCollection
    {
        private static readonly Guid CriticalUpdates = Guid.Parse("e6cf1350-c01b-414d-a61f-263d14d133b4");
        private static readonly Guid DefinitionUpdates = Guid.Parse("e0789628-ce08-4437-be74-2495b842f43b");
        private static readonly Guid SecurityUpdates = Guid.Parse("0fa1201d-4330-4fa8-8ae9-b877473b6441");
        private static readonly Guid ServicePacks = Guid.Parse("68c5b0a3-d1a6-4553-ae49-01d3a7827828");
        private static readonly Guid UpdateRollups = Guid.Parse("28bc880e-0592-4cbf-8f95-c79b17911d5f");
        private static readonly Guid Updates = Guid.Parse("cd5ffd1e-e932-4e3a-bf74-18bf0b1bbd83");
        private static readonly Guid Upgrades = Guid.Parse("3689bdc8-b205-4af4-8d4a-a63924c5e9d5");

        private static readonly Guid[] onlyThese = new Guid[7]
        {
            CriticalUpdates, DefinitionUpdates, SecurityUpdates, ServicePacks,
            UpdateRollups, Updates, Upgrades
        };

        private List<IUpdateClassification> _list;

        public UMClassifications()
            : base() => _list = new List<IUpdateClassification>();

        public IUpdateClassification this[int index] { get => _list[index]; set => _list[index] = value; }

        public object this[string key] =>
            _list.Single(x => x.Title == key);

        object IUMCollection.this[int i] => _list[i];

        public int Count => _list.Count;

        public bool IsReadOnly => false;

        public static implicit operator UMClassifications(UpdateClassificationCollection classes)
        {
            var col = new UMClassifications();
            for (int i = 0; i < classes.Count; i++)
            {
                IUpdateClassification cl = classes[i];
                if (onlyThese.Contains(cl.Id))
                    col.Add(cl);
            }
            return col;
        }
        public static implicit operator UpdateClassificationCollection(UMClassifications umclasses)
        {
            var col = new UpdateClassificationCollection();
            for (int i = 0; i < umclasses.Count; i++)
            {
                IUpdateClassification cl = umclasses[i];
                col.Add(cl);
            }
            return col;
        }

        public void Add(IUpdateClassification item) => _list.Add(item);
        public void Clear() => _list.Clear();
        public bool Contains(IUpdateClassification item) => _list.Contains(item);
        public void CopyTo(IUpdateClassification[] array, int arrayIndex) => _list.CopyTo(array, arrayIndex);
        public IEnumerable Enumerate() => _list.ToArray();
        public IEnumerator<IUpdateClassification> GetEnumerator() => _list.GetEnumerator();
        public int IndexOf(IUpdateClassification item) => _list.IndexOf(item);
        public void Insert(int index, IUpdateClassification item) => _list.Insert(index, item);
        public bool Remove(IUpdateClassification item) => _list.Remove(item);
        public void RemoveAt(int index) => _list.RemoveAt(index);
        IEnumerator IEnumerable.GetEnumerator() => _list.GetEnumerator();
    }
}
