using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Core.UI
{
    public class ServiceClientConsumerBase
    {
        protected void UsingServiceClient<T>(T proxy, Action<T> codeToExecute)
        {
            codeToExecute.Invoke(proxy);

            IDisposable disposibleClient = proxy as IDisposable;
            if (disposibleClient != null)
                disposibleClient.Dispose();
        }

        protected R UsingServiceClient<T, R>(T proxy, Func<T, R> codeToExecute)
        {
            R result = codeToExecute.Invoke(proxy);

            IDisposable disposibleClient = proxy as IDisposable;
            if (disposibleClient != null)
                disposibleClient.Dispose();

            return result;
        }

        protected void FaultHandledOperation(Action codeToExecute)
        {
            try
            {
                codeToExecute.Invoke();
            }
            catch (FaultException e)
            {
                MessageBox.Show(e.Message, string.Empty, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
