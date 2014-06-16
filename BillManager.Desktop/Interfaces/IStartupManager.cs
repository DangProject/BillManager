using System;

namespace BillManager.Desktop.Interfaces
{
    public interface IStartupManager
    {
        void LoadLogin(string userName, string password);
    }
}
