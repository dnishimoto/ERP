using lssWebApi2.AbstractFactory;
using lssWebApi2.AutoMapper;
using lssWebApi2.InvoiceDetailDomain;
using lssWebApi2.InvoicesDomain;
using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using lssWebApi2.Interfaces;
using lssWebApi2.MapperAbstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lssWebApi2.FluentAPI
{
    public class FluentInvoiceQuery : MapperAbstract<Invoice, InvoiceView>, IFluentInvoiceQuery
    {
        public UnitOfWork _unitOfWork = null;
        private ApplicationViewFactory applicationViewFactory = new ApplicationViewFactory();

        public FluentInvoiceQuery() { }
        public FluentInvoiceQuery(UnitOfWork unitOfWork) { _unitOfWork = unitOfWork; }

        public override async Task<Invoice> MapToEntity(InvoiceView inputObject)
        {

            Invoice outObject = mapper.Map<Invoice>(inputObject);
            await Task.Yield();
            return outObject;
        }
        private async Task<InvoiceDetailView> MapToInvoiceDetailView(InvoiceDetail inputObject)
        {

            InvoiceDetailView outObject = mapper.Map<InvoiceDetailView>(inputObject);
            await Task.Yield();
            return outObject;
        }

        public override async Task<List<Invoice>> MapToEntity(List<InvoiceView> inputObjects)
        {
            List<Invoice> list = new List<Invoice>();

            foreach (var item in inputObjects)
            {
                Invoice outObject = mapper.Map<Invoice>(item);
                list.Add(outObject);
            }
            await Task.Yield();
            return list;

        }
        public override async Task<InvoiceView> MapToView(Invoice inputObject)
        {

            InvoiceView outObject = mapper.Map<InvoiceView>(inputObject);
            Supplier supplier = await _unitOfWork.supplierRepository.GetEntityById(inputObject.SupplierId);
            AddressBook addressBook = await _unitOfWork.addressBookRepository.GetEntityById(supplier?.AddressId);

            outObject.SupplierName = addressBook?.Name;

            IList<InvoiceDetail> list = await _unitOfWork.invoiceDetailRepository.GetEntitiesByInvoiceId(inputObject.InvoiceId);
            List<InvoiceDetailView> viewsList = new List<InvoiceDetailView>();
            foreach (var item in list)
            {
                viewsList.Add(await MapToInvoiceDetailView(item));
            }
            outObject.InvoiceDetailViews = viewsList;
            await Task.Yield();
            return outObject;
        }
        public override async Task<InvoiceView> GetViewById(long? invoiceId)
        {
            Invoice detailItem = await _unitOfWork.invoiceRepository.GetEntityById(invoiceId);

            return await MapToView(detailItem);
        }
        public async Task<InvoiceView> GetViewByNumber(long invoiceNumber)
        {
            Invoice detailItem = await _unitOfWork.invoiceRepository.GetEntityByNumber(invoiceNumber);

            return await MapToView(detailItem);
        }

        public override async Task<Invoice> GetEntityById(long? invoiceId)
        {
            return await _unitOfWork.invoiceRepository.GetEntityById(invoiceId);

        }
        public async Task<Invoice> GetEntityByNumber(long invoiceNumber)
        {
            return await _unitOfWork.invoiceRepository.GetEntityByNumber(invoiceNumber);
        }

        public List<InvoiceFlatView> GetInvoicesByDate(DateTime startInvoiceDate, DateTime endInvoiceDate)
        {
            return _unitOfWork.invoiceRepository.GetInvoicesByDate(startInvoiceDate, endInvoiceDate);
        }
        public async Task<IList<InvoiceView>> GetInvoiceViewsByCustomerId(long? customerId, long? invoiceId = null)
        {
            try
            {
                IEnumerable<Invoice> invoiceList = null;
                IQueryable<Invoice> resultList = await _unitOfWork.invoiceRepository.GetQueryableByCustomerId(customerId);

                IList<InvoiceView> list = new List<InvoiceView>();
                if (invoiceId != null)
                {
                    invoiceList = resultList.Where(f => f.InvoiceId == invoiceId);
                }
                else
                {
                    invoiceList = resultList;
                }

                foreach (var item in invoiceList)
                {
                    list.Add(await MapToView(item));
                }
                return list;
            }
            catch (Exception ex)
            { throw new Exception(GetMyMethodName(), ex); }

        }

    }
}
