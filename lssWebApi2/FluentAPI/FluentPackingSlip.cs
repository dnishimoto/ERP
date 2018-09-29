using ERP_Core2.AbstractFactory;
using ERP_Core2.AccountPayableDomain;
using ERP_Core2.Interfaces;
using ERP_Core2.PackingSlipDomain;
using ERP_Core2.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_Core2.FluentAPI
{
    public class FluentPackingSlip : AbstractErrorHandling, IPackingSlip
    {
        private CreateProcessStatus processStatus;
        UnitOfWork unitOfWork = new UnitOfWork();
        public IPackingSlip Apply()
        {
            if ((processStatus == CreateProcessStatus.Insert) || (processStatus == CreateProcessStatus.Update) || (processStatus == CreateProcessStatus.Delete))
            {
                unitOfWork.CommitChanges();
            }
            return this as IPackingSlip;

        }

        public IPackingSlip CreatePackingSlip(PackingSlipView packingSlipView)
        {
            Task<CreateProcessStatus> resultTask = Task.Run(() => unitOfWork.packingSlipRepository.CreatePackingSlipByView(packingSlipView));
            Task.WaitAll(resultTask);
            processStatus = resultTask.Result;
            return this as IPackingSlip;
        }
        public IPackingSlip CreatePackingSlipDetails(PackingSlipView packingSlipView)
        {
            Task<CreateProcessStatus> resultTask = Task.Run(() => unitOfWork.packingSlipRepository.CreatePackingSlipDetailsByView(packingSlipView));
            Task.WaitAll(resultTask);
            processStatus = resultTask.Result;
            return this as IPackingSlip;
        }
        public IPackingSlip CreateInventoryByPackingSlip(PackingSlipView packingSlipView)
        {
            Task<PackingSlipView> lookupViewTask = Task.Run(() => unitOfWork.packingSlipRepository.GetPackingSlipViewBySlipDocument(packingSlipView.SlipDocument));
            Task.WaitAll(lookupViewTask);
            Task<CreateProcessStatus> resultTask = unitOfWork.inventoryRepository.CreateInventoryByPackingSlipView(lookupViewTask.Result);
            Task.WaitAll(resultTask);
            processStatus = resultTask.Result;
            return this as IPackingSlip;
        }
    }
}
