using ERP_Core2.AbstractFactory;
using ERP_Core2.Services;
using lssWebApi2.EntityFramework;
using lssWebApi2.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lssWebApi2.FluentAPI
{
    public class FluentAccountPayableQuery : AbstractErrorHandling, IAccountPayableQuery
    {
        private UnitOfWork _unitOfWork = null;
        public FluentAccountPayableQuery() { }
        public FluentAccountPayableQuery(UnitOfWork unitOfWork) { _unitOfWork = unitOfWork; }
        public AcctPay GetAcctPayByPONumber(string poNumber)
        {

            Task<AcctPay> acctPayTask = Task.Run(async () => await _unitOfWork.accountPayableRepository.GetAcctPayByPONumber(poNumber));
            return acctPayTask.Result;

        }
    }
}
