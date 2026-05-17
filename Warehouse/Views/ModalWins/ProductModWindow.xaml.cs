using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WarehouseData.Models;
using WarehouseData.Services;

namespace Warehouse.Views.ModalWins
{
    /// <summary>
    /// Логика взаимодействия для ProductModWindow.xaml
    /// </summary>
    public partial class ProductModWindow : Window
    {
        private ServiceProds _service;
        private Product? _product;
        private int _mode;

        public ProductModWindow(
            string title,
            Product? product,
            ServiceProds service,
            int mode)
        {
            InitializeComponent();

            _service = service;
            _product = product;
            _mode = mode;

            Title = title;

            FillDirectories();

            if (_product != null)
            {
                tbArticle.Text = _product.Article;
                tbName.Text = _product.Name;
                tbUnit.Text = _product.Unit;
                tbPrice.Text = _product.Price.ToString();
                tbDiscount.Text = _product.DiscountPercent.ToString();
                tbQuantity.Text = _product.StockQuantity.ToString();
                tbDescription.Text = _product.Description;

                cbCategory.SelectedItem = _product.Category;
                cbManufacturer.SelectedItem = _product.Manufacturer;
                cbSupplier.SelectedItem = _product.Supplier;
            }
        }

        private void FillDirectories()
        {
            cbCategory.ItemsSource = _service.Context.Categories;
            cbManufacturer.ItemsSource = _service.Context.Manufacturers;
            cbSupplier.ItemsSource = _service.Context.Suppliers;
        }

        private void BtnOk_Click(object sender, RoutedEventArgs e)
        {
            switch (_mode)
            {
                case 1:
                    if (_service.Warehouse != null)
                    {
                        Product newProduct = new Product
                        {
                            Article = tbArticle.Text,
                            Name = tbName.Text,
                            Unit = tbUnit.Text,
                            Price = decimal.Parse(tbPrice.Text),
                            DiscountPercent = decimal.Parse(tbDiscount.Text),
                            StockQuantity = int.Parse(tbQuantity.Text),
                            Description = tbDescription.Text,

                            Category = (Category)cbCategory.SelectedItem,
                            Manufacturer = (Manufacturer)cbManufacturer.SelectedItem,
                            Supplier = (Supplier)cbSupplier.SelectedItem
                        };

                        _service.Add(newProduct);
                    }
                    
                    break;

                case 2:
                    if (_service.Warehouse != null && _product != null)
                    {
                        _product.Article = tbArticle.Text;
                        _product.Name = tbName.Text;
                        _product.Unit = tbUnit.Text;
                        _product.Price = decimal.Parse(tbPrice.Text);
                        _product.DiscountPercent = decimal.Parse(tbDiscount.Text);
                        _product.StockQuantity = int.Parse(tbQuantity.Text);
                        _product.Description = tbDescription.Text;

                        _product.Category = (Category)cbCategory.SelectedItem;
                        _product.Manufacturer = (Manufacturer)cbManufacturer.SelectedItem;
                        _product.Supplier = (Supplier)cbSupplier.SelectedItem;

                        _service.Update(_product);
                    }
                    break;

                case 3:
                    if (_service.Warehouse != null && _product != null)
                    {
                        _service.Delete(_product);
                    }
                    break;
            }

            DialogResult = true;
            Close();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
