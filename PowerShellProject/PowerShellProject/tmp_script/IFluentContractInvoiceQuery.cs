using lssWebApi2.AutoMapper;
using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace lssWebApi2.ContractInvoiceDomain
{
  public interface IFluentContractInvoiceQuery
  {
     Task<ContractInvoice> MapToEntity(ContractInvoiceView inputObject);
     Task<List<ContractInvoice>> MapToEntity(List<ContractInvoiceView> inputObjects);
     Task<ContractInvoiceView> MapToView(ContractInvoice inputObject);
     Task<NextNumber> GetNextNumber();
	 Task<ContractInvoice> GetEntityById(long ? contractInvoiceId);
	 Task<ContractInvoice> GetEntityByNumber(long contractInvoiceNumber);
	 Task<ContractInvoiceView> GetViewById(long ? contractInvoiceId);
	 Task<ContractInvoiceView> GetViewByNumber(long contractInvoiceNumber);
  }
}
