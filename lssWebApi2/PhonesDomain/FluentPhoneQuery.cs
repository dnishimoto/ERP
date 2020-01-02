using lssWebApi2.AutoMapper;
using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using lssWebApi2.MapperAbstract;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace lssWebApi2.AddressBookDomain
{
    public class FluentPhoneQuery : MapperAbstract<PhoneEntity,PhoneEntityView>, IFluentPhoneQuery
    {
        private UnitOfWork _unitOfWork = null;
        public FluentPhoneQuery() { }
        public FluentPhoneQuery(UnitOfWork unitOfWork) { _unitOfWork = unitOfWork; }

        public override async Task<PhoneEntity> MapToEntity(PhoneEntityView inputObject)
        {
           
            PhoneEntity outObject = mapper.Map<PhoneEntity>(inputObject);
            await Task.Yield();
            return outObject;
        }

        public override async Task<List<PhoneEntity>> MapToEntity(List<PhoneEntityView> inputObjects)
        {
            List<PhoneEntity> list = new List<PhoneEntity>();

            foreach (var item in inputObjects)
            {
                PhoneEntity outObject = mapper.Map<PhoneEntity>(item);
                list.Add(outObject);
            }
            await Task.Yield();
            return list;

        }

        public override async Task<PhoneEntityView> MapToView(PhoneEntity inputObject)
        {
 
            PhoneEntityView outObject = mapper.Map<PhoneEntityView>(inputObject);
            AddressBook addressBook = await _unitOfWork.addressBookRepository.GetEntityById(inputObject.AddressId);

            outObject.Name = addressBook.Name;

            return outObject;
        }

        public async Task<IList<PhoneEntity>> GetPhonesByAddressId(long ? addressId)
        {
            try
            {
                IList<PhoneEntity> result =  await _unitOfWork.phoneRepository.GetEntitiesByAddressId(addressId);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("GetPhonesByAddressId", ex);

            }
        }
        public async Task<NextNumber> GetNextNumber()
        {
            return await _unitOfWork.phoneRepository.GetNextNumber(TypeOfPhoneEntity.PhoneEntityNumber.ToString());
        }
        public override async Task<PhoneEntityView> GetViewById(long ? phonesId)
        {
            PhoneEntity detailItem = await _unitOfWork.phoneRepository.GetEntityById(phonesId);

            return await MapToView(detailItem);
        }
        public async Task<PhoneEntityView> GetViewByNumber(long phonesNumber)
        {
            PhoneEntity detailItem = await _unitOfWork.phoneRepository.GetEntityByNumber(phonesNumber);

            return await MapToView(detailItem);
        }

        public override async Task<PhoneEntity> GetEntityById(long ? phonesId)
        {
            return await _unitOfWork.phoneRepository.GetEntityById(phonesId);

        }
        public async Task<PhoneEntity> GetEntityByNumber(long phonesNumber)
        {
            return await _unitOfWork.phoneRepository.GetEntityByNumber(phonesNumber);
        }
        public async Task<IList<PhoneEntityView>> GetPhoneEntityViewsByCustomerId(long? customerId)
        {

            IList<PhoneEntity> list = await _unitOfWork.phoneRepository.GetPhonesByCustomerId(customerId);
            IList<PhoneEntityView> views = new List<PhoneEntityView>();
            foreach (var item in list)
            {
                views.Add(await MapToView(item));
            }
            return views;
        }
    }
}
