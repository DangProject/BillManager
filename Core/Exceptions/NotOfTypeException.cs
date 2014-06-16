using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Exceptions
{
    [Serializable]
    public class NotOfTypeException : ApplicationException
    {
        public NotOfTypeException(string message)
            : base(message)
        {
        }

        public NotOfTypeException(string message, Exception exception)
            : base(message, exception)
        {
        }
    }
}
