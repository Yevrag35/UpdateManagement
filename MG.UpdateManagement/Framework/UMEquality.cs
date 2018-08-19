using MG.UpdateManagement.Objects;
using System;
using System.Collections.Generic;

namespace MG.UpdateManagement.Framework
{
    internal class UMEquality : EqualityComparer<IUMObject>
    {
        public override bool Equals(IUMObject x, IUMObject y) => x.ObjectId == y.ObjectId ? true : false;
        public override int GetHashCode(IUMObject obj) => throw new NotImplementedException();
    }

    internal abstract class UMCollectionEquality : IEqualityComparer<IUMCollection>
    {
        public abstract bool Equals(IUMCollection x, IUMCollection y);
        public int GetHashCode(IUMCollection col) => throw new NotImplementedException();
    }

    internal class UMUpdateColEquality : UMCollectionEquality, IEqualityComparer<UMUpdateCollection>
    {
        public bool Equals(UMUpdateCollection x, UMUpdateCollection y)
        {
            bool result = true;
            if (x.Count != y.Count)
            {
                result = false;
            }
            else
            {
                for (int i = 0; i < x.Count; i++)
                {
                    //var z = x[i];
                    IUMObject w = x.Cast(x[i]);
                    IUMObject v = y.Cast(y[i]);
                    if (!w.Equals(v))
                    {
                        return false;
                    }
                }
            }
            return result;
        }

        public override bool Equals(IUMCollection x, IUMCollection y) =>
            Equals((UMUpdateCollection)x, (UMUpdateCollection)y);
        public int GetHashCode(UMUpdateCollection obj) => throw new NotImplementedException();
    }
}
