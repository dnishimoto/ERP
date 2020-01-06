using lssWebApi2.AutoMapper;
using lssWebApi2.PackingSlipDetailDomain;
using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using lssWebApi2.MapperAbstract;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace lssWebApi2.PackingSlipDomain
{
public class FluentPackingSlipQuery:MapperAbstract<PackingSlip,PackingSlipView>,IFluentPackingSlipQuery
    {
        private UnitOfWork _unitOfWork = null;
        public FluentPackingSlipQuery() { }
        public FluentPackingSlipQuery(UnitOfWork unitOfWork) { _unitOfWork = unitOfWork; }

 public override async Task<PackingSlip> MapToEntity(PackingSlipView inputObject)
        {
  
            PackingSlip outObject = mapper.Map<PackingSlip>(inputObject);
            await Task.Yield();
            return outObject;
        }

  public override async Task<IList<PackingSlip>> MapToEntity(IList<PackingSlipView> inputObjects)
        {
            IList<PackingSlip> list = new List<PackingSlip>();

            foreach (var item in inputObjects)
            {
                PackingSlip outObject = mapper.Map<PackingSlip>(item);
                list.Add(outObject);
            }
            await Task.Yield();
            return list;

        }
        private async Task<PackingSlipDetailView> MapToPackingSlipDetailView(PackingSlipDetail inputObject)
        {

            PackingSlipDetailView outObject = mapper.Map<PackingSlipDetailView>(inputObject);
            await Task.Yield();
            return outObject;
        }
        public override async Task<PackingSlipView> MapToView(PackingSlip inputObject)
        {

            PackingSlipView outObject = mapper.Map<PackingSlipView>(inputObject);

            IList<PackingSlipDetail> list = await _unitOfWork.packingSlipDetailRepository.GetEntitiesByPackingSlipId(inputObject.PackingSlipId);
            List<PackingSlipDetailView> viewsList = new List<PackingSlipDetailView>();
            foreach (var item in list)
            {
                viewsList.Add(await MapToPackingSlipDetailView(item));
            }

            outObject.PackingSlipDetailViews = viewsList;

            return outObject;
        }
      
        
  public async Task<NextNumber>GetNextNumber()
        {
            return await _unitOfWork.packingSlipRepository.GetNextNumber(TypeOfPackingSlip.PackingSlipNumber.ToString());
        }
 public override async Task<PackingSlipView> GetViewById(long ? packingSlipId)
        {
            PackingSlip detailItem = await _unitOfWork.packingSlipRepository.GetEntityById(packingSlipId);

            return await MapToView(detailItem);
        }
 public async Task<PackingSlipView> GetViewByNumber(long packingSlipNumber)
        {
            PackingSlip detailItem = await _unitOfWork.packingSlipRepository.GetEntityByNumber(packingSlipNumber);

            return await MapToView(detailItem);
        }

public override async Task<PackingSlip> GetEntityById(long ? packingSlipId)
        {
            return await _unitOfWork.packingSlipRepository.GetEntityById(packingSlipId);

        }
 public async Task<PackingSlip> GetEntityByNumber(long packingSlipNumber)
        {
            return await _unitOfWork.packingSlipRepository.GetEntityByNumber(packingSlipNumber);
        }
}
}
