using Company.Business.Abstract;
using Company.Domain;
using Company.Repository.Entities;
using Company.Repository.Infrastructure.Abstract;
using Company.Repository.Infrastructure.Repository.Concrete;
using System.Collections.Generic;
using System.Linq;

namespace Company.Business.Concrete
{
    public class EquipmentBusiness : IEquipmentBusiness
    {
        public IEnumerable<EquipmentDomainModel> GetAllEquipment(int ID)
        {
            using (IUnitOfWork unitOfWork = new UnitOfWork())
            {
                return unitOfWork.Equipments.Where(x => x.EmployeeID == ID)
                    .Select(x => new EquipmentDomainModel
                    {
                        Name = x.Name,
                    });
            }
        }
        public IEnumerable<EquipmentDomainModel> GetAllEquipment()
        {
            using (IUnitOfWork unitOfWork = new UnitOfWork())
            {
                return unitOfWork.Equipments.GetAll()
                    .Select(x => new EquipmentDomainModel
                    {
                        EquipmentID = x.EquipmentID,
                        Name = x.Name,
                        Description = x.Name,
                        Status = x.Status,
                        CategoryID = x.CategoryID
                    });
            }
        }

        public EquipmentDomainModel GetEquipmentByID(int ID)
        {
            using (IUnitOfWork unitOfWork = new UnitOfWork())
            {
                var equipment = unitOfWork.Equipments.GetByID(ID);
                var equipmentDomainModel = new EquipmentDomainModel
                {
                    EquipmentID = equipment.EquipmentID,
                    Name = equipment.Name,
                    Description = equipment.Description,
                    Status = equipment.Status,
                    CategoryID = equipment.CategoryID
                };

                return equipmentDomainModel;
            }
        }

        public int AddEquipment(EquipmentDomainModel equipmentDomainModel)
        {
            using (IUnitOfWork unitOfWork = new UnitOfWork())
            {
                var equipment = new Equipment
                {
                    Name = equipmentDomainModel.Name,
                    Description = equipmentDomainModel.Description,
                    Status = equipmentDomainModel.Status,
                    CategoryID = equipmentDomainModel.CategoryID
                };

                unitOfWork.Equipments.Add(equipment);
                return unitOfWork.Complete();
            }
        }

        public int UpdateEquipment(EquipmentDomainModel equipmentDomainModel)
        {
            using (IUnitOfWork unitOfWork = new UnitOfWork())
            {
                var equipment = unitOfWork.Equipments.GetByID(equipmentDomainModel.EquipmentID);
                equipment.Name = equipmentDomainModel.Name;
                equipment.Description = equipmentDomainModel.Description;
                equipment.Status = equipmentDomainModel.Status;
                equipment.CategoryID = equipmentDomainModel.CategoryID;

                return unitOfWork.Complete();
            }
        }

        public int DeleteEquipment(int ID)
        {
            using (IUnitOfWork unitOfWork = new UnitOfWork())
            {
                var equipment = unitOfWork.Equipments.GetByID(ID);

                unitOfWork.Equipments.Remove(equipment);
                return unitOfWork.Complete();
            }
        }

        public bool ChangeStatus(int ID)
        {
            using (IUnitOfWork unitOfWork = new UnitOfWork())
            {
                var equipment = unitOfWork.Equipments.GetByID(ID);
                equipment.Status = !equipment.Status.Value;

                unitOfWork.Complete();
                return equipment.Status.Value;
            }
        }  
    }
}

