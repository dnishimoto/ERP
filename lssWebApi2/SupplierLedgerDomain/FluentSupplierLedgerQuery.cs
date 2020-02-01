using lssWebApi2.AutoMapper;
using lssWebApi2.CustomerLedgerDomain;
using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using lssWebApi2.MapperAbstract;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace lssWebApi2.SupplierLedgerDomain
{
    public class FluentSupplierLedgerQuery : MapperAbstract<SupplierLedger, SupplierLedgerView>, IFluentSupplierLedgerQuery
    {
        private UnitOfWork _unitOfWork = null;
        public FluentSupplierLedgerQuery() { }
        public FluentSupplierLedgerQuery(UnitOfWork unitOfWork) { _unitOfWork = unitOfWork; }

        public async Task<SupplierLedgerView> GetSupplierLedgerByDocNumber(long? docNumber, string docType)
        {
            SupplierLedger supplierLedger = await _unitOfWork.supplierLedgerRepository.GetEntityByDocNumber(docNumber, docType);
            SupplierLedgerView view = await MapToView(supplierLedger);
            return view;
        }

        public override async Task<SupplierLedger> MapToEntity(SupplierLedgerView inputObject)
        {

            SupplierLedger outObject = mapper.Map<SupplierLedger>(inputObject);
            await Task.Yield();
            return outObject;
        }

        public override async Task<IList<SupplierLedger>> MapToEntity(IList<SupplierLedgerView> inputObjects)
        {
            IList<SupplierLedger> list = new List<SupplierLedger>();

            foreach (var item in inputObjects)
            {
                SupplierLedger outObject = mapper.Map<SupplierLedger>(item);
                list.Add(outObject);
            }
            await Task.Yield();
            return list;

        }

        public override async Task<SupplierLedgerView> MapToView(SupplierLedger inputObject)
        {

            SupplierLedgerView outObject = mapper.Map<SupplierLedgerView>(inputObject);
            await Task.Yield();
            return outObject;
        }


        public async Task<NextNumber> GetNextNumber()
        {
            return await _unitOfWork.nextNumberRepository.GetNextNumber(TypeOfSupplierLedger.SupplierLedgerNumber.ToString());
        }
        public override async Task<SupplierLedgerView> GetViewById(long ? supplierLedgerId)
        {
            SupplierLedger detailItem = await _unitOfWork.supplierLedgerRepository.GetEntityById(supplierLedgerId);

            return await MapToView(detailItem);
        }
        public async Task<SupplierLedgerView> GetViewByNumber(long supplierLedgerNumber)
        {
            SupplierLedger detailItem = await _unitOfWork.supplierLedgerRepository.GetEntityByNumber(supplierLedgerNumber);

            return await MapToView(detailItem);
        }

        public override async Task<SupplierLedger> GetEntityById(long ? supplierLedgerId)
        {
            return await _unitOfWork.supplierLedgerRepository.GetEntityById(supplierLedgerId);

        }
        public async Task<SupplierLedger> GetEntityByNumber(long supplierLedgerNumber)
        {
            return await _unitOfWork.supplierLedgerRepository.GetEntityByNumber(supplierLedgerNumber);
        }
    }
}
