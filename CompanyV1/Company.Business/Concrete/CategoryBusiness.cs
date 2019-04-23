using Company.Business.Abstract;
using Company.Domain;
using Company.Repository.Infrastructure.Abstract;
using Company.Repository.Infrastructure.Repository.Concrete;
using System.Collections.Generic;
using System.Linq;

namespace Company.Business.Concrete
{
    public class CategoryBusiness : ICategoryBusiness
    {
        public IEnumerable<CategoryDomainModel> GetAllCategory()
        {
            using (IUnitOfWork unitOfWork = new UnitOfWork())
            {
                return unitOfWork.Categories.GetAll()
                    .Select(x => new CategoryDomainModel
                    {
                        CategoryID = x.CategoryID,
                        Name = x.Name
                    }).ToList();
            }
        }
    }
}
