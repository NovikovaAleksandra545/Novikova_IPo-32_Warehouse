using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseData.Models;

namespace WarehouseData.Context
{
    public class ApplicationContext
    {
        public ObservableCollection<Organization> orgs;
        //public ObservableCollection<Warehouse> whs;

        public ApplicationContext()
        {
            orgs = new ObservableCollection<Organization>();
            //whs = new ObservableCollection<Warehouse>();

            OrgsFill();
        }

        public List<Category> Categories { get; set; }
        public List<Manufacturer> Manufacturers { get; set; }
        public List<Supplier> Suppliers { get; set; }

        public void OrgsFill()
        {
            Organization org1 = new Organization("Poгa и копыта, 000"); 
            Organization org2 = new Organization("Пупкин и сыновья, 000");

            Warehouse wh1 = new Warehouse("Склад Рогов и Копыт №1",
            "Tам, за лесом.", org1.OrgId);
            Warehouse wh2 = new Warehouse("Склад Рогов и Копыт №2",
            "Tам, за лесом.", org1.OrgId);
            Warehouse wh3 = new Warehouse("Склад Пупкина №1",
            "Там, за горой.", org2.OrgId);
            Warehouse wh4 = new Warehouse("Cклад Пупкина №2", 
                "Taм, зa гopoй.", org2.OrgId);

            // Справочники
            Category catFood = new Category { Id = 1, Name = "Продукты" };
            Category catTech = new Category { Id = 2, Name = "Техника" };
            Categories = new List<Category>
            {
                    catFood,
                    catTech
            };

            Manufacturer manSamsung = new Manufacturer { Id = 1, Name = "Samsung" };
            Manufacturer manNestle = new Manufacturer { Id = 2, Name = "Nestle" };
            Manufacturers = new List<Manufacturer>
            {
                manSamsung,
                manNestle
            };

            Supplier supMetro = new Supplier { Id = 1, Name = "Metro" };
            Supplier supDns = new Supplier { Id = 2, Name = "DNS" };
            Suppliers = new List<Supplier>
            {
                supMetro,
                supDns
            };

            // Товары для склада 1
            wh1.products.Add(new Product
            {
                Article = "MILK001",
                Name = "Молоко 3.2%",
                Unit = "шт",
                Price = 2.50m,
                CategoryId = catFood.Id,
                ManufacturerId = manNestle.Id,
                SupplierId = supMetro.Id,
                DiscountPercent = 5,
                StockQuantity = 120,
                Description = "Пастеризованное молоко",
                Category = catFood,
                Manufacturer = manNestle,
                Supplier = supMetro
            });

            wh1.products.Add(new Product
            {
                Article = "TV001",
                Name = "Телевизор 42",
                Unit = "шт",
                Price = 399.99m,
                CategoryId = catTech.Id,
                ManufacturerId = manSamsung.Id,
                SupplierId = supDns.Id,
                DiscountPercent = 10,
                StockQuantity = 7,
                Description = "Smart TV",
                Category = catTech,
                Manufacturer = manSamsung,
                Supplier = supDns
            });

            // Товары для склада 2
            wh2.products.Add(new Product
            {
                Article = "COF001",
                Name = "Кофе растворимый",
                Unit = "шт",
                Price = 6.20m,
                CategoryId = catFood.Id,
                ManufacturerId = manNestle.Id,
                SupplierId = supMetro.Id,
                DiscountPercent = 0,
                StockQuantity = 40,
                Category = catFood,
                Manufacturer = manNestle,
                Supplier = supMetro
            });

            wh2.products.Add(new Product
            {
                Article = "MON001",
                Name = "Монитор 24",
                Unit = "шт",
                Price = 149.90m,
                CategoryId = catTech.Id,
                ManufacturerId = manSamsung.Id,
                SupplierId = supDns.Id,
                DiscountPercent = 15,
                StockQuantity = 9,
                Category = catTech,
                Manufacturer = manSamsung,
                Supplier = supDns
            });

            // Товары для склада 3
            wh3.products.Add(new Product
            {
                Article = "TEA001",
                Name = "Чай черный",
                Unit = "шт",
                Price = 3.40m,
                CategoryId = catFood.Id,
                ManufacturerId = manNestle.Id,
                SupplierId = supMetro.Id,
                DiscountPercent = 2,
                StockQuantity = 75,
                Category = catFood,
                Manufacturer = manNestle,
                Supplier = supMetro
            });

            wh3.products.Add(new Product
            {
                Article = "TAB001",
                Name = "Планшет",
                Unit = "шт",
                Price = 220.00m,
                CategoryId = catTech.Id,
                ManufacturerId = manSamsung.Id,
                SupplierId = supDns.Id,
                DiscountPercent = 8,
                StockQuantity = 5,
                Category = catTech,
                Manufacturer = manSamsung,
                Supplier = supDns
            });

            // Товары для склада 4
            wh4.products.Add(new Product
            {
                Article = "BRED001",
                Name = "Хлеб",
                Unit = "шт",
                Price = 1.10m,
                CategoryId = catFood.Id,
                ManufacturerId = manNestle.Id,
                SupplierId = supMetro.Id,
                DiscountPercent = 0,
                StockQuantity = 200,
                Category = catFood,
                Manufacturer = manNestle,
                Supplier = supMetro
            });

            wh4.products.Add(new Product
            {
                Article = "PHONE001",
                Name = "Смартфон",
                Unit = "шт",
                Price = 799.00m,
                CategoryId = catTech.Id,
                ManufacturerId = manSamsung.Id,
                SupplierId = supDns.Id,
                DiscountPercent = 12,
                StockQuantity = 11,
                Category = catTech,
                Manufacturer = manSamsung,
                Supplier = supDns
            });

            // Накладные для склада 1
            Order order1 = new Order
            {
                Number = "INC-001",
                Type = OrderType.Incoming,
                Status = OrderStatus.NotApproved,
                WarehouseId = wh1.WhId
            };

            order1.Products.Add(new Product
            {
                Article = "NEW001",
                Name = "Новый товар",
                Unit = "шт",
                Price = 15.5m,
                CategoryId = catFood.Id,
                ManufacturerId = manNestle.Id,
                SupplierId = supMetro.Id,
                StockQuantity = 30,
                DiscountPercent = 0,
                Category = catFood,
                Manufacturer = manNestle,
                Supplier = supMetro
            });

            Order order2 = new Order
            {
                Number = "OUT-001",
                Type = OrderType.Outgoing,
                Status = OrderStatus.NotApproved,
                WarehouseId = wh1.WhId
            };

            order2.Products.Add(new Product
            {
                Article = "MILK001",
                Name = "Молоко 3.2%",
                Unit = "шт",
                Price = 2.50m,
                CategoryId = catFood.Id,
                ManufacturerId = manNestle.Id,
                SupplierId = supMetro.Id,
                StockQuantity = 10,
                DiscountPercent = 5,
                Category = catFood,
                Manufacturer = manNestle,
                Supplier = supMetro
            });

            wh1.Orders.Add(order1);
            wh1.Orders.Add(order2);


            // Накладные для склада 2
            Order order3 = new Order
            {
                Number = "INC-002",
                Type = OrderType.Incoming,
                Status = OrderStatus.NotApproved,
                WarehouseId = wh2.WhId
            };

            order3.Products.Add(new Product
            {
                Article = "COF002",
                Name = "Кофе зерновой",
                Unit = "шт",
                Price = 8.30m,
                CategoryId = catFood.Id,
                ManufacturerId = manNestle.Id,
                SupplierId = supMetro.Id,
                StockQuantity = 20,
                DiscountPercent = 3,
                Category = catFood,
                Manufacturer = manNestle,
                Supplier = supMetro
            });

            wh2.Orders.Add(order3);


            // Накладные для склада 3
            Order order4 = new Order
            {
                Number = "OUT-002",
                Type = OrderType.Outgoing,
                Status = OrderStatus.Approved,
                WarehouseId = wh3.WhId
            };

            order4.Products.Add(new Product
            {
                Article = "TAB001",
                Name = "Планшет",
                Unit = "шт",
                Price = 220.00m,
                CategoryId = catTech.Id,
                ManufacturerId = manSamsung.Id,
                SupplierId = supDns.Id,
                StockQuantity = 1,
                DiscountPercent = 8,
                Category = catTech,
                Manufacturer = manSamsung,
                Supplier = supDns
            });

            wh3.Orders.Add(order4);


            // Накладные для склада 4
            Order order5 = new Order
            {
                Number = "INC-003",
                Type = OrderType.Incoming,
                Status = OrderStatus.NotApproved,
                WarehouseId = wh4.WhId
            };

            order5.Products.Add(new Product
            {
                Article = "PHONE002",
                Name = "Смартфон Lite",
                Unit = "шт",
                Price = 499.00m,
                CategoryId = catTech.Id,
                ManufacturerId = manSamsung.Id,
                SupplierId = supDns.Id,
                StockQuantity = 4,
                DiscountPercent = 0,
                Category = catTech,
                Manufacturer = manSamsung,
                Supplier = supDns
            });

            wh4.Orders.Add(order5);

            org1.warehouses.Add(wh1);
            org1.warehouses.Add(wh2);
            org2.warehouses.Add(wh3);
            org2.warehouses.Add(wh4);

            orgs.Add(org1);
            orgs.Add(org2);


        }
    }
}
