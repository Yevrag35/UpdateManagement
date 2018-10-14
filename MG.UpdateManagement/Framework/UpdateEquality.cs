using MG.UpdateManagement.Objects;
using Microsoft.UpdateServices.Administration;
using Microsoft.UpdateServices.Internal.BaseApi;
using System;
using System.Collections.Generic;

namespace MG.UpdateManagement.Framework
{
    public class UpdateEquality : EqualityComparer<IUpdate>
    {
        public override bool Equals(IUpdate x, IUpdate y) => 
            x.Id.UpdateId == y.Id.UpdateId && x.Id.RevisionNumber == y.Id.RevisionNumber ? 
                true : false;

        public override int GetHashCode(IUpdate obj) =>
            throw new NotImplementedException();
    }
}
