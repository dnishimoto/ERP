using lssWebApi2.AutoMapper;
using lssWebApi2.PackingSlipDetailDomain;
using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using System.Collections.Generic;
using System.Threading.Tasks;


public interface IFluentPackingSlipDetailQuery
{
        Task<PackingSlipDetail> MapToEntity(PackingSlipDetailView inputObject);
        Task<List<PackingSlipDetail>> MapToEntity(List<PackingSlipDetailView> inputObjects);
        Task<PackingSlipDetailView> MapToView(PackingSlipDetail inputObject);
        Task<NextNumber> GetNextNumber();
	Task<PackingSlipDetail> GetEntityById(long ? packingSlipDetailId);
	  Task<PackingSlipDetail> GetEntityByNumber(long packingSlipDetailNumber);
	Task<PackingSlipDetailView> GetViewById(long ? packingSlipDetailId);
	Task<PackingSlipDetailView> GetViewByNumber(long packingSlipDetailNumber);
}
