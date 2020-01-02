
using lssWebApi2.AbstractFactory;
using lssWebApi2.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace lssWebApi2.ShipmentsDomain
{
public interface IShipmentRepository
    {
        Task<Shipment> GetEntityById(long ? shipmentsId);
	    Task<Shipment> GetEntityByNumber(long shipmentsNumber);
        IQueryable<Shipment> GetEntitiesByExpression(Expression<Func<Shipment, bool>> predicate);


    }
}
