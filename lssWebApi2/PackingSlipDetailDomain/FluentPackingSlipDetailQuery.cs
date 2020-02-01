using lssWebApi2.AutoMapper;
using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using lssWebApi2.MapperAbstract;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace lssWebApi2.PackingSlipDetailDomain
{
public class FluentPackingSlipDetailQuery:MapperAbstract<PackingSlipDetail,PackingSlipDetailView>,IFluentPackingSlipDetailQuery
    {
        private UnitOfWork _unitOfWork = null;
        public FluentPackingSlipDetailQuery() { }
        public FluentPackingSlipDetailQuery(UnitOfWork unitOfWork) { _unitOfWork = unitOfWork; }

 public override async Task<PackingSlipDetail> MapToEntity(PackingSlipDetailView inputObject)
        {
 
            PackingSlipDetail outObject = mapper.Map<PackingSlipDetail>(inputObject);
            await Task.Yield();
            return outObject;
        }

  public override async Task<IList<PackingSlipDetail>> MapToEntity(IList<PackingSlipDetailView> inputObjects)
        {
            IList<PackingSlipDetail> list = new List<PackingSlipDetail>();

            foreach (var item in inputObjects)
            {
                PackingSlipDetail outObject = mapper.Map<PackingSlipDetail>(item);
                list.Add(outObject);
            }
            await Task.Yield();
            return list;

        }

 public override async Task<PackingSlipDetailView> MapToView(PackingSlipDetail inputObject)
        {

            PackingSlipDetailView outObject = mapper.Map<PackingSlipDetailView>(inputObject);
            ItemMaster itemMaster = await _unitOfWork.itemMasterRepository.GetEntityById(inputObject.ItemId);

            outObject.ItemCode = itemMaster.ItemCode;
            outObject.Branch = itemMaster.Branch;
            outObject.ItemDescription = itemMaster.Description;

            return outObject;
        }
      
        
  public async Task<NextNumber>GetNextNumber()
        {
            return await _unitOfWork.nextNumberRepository.GetNextNumber(TypeOfPackingSlipDetail.PackingSlipDetailNumber.ToString());
        }
 public override async Task<PackingSlipDetailView> GetViewById(long ? packingSlipDetailId)
        {
            PackingSlipDetail detailItem = await _unitOfWork.packingSlipDetailRepository.GetEntityById(packingSlipDetailId);

            return await MapToView(detailItem);
        }
 public async Task<PackingSlipDetailView> GetViewByNumber(long packingSlipDetailNumber)
        {
            PackingSlipDetail detailItem = await _unitOfWork.packingSlipDetailRepository.GetEntityByNumber(packingSlipDetailNumber);

            return await MapToView(detailItem);
        }

public override async Task<PackingSlipDetail> GetEntityById(long ? packingSlipDetailId)
        {
            return await _unitOfWork.packingSlipDetailRepository.GetEntityById(packingSlipDetailId);

        }
 public async Task<PackingSlipDetail> GetEntityByNumber(long packingSlipDetailNumber)
        {
            return await _unitOfWork.packingSlipDetailRepository.GetEntityByNumber(packingSlipDetailNumber);
        }
}
}
