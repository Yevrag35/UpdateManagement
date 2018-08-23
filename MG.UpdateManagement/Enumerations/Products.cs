using MG.Attributes;
using System;

namespace MG.UpdateManagement.Enumerations
{
    public enum UMProducts : int
    {
        [MGName("Windows Server 2016")]
        [ID("569e8e8f-c6cd-42c8-92a3-efbb20a0f6f5")]
        [Base("Windows Server 2016")]
        //[AllowedPlatforms(new string[1] { "x64" })]
        [AllowedPlatforms(new Architectures[1] { Architectures.x64 })]
        [MutuallyExclusiveTo(new string[2]
        {
            "Windows Server 2016 (1709)",
            "Windows Server 2016 (1803)"
        })]
        [Category("Server")]
        Server2016 = 1
        ,
        [MGName("Windows Server 2008 R2")]
        [ID("fdfe8200-9d98-44ba-a12a-772282bf60ef")]
        [Base("Windows Server 2008 R2")]
        //[AllowedPlatforms(new string[2] { "x64", "Itanium" })]
        [AllowedPlatforms(new Architectures[2] { Architectures.x64, Architectures.Itanium }) ]
        [MutuallyExclusiveTo(new string[] { })]
        [Category("Server")]
        Server2008R2 = 2
        ,
        [MGName("Windows Server 2012")]
        [ID("a105a108-7c9b-4518-bbbe-73f0fe30012b")]
        [Base("Windows Server 2012")]
        //[AllowedPlatforms(new string[1] { "x64" })]
        [AllowedPlatforms(new Architectures[1] { Architectures.x64 })]
        [MutuallyExclusiveTo(new string[1] {
            "Windows Server 2012 R2"
        })]
        [Category("Server")]
        Server2012 = 3
        ,
        [MGName("Windows Server 2012 R2")]
        [ID("d31bd4c3-d872-41c9-a2e7-231f372588cb")]
        [Base("Windows Server 2012 R2")]
        //[AllowedPlatforms(new string[1] { "x64" })]
        [AllowedPlatforms(new Architectures[1] { Architectures.x64 })]
        [MutuallyExclusiveTo(new string[] { })]
        [Category("Server")]
        Server2012R2 = 4
        ,
        [MGName("Windows Server 2016 (1709)")]
        [ID("569e8e8f-c6cd-42c8-92a3-efbb20a0f6f5")]
        [Base("Windows Server 2016")]
        //[AllowedPlatforms(new string[1] { "x64" })]
        [AllowedPlatforms(new Architectures[1] { Architectures.x64 })]
        [MutuallyExclusiveTo(new string[] { })]
        [Category("Server")]
        Server1709 = 5
        ,
        [MGName("Windows Server 2016 (1803)")]
        [ID("569e8e8f-c6cd-42c8-92a3-efbb20a0f6f5")]
        [Base("Windows Server 2016")]
        //[AllowedPlatforms(new string[1] { "x64" })]
        [AllowedPlatforms(new Architectures[1] { Architectures.x64 })]
        [MutuallyExclusiveTo(new string[] { })]
        [Category("Server")]
        Server1803 = 6
        ,
        [MGName("Windows 7")]
        [ID("bfe5b177-a086-47a0-b102-097e4fa1f807")]
        [Base("Windows 7")]
        //[AllowedPlatforms(new string[2] { "x64", "x86" })]
        [AllowedPlatforms(new Architectures[2] { Architectures.x64, Architectures.x86 })]
        [MutuallyExclusiveTo(new string[] { })]
        [Category("Desktop")]
        Win7 = 7
        ,
        [MGName("Windows 8.1")]
        [ID("6407468e-edc7-4ecd-8c32-521f64cee65e")]
        [Base("Windows 8.1")]
        //[AllowedPlatforms(new string[2] { "x64", "x86" })]
        [AllowedPlatforms(new Architectures[2] { Architectures.x64, Architectures.x86 })]
        [MutuallyExclusiveTo(new string[] { })]
        [Category("Desktop")]
        Win81 = 8
        ,
        [MGName("Windows 10 Version 1607")]
        [ID("a3c2375d-0c8a-42f9-bce0-28333e198407")]
        [Base("Windows 10")]
        [AllowedPlatforms(new Architectures[3] { Architectures.x64, Architectures.x86, Architectures.ARM })]
        [MutuallyExclusiveTo(new string[] { })]
        [Category("Desktop")]
        Win1607 = 9
        ,
        [MGName("Windows 10 Version 1703")]
        [ID("a3c2375d-0c8a-42f9-bce0-28333e198407")]
        [Base("Windows 10")]
        [AllowedPlatforms(new Architectures[3] { Architectures.x64, Architectures.x86, Architectures.ARM })]
        [MutuallyExclusiveTo(new string[] { })]
        [Category("Desktop")]
        Win1703 = 10
        ,
        [MGName("Windows 10 Version 1709")]
        [ID("a3c2375d-0c8a-42f9-bce0-28333e198407")]
        [Base("Windows 10")]
        //[AllowedPlatforms(new string[3] { "x64", "x86", "ARM" })]
        [AllowedPlatforms(new Architectures[3] { Architectures.x64, Architectures.x86, Architectures.ARM })]
        [MutuallyExclusiveTo(new string[] { })]
        [Category("Desktop")]
        Win1709 = 11
        ,
        [MGName("Windows 10 Version 1803")]
        [ID("a3c2375d-0c8a-42f9-bce0-28333e198407")]
        [Base("Windows 10")]
        //[AllowedPlatforms(new string[3] { "x64", "x86", "ARM" })]
        [AllowedPlatforms(new Architectures[3] { Architectures.x64, Architectures.x86, Architectures.ARM })]
        [MutuallyExclusiveTo(new string[] { })]
        [Category("Desktop")]
        Win1803 = 12
        ,
        [MGName("Office 2010")]
        [ID("84f5f325-30d7-41c4-81d1-87a0e6535b66")]
        [Base("Office 2010")]
        //[AllowedPlatforms(new string[2] { "32-Bit", "64-Bit" })]
        [AllowedPlatforms(new Architectures[2] { Architectures.Bit32, Architectures.Bit64 })]
        [MutuallyExclusiveTo(new string[] { })]
        [Category("Office")]
        Office2010 = 13
        ,
        [MGName("Office 2013")]
        [ID("704a0a4a-518f-4d69-9e03-10ba44198bd5")]
        [Base("Office 2013")]
        //[AllowedPlatforms(new string[2] { "32-Bit", "64-Bit" })]
        [AllowedPlatforms(new Architectures[2] { Architectures.Bit32, Architectures.Bit64 })]
        [MutuallyExclusiveTo(new string[] { })]
        [Category("Office")]
        Office2013 = 14
        ,
        [MGName("Office 2016")]
        [ID("25aed893-7c2d-4a31-ae22-28ff8ac150ed")]
        [Base("Office 2016")]
        //[AllowedPlatforms(new string[2] { "32-Bit", "64-Bit" })]
        [AllowedPlatforms(new Architectures[2] { Architectures.Bit32, Architectures.Bit64 })]
        [MutuallyExclusiveTo(new string[] { })]
        [Category("Office")]
        Office2016 = 15
    }
}
