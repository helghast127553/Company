using Company.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Business.Abstract
{
    public interface IUserGroupBusiness
    {
        IEnumerable<UserGroupDomainModel> GetAllUserGroup();
    }
}
