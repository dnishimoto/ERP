

using lssWebApi2.EntityFramework;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq.Expressions;
using System;
using System.Linq;

namespace lssWebApi2.UDCDomain
{
    public interface IUdcRepository
    {
        Task<Udc> GetEntityById(long? udcId);
        Task<IQueryable<Udc>> GetUDCValuesByProductCode(string productCode);
        Task<Udc> GetUdc(string productCode, string keyCode);
        }
}
