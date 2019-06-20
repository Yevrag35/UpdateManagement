using MG.Attributes;
using System;

namespace MG.UpdateManagement.Enumerations
{
    public class AllowedPlatformsAttribute : MGAbstractAttribute
    {
        public AllowedPlatformsAttribute(Architectures[] arcs)
            : base(arcs)
        {
        }
    }

    public class IDAttribute : MGAbstractAttribute
    {
        public IDAttribute(string id)
            : base(id)
        {
        }
    }

    public class CategoryAttribute : MGAbstractAttribute
    {
        public CategoryAttribute(string cat)
            : base(cat)
        {
        }
    }

    public class BaseAttribute : MGAbstractAttribute
    {
        public BaseAttribute(string b)
            : base(b)
        {
        }
    }

    public class MutuallyExclusiveToAttribute : MGAbstractAttribute
    {
        public MutuallyExclusiveToAttribute(string[] releases)
            : base(releases)
        {
        }
    }
}
