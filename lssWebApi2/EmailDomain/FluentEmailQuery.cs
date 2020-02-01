using lssWebApi2.AutoMapper;
using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using lssWebApi2.MapperAbstract;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace lssWebApi2.EmailDomain
{
    public class FluentEmailQuery : MapperAbstract<EmailEntity, EmailEntityView>, IFluentEmailQuery
    {
        private UnitOfWork _unitOfWork = null;
        public FluentEmailQuery() { }
        public FluentEmailQuery(UnitOfWork unitOfWork) { _unitOfWork = unitOfWork; }

        public override async Task<EmailEntity> MapToEntity(EmailEntityView inputObject)
        {

            EmailEntity outObject = mapper.Map<EmailEntity>(inputObject);
            await Task.Yield();
            return outObject;
        }

        public override async Task<IList<EmailEntity>> MapToEntity(IList<EmailEntityView> inputObjects)
        {
            IList<EmailEntity> list = new List<EmailEntity>();

            foreach (var item in inputObjects)
            {
                EmailEntity outObject = mapper.Map<EmailEntity>(item);
                list.Add(outObject);
            }
            await Task.Yield();
            return list;

        }

        public override async Task<EmailEntityView> MapToView(EmailEntity inputObject)
        {

            EmailEntityView outObject = mapper.Map<EmailEntityView>(inputObject);
            await Task.Yield();
            return outObject;
        }

        public async Task<IList<EmailEntity>> GetEmailsByAddressId(long addressId)
        {
            try
            {
                IList<EmailEntity> result = await _unitOfWork.emailRepository.GetEmailsByAddressId(addressId);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("GetEmailsByAddressId", ex);
            }

        }
        public async Task<NextNumber> GetNextNumber()
        {
            return await _unitOfWork.nextNumberRepository.GetNextNumber(TypeOfEmailEntity.EmailEntityNumber.ToString());
        }
        public override async Task<EmailEntityView> GetViewById(long? emailId)
        {
            EmailEntity detailItem = await _unitOfWork.emailRepository.GetEntityById(emailId);

            return await MapToView(detailItem);
        }
        public async Task<EmailEntityView> GetViewByNumber(long emailNumber)
        {
            EmailEntity detailItem = await _unitOfWork.emailRepository.GetEntityByNumber(emailNumber);

            return await MapToView(detailItem);
        }

        public override async Task<EmailEntity> GetEntityById(long? emailId)
        {
            return await _unitOfWork.emailRepository.GetEntityById(emailId);

        }
        public async Task<EmailEntity> GetEntityByNumber(long emailNumber)
        {
            return await _unitOfWork.emailRepository.GetEntityByNumber(emailNumber);
        }
        public async Task<IList<EmailEntityView>> GetEmailsViewsByCustomerId(long? customerId)
        {
            IList<EmailEntity> list = await _unitOfWork.emailRepository.GetEmailByCustomerId(customerId);
            IList<EmailEntityView> views = new List<EmailEntityView>();
            foreach (var item in list)
            {
                views.Add(await MapToView(item));
            }
            return views;
        }
    }
}
