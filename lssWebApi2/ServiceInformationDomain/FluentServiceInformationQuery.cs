using lssWebApi2.AutoMapper;
using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using System.Collections.Generic;
using System.Threading.Tasks;
using lssWebApi2.MapperAbstract;
using System.Linq;
using System;

namespace lssWebApi2.ServiceInformationDomain
{
public class FluentServiceInformationQuery:MapperAbstract<ServiceInformation,ServiceInformationView>,IFluentServiceInformationQuery
    {
        private UnitOfWork _unitOfWork = null;
        public FluentServiceInformationQuery() { }
        public FluentServiceInformationQuery(UnitOfWork unitOfWork) { _unitOfWork = unitOfWork; }

 public override async Task<ServiceInformation> MapToEntity(ServiceInformationView inputObject)
        {
            ServiceInformation outObject = mapper.Map<ServiceInformation>(inputObject);
            await Task.Yield();
            return outObject;
        }

  public override async Task<IList<ServiceInformation>> MapToEntity(IList<ServiceInformationView> inputObjects)
        {
            IList<ServiceInformation> list = new List<ServiceInformation>();
            foreach (var item in inputObjects)
            {
                ServiceInformation outObject = mapper.Map<ServiceInformation>(item);
                list.Add(outObject);
            }
            await Task.Yield();
            return list;

        }

 public override async Task<ServiceInformationView> MapToView(ServiceInformation inputObject)
        {
            ServiceInformationView outObject = mapper.Map<ServiceInformationView>(inputObject);
            await Task.Yield();
            return outObject;
        }
      
        
  public async Task<NextNumber>GetNextNumber()
        {
            return await _unitOfWork.serviceInformationRepository.GetNextNumber(TypeOfServiceInformation.ServiceInformationNumber.ToString());
        }
 public override async Task<ServiceInformationView> GetViewById(long ? serviceInformationId)
        {
            ServiceInformation detailItem = await _unitOfWork.serviceInformationRepository.GetEntityById(serviceInformationId);

            return await MapToView(detailItem);
        }
 public async Task<ServiceInformationView> GetViewByNumber(long serviceInformationNumber)
        {
            ServiceInformation detailItem = await _unitOfWork.serviceInformationRepository.GetEntityByNumber(serviceInformationNumber);

            return await MapToView(detailItem);
        }

public override async Task<ServiceInformation> GetEntityById(long ? serviceInformationId)
        {
            return await _unitOfWork.serviceInformationRepository.GetEntityById(serviceInformationId);

        }
 public async Task<ServiceInformation> GetEntityByNumber(long serviceInformationNumber)
        {
            return await _unitOfWork.serviceInformationRepository.GetEntityByNumber(serviceInformationNumber);
        }
}
}
