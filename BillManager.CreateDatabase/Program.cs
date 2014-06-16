using BillManager.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillManager.CreateDatabase
{
    class Program
    {
        static void Main(string[] args)
        {
            GetAccounts();
            Console.WriteLine("Finished!");
            Console.ReadLine();
        }

        static void GetAccounts()
        {
            using (BillManagerContext context = new BillManagerContext())
            {
                var accounts = context.Accounts.ToList();
                foreach (var account in accounts)
                {
                    Console.WriteLine(account.FirstName);
                }
            }
        }
    }
}
