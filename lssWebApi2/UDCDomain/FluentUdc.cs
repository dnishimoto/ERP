using lssWebApi2.AccountPayableDomain;
using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using System.Collections.Generic;
using System;
using lssWebApi2.UDCDomain;
using lssWebApi2.Enumerations;

namespace lssWebApi2.UDCDomain
{

public class FluentUdc :IFluentUdc
    {
 private UnitOfWork unitOfWork = new UnitOfWork();
        private CreateProcessStatus processStatus;

        public FluentUdc() { }
        public IFluentUdcQuery Query()
        {
            return new FluentUdcQuery(unitOfWork) as IFluentUdcQuery;
        }
        public IFluentUdc Apply() {
			try{
            if (this.processStatus == CreateProcessStatus.Insert || this.processStatus == CreateProcessStatus.Update || this.processStatus == CreateProcessStatus.Delete)
            { unitOfWork.CommitChanges(); }
            return this as IFluentUdc;
		    }
            catch (Exception ex) { throw new Exception("Apply", ex); }
        }
        public IFluentUdc AddUdcs(List<Udc> newObjects)
        {
            unitOfWork.udcRepository.AddObjects(newObjects);
            this.processStatus = CreateProcessStatus.Insert;
            return this as IFluentUdc;
        }
        public IFluentUdc UpdateUdcs(List<Udc> newObjects)
        {
            foreach (var item in newObjects)
            {
                unitOfWork.udcRepository.UpdateObject(item);
            }
            this.processStatus = CreateProcessStatus.Update;
            return this as IFluentUdc;
        }
        public IFluentUdc AddUdc(Udc newObject) {
            unitOfWork.udcRepository.AddObject(newObject);
            this.processStatus = CreateProcessStatus.Insert;
            return this as IFluentUdc;
        }
        public IFluentUdc UpdateUdc(Udc updateObject) {
            unitOfWork.udcRepository.UpdateObject(updateObject);
            this.processStatus = CreateProcessStatus.Update;
            return this as IFluentUdc;

        }
        public IFluentUdc DeleteUdc(Udc deleteObject) {
            unitOfWork.udcRepository.DeleteObject(deleteObject);
            this.processStatus = CreateProcessStatus.Delete;
            return this as IFluentUdc;
        }
   	public IFluentUdc DeleteUdcs(List<Udc> deleteObjects)
        {
            unitOfWork.udcRepository.DeleteObjects(deleteObjects);
            this.processStatus = CreateProcessStatus.Delete;
            return this as IFluentUdc;
        }
    }
}
