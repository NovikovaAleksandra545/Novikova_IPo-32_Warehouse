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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Warehouse.Views.ModalWins;
using WarehouseData.Models;
using WarehouseData.Services;

namespace Warehouse.Controls
{
    /// <summary>
    /// Логика взаимодействия для UserControlOrders.xaml
    /// </summary>
    public partial class UserControlOrders : UserControl
    {
        private ServiceOrds _serviceOrders;
        private ServiceProds _serviceProducts;
        private WarehouseData.Models.Warehouse _warehouse;

        private Order? _order;
        private Product? _product;

        public UserControlOrders(ServiceOrds serviceOrds)
        {
            InitializeComponent();

            _warehouse = serviceOrds.Warehouse;
            _serviceOrders = serviceOrds;

            FillOrdersCollection();

            FillProductsCollection();
            lbxOrders.Focus();

            if (_warehouse != null)
            {
               _serviceProducts = new ServiceProds(_serviceOrders.Context, _order);
            }
        }

        private void FillOrdersCollection()
        {
            int idx = 0;
            if (lbxOrders.SelectedIndex > 0) idx = lbxOrders.SelectedIndex;
            lbxOrders.ItemsSource = null;
            lbxOrders.Items.Clear();
            lbxOrders.ItemsSource = _warehouse.Orders;
            if (lbxOrders.Items.Count > 0)
            {
                try
                {
                    lbxOrders.SelectedIndex = idx;
                }
                catch
                {
                    lbxOrders.SelectedIndex = -1;
                }
            }

            //lbxOrders.ItemsSource = null;
            //lbxOrders.ItemsSource = _warehouse.Orders;
        }

        private void FillProductsCollection()
        {
            int idx = 0;
            if (DgProducts.SelectedIndex > 0) idx = DgProducts.SelectedIndex;
            DgProducts.ItemsSource = null;
            if (_order != null)
            {
                DgProducts.ItemsSource = _order.Products;

                if (idx >= 0 && idx < DgProducts.Items.Count)
                    DgProducts.SelectedIndex = idx;
            }
        }

        //private void FillProductsCollection()
        //{
        //    DgProducts.ItemsSource = null;

        //    if (_order != null)
        //        DgProducts.ItemsSource = _order.Products;
        //}

        private void lbxOrders_SelectionChanged(
            object sender,
            SelectionChangedEventArgs e)
        {
            _order = (Order?)lbxOrders.SelectedItem;
            //_order = lbxOrders.SelectedItem as Order;
            //FillProductsCollection();
        }

        private void AddOrder_Executed(
            object sender,
            System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            OrderModWindow wnd =
                new OrderModWindow(
                    "Создать накладную",
                    null,
                    _serviceOrders,
                    1);

            wnd.Owner = Window.GetWindow(this);

            if (wnd.ShowDialog() == true)
                FillOrdersCollection();
        }

        private void ApproveOrder_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (_order != null)
            {
                _serviceOrders.Approve(_order);
                FillOrdersCollection();
                FillProductsCollection();
            }
        }

        private void RemoveOrder_Executed(
            object sender,
            System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            if (_order == null)
                return;

            OrderModWindow wnd =
                new OrderModWindow(
                    "Удалить накладную",
                    _order,
                    _serviceOrders,
                    3);

            wnd.Owner = Window.GetWindow(this);

            if (wnd.ShowDialog() == true)
                FillOrdersCollection();
        }


        private void GetProducts_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            FillProductsCollection();
        }



        private void AddProduct_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (_order == null) return;

            _serviceProducts.Order = _order;
            ProductModWindow wnd =
                new ProductModWindow("Добавить товар", null, _serviceProducts, 1);

            wnd.Owner = Window.GetWindow(this);

            if (wnd.ShowDialog() == true)
                FillProductsCollection();
        }

        private void EditProduct_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (_product != null)
            {
                _serviceProducts.Order = _order;
                ProductModWindow wnd =
                    new ProductModWindow("Изменить товар", _product, _serviceProducts, 2);

                wnd.Owner = Window.GetWindow(this);

                if (wnd.ShowDialog() == true)
                    FillProductsCollection();
            }
        }

        private void RemoveProduct_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (_product != null)
            {
                _serviceProducts.Order = _order;
                ProductModWindow wnd =
                    new ProductModWindow("Удалить товар", _product, _serviceProducts, 3);

                wnd.Owner = Window.GetWindow(this);
                wnd.tbArticle.IsReadOnly = true;
                wnd.tbName.IsReadOnly = true;
                wnd.tbUnit.IsReadOnly = true;
                wnd.cbCategory.IsEnabled = false;
                wnd.cbManufacturer.IsEnabled = false;
                wnd.cbSupplier.IsEnabled = false;

                if (wnd.ShowDialog() == true)
                    FillProductsCollection();
            }
        }

        private void DgProducts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _product = DgProducts.SelectedItem as Product;
        }

        private void DgProducts_MouseDoubleClick(object sender, RoutedEventArgs e)
        {
            FillProductsCollection();
        }

        private void DgOrds_MouseDoubleClick(object sender, RoutedEventArgs e)
        {
            FillProductsCollection();
        }
    }
}
