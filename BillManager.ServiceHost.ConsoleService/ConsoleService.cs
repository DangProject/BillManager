using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BillManager.Business;
using BillManager.Business.ServiceManagers;
using Core.MEF;
using SM = System.ServiceModel;

namespace BillManager.ServiceHost.ConsoleService
{
    class ConsoleService
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting up services...");
            Console.WriteLine("");

            MEF.Container = new Bootstapper().InitializeContainer();            

            SM.ServiceHost hostAccountServiceManager = new SM.ServiceHost(typeof(AccountServiceManager));
            SM.ServiceHost hostPaymentServiceManager = new SM.ServiceHost(typeof(PaymentServiceManager));
            //SM.ServiceHost hostTestServiceManager = new SM.ServiceHost(typeof(AccountServiceManager));

            StartService(hostAccountServiceManager, "AccountServiceManager");
            StartService(hostPaymentServiceManager, "PaymentServiceManager");
            //StartService(hostTestServiceManager, "TestServiceManager");

            Console.WriteLine("");
            Console.WriteLine("Press [Enter] to exit.");
            Console.ReadLine();
            Console.WriteLine("");

            StopService(hostAccountServiceManager, "AccountServiceManager");
            StopService(hostPaymentServiceManager, "PaymentServiceManager");
            //StopService(hostTestServiceManager, "TestServiceManager");
            Console.ReadLine();
        }

        static void StartService(SM.ServiceHost host, string serviceDescription)
        {
            host.Open();
            Console.WriteLine("Service '{0}' started.", serviceDescription);

            foreach (var endpoint in host.Description.Endpoints)
            {
                Console.WriteLine(string.Format("Listening on endpoint:"));
                Console.WriteLine(string.Format("Address: {0}", endpoint.Address.Uri.ToString()));
                Console.WriteLine(string.Format("Binding: {0}", endpoint.Binding.Name));
                Console.WriteLine(string.Format("Contract: {0}", endpoint.Contract.ConfigurationName));
            }

            Console.WriteLine();
        }

        static void StopService(SM.ServiceHost host, string serviceDescription)
        {
            host.Close();
            Console.WriteLine("Service '{0}' stopped.", serviceDescription);
        }
    }
}
