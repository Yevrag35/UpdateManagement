using MG;
using MG.Attributes;
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

namespace MG.UpdateManagement.Cmdlets
{
    public abstract partial class BaseGetCmdlet : BaseCmdlet
    {
        public List<UMUpdate> WittleDown(UMProducts prod, bool? super = null, bool? appr = null, bool? decline = null)
        {
            var list = new List<UMUpdate>();
            var info = new UMProductInfo(prod, super, appr, decline);
            for (int i = 0; i < UMContext.AllUpdates.Count; i++)
            {
                var up = (UMUpdate)UMContext.AllUpdates[i];
                if (up.IsMatchInfo(info))
                {
                    list.Add(up);
                }
            }
            return list;
        }
    }

    public class UMProductInfo : MGNameResolver
    {
        #region Private Fields
        private string _name;
        private string _id;
        private string _base;
        private object[] _ap;
        private object[] _fr;
        private string _cat;
        private UMProducts _pr;

        #endregion

        #region Public Properties
        public UMProducts Product
        {
            get => _pr;
            set
            {
                _pr = value;
                GetUMProductAttributes();
            }
        }
        public string Name => _name;
        public string Id => _id;
        public string BaseProduct => _base;
        public object[] AllowedPlatforms => _ap;
        public object[] FutureReleases => _fr;
        public string Category => _cat;

        public Dictionary<string, bool?> LookingFor { get; set; }
        internal bool IsSet(string s) => LookingFor[s].HasValue;

        public readonly string[] Is = new string[3] { "IsSuperseded", "IsApproved", "IsDeclined" };

        #endregion

        #region Constructors
        public UMProductInfo() { }
        public UMProductInfo(UMProducts prod, bool? super = null, bool? appr = null, bool? decline = null)
        {
            _pr = prod;
            GetUMProductAttributes();
            LookingFor = new Dictionary<string, bool?>();
            if (super.HasValue)
            {
                LookingFor.Add("IsSuperseded", super.Value);
            }
            if (appr.HasValue)
            {
                LookingFor.Add("IsApproved", appr.Value);
            }
            if (decline.HasValue)
            {
                LookingFor.Add("IsDeclined", decline.Value);
            }
        }

        #endregion

        #region Methods
        private void GetUMProductAttributes()
        {
            _name = GetAttributeName(_pr);
            _id = (string)GetAttributeValue<IDAttribute>(_pr);
            _base = (string)GetAttributeValue<BaseAttribute>(_pr);
            _ap = GetAttributeValues<AllowedPlatformsAttribute>(_pr);
            try
            {
                _fr = GetAttributeValues<FutureReleasesAttribute>(_pr);
            }
            catch
            {
                _fr = null;
            }
            
            _cat = (string)GetAttributeValue<CategoryAttribute>(_pr);
        }

        #endregion
    }
}
