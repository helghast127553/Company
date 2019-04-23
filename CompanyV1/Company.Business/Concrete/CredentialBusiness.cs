using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Company.Business.Abstract;
using Company.Repository.Infrastructure.Abstract;
using Company.Repository.Infrastructure.Repository.Concrete;

namespace Company.Business.Concrete
{
    public class CredentialBusiness: ICredentialBusiness
    {
        public IEnumerable<string> GetCredentials(string userName)
        {
            using (IUnitOfWork unitOfWork = new UnitOfWork())
            {
                return unitOfWork.Credentials.GetCredentials(userName);
            }
        }
    }
}
