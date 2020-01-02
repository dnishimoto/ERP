using lssWebApi2.AutoMapper;
using lssWebApi2.CompanyDomain;
using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using System.Collections.Generic;
using System.Threading.Tasks;


public interface IFluentCompanyQuery
{
    Task<Company> MapToEntity(CompanyView inputObject);
    Task<List<Company>> MapToEntity(List<CompanyView> inputObjects);
    Task<CompanyView> MapToView(Company inputObject);
    Task<NextNumber> GetNextNumber();
    Task<Company> GetEntityById(long  ? companyId);
    Task<Company> GetEntityByNumber(long companyNumber);
    Task<CompanyView> GetViewById(long ? companyId);
    Task<CompanyView> GetViewByNumber(long companyNumber);
}
