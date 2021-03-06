﻿using ERP_Core2.AbstractFactory;
using ERP_Core2.FluentAPI;
using ERP_Core2.Interfaces;
using ERP_Core2.AccountsReceivableDomain;
using ERP_Core2.GeneralLedgerDomain;
using ERP_Core2.InvoicesDomain;
using ERP_Core2.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ERP_Core2.AccountPayableDomain.AccountsPayableModule;

namespace ERP_Core2.InvoiceDomain
{


    public class InvoiceModule : AbstractModule
    {
        public FluentInvoice Invoice = new FluentInvoice();
        public FluentInvoiceDetail InvoiceDetail = new FluentInvoiceDetail();
        public FluentAccountsReceivable AccountsReceivable = new FluentAccountsReceivable();
        public FluentGeneralLedger GeneralLedger = new FluentGeneralLedger();
    }
}
