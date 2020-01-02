
using lssWebApi2.AccountPayableDomain;
using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace lssWebApi2.ShipmentsDomain
{
public class FluentShipment :IFluentShipment
    {
 private UnitOfWork unitOfWork = new UnitOfWork();
        private CreateProcessStatus processStatus;

        public FluentShipment() { }
        public IFluentShipmentQuery Query()
        {
            return new FluentShipmentQuery(unitOfWork) as IFluentShipmentQuery;
        }
        public IFluentShipment Apply() {
            try
            {
                if (this.processStatus == CreateProcessStatus.Insert || this.processStatus == CreateProcessStatus.Update || this.processStatus == CreateProcessStatus.Delete)
                { unitOfWork.CommitChanges(); }
                return this as IFluentShipment;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
      
        public IFluentShipment AddShipments(List<Shipment> newObjects)
        {
            unitOfWork.shipmentRepository.AddObjects(newObjects);
            this.processStatus = CreateProcessStatus.Insert;
            return this as IFluentShipment;
        }
        public IFluentShipment UpdateShipments(List<Shipment> newObjects)
        {
            foreach (var item in newObjects)
            {
                unitOfWork.shipmentRepository.UpdateObject(item);
            }
            this.processStatus = CreateProcessStatus.Update;
            return this as IFluentShipment;
        }
        public IFluentShipment AddShipment(Shipment newObject) {
            unitOfWork.shipmentRepository.AddObject(newObject);
            this.processStatus = CreateProcessStatus.Insert;
            return this as IFluentShipment;
        }
        public IFluentShipment UpdateShipment(Shipment updateObject) {
            unitOfWork.shipmentRepository.UpdateObject(updateObject);
            this.processStatus = CreateProcessStatus.Update;
            return this as IFluentShipment;

        }
        public IFluentShipment DeleteShipment(Shipment deleteObject) {
            unitOfWork.shipmentRepository.DeleteObject(deleteObject);
            this.processStatus = CreateProcessStatus.Delete;
            return this as IFluentShipment;
        }
   	public IFluentShipment DeleteAllShipments(List<Shipment> deleteObjects)
        {
            unitOfWork.shipmentRepository.DeleteObjects(deleteObjects);
            this.processStatus = CreateProcessStatus.Delete;
            return this as IFluentShipment;
        }
}
}
