using System;
using System.Collections.Generic;

namespace MG.UpdateManagement.Objects
{
    public interface IUMCollection : IEnumerable<IUMObject>
    {
        int Count { get; }
        object this[int i] { get; set; }
        object this[string key] { get; }

        IUMObject Cast(object o);
    }
}
