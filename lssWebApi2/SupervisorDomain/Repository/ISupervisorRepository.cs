

using lssWebApi2.EntityFramework;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq.Expressions;

using System;

namespace lssWebApi2.SupervisorDomain
{
public interface ISupervisorRepository
    {
        Task<Supervisor> GetEntityById(long ? supervisorId);
	    Task<Supervisor> GetEntityByNumber(long supervisorNumber);
      
    }
}
