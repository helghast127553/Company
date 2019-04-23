using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Repository.Infrastructure.Abstract
{
    public interface IUnitOfWork : IDisposable
    {
        IEmployeeRepository Employees { get; }
        IEquipmentRepository Equipments { get; }
        IUserGroupRepository UserGroup { get; }
        ICategoryRepository Categories { get; }
        ICredentialRepository Credentials{ get; }
        int Complete();
    }
}
