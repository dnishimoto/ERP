using lssWebApi2.AccountPayableDomain;
using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using System.Collections.Generic;
using System;
using lssWebApi2.EquipmentDomain;
using lssWebApi2.Enumerations;

namespace lssWebApi2.EquipmentDomain
{

public class FluentEquipment :IFluentEquipment
    {
 private UnitOfWork unitOfWork = new UnitOfWork();
        private CreateProcessStatus processStatus;

        public FluentEquipment() { }
        public IFluentEquipmentQuery Query()
        {
            return new FluentEquipmentQuery(unitOfWork) as IFluentEquipmentQuery;
        }
        public IFluentEquipment Apply() {
			try{
            if (this.processStatus == CreateProcessStatus.Insert || this.processStatus == CreateProcessStatus.Update || this.processStatus == CreateProcessStatus.Delete)
            { unitOfWork.CommitChanges(); }
            return this as IFluentEquipment;
		    }
            catch (Exception ex) { throw new Exception("Apply", ex); }
        }
        public IFluentEquipment AddEquipments(List<Equipment> newObjects)
        {
            unitOfWork.equipmentRepository.AddObjects(newObjects);
            this.processStatus = CreateProcessStatus.Insert;
            return this as IFluentEquipment;
        }
        public IFluentEquipment UpdateEquipments(IList<Equipment> newObjects)
        {
            foreach (var item in newObjects)
            {
                unitOfWork.equipmentRepository.UpdateObject(item);
            }
            this.processStatus = CreateProcessStatus.Update;
            return this as IFluentEquipment;
        }
        public IFluentEquipment AddEquipment(Equipment newObject) {
            unitOfWork.equipmentRepository.AddObject(newObject);
            this.processStatus = CreateProcessStatus.Insert;
            return this as IFluentEquipment;
        }
        public IFluentEquipment UpdateEquipment(Equipment updateObject) {
            unitOfWork.equipmentRepository.UpdateObject(updateObject);
            this.processStatus = CreateProcessStatus.Update;
            return this as IFluentEquipment;

        }
        public IFluentEquipment DeleteEquipment(Equipment deleteObject) {
            unitOfWork.equipmentRepository.DeleteObject(deleteObject);
            this.processStatus = CreateProcessStatus.Delete;
            return this as IFluentEquipment;
        }
   	public IFluentEquipment DeleteEquipments(List<Equipment> deleteObjects)
        {
            unitOfWork.equipmentRepository.DeleteObjects(deleteObjects);
            this.processStatus = CreateProcessStatus.Delete;
            return this as IFluentEquipment;
        }
    }
}
