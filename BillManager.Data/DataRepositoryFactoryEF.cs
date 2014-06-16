using Core.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using BillManager.Data.Interfaces;
using BillManager.Data.DataRepositories.EntityFrameworkRepositories;
using System.ComponentModel.Composition;
using Core.MEF;

namespace BillManager.Data
{
    [Export(typeof(IDataRepositoryFactory))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class DataRepositoryFactoryEF : IDataRepositoryFactory
    {
        //IUnityContainer container;
        //public DataRepositoryFactoryEF(IUnityContainer container)
        //{
        //    this.container = container;
        //    container.RegisterType<IAccountRepository, AccountRepositoryEF>();
        //    container.RegisterType<IBillRepository, BillRepositoryEF>();
        //    container.RegisterType<ICategoryRepository, CategoryRepositoryEF>();
        //    container.RegisterType<IPaymentRepository, PaymentRepositoryEF>();
        //    container.RegisterType<IWebsiteRepository, WebsiteRepositoryEF>();
        //}

        T IDataRepositoryFactory.GetDataRepository<T>()
        {
            return MEF.Container.GetExportedValue<T>();
            //return container.Resolve<T>();
        }
    }
}
