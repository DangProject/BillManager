using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Exceptions
{    
    [Serializable]
    public class NotFoundException : ApplicationException
    {
        public NotFoundException(string message) 
            : base(message)
        {
        }

        public NotFoundException(string message, Exception exception)
            :base(message, exception)
        {
        }
    }
}
