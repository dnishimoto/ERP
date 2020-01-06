using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using System.Collections.Generic;
using System;
using lssWebApi2.GeneralLedgerDomain;
using System.Threading.Tasks;
using lssWebApi2.AbstractFactory;
using lssWebApi2.SupplierInvoiceDomain;
using lssWebApi2.Enumerations;
using System.Data.SqlClient;
using lssWebApi2.AutoMapper;
using Microsoft.EntityFrameworkCore;


namespace lssWebApi2.SupplierLedgerDomain
{

    public class FluentSupplierLedger : IFluentSupplierLedger
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private CreateProcessStatus processStatus;
        private ApplicationViewFactory applicationViewFactory = new ApplicationViewFactory();

        public FluentSupplierLedger() { }
        public IFluentSupplierLedgerQuery Query()
        {
            return new FluentSupplierLedgerQuery(unitOfWork) as IFluentSupplierLedgerQuery;
        }
        public IFluentSupplierLedger UpdateSupplierLedgerByGeneralLedgerView(GeneralLedgerView ledgerView)

        {
            Task<SupplierLedger> supplierLedgerTask = Task.Run(async () => await unitOfWork.supplierLedgerRepository.GetEntityByDocNumber(ledgerView.DocNumber, ledgerView.DocType));
            Task.WaitAll(supplierLedgerTask);

            supplierLedgerTask.Result.SupplierId = ledgerView.SupplierId ?? 0;
            supplierLedgerTask.Result.InvoiceId = ledgerView.InvoiceId ?? 0;
            supplierLedgerTask.Result.AcctPayId = ledgerView.AcctPayId ?? 0;
            supplierLedgerTask.Result.DocNumber = ledgerView.DocNumber;
            supplierLedgerTask.Result.DocType = ledgerView.DocType;
            supplierLedgerTask.Result.Amount = ledgerView.Amount;
            supplierLedgerTask.Result.Gldate = ledgerView.GLDate;
            supplierLedgerTask.Result.CreatedDate = ledgerView.CreatedDate;
            supplierLedgerTask.Result.AccountId = ledgerView.AccountId;
            supplierLedgerTask.Result.AddressId = ledgerView.AddressId;
            supplierLedgerTask.Result.Comment = ledgerView.Comment;
            supplierLedgerTask.Result.DebitAmount = ledgerView.DebitAmount;
            supplierLedgerTask.Result.CreditAmount = ledgerView.CreditAmount;
            supplierLedgerTask.Result.FiscalPeriod = ledgerView.FiscalPeriod;
            supplierLedgerTask.Result.FiscalYear = ledgerView.FiscalYear;
            supplierLedgerTask.Result.GeneralLedgerId = ledgerView.GeneralLedgerId;

            UpdateSupplierLedger(supplierLedgerTask.Result);

            return this as IFluentSupplierLedger;

        }

        public IFluentSupplierLedger CreateEntityByView(SupplierLedgerView view)
        {
            try
            {
                //SupplierLedger supplierLedger = new SupplierLedger();

                Task<SupplierLedger> supplierLedgerQueryTask = Task.Run(async () => await unitOfWork.supplierLedgerRepository.GetEntityByView(view));
                Task.WaitAll(supplierLedgerQueryTask);


                if (supplierLedgerQueryTask.Result == null)
                {
                    SupplierLedger supplierLedger = MapToEntity(view);
                    

                    AddSupplierLedger(supplierLedger);

                    processStatus = CreateProcessStatus.Insert;

                    return this as IFluentSupplierLedger;


                }
                processStatus = CreateProcessStatus.AlreadyExists;
                return this as IFluentSupplierLedger;
            }
            catch (Exception ex)
            { throw new Exception("CreateEntityByView", ex); }

        }
        private SupplierLedger MapToEntity(SupplierLedgerView inputObject)
        {
            Mapper mapper = new Mapper();
            SupplierLedger outObject = mapper.Map<SupplierLedger>(inputObject);

            return outObject;
        }
        public IFluentSupplierLedger UpdateEntityByView(SupplierLedgerView supplierLedgerView)

