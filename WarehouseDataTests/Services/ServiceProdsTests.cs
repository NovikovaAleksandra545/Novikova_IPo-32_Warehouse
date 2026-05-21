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
    // Проверка класса ServiceProds: конструктора
    // и методов Add, Update, Delete для товаров на складе,
    // и методов Add, Update, Delete для товаров в накладных 
    [TestClass()]
    public class ServiceProdsTests
    {
        [TestMethod()]
        public void ServiceProdsTest()
        {
            ApplicationContext context = new ApplicationContext();

            Warehouse warehouse =
                new Warehouse("Склад 100", "Там", context.orgs.First().OrgId);

            ServiceProds svc = new ServiceProds(context, warehouse);

            Assert.IsNotNull(svc);
            Assert.IsInstanceOfType(svc, typeof(ServiceProds));
        }

        [TestMethod()]
        public void AddTest()
        {
            ApplicationContext context = new ApplicationContext();

            Warehouse warehouse =
                new Warehouse("Склад 100", "Там", context.orgs.First().OrgId);

            ServiceProds svc = new ServiceProds(context, warehouse);

            Product product = new Product();
            product.Article = "A001";

            Assert.IsTrue(svc.Add(product));
            Assert.AreEqual(1, warehouse.products.Count());
        }

        [TestMethod()]
        public void UpdateTest()
        {
            ApplicationContext context = new ApplicationContext();

            Warehouse warehouse =
                new Warehouse("Склад 100", "Там", context.orgs.First().OrgId);

            ServiceProds svc = new ServiceProds(context, warehouse);

            Product product = new Product();
            product.Article = "A001";
            product.Name = "Товар 1";

            svc.Add(product);

            Product? prod =
                warehouse.products.FirstOrDefault(
                    p => p.Article == "A001");

            Assert.IsNotNull(prod);

            prod.Name = "Новый товар";

            Assert.IsTrue(svc.Update(prod));
        }

        [TestMethod()]
        public void DeleteTest()
        {
            ApplicationContext context = new ApplicationContext();

            Warehouse warehouse =
                new Warehouse("Склад 100", "Там", context.orgs.First().OrgId);

            ServiceProds svc = new ServiceProds(context, warehouse);

            Product product = new Product();
            product.Article = "A001";

            svc.Add(product);

            Product? prod = warehouse.products.FirstOrDefault();

            Assert.IsNotNull(prod);

            Assert.IsTrue(svc.Delete(prod));
            Assert.AreEqual(0, warehouse.products.Count());
        }

        [TestMethod()]
        public void AddInOrderTest()
        {
            ApplicationContext context = new ApplicationContext();

            Order order = new Order();

            ServiceProds svc = new ServiceProds(context, order);

            Product product = new Product();
            product.Article = "A001";

            Assert.IsTrue(svc.AddInOrder(product));
            Assert.AreEqual(1, order.Products.Count());
        }

        [TestMethod()]
        public void UpdateInOrderTest()
        {
            ApplicationContext context = new ApplicationContext();

            Order order = new Order();

            ServiceProds svc = new ServiceProds(context, order);

            Product product = new Product();
            product.Article = "A001";
            product.Name = "Товар 1";

            svc.AddInOrder(product);

            Product? prod =
                order.Products.FirstOrDefault(
                    p => p.Article == "A001");

            Assert.IsNotNull(prod);

            prod.Name = "Новый товар";

            Assert.IsTrue(svc.UpdateInOrder(prod));
        }

        [TestMethod()]
        public void DeleteInOrderTest()
        {
            ApplicationContext context = new ApplicationContext();

            Order order = new Order();

            ServiceProds svc = new ServiceProds(context, order);

            Product product = new Product();
            product.Article = "A001";

            svc.AddInOrder(product);

            Product? prod = order.Products.FirstOrDefault();

            Assert.IsNotNull(prod);

            Assert.IsTrue(svc.DeleteInOrder(prod));
            Assert.AreEqual(0, order.Products.Count());
        }
    }
}