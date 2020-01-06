using lssWebApi2.AbstractFactory;
using lssWebApi2.Interfaces;
using lssWebApi2.AddressBookDomain;
using lssWebApi2.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using lssWebApi2.EntityFramework;
using lssWebApi2.AutoMapper;
using lssWebApi2.MapperAbstract;
using X.PagedList;

namespace lssWebApi2.FluentAPI
{
    public class FluentAddressBookQuery : MapperAbstract<AddressBook,AddressBookView>, IFluentAddressBookQuery
    {
        protected UnitOfWork _unitOfWork;
        public FluentAddressBookQuery(UnitOfWork unitOfWork) { _unitOfWork = unitOfWork; }

        public override async Task<AddressBook> MapToEntity(AddressBookView inputObject)
        {
            AddressBook outObject = mapper.Map<AddressBook>(inputObject);
            await Task.Yield();
            return outObject;
        }

        public override async Task<IList<AddressBook>> MapToEntity(IList<AddressBookView> inputObjects)
        {
            IList<AddressBook> list = new List<AddressBook>();
            foreach (var item in inputObjects)
            {
                AddressBook outObject = mapper.Map<AddressBook>(item);
                list.Add(outObject);
            }
            await Task.Yield();
            return list;

        }

       public override async Task<AddressBookView> MapToView(AddressBook inputObject)
        {
            AddressBookView outObject = mapper.Map<AddressBookView>(inputObject);
            await Task.Yield();
            return outObject;
        }

        public async Task<PageListViewContainer<AddressBookView>> GetViewsByPage(Expression<Func<AddressBook, bool>> predicate, Expression<Func<AddressBook, object>> order, int pageSize, int pageNumber)
        {
            try
            {
                var query = _unitOfWork.addressBookRepository.GetEntitiesByExpression(predicate);
                query = query.OrderByDescending(order).Select(e => e);

                IPagedList<AddressBook> list = await query.ToPagedListAsync(pageNumber, pageSize);

                PageListViewContainer<AddressBookView> container = new PageListViewContainer<AddressBookView>();
                container.PageNumber = pageNumber;
                container.PageSize = pageSize;
                container.TotalItemCount = list.TotalItemCount;


                foreach (var item in list)
                {
                    AddressBookView view = await MapToView(item);
                    container.Items.Add(view);
                }

                //await Task.Yield();
                return container;
            }
            catch (Exception ex)
            {
                throw new Exception(GetMyMethodName(), ex);
            }

        }

        public async Task<List<AddressBookView>> GetAddressBookByName(string namePattern)
        {
            try
            {
               List<AddressBook> list= _unitOfWork.addressBookRepository.GetEntityByName(namePattern);
                List<AddressBookView>views=new List<AddressBookView>();

                list.ForEach(async e => views.Add(await MapToView(e)));

                await Task.Yield();

                return views;
            }
            catch (Exception ex)
            {
                throw new Exception("GetAddressBookByName", ex);
            }

        }
        public override async Task<AddressBookView> GetViewById(long ? addressId)
        {
            try
            {
                AddressBookView result =  await MapToView(await _unitOfWork.addressBookRepository.GetEntityById(addressId));
             
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("GetViewById", ex);
            }
        }
        public override async Task<AddressBook> GetEntityById(long ? addressId)
        {
            try
            {
                AddressBook result =  await _unitOfWork.addressBookRepository.GetEntityById(addressId);
              
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("GetEntityById", ex);
            }

        }

        public IQueryable<AddressBook> GetAddressBooksByExpression(Expression<Func<AddressBook, bool>> predicate)
        {
            try
            {
                //queryableAddressBook = 
                return _unitOfWork.addressBookRepository.GetEntitiesByExpression(predicate);
                //return this as IAddressBookQuery;
            }
            catch (Exception ex)
            {
                throw new Exception("GetAddressBooksByExpression", ex);
            }
        }
        public async Task<long> GetAddressIdByCustomerId(long? customerId)
        {
            return await _unitOfWork.addressBookRepository.GetAddressIdByCustomerId(customerId);
        }
        public async Task<AddressBook> GetAddressBookbyEmail(string email)
        {
            return await _unitOfWork.addressBookRepository.GetEntityByAccountEmail(email);
        }
    }
}
