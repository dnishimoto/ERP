using lssWebApi2.AutoMapper;
using lssWebApi2.Services;
using lssWebApi2.UDCDomain;
using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


public interface IFluentUdcQuery
{
    Task<Udc> MapToEntity(UdcView inputObject);
    Task<IList<Udc>> MapToEntity(IList<UdcView> inputObjects);
    Task<UdcView> MapToView(Udc inputObject);
    Task<NextNumber> GetNextNumber();
    Task<Udc> GetEntityById(long ? udcId);
    Task<Udc> GetEntityByNumber(long udcNumber);
    Task<UdcView> GetViewById(long ? udcId);
    Task<UdcView> GetViewByNumber(long udcNumber);
    Task<IQueryable<Udc>> GetUDCValuesByProductCode(string productCode);
    Task<Udc> GetUdc(string productCode, string keyCode);
}
