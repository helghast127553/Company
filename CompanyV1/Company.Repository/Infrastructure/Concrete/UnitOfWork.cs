using Company.Repository.Infrastructure.Abstract;
using Company.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Company.Repository.Infrastructure.Concrete;

namespace Company.Repository.Infrastructure.Repository.Concrete
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CompanyEntities _dbContext;

        public UnitOfWork()
        {
            _dbContext = new CompanyEntities();
            Employees = new EmployeeRepository(_dbContext);
            Equipments = new EquipmentRepository(_dbContext);
            UserGroup = new UserGroupRepository(_dbContext);
            Categories = new CategoryRepository(_dbContext);
            Credentials = new CredentialRepository(_dbContext);
        }

        public IEmployeeRepository Employees { get; private set; }
        public IEquipmentRepository Equipments { get; private set; }
        public IUserGroupRepository UserGroup { get; private set; }
        public ICategoryRepository Categories { get; private set; }
        public ICredentialRepository Credentials { get; private set; }

        public int Complete()
        {
            return _dbContext.SaveChanges();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
