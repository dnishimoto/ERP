using lssWebApi2.AccountPayableDomain;
using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using System.Collections.Generic;
using System;
using lssWebApi2.PackingSlipDetailDomain;
using lssWebApi2.Enumerations;
using lssWebApi2.PackingSlipDomain;
using System.Threading.Tasks;
using lssWebApi2.AutoMapper;

namespace lssWebApi2.PackingSlipDetailDomain
{

public class FluentPackingSlipDetail :IFluentPackingSlipDetail
    {
 private UnitOfWork unitOfWork = new UnitOfWork();
        private CreateProcessStatus processStatus;

        public FluentPackingSlipDetail() { }
        public IFluentPackingSlipDetailQuery Query()
        {
            return new FluentPackingSlipDetailQuery(unitOfWork) as IFluentPackingSlipDetailQuery;
        }
        private PackingSlipDetail MapToEntity(PackingSlipDetailView inputObject)
        {
            Mapper mapper = new Mapper();
            PackingSlipDetail outObject = mapper.Map<PackingSlipDetail>(inputObject);
            return outObject;
        }
        public IFluentPackingSlipDetail CreatePackingSlipDetailsByView(PackingSlipView view)
        {
            try
            {


                Task<PackingSlip> packingSlipTask = Task.Run(async () => await unitOfWork.packingSlipRepository.GetEntityBySlipDocument(view.SlipDocument));
                if (packingSlipTask.Result != null)
                {
                    long packingSlipId = view.PackingSlipId;

                    //TODO - send a list of ids and return back the views
                    foreach (var detailView in view.PackingSlipDetailViews)
                    {
                        detailView.PackingSlipId = packingSlipId;

                        PackingSlipDetail newDetail = new PackingSlipDetail();
                    

                        newDetail = MapToEntity(detailView);

                        Task<IList<PackingSlipDetail>> listTask = Task.Run(async () => await unitOfWork.packingSlipDetailRepository.FindByExpression(e => e.ItemId == detailView.ItemId && e.PackingSlipId == newDetail.PackingSlipId));
                        Task.WaitAll(listTask);
                                                 
                        if (listTask.Result == null)
                        {
                            //_dbContext.Set<PackingSlipDetail>().Add(newDetail);
                            AddPackingSlipDetail(newDetail);
                            return this as IFluentPackingSlipDetail;
                        }
                    }
            
                }
                processStatus= CreateProcessStatus.AlreadyExists;
                return this as IFluentPackingSlipDetail;

            }
            catch (Exception ex) { throw new Exception("CreatePackingSlipDetailsByView", ex); }
        }
        public IFluentPackingSlipDetail Apply() {
			try{
            if (this.processStatus == CreateProcessStatus.Insert || this.processStatus == CreateProcessStatus.Update || this.processStatus == CreateProcessStatus.Delete)
            { unitOfWork.CommitChanges(); }
            return this as IFluentPackingSlipDetail;
		    }
            catch (Exception ex) { throw new Exception("Apply", ex); }
        }
        public IFluentPackingSlipDetail AddPackingSlipDetails(List<PackingSlipDetail> newObjects)
        {
            unitOfWork.packingSlipDetailRepository.AddObjects(newObjects);
            this.processStatus = CreateProcessStatus.Insert;
            return this as IFluentPackingSlipDetail;
        }
        public IFluentPackingSlipDetail UpdatePackingSlipDetails(IList<PackingSlipDetail> newObjects)
        {
            foreach (var item in newObjects)
            {
                unitOfWork.packingSlipDetailRepository.UpdateObject(item);
            }
            this.processStatus = CreateProcessStatus.Update;
            return this as IFluentPackingSlipDetail;
        }
        public IFluentPackingSlipDetail AddPackingSlipDetail(PackingSlipDetail newObject) {
            unitOfWork.packingSlipDetailRepository.AddObject(newObject);
            this.processStatus = CreateProcessStatus.Insert;
            return this as IFluentPackingSlipDetail;
        }
        public IFluentPackingSlipDetail UpdatePackingSlipDetail(PackingSlipDetail updateObject) {
            unitOfWork.packingSlipDetailRepository.UpdateObject(updateObject);
            this.processStatus = CreateProcessStatus.Update;
            return this as IFluentPackingSlipDetail;

        }
        public IFluentPackingSlipDetail DeletePackingSlipDetail(PackingSlipDetail deleteObject) {
            unitOfWork.packingSlipDetailRepository.DeleteObject(deleteObject);
            this.processStatus = CreateProcessStatus.Delete;
            return this as IFluentPackingSlipDetail;
        }
   	public IFluentPackingSlipDetail DeletePackingSlipDetails(List<PackingSlipDetail> deleteObjects)
        {
            unitOfWork.packingSlipDetailRepository.DeleteObjects(deleteObjects);
            this.processStatus = CreateProcessStatus.Delete;
            return this as IFluentPackingSlipDetail;
        }
    }
}
