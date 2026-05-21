using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseData.Context;
using WarehouseData.Models;
using WarehouseData.Services;

namespace WarehouseData.Services.Tests
{
    // Проверка конструктора
    // и методов Add, Update, Delete класса ServiceWhs
    [TestClass()]
    public class ServiceWhsTests
    {
        [TestMethod()]
        public void ServiceWhsTest()
        {
            ApplicationContext context = new ApplicationContext();

            Organization org = context.orgs.First();

            ServiceWhs svc = new ServiceWhs(context, org);

            Assert.IsNotNull(svc);
            Assert.IsInstanceOfType(svc, typeof(ServiceWhs));
        }

        [TestMethod()]
        public void AddTest()
        {
            ApplicationContext context = new ApplicationContext();

            Organization org = context.orgs.First();

            ServiceWhs svc = new ServiceWhs(context, org);

            Warehouse warehouse = new Warehouse("Склад 100", "Там", org.OrgId);

            Assert.IsTrue(svc.Add(warehouse));
            Assert.AreEqual(3, org.warehouses.Count());
        }

        [TestMethod()]
        public void UpdateTest()
        {
            ApplicationContext context = new ApplicationContext();

            Organization org = context.orgs.First();

            ServiceWhs svc = new ServiceWhs(context, org);

            Warehouse warehouse = new Warehouse("Склад 100", "Там", org.OrgId);

            svc.Add(warehouse);

            Warehouse? wh =
                org.warehouses.FirstOrDefault(
                    w => w.WhName == "Склад 100");

            Assert.IsNotNull(wh);

            wh.WhName = "Главный склад";

            Assert.IsTrue(svc.Update(wh));
        }

        [TestMethod()]
        public void DeleteTest()
        {
            ApplicationContext context = new ApplicationContext();

            Organization org = context.orgs.First();

            ServiceWhs svc = new ServiceWhs(context, org);

            Warehouse warehouse = new Warehouse("Склад 100", "Там", org.OrgId);

            svc.Add(warehouse);

            Warehouse? wh = org.warehouses.FirstOrDefault();

            Assert.IsNotNull(wh);

            Assert.IsTrue(svc.Delete(wh));
            Assert.AreEqual(2, org.warehouses.Count());
        }
    }
}