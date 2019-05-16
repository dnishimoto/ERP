using ERP_Core2.GeneralLedgerDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_Core2.Interfaces
{
    public interface IFluentSupplier
    {
        IFluentSupplier UpdateSupplierLedgerWithGeneralLedger(GeneralLedgerView generalLedgerView);
        IFluentSupplier UpdateAccountsPayable(GeneralLedgerView generalLedger);
        IFluentSupplier CreateSupplierLedger(GeneralLedgerView generalLedger);
        IFluentSupplier Apply();
    }
}
