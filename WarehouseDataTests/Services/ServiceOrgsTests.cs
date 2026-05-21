using Microsoft.VisualStudio.TestTools.UnitTesting;
using WarehouseData.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseData.Context;
using WarehouseData.Models;

namespace WarehouseData.Services.Tests
{
    // Проверка конструктора, GetContext
    // и методов Add, Edit, Delete класса ServiceOrgs
    [TestClass()]
    public class ServiceOrgsTests
    {
        [TestMethod()]
        public void ServiceOrgsTest()
        {
            ApplicationContext context = new ApplicationContext();
            ServiceOrgs svc = new ServiceOrgs(context);

            Assert.IsNotNull(svc);
            Assert.IsInstanceOfType(svc, typeof(ServiceOrgs));
        }

        [TestMethod()]
        public void AddOrgTest()
        {
            ApplicationContext context = new ApplicationContext();
            ServiceOrgs svc = new ServiceOrgs(context);

            Organization org = new Organization("Всякая всячина, ООО");

            Assert.IsNotNull(org);
            Assert.IsTrue(svc.AddOrg(org));
            Assert.AreEqual(3, svc.GetContext().orgs.Count());
        }

        [TestMethod()]
        public void EditOrgTest()
        {
            ApplicationContext context = new ApplicationContext();
            ServiceOrgs svc = new ServiceOrgs(context);

            Organization org = new Organization("Всякая всячина, ООО");
            svc.AddOrg(org);

            Organization? org1 = svc.GetContext()
                .orgs.FirstOrDefault(o => o.OrgName == "Всякая всячина, ООО");

            Assert.IsNotNull(org1);

            org1.OrgName = "Разные разности";

            Assert.IsTrue(svc.EditOrg(org1));
        }

        [TestMethod()]
        public void DelOrgTest()
        {
            ApplicationContext context = new ApplicationContext();
            ServiceOrgs svc = new ServiceOrgs(context);

            Organization org = new Organization("Всякая всячина, ООО");
            svc.AddOrg(org);

            Organization? org1 = svc.GetContext().orgs.First();

            Assert.IsNotNull(org1);
            Assert.IsTrue(svc.DelOrg(org1));
            Assert.AreEqual(2, svc.GetContext().orgs.Count());
        }

        [TestMethod()]
        public void GetContextTest()
        {
            ApplicationContext context = new ApplicationContext();
            ServiceOrgs svc = new ServiceOrgs(context);

            Assert.IsInstanceOfType(svc.GetContext(), typeof(ApplicationContext));
        }
    }
}