using System;

namespace MG.UpdateManagement.Objects
{
    public interface IUMObject
    {
        Guid ObjectId { get; }
        string ObjectName { get; }
    }
}
