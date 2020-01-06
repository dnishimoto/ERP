

using lssWebApi2.EntityFramework;
using System.Collections.Generic;

namespace lssWebApi2.PayRollDomain
{ 

public interface IFluentPayRollTransactionsByEmployee
    {
        IFluentPayRollTransactionsByEmployeeQuery Query();
        IFluentPayRollTransactionsByEmployee Apply();
        IFluentPayRollTransactionsByEmployee AddPayRollTransactionsByEmployee(PayRollTransactionsByEmployee payRollTransactionsByEmployee);
        IFluentPayRollTransactionsByEmployee UpdatePayRollTransactionsByEmployee(PayRollTransactionsByEmployee payRollTransactionsByEmployee);
        IFluentPayRollTransactionsByEmployee DeletePayRollTransactionsByEmployee(PayRollTransactionsByEmployee payRollTransactionsByEmployee);
     	IFluentPayRollTransactionsByEmployee UpdatePayRollTransactionsByEmployees(IList<PayRollTransactionsByEmployee> newObjects);
        IFluentPayRollTransactionsByEmployee AddPayRollTransactionsByEmployees(List<PayRollTransactionsByEmployee> newObjects);
        IFluentPayRollTransactionsByEmployee DeletePayRollTransactionsByEmployees(List<PayRollTransactionsByEmployee> deleteObjects);
    }
}
