using lssWebApi2.AccountPayableDomain;
using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using System.Collections.Generic;
using System;
using lssWebApi2.PackingSlipDomain;
using lssWebApi2.Enumerations;
using System.Threading.Tasks;
using lssWebApi2.AutoMapper;

namespace lssWebApi2.PackingSlipDomain
{

public class FluentPackingSlip :IFluentPackingSlip
    {
 private UnitOfWork unitOfWork = new UnitOfWork();
        private CreateProcessStatus processStatus;

        public FluentPackingSlip() { }
        public IFluentPackingSlipQuery Query()
        {
            return new FluentPackingSlipQuery(unitOfWork) as IFluentPackingSlipQuery;
        }
        private PackingSlip MapToEntity(PackingSlipView inputObject)
        {
            Mapper mapper = new Mapper();
            PackingSlip outObject = mapper.Map<PackingSlip>(inputObject);
            return outObject;
        }
        public IFluentPackingSlip CreatePackingSlipByView(PackingSlipView view)
        {
            decimal amount = 0;
            try
            {
                Task<PackingSlip> packingSlipTask = Task.Run(async () => await unitOfWork.packingSlipRepository.GetEntityBySlipDocument(view.SlipDocument));
                Task.WaitAll(packingSlipTask);

                  
                if (packingSlipTask.Result != null) { processStatus=CreateProcessStatus.AlreadyExists; return this as IFluentPackingSlip; }


                foreach (var detail in view.PackingSlipDetailViews)
                {
                    amount += detail.ExtendedCost ?? 0;
                }
                view.Amount = amount;

                PackingSlip packingSlip=null;

                packingSlip=MapToEntity(view);

                AddPackingSlip(packingSlip);

                return this as IFluentPackingSlip;
      
            }
            catch (Exception ex) { throw new Exception("CreatePackingSlipByView", ex); }
        }
        public IFluentPackingSlip Apply() {
			try{
            if (this.processStatus == CreateProcessStatus.Insert || this.processStatus == CreateProcessStatus.Update || this.processStatus == CreateProcessStatus.Delete)
            { unitOfWork.CommitChanges(); }
            return this as IFluentPackingSlip;
		    }
            catch (Exception ex) { throw new Exception("Apply", ex); }
        }
        public IFluentPackingSlip AddPackingSlips(List<PackingSlip> newObjects)
        {
            unitOfWork.packingSlipRepository.AddObjects(newObjects);
            this.processStatus = CreateProcessStatus.Insert;
            return this as IFluentPackingSlip;
        }
        public IFluentPackingSlip UpdatePackingSlips(IList<PackingSlip> newObjects)
        {
            foreach (var item in newObjects)
            {
                unitOfWork.packingSlipRepository.UpdateObject(item);
            }
            this.processStatus = CreateProcessStatus.Update;
            return this as IFluentPackingSlip;
        }
        public IFluentPackingSlip AddPackingSlip(PackingSlip newObject) {
            unitOfWork.packingSlipRepository.AddObject(newObject);
            this.processStatus = CreateProcessStatus.Insert;
            return this as IFluentPackingSlip;
        }
        public IFluentPackingSlip UpdatePackingSlip(PackingSlip updateObject) {
            unitOfWork.packingSlipRepository.UpdateObject(updateObject);
            this.processStatus = CreateProcessStatus.Update;
            return this as IFluentPackingSlip;

        }
        public IFluentPackingSlip DeletePackingSlip(PackingSlip deleteObject) {
            unitOfWork.packingSlipRepository.DeleteObject(deleteObject);
            this.processStatus = CreateProcessStatus.Delete;
            return this as IFluentPackingSlip;
        }
   	public IFluentPackingSlip DeletePackingSlips(List<PackingSlip> deleteObjects)
        {
            unitOfWork.packingSlipRepository.DeleteObjects(deleteObjects);
            this.processStatus = CreateProcessStatus.Delete;
            return this as IFluentPackingSlip;
        }
    }
}
