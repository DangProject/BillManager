using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BillManager.Business.Entities;
using Core;
using Core.Extensions;
using NUnit.Framework;

namespace BillManager.Test.DataTest
{
    [TestFixture]
    public class GeneralTest
    {
        [Test]
        public void TestTimes()
        {
            Debug.WriteLine(TimeSpan.FromMinutes(-1).ToString());
        }
        [Test]
        public void TestDateFormat()
        {
            DateTime date = DateTime.Now;

            Debug.WriteLine(date.ToString("ddd M/d"));
        }
        [Test]
        public void TestSimpleMapper()
        {
            IEnumerable<Website> websites = new List<Website>()
            {
                new Website() { Description = "Chase" },
                new Website() { Description = "Bill Pay" }
            };

            IList<Client.Model.Website> destination = new List<Client.Model.Website>();

            //SimpleMapper.PropertyMap(websites, destination);

            websites.ForEach(w => destination.Add(EntityMapper.PropertyMap<Website, Client.Model.Website>(w)));
                //.Add(SimpleMapper.PropertyMap(w, new Website())));
        }
    }
}
