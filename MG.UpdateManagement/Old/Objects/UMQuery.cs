using Microsoft.UpdateServices.Administration;
using Microsoft.UpdateServices.Internal.BaseApi;
using System;

namespace MG.UpdateManagement.Objects
{
    public class UMQuery
    {
        public string[] ProductTitles { get; set; }
        public bool Superseded { get; set; }
        public bool Approved { get; set; }
        public bool Declined { get; set; }
        public string UpdateTitle { get; set; }

        public UMQuery() { }
        public UMQuery(UMUpdate update)
        {
            ProductTitles = update.Products;
            Superseded = update.IsSuperseded;
            Approved = update.IsApproved;
            Declined = update.IsDeclined;
            UpdateTitle = update.Title;
        }
    }
}
