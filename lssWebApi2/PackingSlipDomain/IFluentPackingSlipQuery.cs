using lssWebApi2.AutoMapper;
using lssWebApi2.PackingSlipDomain;
using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using System.Collections.Generic;
using System.Threading.Tasks;


public interface IFluentPackingSlipQuery
{
    Task<PackingSlip> MapToEntity(PackingSlipView inputObject);
    Task<IList<PackingSlip>> MapToEntity(IList<PackingSlipView> inputObjects);
    Task<PackingSlipView> MapToView(PackingSlip inputObject);
    Task<NextNumber> GetNextNumber();
    Task<PackingSlip> GetEntityById(long ? packingSlipId);
    Task<PackingSlip> GetEntityByNumber(long packingSlipNumber);
    Task<PackingSlipView> GetViewById(long ? packingSlipId);
    Task<PackingSlipView> GetViewByNumber(long packingSlipNumber);
}
