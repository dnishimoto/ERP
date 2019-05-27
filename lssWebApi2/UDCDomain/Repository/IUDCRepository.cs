using lssWebApi2.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lssWebApi2.UDCDomain.Repository
{
    public interface IUDCRepository
    {
        Task<IQueryable<Udc>> GetUDCValuesByProductCode(string productCode);
    }
}
