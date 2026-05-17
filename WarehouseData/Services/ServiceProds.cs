using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseData.Context;
using WarehouseData.Models;

namespace WarehouseData.Services
{
    public interface IServiceProds
    {
        bool Add(Product product);

        bool Update(Product product);

        bool Delete(Product product);
    }
    public class ServiceProds : IServiceProds
    {
        public ApplicationContext Context { get; set; }

        public Warehouse Warehouse { get; set; }

        public ServiceProds(ApplicationContext context, Warehouse warehouse)
        {
            if (context != null && warehouse != null)
            {
                Context = context;
                Warehouse = warehouse;
            }
        }

        public bool Add(Product product)
        {
            bool result = false;
            if (ValidateProduct(product))
            {
                if (!Warehouse.products.Contains(product))
                {
                    Warehouse.products.Add(product);
                    result = true;
                }
            }
            return result;
        }

        public bool Update(Product product)
        {
            bool result = false;
            if (ValidateProduct(product))
            {
                Product? target = Warehouse.products
                    .FirstOrDefault(p => p.Article == product.Article);

                if (target != null)
                {
                    int idx = Warehouse.products.IndexOf(target);

                    if (idx != -1)
                    {
                        Warehouse.products[idx] = product;
                        result = true;
                    }
                }
            }
            return result;
        }

        public bool Delete(Product product)
        {
            bool result = false;
            if (product != null)
            {
                if (Warehouse.products.Contains(product))
                {
                    Warehouse.products.Remove(product);
                    result = true;
                }
            }
            return result;
        }

        private bool ValidateProduct(Product product)
        {
            if (product == null)
                return false;

            if (string.IsNullOrWhiteSpace(product.Article))
                return false;

            if (Warehouse.products.Any(p =>
                    p.Article == product.Article &&
                    p.ProdId != product.ProdId))
                return false;

            if (string.IsNullOrWhiteSpace(product.Name))
                return false;

            if (string.IsNullOrWhiteSpace(product.Unit))
                return false;

            if (product.Price < 0)
                return false;

            if (product.DiscountPercent < 0 || product.DiscountPercent > 100)
                return false;

            if (product.StockQuantity <= 0)
                return false;

            if (product.CategoryId <= 0)
                return false;

            if (product.ManufacturerId <= 0)
                return false;

            if (product.SupplierId <= 0)
                return false;

            return true;
        }
    }
}
