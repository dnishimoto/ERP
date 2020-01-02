

using lssWebApi2.EntityFramework;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq.Expressions;
using System;

namespace lssWebApi2.EquipmentDomain
{
public interface IEquipmentRepository
    {
        Task<Equipment> GetEntityById(long ? equipmentId);
	    Task<Equipment> FindEntityByExpression(Expression<Func<Equipment, bool>> predicate);
		Task<IList<Equipment>> FindEntitiesByExpression(Expression<Func<Equipment, bool>> predicate);
    }
}
