using Dynamic;
using MG;
using MG.UpdateManagement.Cmdlets;
using MG.UpdateManagement.Enumerations;
using MG.UpdateManagement.Exceptions;
using MG.UpdateManagement.Framework;
using MG.UpdateManagement.Objects;
using Microsoft.UpdateServices.Administration;
using Microsoft.UpdateServices.Internal.BaseApi;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;

namespace MG.UpdateManagement.Framework
{
    public class ArchitectureParameter : Parameter
    {
        private protected const string pName = "Architecture";
        private protected static readonly Type type = typeof(string);
        private protected readonly Dictionary<string, object> atts = new Dictionary<string, object>()
        {
            { "Mandatory", false },
            { "Position", 2 }
        };

        internal Array enumValues;
        private UMProductInfo info = new UMProductInfo();

        public override bool AllowNull { get; set; }
        public override bool AllowEmptyCollection { get; set; }
        public override bool AllowEmptyString { get; set; }
        public override bool ValidateNotNull { get; set; }
        public override bool ValidateNotNullOrEmpty { get; set; }

        public ArchitectureParameter(UMProducts[] prods = null)
            : base(pName, type)
        {
            var list = new List<string>();

            if (prods != null)
            { 
                for (int d = 0; d < prods.Length; d++)
                {
                    UMProducts p = prods[d];
                    var pi = new UMProductInfo(p, null, false, false, false);
                    list.AddRange(pi.AllowedPlatforms);
                }
            }
            if (enumValues == null)
            {
                enumValues = typeof(Architectures).GetEnumValues();
            }
            var strArr = new List<string>();
            for (int i = 0; i < enumValues.Length; i++)
            {
                var arc = (Architectures)enumValues.GetValue(i);
                strArr.Add(info.GetAttributeName(arc));
            }
            for (int t = 0; t < list.Count; t++)
            {
                var inf = list[t];
                for (int f = strArr.Count - 1; f >= 0; f--)
                {
                    var s = strArr[f];
                    if (!list.Contains(s))
                    {
                        strArr.Remove(s);
                    }
                }
            }

            ValidatedItems = strArr.ToArray();
            Aliases = new string[1] { "arc" };
            SetParameterAttributes(atts);

            AllowNull = true;

            CommitAttributes();
        }

        internal Architectures? MatchChoiceToEnum(string choice)
        {
            if (choice != null)
            {
                for (int i = 0; i < enumValues.Length; i++)
                {
                    var e = (Architectures)enumValues.GetValue(i);
                    var s = info.GetAttributeName(e);
                    if (choice.Equals(s))
                    {
                        return e;
                    }
                }
            }
            return null;
        }
        
    }
}
