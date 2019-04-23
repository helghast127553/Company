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
    public class CredentialRepository : Repository<Credential>, ICredentialRepository
    {
        public CredentialRepository(DbContext dbContextParam) : base(dbContextParam)
        {
        }

        public CompanyEntities CompanyEntities { get => dbContext as CompanyEntities; }

        public IEnumerable<string> GetCredentials(string userName)
        {
            var user = CompanyEntities.Employees.Single(x => x.Username.Equals(userName));
            var data = (from a in CompanyEntities.Credentials
                        join b in CompanyEntities.UserGroups on a.UserGroupID equals b.UserGroupID
                        join c in CompanyEntities.Roles on a.RoleID equals c.RoleID
                        where b.UserGroupID == user.UserGroupID
                        select new
                        {
                            RoleID = a.RoleID,
                            UserGroupID = a.UserGroupID
                        }).AsEnumerable().Select(x => new Credential()
                        {
                            RoleID = x.RoleID,
                            UserGroupID = x.UserGroupID
                        });
            return data.Select(x => x.RoleID).ToList();
        }
    }
}
