using ERP_Core2.AccountPayableDomain;
using ERP_Core2.Services;
using lssWebApi2.EntityFramework;
using System;
using System.Collections.Generic;

namespace lssWebApi2.ShipmentsDomain
{

    public class FluentShipmentsDetail : IFluentShipmentsDetail
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private CreateProcessStatus processStatus;

        public FluentShipmentsDetail() { }
        public IFluentShipmentsDetailQuery Query()
        {
            return new FluentShipmentsDetailQuery(unitOfWork) as IFluentShipmentsDetailQuery;
        }
        public IFluentShipmentsDetail Apply()
        {
            try
            {
                if (this.processStatus == CreateProcessStatus.Insert || this.processStatus == CreateProcessStatus.Update || this.processStatus == CreateProcessStatus.Delete)
                { unitOfWork.CommitChanges(); }
                return this as IFluentShipmentsDetail;
            }
            catch (Exception ex)
            {
                throw new System.Exception(ex.Message);
            }
        }
        public IFluentShipmentsDetail AddShipmentsDetails(List<ShipmentsDetail> newObjects)
        {
            unitOfWork.shipmentsDetailRepository.AddObjects(newObjects);
            this.processStatus = CreateProcessStatus.Insert;
            return this as IFluentShipmentsDetail;
        }
        public IFluentShipmentsDetail UpdateShipmentsDetails(List<ShipmentsDetail> newObjects)
        {
            foreach (var item in newObjects)
            {
                unitOfWork.shipmentsDetailRepository.UpdateObject(item);
            }
            this.processStatus = CreateProcessStatus.Update;
            return this as IFluentShipmentsDetail;
        }
        public IFluentShipmentsDetail AddShipmentsDetail(ShipmentsDetail newObject)
        {
            unitOfWork.shipmentsDetailRepository.AddObject(newObject);
            this.processStatus = CreateProcessStatus.Insert;
            return this as IFluentShipmentsDetail;
        }
        public IFluentShipmentsDetail UpdateShipmentsDetail(ShipmentsDetail updateObject)
        {
            unitOfWork.shipmentsDetailRepository.UpdateObject(updateObject);
            this.processStatus = CreateProcessStatus.Update;
            return this as IFluentShipmentsDetail;

        }
        public IFluentShipmentsDetail DeleteShipmentsDetail(ShipmentsDetail deleteObject)
        {
            unitOfWork.shipmentsDetailRepository.DeleteObject(deleteObject);
            this.processStatus = CreateProcessStatus.Delete;
            return this as IFluentShipmentsDetail;
        }
        public IFluentShipmentsDetail DeleteShipmentsDetails(List<ShipmentsDetail> deleteObjects)
        {
            unitOfWork.shipmentsDetailRepository.DeleteObjects(deleteObjects);
            this.processStatus = CreateProcessStatus.Delete;
            return this as IFluentShipmentsDetail;
        }
    }
}