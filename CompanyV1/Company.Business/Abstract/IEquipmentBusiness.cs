using Company.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Business.Abstract
{
    public interface IEquipmentBusiness
    {
        IEnumerable<EquipmentDomainModel> GetAllEquipment();
        IEnumerable<EquipmentDomainModel> GetAllEquipment(int ID);
        EquipmentDomainModel GetEquipmentByID(int ID);
        bool ChangeStatus(int ID);

        int AddEquipment(EquipmentDomainModel equipmentDomainModel);
        int UpdateEquipment(EquipmentDomainModel equipmentDomainModel);
        int DeleteEquipment(int ID);
    }
}
