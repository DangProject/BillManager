using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BillManager.Client.Model;

namespace BillManager.Desktop
{
    public class SessionData
    {
        static Lazy<SessionData> lazy = new Lazy<SessionData>(() => new SessionData());

        public static SessionData Instance
        {
            get { return lazy.Value; }
        }

        public Account Account { get; set; }
    }
}
