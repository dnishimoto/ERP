using System;
using System.Collections.Generic;

namespace lssWebApi2.EntityFramework
{
    public partial class Company
    {
        public Company()
        {
            ChartOfAccts = new HashSet<ChartOfAccts>();
            Invoice = new HashSet<Invoice>();
        }

        public long CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string CompanyCode { get; set; }
        public string CompanyStreet { get; set; }
        public string CompanyCity { get; set; }
        public string CompanyState { get; set; }
        public string CompanyZipcode { get; set; }
        public string TaxCode1 { get; set; }
        public string TaxCode2 { get; set; }

        public virtual ICollection<ChartOfAccts> ChartOfAccts { get; set; }
        public virtual ICollection<Invoice> Invoice { get; set; }

    }
}