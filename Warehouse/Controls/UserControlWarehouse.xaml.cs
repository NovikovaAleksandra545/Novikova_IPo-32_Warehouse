using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
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
using System.Xml.Linq;
using Warehouse.Views.ModalWins;
using WarehouseData.Models;
using WarehouseData.Services;

namespace Warehouse.Controls
{
    /// <summary>
    /// Логика взаимодействия для UserControlWarehouse.xaml
    /// </summary>
    public partial class UserControlWarehouse : UserControl
    {

        public ServiceWhs ServiceWarehouse {  get; private set; }

        public Organization Org {  get; private set; }

        private WarehouseData.Models.Warehouse? _whs;

        private ServiceProds _serviceProducts;
        private Product? _product;

        public UserControlWarehouse(ServiceWhs serviceWhs)
        {
            InitializeComponent();
            Org = serviceWhs.Org;
            ServiceWarehouse = serviceWhs;
            



            FillWarehouseCollection();
            FillProductsCollection();
            lbxWhList.Focus();

            if (Org != null)
            {
                if (Org.warehouses.Count > 0)
                {
                    _serviceProducts = new ServiceProds(ServiceWarehouse.Context, _whs);
                }
            }
        }

        private void FillWarehouseCollection()
        {
            int idx = 0;
            if (lbxWhList.SelectedIndex > 0) idx = lbxWhList.SelectedIndex;
            lbxWhList.ItemsSource = null;
            lbxWhList.Items.Clear();
            lbxWhList.ItemsSource = Org.warehouses;
            if (lbxWhList.Items.Count > 0)
            {
                try
                {
                    lbxWhList.SelectedIndex = idx;
                }
                catch
                {
                    lbxWhList.SelectedIndex = -1;
                }
            }

        }

        private void FillProductsCollection()
        {
            int idx = 0;
            if (DgProducts.SelectedIndex > 0) idx = DgProducts.SelectedIndex;
            DgProducts.ItemsSource = null;
            if (_whs != null)
            {
                DgProducts.ItemsSource = _whs.products;

                if (idx >= 0 && idx < DgProducts.Items.Count)
                    DgProducts.SelectedIndex = idx;
            } 
        }

        private void WhAdd_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            WhModWindow cmdWindow = new WhModWindow("Добавить склад", Org, null, ServiceWarehouse, 1);
            cmdWindow.Owner = Window.GetWindow(this);
            if (cmdWindow.ShowDialog() == true) FillWarehouseCollection();
        }

        private void WhEdit_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (_whs != null)
            {
                WhModWindow cmdWindow = new WhModWindow("Изменить склад", Org, _whs, ServiceWarehouse, 2);
                cmdWindow.Owner = Window.GetWindow(this);
                if (cmdWindow.ShowDialog() == true) FillWarehouseCollection();
            }
        }

        private void WhDelete_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (_whs != null)
            {
                WhModWindow cmdWindow = new WhModWindow("Удалить склад", Org, _whs, ServiceWarehouse, 3);
                cmdWindow.Owner = Window.GetWindow(this);
                cmdWindow.tbName.IsReadOnly = true;
                cmdWindow.tbAdress.IsReadOnly = true;
                if (cmdWindow.ShowDialog() == true)
                {
                    FillWarehouseCollection();
                }
            }
        }

        private void AddProduct_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            //ProductModWindow wnd =
            //    new ProductModWindow(_serviceProducts, null, 1);

            //wnd.Owner = Window.GetWindow(this);

            //if (wnd.ShowDialog() == true)
            //    FillProductsCollection();
        }

        private void EditProduct_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (_product != null)
            {
                //ProductModWindow wnd =
                //    new ProductModWindow(_serviceProducts, _product, 2);

                //wnd.Owner = Window.GetWindow(this);

                //if (wnd.ShowDialog() == true)
                //    FillProductsCollection();
            }
        }

        private void RemoveProduct_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (_product != null)
            {
                //ProductModWindow wnd =
                //    new ProductModWindow(_serviceProducts, _product, 3);

                //wnd.Owner = Window.GetWindow(this);

                //if (wnd.ShowDialog() == true)
                //    FillProductsCollection();
            }
        }

        private void DgProducts_MouseDoubleClick(object sender, RoutedEventArgs e)
        {

        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {

        }

        private void TxtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void lbxWhList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _whs = (WarehouseData.Models.Warehouse?)lbxWhList.SelectedItem;
            //if (_whs != null && !String.IsNullOrEmpty(_whs.WhName))
            //{
            //    tbStatus.Text = _whs.OrgName;
            //}
        }

        private void DgProducts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _product = DgProducts.SelectedItem as Product;
        }
    }
}
