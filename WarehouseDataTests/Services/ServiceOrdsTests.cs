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
    // и методов Add, Approve, Delete класса ServiceOrds
    [TestClass()]
    public class ServiceOrdsTests
    {
        [TestMethod()]
        public void ServiceOrdsTest()
        {
            ApplicationContext context = new ApplicationContext();

            Warehouse warehouse = 
                new Warehouse("Склад 100", "Там", context.orgs.First().OrgId);

            ServiceOrds svc = new ServiceOrds(context, warehouse);

            Assert.IsNotNull(svc);
            Assert.IsInstanceOfType(svc, typeof(ServiceOrds));
        }


        [TestMethod()]
        public void AddTest()
        {
            ApplicationContext context = new ApplicationContext();

            Warehouse warehouse =
                new Warehouse("Склад 100", "Там", context.orgs.First().OrgId);

            ServiceOrds svc = new ServiceOrds(context, warehouse);

            Order order = new Order();
            order.Number = "НАКЛ-001";

            Assert.IsTrue(svc.Add(order));
            Assert.AreEqual(1, warehouse.Orders.Count());
        }

        [TestMethod()]
        public void ApproveTest()
        {
            ApplicationContext context = new ApplicationContext();

            Warehouse warehouse =
                new Warehouse("Склад 100", "Там", context.orgs.First().OrgId);

            ServiceOrds svc = new ServiceOrds(context, warehouse);

            Order order = new Order();
            order.Number = "НАКЛ-001";
            order.Type = OrderType.Incoming;

            Product product = new Product();
            product.Article = "A001";
            product.StockQuantity = 10;

            order.Products.Add(product);

            svc.Add(order);

            Assert.IsTrue(svc.Approve(order));
            Assert.AreEqual(OrderStatus.Approved, order.Status);
            Assert.AreEqual(1, warehouse.products.Count());
        }

        [TestMethod()]
        public void DeleteTest()
        {
            ApplicationContext context = new ApplicationContext();

            Warehouse warehouse =
                new Warehouse("Склад 100", "Там", context.orgs.First().OrgId);

            ServiceOrds svc = new ServiceOrds(context, warehouse);

            Order order = new Order();
            order.Number = "НАКЛ-001";

            svc.Add(order);

            Assert.IsTrue(svc.Delete(order));
            Assert.AreEqual(0, warehouse.Orders.Count());
        }
    }
}