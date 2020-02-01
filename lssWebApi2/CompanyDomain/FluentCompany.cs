using lssWebApi2.AccountPayableDomain;
using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using System.Collections.Generic;
using System;
using lssWebApi2.CompanyDomain;
using lssWebApi2.Enumerations;

namespace lssWebApi2.CompanyDomain
{

public class FluentCompany :IFluentCompany
    {
 private UnitOfWork unitOfWork;
        private CreateProcessStatus processStatus;

        public FluentCompany(UnitOfWork paramUnitOfWork) { unitOfWork = paramUnitOfWork; }
        public IFluentCompanyQuery Query()
        {
            return new FluentCompanyQuery(unitOfWork) as IFluentCompanyQuery;
        }
        public IFluentCompany Apply() {
			try{
            if (this.processStatus == CreateProcessStatus.Insert || this.processStatus == CreateProcessStatus.Update || this.processStatus == CreateProcessStatus.Delete)
            { unitOfWork.CommitChanges(); }
            return this as IFluentCompany;
		    }
            catch (Exception ex) { throw new Exception("Apply", ex); }
        }
        public IFluentCompany AddCompanys(List<Company> newObjects)
        {
            unitOfWork.companyRepository.AddObjects(newObjects);
            this.processStatus = CreateProcessStatus.Insert;
            return this as IFluentCompany;
        }
        public IFluentCompany UpdateCompanys(IList<Company> newObjects)
        {
            foreach (var item in newObjects)
            {
                unitOfWork.companyRepository.UpdateObject(item);
            }
            this.processStatus = CreateProcessStatus.Update;
            return this as IFluentCompany;
        }
        public IFluentCompany AddCompany(Company newObject) {
            unitOfWork.companyRepository.AddObject(newObject);
            this.processStatus = CreateProcessStatus.Insert;
            return this as IFluentCompany;
        }
        public IFluentCompany UpdateCompany(Company updateObject) {
            unitOfWork.companyRepository.UpdateObject(updateObject);
            this.processStatus = CreateProcessStatus.Update;
            return this as IFluentCompany;

        }
        public IFluentCompany DeleteCompany(Company deleteObject) {
            unitOfWork.companyRepository.DeleteObject(deleteObject);
            this.processStatus = CreateProcessStatus.Delete;
            return this as IFluentCompany;
        }
   	public IFluentCompany DeleteCompanys(List<Company> deleteObjects)
        {
            unitOfWork.companyRepository.DeleteObjects(deleteObjects);
            this.processStatus = CreateProcessStatus.Delete;
            return this as IFluentCompany;
        }
    }
}
