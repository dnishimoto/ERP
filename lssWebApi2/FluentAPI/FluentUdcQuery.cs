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
    public class FluentUDCQuery : AbstractErrorHandling, IUDCQuery
    {
        private UnitOfWork _unitOfWork = null;
        public FluentUDCQuery(UnitOfWork unitOfWork) { _unitOfWork = unitOfWork; }
        public Udc GetUdc(string productCode, string keyCode)
        {
            Task<Udc> udcTask = Task.Run(async () => await _unitOfWork.generalLedgerRepository.GetUdc(productCode, keyCode));
            Task.WaitAll(udcTask);
            return udcTask.Result;
        }
    }
}
