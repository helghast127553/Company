using Company.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Business.Abstract
{
    public interface IEmployeeBusiness
    {
        IEnumerable<EmployeeDomainModel> GetAllEmployee();
        IEnumerable<EmployeeDomainModel> GetAllEmployeeByID(int ID);
        EmployeeDomainModel GetEmployeeByID(int ID);

        //int AddEmployee(EmployeeDomainModel employeeDomainModel);
        int UpdateEmployee(EmployeeDomainModel employeeDomainModel);
        int DeleteEmployee(int ID);
    }
}
