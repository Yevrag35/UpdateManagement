using MG.Attributes;
using System;

namespace MG.UpdateManagement.Enumerations
{
    public enum Architectures : int
    {
        [MGName("x64")]
        x64 = 0,

        [MGName("x86")]
        x86 = 1,

        [MGName("ARM64")]
        ARM = 2,

        [MGName("32-Bit")]
        Bit32 = 3,

        [MGName("64-Bit")]
        Bit64 = 4,

        [MGName("Itanium")]
        Itanium = 5,
    }
}
