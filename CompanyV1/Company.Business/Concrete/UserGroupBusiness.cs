using Company.Business.Abstract;
using Company.Domain;
using Company.Repository.Infrastructure.Abstract;
using Company.Repository.Infrastructure.Repository.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Business.Concrete
{
    public class UserGroupBusiness : IUserGroupBusiness
    {
        public IEnumerable<UserGroupDomainModel> GetAllUserGroup()
        {
            using (IUnitOfWork unitOfWork = new UnitOfWork())
            {
                return unitOfWork.UserGroup.GetAll()
                    .Select(x => new UserGroupDomainModel
                    {
                        UserGroupID = x.UserGroupID,
                        Name = x.Name
                    }).ToList();
            }
        }
    }
}
