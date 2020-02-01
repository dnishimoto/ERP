using lssWebApi2.AbstractFactory;
using lssWebApi2.SupplierInvoiceDetailDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lssWebApi2.InvoiceDetailDomain;
using lssWebApi2.SupplierInvoiceDomain;
using lssWebApi2.ItemMasterDomain;
using lssWebApi2.InvoiceDomain;
using lssWebApi2.Services;

namespace lssWebApi2.SupplierInvoiceDetailDomain
{
    public class SupplierInvoiceDetailModule : AbstractModule
    {
        private UnitOfWork unitOfWork;
        public FluentSupplierInvoiceDetail SupplierInvoiceDetail;
        public FluentInvoice Invoice;
        public FluentInvoiceDetail InvoiceDetail;
        public FluentSupplierInvoice SupplierInvoice;
        public FluentItemMaster ItemMaster;

        public SupplierInvoiceDetailModule()
        {
            unitOfWork = new UnitOfWork();
            SupplierInvoiceDetail = new FluentSupplierInvoiceDetail(unitOfWork);
            Invoice = new FluentInvoice(unitOfWork);
            InvoiceDetail = new FluentInvoiceDetail(unitOfWork);
            SupplierInvoice = new FluentSupplierInvoice(unitOfWork);
            ItemMaster = new FluentItemMaster(unitOfWork);
        }
    }
}
