using Microsoft.UpdateServices.Administration;
using Microsoft.UpdateServices.Internal.BaseApi;
using MG.UpdateManagement.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace MG.UpdateManagement.Objects
{
    public sealed class UMUpdateCollection : UpdateCollection, IUMCollection, IEnumerable<IUpdate>, IEquatable<UMUpdateCollection>
    {

        #region Constructors
        public UMUpdateCollection()
            : base()
        {
        }

        
        object IUMCollection.this[int i]
        {
            get => (IUMObject)base[i];
            set => this[i] = (IUpdate)value;
        }
        public object this[string key]
        {
            get
            {
                var col = this as IEnumerable<IUMObject>;
                return col.Single(x => x.ObjectName == key);
            }
        }

        //public bool Equals(IUMCollection other) =>
        //    Equals((UMUpdateCollection)other);

        public bool Equals(UMUpdateCollection other)
        {
            var ceq = new UMUpdateColEquality();
            return ceq.Equals(this, other) ? true : false;
        }

        #endregion

        public IUMObject Cast(object o)
        {
            var u = (IUpdate)o;
            var umu = new UMUpdate(u);
            return umu;
        }


        #region IEnumerable<IUpdate>
        IEnumerator<IUpdate> IEnumerable<IUpdate>.GetEnumerator()
        {
            var list = new IUpdate[Count];
            for (int i = 0; i < Count; i++)
            {
                list[i] = this[i];
            }
            return list.ToList() as IEnumerator<IUpdate>;
        }

        IEnumerator<IUMObject> IEnumerable<IUMObject>.GetEnumerator()
        {
            var list = new List<IUMObject>();
            for (int i = 0; i < Count; i++)
            {
                IUMObject o = this[i].GetType() == typeof(UMUpdate) ? (IUMObject)this[i] : Cast(this[i]);
                list.Add(o);
            }
            return list.GetEnumerator();
        }

        #endregion
    }
}
