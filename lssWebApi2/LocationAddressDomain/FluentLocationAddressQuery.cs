using lssWebApi2.AutoMapper;
using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using lssWebApi2.MapperAbstract;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace lssWebApi2.LocationAddressDomain
{
public class FluentLocationAddressQuery:MapperAbstract<LocationAddress,LocationAddressView>,IFluentLocationAddressQuery
    {
        private UnitOfWork _unitOfWork = null;
        public FluentLocationAddressQuery() { }
        public FluentLocationAddressQuery(UnitOfWork unitOfWork) { _unitOfWork = unitOfWork; }

 public override async Task<LocationAddress> MapToEntity(LocationAddressView inputObject)
        {
         
            LocationAddress outObject = mapper.Map<LocationAddress>(inputObject);
            await Task.Yield();
            return outObject;
        }

  public override async Task<IList<LocationAddress>> MapToEntity(IList<LocationAddressView> inputObjects)
        {
            IList<LocationAddress> list = new List<LocationAddress>();
           
            foreach (var item in inputObjects)
            {
                LocationAddress outObject = mapper.Map<LocationAddress>(item);
                list.Add(outObject);
            }
            await Task.Yield();
            return list;

        }

 public override async Task<LocationAddressView> MapToView(LocationAddress inputObject)
        {
          
            LocationAddressView outObject = mapper.Map<LocationAddressView>(inputObject);
            Task<AddressBook> addressBookTask = _unitOfWork.addressBookRepository.GetEntityById(inputObject.AddressId);
            Task<Udc> udcTask = _unitOfWork.udcRepository.GetEntityById(inputObject.TypeXrefId);

            AddressBook addressBook = await addressBookTask;
            Udc udc = await udcTask;

            outObject.Name = addressBook.Name;
            outObject.Type = udc.Value;

            return outObject;
        }
      
        
  public async Task<NextNumber>GetNextNumber()
        {
            return await _unitOfWork.nextNumberRepository.GetNextNumber(TypeOfLocationAddress.LocationAddressNumber.ToString());
        }
 public override async Task<LocationAddressView> GetViewById(long ? locationAddressId)
        {
            LocationAddress detailItem = await _unitOfWork.locationAddressRepository.GetEntityById(locationAddressId);

            return await MapToView(detailItem);
        }
 public async Task<LocationAddressView> GetViewByNumber(long locationAddressNumber)
        {
            LocationAddress detailItem = await _unitOfWork.locationAddressRepository.GetEntityByNumber(locationAddressNumber);

            return await MapToView(detailItem);
        }

public override async Task<LocationAddress> GetEntityById(long ? locationAddressId)
        {
            return await _unitOfWork.locationAddressRepository.GetEntityById(locationAddressId);

        }
 public async Task<LocationAddress> GetEntityByNumber(long locationAddressNumber)
        {
            return await _unitOfWork.locationAddressRepository.GetEntityByNumber(locationAddressNumber);
        }
        public async Task<IList<LocationAddressView>> GetLocationAddressViewsByCustomerId(long? customerId)
        {

            IList<LocationAddress> list = await _unitOfWork.locationAddressRepository.GetLocationAddressByCustomerId(customerId);
            IList<LocationAddressView> views = new List<LocationAddressView>();
            foreach (var item in list)
            {
                views.Add(await MapToView(item));
            }
            return views;
        }
    }
}
