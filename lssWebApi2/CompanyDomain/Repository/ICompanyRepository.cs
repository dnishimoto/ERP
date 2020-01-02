

using lssWebApi2.EntityFramework;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq.Expressions;
using System;

namespace lssWebApi2.CompanyDomain
{
public interface ICompanyRepository
    {
        Task<Company> GetEntityById(long ? companyId);
	
    }
}
