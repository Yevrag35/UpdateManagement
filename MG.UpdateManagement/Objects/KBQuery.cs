using MG;
using MG.UpdateManagement.Enumerations;
using System;
using System.Collections.Generic;

namespace MG.UpdateManagement.Objects
{
    public class UMKbQuery : MGNameResolver
    {
        private readonly string _sf;
        private readonly string _p;
        private readonly string _arc;

        public string SearchFor => _sf;
        public string Product => _p;
        public string Arc => _arc;

        public UMKbQuery(string kbText, UMProducts? prod, Architectures? arc)
        {
            _sf = kbText;
            if (prod.HasValue)
            {
                _p = GetAttributeName(prod.Value);
            }
            if (arc.HasValue)
            {
                _arc = GetAttributeName(arc.Value);
            }
        }
    }
}
