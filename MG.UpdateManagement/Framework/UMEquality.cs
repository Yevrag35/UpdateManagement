using MG.UpdateManagement.Objects;
using System;
using System.Collections.Generic;

namespace MG.UpdateManagement.Framework
{
    internal class UMEquality : EqualityComparer<IUMObject>
    {
        public override bool Equals(IUMObject x, IUMObject y) => 
            x.ObjectId == y.ObjectId ? 
                true : false;

        public override int GetHashCode(IUMObject obj) => 
            throw new NotImplementedException();
    }
}
