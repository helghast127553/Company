using Company.Repository.Entities;
using Company.Repository.Infrastructure.Abstract;
using Company.Repository.Infrastructure.Repository.Concrete;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Repository.Infrastructure.Concrete
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(DbContext dbContextParam) : base(dbContextParam)
        {
        }
    }
}
