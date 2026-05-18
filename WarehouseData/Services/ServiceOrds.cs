using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseData.Context;
using WarehouseData.Models;

namespace WarehouseData.Services
{
    public interface IServiceOrders
    {
        bool Add(Order order);
        bool Approve(Order order);
        bool Delete(Order order);
    }

    public class ServiceOrds : IServiceOrders
    {
        public ApplicationContext Context { get; set; }

        public Warehouse Warehouse { get; set; }

        public ServiceOrds(
            ApplicationContext context, 
            WarehouseData.Models.Warehouse warehouse)
        {
            if (context != null && warehouse != null)
            {
                Context = context;
                Warehouse = warehouse;
            }
        }

        public bool Add(Order order)
        {
            if (order == null)
                return false;

            if (string.IsNullOrWhiteSpace(order.Number))
                return false;

            Warehouse.Orders.Add(order);
            return true;
        }

        public bool Approve(Order order)
        {
            if (order == null)
                return false;

            if (order.Status == OrderStatus.Approved)
                return false;

            if (order.Products.Count == 0)
                return false;

            // Приход
            if (order.Type == OrderType.Incoming)
            {
                foreach (Product product in order.Products)
                {
                    Product? existing =
                        Warehouse.products.FirstOrDefault(
                            p => p.Article == product.Article);

                    if (existing == null)
                    {
                        Warehouse.products.Add(product);
                    }
                    else
                    {
                        existing.StockQuantity += product.StockQuantity;
                    }
                }
            }

            // расход
            if (order.Type == OrderType.Outgoing)
            {
                // Валидация(товара нет на складе или кол - во товара меньше, чем в накладной)
                foreach (Product product in order.Products)
                {
                    Product? existing =
                        Warehouse.products.FirstOrDefault(
                            p => p.Article == product.Article);

                    if (existing == null)
                        return false;

                    if (existing.StockQuantity < product.StockQuantity)
                        return false;
                }

                // Списание
                foreach (Product product in order.Products)
                {
                    Product existing =
                        Warehouse.products.First(
                            p => p.Article == product.Article);

                    existing.StockQuantity -= product.StockQuantity;

                    if (existing.StockQuantity == 0)
                        Warehouse.products.Remove(existing);
                }
            }

            order.Status = OrderStatus.Approved;
            return true;
        }

        public bool Delete(Order order)
        {
            if (order == null)
                return false;

            // утвержденные удалять нельзя
            if (order.Status == OrderStatus.Approved)
                return false;

            return Warehouse.Orders.Remove(order);
        }
    }
}
