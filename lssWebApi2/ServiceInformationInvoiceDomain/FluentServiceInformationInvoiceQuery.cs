using lssWebApi2.AutoMapper;
using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using System.Collections.Generic;
using System.Threading.Tasks;
using lssWebApi2.MapperAbstract;
using System.Linq;
using System;

namespace lssWebApi2.ServiceInformationInvoiceDomain
{
public class FluentServiceInformationInvoiceQuery:MapperAbstract<ServiceInformationInvoice,ServiceInformationInvoiceView>,IFluentServiceInformationInvoiceQuery
    {
        private UnitOfWork _unitOfWork = null;
        public FluentServiceInformationInvoiceQuery() { }
        public FluentServiceInformationInvoiceQuery(UnitOfWork unitOfWork) { _unitOfWork = unitOfWork; }

 public override async Task<ServiceInformationInvoice> MapToEntity(ServiceInformationInvoiceView inputObject)
        {
            ServiceInformationInvoice outObject = mapper.Map<ServiceInformationInvoice>(inputObject);
            await Task.Yield();
            return outObject;
        }

  public override async Task<IList<ServiceInformationInvoice>> MapToEntity(IList<ServiceInformationInvoiceView> inputObjects)
        {
            IList<ServiceInformationInvoice> list = new List<ServiceInformationInvoice>();
            foreach (var item in inputObjects)
            {
                ServiceInformationInvoice outObject = mapper.Map<ServiceInformationInvoice>(item);
                list.Add(outObject);
            }
            await Task.Yield();
            return list;

        }

 public override async Task<ServiceInformationInvoiceView> MapToView(ServiceInformationInvoice inputObject)
        {
            ServiceInformationInvoiceView outObject = mapper.Map<ServiceInformationInvoiceView>(inputObject);
            await Task.Yield();
            return outObject;
        }

        public async Task<IList<ServiceInformationInvoiceView>> GetViewsByServiceId(long? serviceId)
        {
            IList<ServiceInformationInvoice> list = await _unitOfWork.serviceInformationInvoiceRepository.GetEntitiesByServiceId(serviceId);
            IList<ServiceInformationInvoiceView> views = new List<ServiceInformationInvoiceView>();
            foreach (var item in list)
            {
               views.Add(await MapToView(item));
            }
            return views;
        }
  public async Task<NextNumber>GetNextNumber()
        {
            return await _unitOfWork.serviceInformationInvoiceRepository.GetNextNumber(TypeOfServiceInformationInvoice.ServiceInformationInvoiceNumber.ToString());
        }
 public override async Task<ServiceInformationInvoiceView> GetViewById(long ? serviceInformationInvoiceId)
        {
            ServiceInformationInvoice detailItem = await _unitOfWork.serviceInformationInvoiceRepository.GetEntityById(serviceInformationInvoiceId);

            return await MapToView(detailItem);
        }
 public async Task<ServiceInformationInvoiceView> GetViewByNumber(long serviceInformationInvoiceNumber)
        {
            ServiceInformationInvoice detailItem = await _unitOfWork.serviceInformationInvoiceRepository.GetEntityByNumber(serviceInformationInvoiceNumber);

            return await MapToView(detailItem);
        }

public override async Task<ServiceInformationInvoice> GetEntityById(long ? serviceInformationInvoiceId)
        {
            return await _unitOfWork.serviceInformationInvoiceRepository.GetEntityById(serviceInformationInvoiceId);

        }
 public async Task<ServiceInformationInvoice> GetEntityByNumber(long serviceInformationInvoiceNumber)
        {
            return await _unitOfWork.serviceInformationInvoiceRepository.GetEntityByNumber(serviceInformationInvoiceNumber);
        }
}
}
