using lssWebApi2.AutoMapper;
using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using lssWebApi2.MapperAbstract;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace lssWebApi2.EquipmentDomain
{
public class FluentEquipmentQuery:MapperAbstract<Equipment,EquipmentView>,IFluentEquipmentQuery
    {
        private UnitOfWork _unitOfWork = null;
        public FluentEquipmentQuery() { }
        public FluentEquipmentQuery(UnitOfWork unitOfWork) { _unitOfWork = unitOfWork; }

 public override async Task<Equipment> MapToEntity(EquipmentView inputObject)
        {
      
            Equipment outObject = mapper.Map<Equipment>(inputObject);
            await Task.Yield();
            return outObject;
        }

  public override async Task<IList<Equipment>> MapToEntity(IList<EquipmentView> inputObjects)
        {
            IList<Equipment> list = new List<Equipment>();
   
            foreach (var item in inputObjects)
            {
                Equipment outObject = mapper.Map<Equipment>(item);
                list.Add(outObject);
            }
            await Task.Yield();
            return list;

        }

 public override async Task<EquipmentView> MapToView(Equipment inputObject)
        {
      
            EquipmentView outObject = mapper.Map<EquipmentView>(inputObject);
            await Task.Yield();
            return outObject;
        }
      
        
  public async Task<NextNumber>GetNextNumber()
        {
            return await _unitOfWork.equipmentRepository.GetNextNumber(TypeOfEquipment.EquipmentNumber.ToString());
        }
 public override async Task<EquipmentView> GetViewById(long ? equipmentId)
        {
            Equipment detailItem = await _unitOfWork.equipmentRepository.GetEntityById(equipmentId);

            return await MapToView(detailItem);
        }
 public async Task<EquipmentView> GetViewByNumber(long equipmentNumber)
        {
            Equipment detailItem = await _unitOfWork.equipmentRepository.GetEntityByNumber(equipmentNumber);

            return await MapToView(detailItem);
        }

public override async Task<Equipment> GetEntityById(long ? equipmentId)
        {
            return await _unitOfWork.equipmentRepository.GetEntityById(equipmentId);

        }
 public async Task<Equipment> GetEntityByNumber(long equipmentNumber)
        {
            return await _unitOfWork.equipmentRepository.GetEntityByNumber(equipmentNumber);
        }
}
}
