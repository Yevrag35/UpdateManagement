using MG;
using MG.UpdateManagement.Enumerations;
using System;
using System.Collections.Generic;

namespace MG.UpdateManagement.Objects
{
    public class UMProductInfo : MGNameResolver
    {
        #region Private Fields
        private string _name;
        private string _id;
        private string _base;
        private string[] _ap;
        private string[] _fr;
        private string _cat;
        private UMProducts? _pr;

        #endregion

        #region Public Properties
        public UMProducts? Product
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
        public string[] AllowedPlatforms => _ap;
        public string[] MutuallyExclusiveTo => _fr;
        public string Category => _cat;
        public bool AllProductSearch => !_pr.HasValue;

        public string DesiredPlats { get; set; }

        public Dictionary<string, bool> LookingFor { get; set; }

        internal readonly string[] Is = new string[3] { "IsSuperseded", "IsApproved", "IsDeclined" };

        #endregion

        #region Constructors
        public UMProductInfo() { }
        public UMProductInfo(UMProducts? prod, Architectures? arcs, bool super, bool appr, bool decline)
        {
            if (prod.HasValue || arcs.HasValue)
            {
                _pr = prod;
                GetUMProductAttributes();
            }
            if (arcs.HasValue)
            {
                DesiredPlats = GetAttributeName(arcs.Value);
            }
            LookingFor = new Dictionary<string, bool>()
            {
                { "IsSuperseded", super },
                { "IsApproved", appr },
                { "IsDeclined", decline }
            };
        }

        #endregion

        #region Methods
        private void GetUMProductAttributes()
        {
            _name = GetAttributeName(_pr);
            _id = (string)GetAttributeValue<IDAttribute>(_pr);
            _base = (string)GetAttributeValue<BaseAttribute>(_pr);

            var ap = GetAttributeValues<AllowedPlatformsAttribute>(_pr);
            _ap = new string[ap.Length];
            for (int i1 = 0; i1 < ap.Length; i1++)
            {
                var a = (Architectures)ap[i1];
                _ap[i1] = GetAttributeName(a);
            }

            var fr = GetAttributeValues<MutuallyExclusiveToAttribute>(_pr);
            _fr = new string[fr.Length];
            for (int i2 = 0; i2 < fr.Length; i2++)
            {
                _fr[i2] = (string)fr[i2];
            }

            _cat = (string)GetAttributeValue<CategoryAttribute>(_pr);
        }

        #endregion
    }
}