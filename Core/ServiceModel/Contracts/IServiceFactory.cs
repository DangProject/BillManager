using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ServiceModel.Contracts
{
    public interface IServiceFactory
    {
        T CreateServiceClient<T>() where T : IServiceContract;
    }
}
