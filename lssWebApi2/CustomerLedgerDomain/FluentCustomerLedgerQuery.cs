using lssWebApi2.AutoMapper;
using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using lssWebApi2.MapperAbstract;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lssWebApi2.CustomerLedgerDomain
{
    public class FluentCustomerLedgerQuery : MapperAbstract<CustomerLedger,CustomerLedgerView>,IFluentCustomerLedgerQuery
    {
        private UnitOfWork _unitOfWork = null;
        public FluentCustomerLedgerQuery() { }
        public FluentCustomerLedgerQuery(UnitOfWork unitOfWork) { _unitOfWork = unitOfWork; }

        public override async Task<CustomerLedger> MapToEntity(CustomerLedgerView inputObject)
        {
          
            CustomerLedger outObject = mapper.Map<CustomerLedger>(inputObject);
            await Task.Yield();
            return outObject;
        }

        public override async Task<List<CustomerLedger>> MapToEntity(List<CustomerLedgerView> inputObjects)
        {
            List<CustomerLedger> list = new List<CustomerLedger>();
           
            foreach (var item in inputObjects)
            {
                CustomerLedger outObject = mapper.Map<CustomerLedger>(item);
                list.Add(outObject);
            }
            await Task.Yield();
            return list;

        }

        public override async Task<CustomerLedgerView> MapToView(CustomerLedger inputObject)
        {
           
            CustomerLedgerView outObject = mapper.Map<CustomerLedgerView>(inputObject);

            AddressBook addressBook = null;
            Task<Customer> customerTask =  _unitOfWork.customerRepository.GetEntityById(inputObject.CustomerId);
            Task<Invoice> invoiceTask =  _unitOfWork.invoiceRepository.GetEntityById(inputObject.InvoiceId);
            Task<AccountReceivable> accountReceivableTask = _unitOfWork.accountReceivableRepository.GetEntityById(inputObject.AcctRecId);
            Task<ChartOfAccount> chartOfAccountTask =  _unitOfWork.chartOfAccountRepository.GetEntityById(inputObject.AccountId);
            Task<GeneralLedger> generalLedgerTask =  _unitOfWork.generalLedgerRepository.GetEntityById(inputObject.GeneralLedgerId);

            Task.WaitAll(customerTask, invoiceTask, accountReceivableTask, chartOfAccountTask, generalLedgerTask);

            if (customerTask.Result != null) addressBook = await _unitOfWork.addressBookRepository.GetEntityById(customerTask.Result.AddressId);


            outObject.CustomerName = addressBook?.Name;
            outObject.InvoiceDocument = invoiceTask.Result.InvoiceDocument;
            outObject.Account = chartOfAccountTask.Result.Account;
            outObject.AccountDescription = chartOfAccountTask.Result.Description;
            


            return outObject;
        }

        public async Task<IList<CustomerLedgerView>> GetViewsByCustomerId(long customerId)
        {
            List<CustomerLedger> list = (await _unitOfWork.customerLedgerRepository.GetEntitiesByCustomerId(customerId)).ToList<CustomerLedger>();
            List<CustomerLedgerView> retList = new List<CustomerLedgerView>();

            list.ForEach(async e => retList.Add(await MapToView(e)));
            return retList;
        }

        public async Task<NextNumber> GetNextNumber()
        {
            return await _unitOfWork.customerLedgerRepository.GetNextNumber(TypeOfCustomerLedger.CustomerLedgerNumber.ToString());
        }
        public override async Task<CustomerLedgerView> GetViewById(long ? customerLedgerId)
        {
            CustomerLedger detailItem = await _unitOfWork.customerLedgerRepository.GetEntityById(customerLedgerId);

            return await MapToView(detailItem);
        }
        public async Task<CustomerLedgerView> GetViewByNumber(long customerLedgerNumber)
        {
            CustomerLedger detailItem = await _unitOfWork.customerLedgerRepository.GetEntityByNumber(customerLedgerNumber);

            return await MapToView(detailItem);
        }

        public override async Task<CustomerLedger> GetEntityById(long ? customerLedgerId)
        {
            return await _unitOfWork.customerLedgerRepository.GetEntityById(customerLedgerId);

        }
        public async Task<CustomerLedger> GetEntityByNumber(long customerLedgerNumber)
        {
            return await _unitOfWork.customerLedgerRepository.GetEntityByNumber(customerLedgerNumber);
        }
    }
}
