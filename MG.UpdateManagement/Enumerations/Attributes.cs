using MG.Attributes;
using MG.Attributes.Interfaces;
using System;

namespace MG.UpdateManagement.Enumerations
{
    public class AllowedPlatformsAttribute : MGAbstractAttribute
    {
        public AllowedPlatformsAttribute(string[] arcs)
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

    public class FutureReleasesAttribute : MGAbstractAttribute
    {
        public FutureReleasesAttribute(string[] releases)
            : base(releases)
        {
        }
    }
}
