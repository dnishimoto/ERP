

using lssWebApi2.EntityFramework;
using System.Collections.Generic;
using lssWebApi2.CompanyDomain;

namespace lssWebApi2.CompanyDomain
{ 

public interface IFluentCompany
    {
        IFluentCompanyQuery Query();
        IFluentCompany Apply();
        IFluentCompany AddCompany(Company company);
        IFluentCompany UpdateCompany(Company company);
        IFluentCompany DeleteCompany(Company company);
     	IFluentCompany UpdateCompanys(List<Company> newObjects);
        IFluentCompany AddCompanys(List<Company> newObjects);
        IFluentCompany DeleteCompanys(List<Company> deleteObjects);
    }
}
