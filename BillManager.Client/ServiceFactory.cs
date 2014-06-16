using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BillManager.Client.ServiceContracts;
using BillManager.Client.ServiceProxies;
using Core.ServiceModel.Contracts;
using Microsoft.Practices.Unity;

namespace BillManager.Client
{
    public class ServiceFactory : IServiceFactory
    {
        IUnityContainer container;
        public ServiceFactory(IUnityContainer container)
        {
            this.container = container;
            container.RegisterType<IAccountService, AccountServiceClient>();
            container.RegisterType<IPaymentService, PaymentServiceClient>();            
        }
        public T CreateServiceClient<T>() where T : IServiceContract
        {
            return container.Resolve<T>();
        }
    }
}
