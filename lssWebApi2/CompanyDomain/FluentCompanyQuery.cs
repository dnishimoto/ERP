using lssWebApi2.AutoMapper;
using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using lssWebApi2.MapperAbstract;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace lssWebApi2.CompanyDomain
{
public class FluentCompanyQuery:MapperAbstract<Company,CompanyView>,IFluentCompanyQuery
    {
        private UnitOfWork _unitOfWork = null;
        public FluentCompanyQuery() { }
        public FluentCompanyQuery(UnitOfWork unitOfWork) { _unitOfWork = unitOfWork; }

 public override async Task<Company> MapToEntity(CompanyView inputObject)
        {
         
            Company outObject = mapper.Map<Company>(inputObject);
            await Task.Yield();
            return outObject;
        }

  public override async Task<IList<Company>> MapToEntity(IList<CompanyView> inputObjects)
        {
            IList<Company> list = new List<Company>();
           
            foreach (var item in inputObjects)
            {
                Company outObject = mapper.Map<Company>(item);
                list.Add(outObject);
            }
            await Task.Yield();
            return list;

        }

 public override async Task<CompanyView> MapToView(Company inputObject)
        {
      
            CompanyView outObject = mapper.Map<CompanyView>(inputObject);
            await Task.Yield();
            return outObject;
        }
      
        
  public async Task<NextNumber>GetNextNumber()
        {
            return await _unitOfWork.nextNumberRepository.GetNextNumber(TypeOfCompany.CompanyNumber.ToString());
        }
 public override  async Task<CompanyView> GetViewById(long ? companyId)
        {
            Company detailItem = await _unitOfWork.companyRepository.GetEntityById(companyId);

            return await MapToView(detailItem);
        }
 public async Task<CompanyView> GetViewByNumber(long companyNumber)
        {
            Company detailItem = await _unitOfWork.companyRepository.GetEntityByNumber(companyNumber);

            return await MapToView(detailItem);
        }

public override async Task<Company> GetEntityById(long ? companyId)
        {
            return await _unitOfWork.companyRepository.GetEntityById(companyId);

        }
 public async Task<Company> GetEntityByNumber(long companyNumber)
        {
            return await _unitOfWork.companyRepository.GetEntityByNumber(companyNumber);
        }
}
}
