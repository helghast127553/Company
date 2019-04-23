using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Business.Abstract
{
    public interface ICredentialBusiness
    {
        IEnumerable<string> GetCredentials(string userName);
    }
}
