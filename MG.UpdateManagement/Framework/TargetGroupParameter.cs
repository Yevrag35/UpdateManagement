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
    public class TargetGroupParameter : Parameter
    {
        private protected const string pName = "ComputerGroup";
        private protected static readonly Type type = typeof(string);
        private protected readonly Dictionary<string, object> atts = new Dictionary<string, object>()
        {
            { "Mandatory", true },
            { "Position", 0 }
        };

        internal UMComputerGroupCollection ctgCol => UMContext.AllComputerGroups;

        public override bool AllowNull { get; set; }
        public override bool AllowEmptyCollection { get; set; }
        public override bool AllowEmptyString { get; set; }
        public override bool ValidateNotNull { get; set; }
        public override bool ValidateNotNullOrEmpty { get; set; }

		public TargetGroupParameter()
			: base(pName, type)
        {
            if (ctgCol == null)
            {
                UMContext.AllComputerGroups = UMContext.Context.GetComputerTargetGroups();
            }
            var arr = new string[ctgCol.Count];
			for (int i = 0; i < ctgCol.Count; i++)
            {
                var s = ctgCol[i];
                arr[i] = s.Name;
            }
            ValidatedItems = arr;
            Aliases = new string[2] { "group", "g" };

            ValidateNotNullOrEmpty = true;

            SetParameterAttributes(atts);

            CommitAttributes();
        }

		internal static IComputerTargetGroup GroupFromTheName(string name)
        {
            var col = UMContext.AllComputerGroups;
            if (col == null)
            {
                col = UMContext.Context.GetComputerTargetGroups();
            }
            for (int i = 0; i < col.Count; i++)
            {
                var grp = col[i];
				if (grp.Name == name)
                {
                    return grp;
                }
            }
            throw new Exception("What the fuck?!?!?!?");
        }
    }
}