        {
            try
            {
                Task<SupplierLedger> supplierLedgerBaseTask = Task.Run(async () => await unitOfWork.supplierLedgerRepository.GetEntityById(supplierLedgerView.SupplierLedgerId));
                Task.WaitAll(supplierLedgerBaseTask);
                SupplierLedger supplierLedgerBase = supplierLedgerBaseTask.Result;


                if (supplierLedgerBase != null)

                {
                    applicationViewFactory.MapSupplierLedgerEntity(ref supplierLedgerBase, supplierLedgerView);
                    UpdateSupplierLedger(supplierLedgerBase);

                    processStatus = CreateProcessStatus.Update;
                    return this as IFluentSupplierLedger;
                }

                processStatus = CreateProcessStatus.Failed;
                return this as IFluentSupplierLedger;

            }
            catch (Exception ex)
            {
                throw new Exception("UpdateEntityByView", ex);
            }

        }
        public IFluentSupplierLedger UpdateBalanceByAccountId(long? accountId, int? fiscalYear, int? fiscalPeriod)
        {

            try
            {
                SqlParameter param1 = new SqlParameter("@AccountId", accountId);
                SqlParameter param2 = new SqlParameter("@FiscalPeriod", fiscalPeriod);
                SqlParameter param3 = new SqlParameter("@FiscalYear", fiscalYear);
                //params Object[] parameters;


                var result = Task.Run(async () => await unitOfWork.supplierLedgerRepository._dbContext.Database.ExecuteSqlCommandAsync("usp_RollupCustomerLedgerBalance @AccountId, @FiscalPeriod, @FiscalYear", param1, param2, param3));


                return this as IFluentSupplierLedger;
            }
            catch (Exception ex) { throw new Exception("UpdateBalanceByAccountId", ex); }
        }


        public IFluentSupplierLedger Apply()
        {
            try
            {
                if (this.processStatus == CreateProcessStatus.Insert || this.processStatus == CreateProcessStatus.Update || this.processStatus == CreateProcessStatus.Delete)
                { unitOfWork.CommitChanges(); }
                return this as IFluentSupplierLedger;
            }
            catch (Exception ex) { throw new Exception("Apply", ex); }
        }
        public IFluentSupplierLedger AddSupplierLedgers(List<SupplierLedger> newObjects)
        {
            unitOfWork.supplierLedgerRepository.AddObjects(newObjects);
            this.processStatus = CreateProcessStatus.Insert;
            return this as IFluentSupplierLedger;
        }
        public IFluentSupplierLedger UpdateSupplierLedgers(IList<SupplierLedger> newObjects)
        {
            foreach (var item in newObjects)
            {
                unitOfWork.supplierLedgerRepository.UpdateObject(item);
            }
            this.processStatus = CreateProcessStatus.Update;
            return this as IFluentSupplierLedger;
        }
        public IFluentSupplierLedger AddSupplierLedger(SupplierLedger newObject)
        {
            unitOfWork.supplierLedgerRepository.AddObject(newObject);
            this.processStatus = CreateProcessStatus.Insert;
            return this as IFluentSupplierLedger;
        }
        public IFluentSupplierLedger UpdateSupplierLedger(SupplierLedger updateObject)
        {
            unitOfWork.supplierLedgerRepository.UpdateObject(updateObject);
            this.processStatus = CreateProcessStatus.Update;
            return this as IFluentSupplierLedger;

        }
        public IFluentSupplierLedger DeleteSupplierLedger(SupplierLedger deleteObject)
        {
            unitOfWork.supplierLedgerRepository.DeleteObject(deleteObject);
            this.processStatus = CreateProcessStatus.Delete;
            return this as IFluentSupplierLedger;
        }
        public IFluentSupplierLedger DeleteSupplierLedgers(List<SupplierLedger> deleteObjects)
        {
            unitOfWork.supplierLedgerRepository.DeleteObjects(deleteObjects);
            this.processStatus = CreateProcessStatus.Delete;
            return this as IFluentSupplierLedger;
        }
        public IFluentSupplierLedger CreateSupplierLedgerWithGeneralLedgerView(GeneralLedgerView generalLedgerView)
        {
            Task<GeneralLedger> generalLedgerTask = Task.Run(() => unitOfWork.generalLedgerRepository.GetEntityByDocNumber(generalLedgerView.DocNumber, generalLedgerView.DocType));
            Task.WaitAll(generalLedgerTask);

            SupplierLedgerView supplierLedgerView = applicationViewFactory.MapSupplierLedgerView(generalLedgerView);
            supplierLedgerView.GeneralLedgerId = generalLedgerTask.Result.GeneralLedgerId;

            CreateEntityByView(supplierLedgerView);
            Apply();

            return this as IFluentSupplierLedger;
        }
        public IFluentSupplierLedger UpdateSupplierLedgerWithGeneralLedger(GeneralLedgerView generalLedgerView)
        {
            Task<SupplierLedgerView> supplierLedgerTask = Task.Run(async () => await Query().GetSupplierLedgerByDocNumber(generalLedgerView.DocNumber, generalLedgerView.DocType));
            Task.WaitAll(supplierLedgerTask);

            SupplierLedgerView supplierLedgerView = applicationViewFactory.MapSupplierLedgerView(generalLedgerView);

            supplierLedgerView.SupplierLedgerId = supplierLedgerTask.Result.SupplierLedgerId;

            UpdateEntityByView(supplierLedgerView);
            Apply();

            return this as IFluentSupplierLedger;
        }
       
    }
}
