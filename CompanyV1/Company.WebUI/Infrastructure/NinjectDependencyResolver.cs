using Company.Business.Abstract;
using Company.Business.Concrete;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Company.WebUI.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel = null;

        public NinjectDependencyResolver(IKernel kernelParam)
        {
            kernel = kernelParam;
            AddBindings();
        }

        public object GetService(Type serviceType)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            throw new NotImplementedException();
        }

        public void AddBindings()
        {
            kernel.Bind<IEmployeeBusiness>().To<EmployeeBusiness>();
            kernel.Bind<IEquipmentBusiness>().To<EquipmentBusiness>();
            kernel.Bind<IUserGroupBusiness>().To<UserGroupBusiness>();
            kernel.Bind<IUserBusiness>().To<UserBusiness>();
            kernel.Bind<ICategoryBusiness>().To<CategoryBusiness>();
            kernel.Bind<ICredentialBusiness>().To<CredentialBusiness>();
        }
    }
}