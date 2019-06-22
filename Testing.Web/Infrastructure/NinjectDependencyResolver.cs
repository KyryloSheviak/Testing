using Ninject;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Testing.Domain.Interface;
using Testing.Domain.Repository;

namespace Testing.Web.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;

        public NinjectDependencyResolver(IKernel kernelParam)
        {
            kernel = kernelParam;
            AddBindings();
        }

        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }

        private void AddBindings()
        {
            kernel.Bind<IRepositoryUser>().To<TestingRepositoryUser>();
            kernel.Bind<IRepositoryAdmin>().To<TestingRepositoryAdmin>();
        }
    }
}