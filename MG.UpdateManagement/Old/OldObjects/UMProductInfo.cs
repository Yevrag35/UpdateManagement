using MG.Attributes;
using MG.UpdateManagement.Enumerations;
using System;
using System.Collections.Generic;

namespace MG.UpdateManagement.Objects
{
    public class UMProductInfo : AttributeResolver
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
                DesiredPlats = GetNameAttribute(arcs.Value);
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
            _name = GetNameAttribute(_pr);
            if (_name.Contains("Office"))   // This is done in order to prevent filtering out updates for "Word", "Excel", etc.
                _name = _name.Replace("Office ", string.Empty);

            _id = GetAttributeValue<string>(_pr, typeof(IDAttribute));
            _base = GetAttributeValue<string>(_pr, typeof(BaseAttribute));
            
            var ap = GetAttributeValues<Architectures>(_pr, typeof(AllowedPlatformsAttribute));
            _ap = new string[ap.Length];
            for (int i1 = 0; i1 < ap.Length; i1++)
            {
                var a = ap[i1];
                _ap[i1] = GetNameAttribute(a);
            }
            
            var fr = GetAttributeValues<string>(_pr, typeof(MutuallyExclusiveToAttribute));
            _fr = new string[fr.Length];
            for (int i2 = 0; i2 < fr.Length; i2++)
            {
                _fr[i2] = fr[i2];
            }

            _cat = GetAttributeValue<string>(_pr, typeof(CategoryAttribute));
        }

        #endregion
    }
}