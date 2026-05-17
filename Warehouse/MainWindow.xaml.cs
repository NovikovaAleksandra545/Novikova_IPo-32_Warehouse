using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Warehouse.Controls;
using WarehouseData.Context;
using WarehouseData.Models;
using WarehouseData.Services;

namespace Warehouse
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private ApplicationContext _context;
        private Organization? _org;

        public ServiceWhs serviceWhs {  get; private set; }

        public MainWindow(ApplicationContext applicationContext, Organization org)
        {
            InitializeComponent();

            this._context = applicationContext;
            this._org = org;

            if (_org != null && !String.IsNullOrEmpty(_org.OrgName))
            {
                tbStatus.Text = _org.OrgName;
                this.Title = _org.OrgName;
            }

            serviceWhs = new ServiceWhs(applicationContext, org);


            this.MainFrame.Navigate(new UserControlWarehouse(serviceWhs));
        }

        private void Exit_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this.Close();
        }
    }
}