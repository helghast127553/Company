using Company.Business.Abstract;
using Company.Domain;
using Company.Repository.Entities;
using Company.Repository.Infrastructure.Abstract;
using Company.Repository.Infrastructure.Repository.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Business.Concrete
{
    public class EmployeeBusiness : IEmployeeBusiness
    {
        public IEnumerable<EmployeeDomainModel> GetAllEmployeeByID(int ID)
        {
            using (IUnitOfWork unitOfWork = new UnitOfWork())
            {
                return unitOfWork.Employees.Where(x => x.EmployeeID == ID)
                    .Select(x => new EmployeeDomainModel
                    {
                        EmployeeID = x.EmployeeID,
                        Username = x.Username,
                        Name = x.Name,
                        BirthDate = x.BirthDate.Value,
                        Address = x.Address,
                        Email = x.Email,
                        EquipmentID = x.EquipmentID,
                        UserGroupID = x.UserGroupID
                    });
            }
        }
        public IEnumerable<EmployeeDomainModel> GetAllEmployee()
        {
            using (IUnitOfWork unitOfWork = new UnitOfWork())
            {
                return unitOfWork.Employees.GetAll()
                    .Select(x => new EmployeeDomainModel
                    {
                        EmployeeID = x.EmployeeID,
                        Username = x.Username,
                        Name = x.Name,
                        BirthDate = x.BirthDate.Value,
                        Address = x.Address,
                        Email = x.Email,
                        EquipmentID = x.EquipmentID,
                        UserGroupID = x.UserGroupID
                    });
            }
        }

        public EmployeeDomainModel GetEmployeeByID(int ID)
        {
            using (IUnitOfWork unitOfWork = new UnitOfWork())
            {
                var employee = unitOfWork.Employees.GetByID(ID);
                var employeeDomainModel = new EmployeeDomainModel
                {
                    EmployeeID = employee.EmployeeID,
                    Username = employee.Username,
                    Name = employee.Name,
                    BirthDate = employee.BirthDate.Value,
                    Address = employee.Address,
                    Email = employee.Email,
                    UserGroupID = employee.UserGroupID,
                    EquipmentID = employee.EquipmentID,
                };

                return employeeDomainModel;
            }
        }

        //public int AddEmployee(EmployeeDomainModel employeeDomainModel)
        //{
        //    using (IUnitOfWork unitOfWork = new UnitOfWork())
        //    {
        //        var employee = Mapper.Map<EmployeeDomainModel, Employee>(employeeDomainModel);
        //        unitOfWork.Employees.Add(employee);

        //        return unitOfWork.Complete();
        //    }
        //}

        public int UpdateEmployee(EmployeeDomainModel employeeDomainModel)
        {
            using (IUnitOfWork unitOfWork = new UnitOfWork())
            {
                var employee = unitOfWork.Employees.GetByID(employeeDomainModel.EmployeeID);
                employee.Username = employeeDomainModel.Username;
                employee.Name = employeeDomainModel.Name;
                employee.BirthDate = employeeDomainModel.BirthDate.Value;
                employee.Address = employeeDomainModel.Address;
                employee.Email = employeeDomainModel.Email;
                employee.UserGroupID = employeeDomainModel.UserGroupID;
                employee.EquipmentID = employeeDomainModel.EquipmentID;

                return unitOfWork.Complete();
            }
        }

        public int DeleteEmployee(int ID)
        {
            using (IUnitOfWork unitOfWork = new UnitOfWork())
            {
                var employee = unitOfWork.Employees.GetByID(ID);

                unitOfWork.Employees.Remove(employee);
                return unitOfWork.Complete();
            }
        }
    }
}
