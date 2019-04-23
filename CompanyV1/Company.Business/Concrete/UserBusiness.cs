using Company.Business.Abstract;
using Company.Domain;
using Company.Repository.Infrastructure.Repository.Concrete;
using Company.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Company.Repository.Infrastructure.Abstract;

namespace Company.Business.Concrete
{
    public class UserBusiness : IUserBusiness
    {
        public bool CheckEmail(string email)
        {
            using (IUnitOfWork unitOfWork = new UnitOfWork())
            {
                return unitOfWork.Employees.Count(x => x.Email.Equals(email)) > 0;
            }
        }

        public int CheckLogin(string userName, string password, bool isLoginAdmin = false)
        {
            using (IUnitOfWork unitOfWork = new UnitOfWork())
            {
                var result = unitOfWork.Employees.SingleOrDefault(x => x.Username.Equals(userName) );
                if (result != null)
                {
                    if (isLoginAdmin)
                    {
                        if (result.UserGroupID.Equals("ADMIN") || result.UserGroupID.Equals("MOD") || result.UserGroupID.Equals("MEMBER"))
                        {
                            if (BCrypt.Net.BCrypt.Verify(password, result.Password))
                            {
                                return 1;
                            }
                            else
                            {
                                return -1;
                            }
                        }
                        else
                        {
                            return -2;
                        }
                    }
                    else
                    {
                        if (BCrypt.Net.BCrypt.Verify(password, result.Password))
                        {
                            return 1;
                        }
                        else
                        {
                            return -1;
                        }
                    }
                }
                return -1;
            }
        }

        public bool CheckUserName(string userName)
        {
            using (IUnitOfWork unitOfWork = new UnitOfWork())
            {
                return unitOfWork.Employees.Count(x => x.Username.Equals(userName)) > 0;
            }
        }

        public EmployeeDomainModel GetEmployeeByUserName(string userName)
        {
            using (IUnitOfWork unitOfWork = new UnitOfWork())
            {
                var employee = unitOfWork.Employees.SingleOrDefault(x => x.Username.Equals(userName));
                var result = new EmployeeDomainModel
                {
                    EmployeeID = employee.EmployeeID,
                    Username = employee.Username,
                    UserGroupID = employee.UserGroupID
                };

                return result;
            }
        }

        public int AddUser(EmployeeDomainModel employeeDomainModel)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                var employee = new Employee
                {
                    Username = employeeDomainModel.Username,
                    Password = employeeDomainModel.Password,
                    Name = employeeDomainModel.Name,
                    BirthDate = employeeDomainModel.BirthDate.Value,
                    Address = employeeDomainModel.Address,
                    Email = employeeDomainModel.Email,
                    UserGroupID = "MEMBER"
                };

                unitOfWork.Employees.Add(employee);
                return unitOfWork.Complete();
            }
        }
    }
}
