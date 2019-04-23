using Company.Repository.Infrastructure.Abstract;
using Company.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Repository.Infrastructure.Repository.Concrete
{
    public class EmployeeRepository : Repository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(DbContext dbContextParam) : base(dbContextParam)
        {
        }
    }
}
