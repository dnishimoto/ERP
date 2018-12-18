using ERP_Core2.AbstractFactory;
using ERP_Core2.AccountsReceivableDomain;
using ERP_Core2.Services;
using lssWebApi2.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lssWebApi2.FluentAPI
{
    public class FluentAccountReceivableQuery : AbstractErrorHandling, IQueryAccountReceivable
    {
        private UnitOfWork _unitOfWork;
        public FluentAccountReceivableQuery() { }
        public FluentAccountReceivableQuery(UnitOfWork unitOfWork) { _unitOfWork = unitOfWork; }
        public List<AccountReceivableFlatView> GetOpenAccountReceivables()
        {
            return _unitOfWork.accountReceiveableRepository.GetOpenAcctRec();
        }
        public bool HasLateFee(long? AcctRecId)
        { return _unitOfWork.accountReceiveableRepository.HasLateFee(AcctRecId); }
        public bool IsPaymentLate(long? invoiceId, DateTime asOfDate)
        {
            return _unitOfWork.accountReceiveableRepository.IsPaymentLate(invoiceId, asOfDate);
        }
        


    }
}
