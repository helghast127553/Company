using Company.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Business.Abstract
{
    public interface IUserBusiness
    {
        bool CheckUserName(string userName);
        bool CheckEmail(string email);
        int CheckLogin(string userName, string password, bool isLoginAdmin = false);
        EmployeeDomainModel GetEmployeeByUserName(string userName);

        int AddUser(EmployeeDomainModel employeeDomainModel);
    }
}
