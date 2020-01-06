using lssWebApi2.AutoMapper;
using lssWebApi2.EquipmentDomain;
using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using System.Collections.Generic;
using System.Threading.Tasks;


public interface IFluentEquipmentQuery
{
    Task<Equipment> MapToEntity(EquipmentView inputObject);
    Task<IList<Equipment>> MapToEntity(IList<EquipmentView> inputObjects);
    Task<EquipmentView> MapToView(Equipment inputObject);
    Task<NextNumber> GetNextNumber();
    Task<Equipment> GetEntityById(long ? equipmentId);
    Task<Equipment> GetEntityByNumber(long equipmentNumber);
    Task<EquipmentView> GetViewById(long ? equipmentId);
    Task<EquipmentView> GetViewByNumber(long equipmentNumber);
}
