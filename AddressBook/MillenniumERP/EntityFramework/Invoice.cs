//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MillenniumERP.EntityFramework
{
    using System;
    using System.Collections.Generic;
    
    public partial class Invoice
    {
        public long InvoiceId { get; set; }
        public string InvoiceNumber { get; set; }
        public Nullable<System.DateTime> InvoiceDate { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public Nullable<long> BillToAddressBook { get; set; }
        public string InvoiceDescription { get; set; }
        public Nullable<decimal> TaxAmount { get; set; }
        public Nullable<System.DateTime> PaymentDueDate { get; set; }
        public string PaymentTerms { get; set; }
    }
}
