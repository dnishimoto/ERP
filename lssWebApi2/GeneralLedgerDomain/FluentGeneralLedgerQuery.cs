using lssWebApi2.AbstractFactory;
using lssWebApi2.AutoMapper;
using lssWebApi2.GeneralLedgerDomain;
using lssWebApi2.Interfaces;
using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using lssWebApi2.MapperAbstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;
using lssWebApi2.Enumerations;

namespace lssWebApi2.GeneralLedgerDomain
{
    public class FluentGeneralLedgerQuery : MapperAbstract<GeneralLedger,GeneralLedgerView>, IFluentGeneralLedgerQuery
    {
        UnitOfWork _unitOfWork = null;
        private ApplicationViewFactory applicationViewFactory = new ApplicationViewFactory();
        public FluentGeneralLedgerQuery(UnitOfWork unitOfWork) { _unitOfWork = unitOfWork; }

        public override async Task<GeneralLedger> GetEntityById(long? generalLedgerId)
        {
            return await _unitOfWork.generalLedgerRepository.GetEntityById(generalLedgerId);
        }
        public override async Task<GeneralLedgerView> GetViewById(long? generalLedgerId)
        {
            return await MapToView(await _unitOfWork.generalLedgerRepository.GetEntityById(generalLedgerId));
        }
        public async Task<IList<IncomeStatementView>> GetIncomeStatementViews(long fiscalYear)
        {
            IList<IncomeStatementView> views =  await _unitOfWork.generalLedgerRepository.GetIncomeStatementView(fiscalYear);
            return views;
          }
        public override async Task<GeneralLedger> MapToEntity(GeneralLedgerView inputObject)
        {
        
            GeneralLedger outObject = mapper.Map<GeneralLedger>(inputObject);
            await Task.Yield();
            return outObject;
        }

        public override async Task<IList<GeneralLedger>> MapToEntity(IList<GeneralLedgerView> inputObjects)
        {
            IList<GeneralLedger> list = new List<GeneralLedger>();
       
            foreach (var item in inputObjects)
            {
                GeneralLedger outObject = mapper.Map<GeneralLedger>(item);
                list.Add(outObject);
            }
            await Task.Yield();
            return list;

        }

       public override async Task<GeneralLedgerView> MapToView(GeneralLedger inputObject)
        {
   
            GeneralLedgerView outObject = mapper.Map<GeneralLedgerView>(inputObject);

            await Task.Yield();

            return outObject;
        }
        public async Task<NextNumber> GetDocNumber()
        {
            return await _unitOfWork.nextNumberRepository.GetNextNumber(TypeOfGeneralLedger.DocNumber.ToString());
        }
        public async Task<NextNumber> GetNextNumber()

        {
            return await _unitOfWork.nextNumberRepository.GetNextNumber(TypeOfGeneralLedger.GeneralLedgerNumber.ToString());
        }
        public async Task<GeneralLedgerView> GetViewByDocNumber(long? docNumber, string docType)
        {
         
            try
            {
                GeneralLedger entity = await _unitOfWork.generalLedgerRepository.GetEntityByDocNumber(docNumber, docType);

                GeneralLedgerView view = await MapToView(entity);
                return view;
            }
            catch (Exception ex)
            { throw new Exception(GetMyMethodName(), ex); }
        }

        public IEnumerable<AccountSummaryView> GetAccountSummaryByFiscalYearViews(long fiscalYear)
        {
            try
            {
                IEnumerable<AccountSummaryView> views = _unitOfWork.generalLedgerRepository.GetAccountSummaryByFiscalYearViews(fiscalYear);
                return views;
            }
            catch (Exception ex)

            { throw new Exception(GetMyMethodName(), ex); }
        }
        public async Task<IList<IncomeView>> GetIncomeViews()
        {
            IList<IncomeView> views =  await _unitOfWork.generalLedgerRepository.GetIncomeViews();
            return views;
        }
        public async Task<PageListViewContainer<GeneralLedgerView>> GetViewsByPage(Expression<Func<GeneralLedger, bool>> predicate, Expression<Func<GeneralLedger, object>> order, int pageSize, int pageNumber)
        {
            try
            {
                var query = _unitOfWork.generalLedgerRepository.GetEntitiesByExpression(predicate);
                query = query.OrderByDescending(order).Select(e => e);

                IPagedList<GeneralLedger> list = await query.ToPagedListAsync(pageNumber, pageSize);

                PageListViewContainer<GeneralLedgerView> container = new PageListViewContainer<GeneralLedgerView>();
                container.PageNumber = pageNumber;
                container.PageSize = pageSize;
                container.TotalItemCount = list.TotalItemCount;


                foreach (var item in list)
                {
                    GeneralLedgerView view = await MapToView(item);
                    container.Items.Add(view);
                }

                return container;
            }
            catch (Exception ex)
            {
                throw new Exception(GetMyMethodName(), ex);
            }

        }
        public async Task<GeneralLedgerView> GetLedgerViewByExpression(Expression<Func<GeneralLedger, bool>> predicate)
        {
            try
            {
                GeneralLedgerView view=null;

                GeneralLedger ledger = await _unitOfWork.generalLedgerRepository.FindEntityByExpression(predicate);


                if (ledger!=null)
                {
                    view = await MapToView(ledger);
                }
               
                return view;
            }
            catch (Exception ex)

            { throw new Exception(GetMyMethodName(), ex); }
        }
       
    }
}
