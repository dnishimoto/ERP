

using lssWebApi2.EntityFramework;
using System.Collections.Generic;

namespace ERP_Core2.PayRollDomain
{ 

public interface IFluentPayRollTransactionsByEmployee
    {
        IFluentPayRollTransactionsByEmployeeQuery Query();
        IFluentPayRollTransactionsByEmployee Apply();
        IFluentPayRollTransactionsByEmployee AddPayRollTransactionsByEmployee(PayRollTransactionsByEmployee payRollTransactionsByEmployee);
        IFluentPayRollTransactionsByEmployee UpdatePayRollTransactionsByEmployee(PayRollTransactionsByEmployee payRollTransactionsByEmployee);
        IFluentPayRollTransactionsByEmployee DeletePayRollTransactionsByEmployee(PayRollTransactionsByEmployee payRollTransactionsByEmployee);
     	IFluentPayRollTransactionsByEmployee UpdatePayRollTransactionsByEmployees(List<PayRollTransactionsByEmployee> newObjects);
        IFluentPayRollTransactionsByEmployee AddPayRollTransactionsByEmployees(List<PayRollTransactionsByEmployee> newObjects);
        IFluentPayRollTransactionsByEmployee DeletePayRollTransactionsByEmployees(List<PayRollTransactionsByEmployee> deleteObjects);
    }
}
