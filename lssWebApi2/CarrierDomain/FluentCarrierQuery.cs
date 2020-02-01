using lssWebApi2.AutoMapper;
using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using lssWebApi2.MapperAbstract;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace lssWebApi2.CarrierDomain
{
public class FluentCarrierQuery:MapperAbstract<Carrier,CarrierView>,IFluentCarrierQuery
    {
        private UnitOfWork _unitOfWork = null;
        public FluentCarrierQuery() { }
        public FluentCarrierQuery(UnitOfWork unitOfWork) { _unitOfWork = unitOfWork; }

 public override async Task<Carrier> MapToEntity(CarrierView inputObject)
        {
          
            Carrier outObject = mapper.Map<Carrier>(inputObject);
            await Task.Yield();
            return outObject;
        }

  public override async Task<IList<Carrier>> MapToEntity(IList<CarrierView> inputObjects)
        {
            IList<Carrier> list = new List<Carrier>();
         
            foreach (var item in inputObjects)
            {
                Carrier outObject = mapper.Map<Carrier>(item);
                list.Add(outObject);
            }
            await Task.Yield();
            return list;

        }

 public override async Task<CarrierView> MapToView(Carrier inputObject)
        {
           
            CarrierView outObject = mapper.Map<CarrierView>(inputObject);

            Task<AddressBook> addressBookTask =  _unitOfWork.addressBookRepository.GetEntityById(inputObject.AddressId);
            Task<Udc> udcCarrierTask =  _unitOfWork.udcRepository.GetEntityById(inputObject.CarrierTypeXrefId);
            Task.WaitAll(addressBookTask, udcCarrierTask);

            outObject.CarrierType = udcCarrierTask.Result.Value;
            outObject.CarrierName = addressBookTask.Result.Name;

            await Task.Yield();
            return outObject;
        }
      
        
  public async Task<NextNumber>GetNextNumber()
        {
            return await _unitOfWork.nextNumberRepository.GetNextNumber(TypeOfCarrier.CarrierNumber.ToString());
        }
 public override async Task<CarrierView> GetViewById(long ? carrierId)
        {
            Carrier detailItem = await _unitOfWork.carrierRepository.GetEntityById(carrierId);

            return await MapToView(detailItem);
        }
 public async Task<CarrierView> GetViewByNumber(long carrierNumber)
        {
            Carrier detailItem = await _unitOfWork.carrierRepository.GetEntityByNumber(carrierNumber);

            return await MapToView(detailItem);
        }

public override async Task<Carrier> GetEntityById(long ? carrierId)
        {
            return await _unitOfWork.carrierRepository.GetEntityById(carrierId);

        }
 public async Task<Carrier> GetEntityByNumber(long carrierNumber)
        {
            return await _unitOfWork.carrierRepository.GetEntityByNumber(carrierNumber);
        }
}
}
