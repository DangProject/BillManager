using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BillManager.Business.Entities;
using BillManager.Data.Interfaces;
using Core.Data.Interfaces;
using Microsoft.Practices.Unity;
using NUnit.Framework;
using BillManager.Data;

namespace BillManager.Test
{
    [TestFixture]
    public class DBContextTest
    {
        [Test]
        public void CanInitializeDBContext()
        {
            using (var context = new BillManagerContext())
            {
                try
                {
                    context.Database.Initialize(force: true);
                }
                catch (Exception ex)
                {
                    Assert.Fail(ex.Message);
                }
            }
        }        
    }
}
