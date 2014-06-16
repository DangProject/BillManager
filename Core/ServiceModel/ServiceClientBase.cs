using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Core.ServiceModel
{
    public abstract class ServiceClientBase<T> : ClientBase<T> where T : class
    {
        public ServiceClientBase()
        {            
            //IContextChannel cc = ((IContextChannel)this.Channel);

            //TimeSpan t = cc.OperationTimeout;
            //cc.OperationTimeout = new TimeSpan(0, 5, 0);
            //t = cc.OperationTimeout;

            //was this only for adding the message header?

            //string userName = Thread.CurrentPrincipal.Identity.Name;
            //MessageHeader<string> header = new MessageHeader<string>(userName);

            //OperationContextScope contextScope =
            //                new OperationContextScope(InnerChannel);

            //OperationContext.Current.OutgoingMessageHeaders.Add(
            //                          header.GetUntypedHeader("String", "System"));
        }
    }
}
