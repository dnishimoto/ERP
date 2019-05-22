﻿using ERP_Core2.AccountPayableDomain;
using ERP_Core2.Services;
using lssWebApi2.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lssWebApi2.SalesOrderManagementDomain
{
    public class FluentSalesOrderDetail : IFluentSalesOrderDetail
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private CreateProcessStatus processStatus;

        public FluentSalesOrderDetail() { }
        public IFluentSalesOrderDetailQuery Query()
        {
            return new FluentSalesOrderDetailQuery(unitOfWork) as IFluentSalesOrderDetailQuery;
        }
        public IFluentSalesOrderDetail Apply()
        {
            if (this.processStatus == CreateProcessStatus.Insert || this.processStatus == CreateProcessStatus.Update || this.processStatus == CreateProcessStatus.Delete)
            { unitOfWork.CommitChanges(); }
            return this as IFluentSalesOrderDetail;
        }
        public IFluentSalesOrderDetail AddSalesOrderDetails(List<SalesOrderDetail> newObjects)
        {
            unitOfWork.salesOrderDetailRepository.AddObjects(newObjects);
            this.processStatus = CreateProcessStatus.Insert;
            return this as IFluentSalesOrderDetail;
        }
        public IFluentSalesOrderDetail UpdateSalesOrderDetails(List<SalesOrderDetail> newObjects)
        {
            foreach (var item in newObjects)
            {
                unitOfWork.salesOrderDetailRepository.UpdateObject(item);
            }
            this.processStatus = CreateProcessStatus.Update;
            return this as IFluentSalesOrderDetail;
        }
        public IFluentSalesOrderDetail AddSalesOrderDetail(SalesOrderDetail newObject)
        {
            unitOfWork.salesOrderDetailRepository.AddObject(newObject);
            this.processStatus = CreateProcessStatus.Insert;
            return this as IFluentSalesOrderDetail;
        }
        public IFluentSalesOrderDetail UpdateSalesOrderDetail(SalesOrderDetail updateObject)
        {
            unitOfWork.salesOrderDetailRepository.UpdateObject(updateObject);
            this.processStatus = CreateProcessStatus.Update;
            return this as IFluentSalesOrderDetail;

        }
        public IFluentSalesOrderDetail DeleteSalesOrderDetails(List<SalesOrderDetail> deleteObjects)
        {
            unitOfWork.salesOrderDetailRepository.DeleteObjects(deleteObjects);
            this.processStatus = CreateProcessStatus.Delete;
            return this as IFluentSalesOrderDetail;
        }
        public IFluentSalesOrderDetail DeleteSalesOrderDetail(SalesOrderDetail deleteObject)
        {
            unitOfWork.salesOrderDetailRepository.DeleteObject(deleteObject);
            this.processStatus = CreateProcessStatus.Delete;
            return this as IFluentSalesOrderDetail;
        }
    }
}
