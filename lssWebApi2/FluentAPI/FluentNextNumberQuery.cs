using lssWebApi2.AbstractFactory;
using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using lssWebApi2.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lssWebApi2.FluentAPI
{
    public class FluentNextNumberQuery : AbstractErrorHandling, INextNumberQuery
    {
        private UnitOfWork _unitOfWork = null;
        public FluentNextNumberQuery(UnitOfWork unitOfWork) { _unitOfWork = unitOfWork; }

        public NextNumber GetNextNumber(string nextNumberName)
        {
            Task<NextNumber> nnTask = Task.Run(async () => await _unitOfWork.nextNumberRepository.GetNextNumber(nextNumberName));
            Task.WaitAll(nnTask);
            return nnTask.Result;
        }

    }
}
