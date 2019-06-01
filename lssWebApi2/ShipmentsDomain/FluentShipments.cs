
using ERP_Core2.AccountPayableDomain;
using ERP_Core2.Services;
using lssWebApi2.EntityFramework;
using System.Collections.Generic;

namespace lssWebApi2.ShipmentsDomain
{
public class FluentShipments :IFluentShipments
    {
 private UnitOfWork unitOfWork = new UnitOfWork();
        private CreateProcessStatus processStatus;

        public FluentShipments() { }
        public IFluentShipmentsQuery Query()
        {
            return new FluentShipmentsQuery(unitOfWork) as IFluentShipmentsQuery;
        }
        public IFluentShipments Apply() {
            if (this.processStatus == CreateProcessStatus.Insert || this.processStatus == CreateProcessStatus.Update || this.processStatus == CreateProcessStatus.Delete)
            { unitOfWork.CommitChanges(); }
            return this as IFluentShipments;
        }
        public IFluentShipments AddShipmentss(List<Shipments> newObjects)
        {
            unitOfWork.shipmentsRepository.AddObjects(newObjects);
            this.processStatus = CreateProcessStatus.Insert;
            return this as IFluentShipments;
        }
        public IFluentShipments UpdateShipmentss(List<Shipments> newObjects)
        {
            foreach (var item in newObjects)
            {
                unitOfWork.shipmentsRepository.UpdateObject(item);
            }
            this.processStatus = CreateProcessStatus.Update;
            return this as IFluentShipments;
        }
        public IFluentShipments AddShipments(Shipments newObject) {
            unitOfWork.shipmentsRepository.AddObject(newObject);
            this.processStatus = CreateProcessStatus.Insert;
            return this as IFluentShipments;
        }
        public IFluentShipments UpdateShipments(Shipments updateObject) {
            unitOfWork.shipmentsRepository.UpdateObject(updateObject);
            this.processStatus = CreateProcessStatus.Update;
            return this as IFluentShipments;

        }
        public IFluentShipments DeleteShipments(Shipments deleteObject) {
            unitOfWork.shipmentsRepository.DeleteObject(deleteObject);
            this.processStatus = CreateProcessStatus.Delete;
            return this as IFluentShipments;
        }
   	public IFluentShipments DeleteAllShipments(List<Shipments> deleteObjects)
        {
            unitOfWork.shipmentsRepository.DeleteObjects(deleteObjects);
            this.processStatus = CreateProcessStatus.Delete;
            return this as IFluentShipments;
        }
}
}
