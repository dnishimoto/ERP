﻿using System;
using System.Collections.Generic;

namespace lssWebApi2.entityframework
{
    public partial class ChartOfAccts
    {
        public ChartOfAccts()
        {
            AccountBalance = new HashSet<AccountBalance>();
            AcctPay = new HashSet<AcctPay>();
            AcctRec = new HashSet<AcctRec>();
            Budget = new HashSet<Budget>();
            BudgetRange = new HashSet<BudgetRange>();
            GeneralLedger = new HashSet<GeneralLedger>();
            PurchaseOrder = new HashSet<PurchaseOrder>();
        }

        public long AccountId { get; set; }
        public string Location { get; set; }
        public string BusUnit { get; set; }
        public string Subsidiary { get; set; }
        public string SubSub { get; set; }
        public string Account { get; set; }
        public string Description { get; set; }
        public string CompanyNumber { get; set; }
        public string GenCode { get; set; }
        public string SubCode { get; set; }
        public string ObjectNumber { get; set; }
        public string SupCode { get; set; }
        public string ThirdAccount { get; set; }
        public string CategoryCode1 { get; set; }
        public string CategoryCode2 { get; set; }
        public string CategoryCode3 { get; set; }
        public string PostEditCode { get; set; }
        public long CompanyId { get; set; }
        public int Level { get; set; }

        public Company Company { get; set; }
        public ICollection<AccountBalance> AccountBalance { get; set; }
        public ICollection<AcctPay> AcctPay { get; set; }
        public ICollection<AcctRec> AcctRec { get; set; }
        public ICollection<Budget> Budget { get; set; }
        public ICollection<BudgetRange> BudgetRange { get; set; }
        public ICollection<GeneralLedger> GeneralLedger { get; set; }
        public ICollection<PurchaseOrder> PurchaseOrder { get; set; }
    }
}
