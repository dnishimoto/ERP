

using lssWebApi2.EntityFramework;
using System.Collections.Generic;
using lssWebApi2.EquipmentDomain;

namespace lssWebApi2.EquipmentDomain
{ 

public interface IFluentEquipment
    {
        IFluentEquipmentQuery Query();
        IFluentEquipment Apply();
        IFluentEquipment AddEquipment(Equipment equipment);
        IFluentEquipment UpdateEquipment(Equipment equipment);
        IFluentEquipment DeleteEquipment(Equipment equipment);
     	IFluentEquipment UpdateEquipments(IList<Equipment> newObjects);
        IFluentEquipment AddEquipments(List<Equipment> newObjects);
        IFluentEquipment DeleteEquipments(List<Equipment> deleteObjects);
    }
}
