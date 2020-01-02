

using lssWebApi2.EntityFramework;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq.Expressions;
using System;

namespace lssWebApi2.ServiceInformationDomain
{
public interface IServiceInformationRepository
    {
        Task<ServiceInformation> GetEntityById(long ? serviceInformationId);
	    Task<ServiceInformation> FindEntityByExpression(Expression<Func<ServiceInformation, bool>> predicate);
		Task<IList<ServiceInformation>> FindEntitiesByExpression(Expression<Func<ServiceInformation, bool>> predicate);
    }
}
