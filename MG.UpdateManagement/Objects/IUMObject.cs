using System;

namespace MG.UpdateManagement.Objects
{
    public interface IUMObject : IEquatable<IUMObject>
    {
        Guid ObjectId { get; }
        string ObjectName { get; }
    }
}
