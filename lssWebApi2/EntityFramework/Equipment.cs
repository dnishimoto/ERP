using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lssWebApi2.EntityFramework
{
    public partial class Equipment
    {
        public long EquipmentId { get; set; }
        public string Model { get; set; }
        public string Make { get; set; }
        public string VIN { get; set; }
        public decimal? PurchasePrice { get; set; }
        public decimal? CurrentAppraisalPrice { get; set; }
        public decimal? SalesPrice { get; set; }
        public string Description { get; set; }
        public string SaleOption { get; set; }
        public int YearPurchased { get; set; }
        public string LocationCity { get; set; }
        public string LocationState { get; set; }
        public string Category1 { get; set; }
        public string Category2 { get; set; }
        public string Category3 { get; set; }
    }
}
