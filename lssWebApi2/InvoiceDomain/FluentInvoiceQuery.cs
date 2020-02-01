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
using lssWebApi2.Enumerations;

namespace lssWebApi2.InvoiceDomain
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


        public override async Task<IList<Invoice>> MapToEntity(IList<InvoiceView> inputObjects)
        {
            IList<Invoice> list = new List<Invoice>();

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

            Task<Supplier> supplierTask =  _unitOfWork.supplierRepository.GetEntityById(inputObject?.SupplierId);
            Task<Customer> customerTask =  _unitOfWork.customerRepository.GetEntityById(inputObject?.CustomerId);
            Task<TaxRatesByCode> taxRatesByCodeTask = _unitOfWork.taxRateByCodeRepository.GetEntityById(inputObject.TaxRatesByCodeId);
            Task.WaitAll(supplierTask, customerTask, taxRatesByCodeTask);

            Task<AddressBook> addressBookSupplierTask =  _unitOfWork.addressBookRepository.GetEntityById(supplierTask.Result?.AddressId);
            Task<AddressBook> addressBookCustomerTask = _unitOfWork.addressBookRepository.GetEntityById(customerTask.Result?.AddressId);
            Task.WaitAll(addressBookSupplierTask,addressBookCustomerTask);

            outObject.SupplierName = addressBookSupplierTask.Result?.Name;
            outObject.CustomerName = addressBookCustomerTask.Result?.Name;
            outObject.TaxCode = taxRatesByCodeTask.Result.TaxCode;

            List<InvoiceDetailView> viewsList = new List<InvoiceDetailView>();
            FluentInvoiceDetail fluentInvoiceDetail = new FluentInvoiceDetail(_unitOfWork);
            IList<InvoiceDetail> list = await fluentInvoiceDetail.Query().GetEntitiesByInvoiceId(inputObject?.InvoiceId);

            foreach (var item in list)
            {
                viewsList.Add(await fluentInvoiceDetail.Query().MapToView(item));
            }
            outObject.InvoiceDetailViews = viewsList;
            await Task.Yield();
            return outObject;
        }
        public async Task<NextNumber> GetNextNumber()
        {
            return await _unitOfWork.nextNumberRepository.GetNextNumber(TypeOfInvoice.InvoiceNumber.ToString());
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
        public async Task<IList<Invoice>> GetEntitiesByPurchaseOrderId(long? purchaseOrderId)
        {
            return await _unitOfWork.invoiceRepository.GetEntitiesByExpression(e => e.PurchaseOrderId == purchaseOrderId);
        }
        public async Task<Invoice> GetEntityByInvoiceDocument(string invoiceDocument)
        {
            return await _unitOfWork.invoiceRepository.GetEntityByInvoiceDocument(invoiceDocument);
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
