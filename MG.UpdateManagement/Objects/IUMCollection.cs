using System;
using System.Collections.Generic;

namespace MG.UpdateManagement.Objects
{
    public interface IUMCollection
    {
        int Count { get; }
        object this[int i] { get; }
        object this[string key] { get; }
    }
}
