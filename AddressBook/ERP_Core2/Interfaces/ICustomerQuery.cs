using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_Core2.Interfaces
{
    public interface ICustomerQuery

    {

        ICustomerQuery WithAccountReceivables(long customerId);
        ICustomerQuery WithCustomerLedgers(long customerId);
        ICustomerQuery WithEmails(long customerId);
        ICustomerQuery WithPhones(long customerId);
        ICustomerQuery WithInvoices(long customerId, long? invoiceId);
        ICustomerQuery WithScheduleEvent(long customerId, long serviceId);
        ICustomerQuery WithLocationAddress(long customerId);
        ICustomerQuery WithCustomerClaims(long customerId);
        ICustomerQuery WithContracts(long customerId, long contractId);
    }
}
