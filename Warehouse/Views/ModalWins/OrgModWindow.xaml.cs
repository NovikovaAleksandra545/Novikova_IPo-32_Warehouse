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
    /// Логика взаимодействия для OrgModWindow.xaml
    /// </summary>
    public partial class OrgModWindow : Window
    {
        private ServiceOrgs _service;
        private Organization? _org;
        private int _mode;

        public OrgModWindow(
            string title,
            Organization? org,
            ServiceOrgs service,
            int mode)
        {
            InitializeComponent();

            this._service = service;
            this._org = org;
            this._mode = mode;

            Title = title;

            if (_org != null)
            {
                tbName.Text = _org.OrgName;
            }
        }

        private void BtnOk_Click(object sender, RoutedEventArgs e)
        {
            switch (_mode)
            {
                case 1:
                    Organization newOrg =
                        new Organization(tbName.Text);

                    _service.AddOrg(newOrg);
                    break;

                case 2:
                    if (_org != null)
                    {
                        _org.OrgName = tbName.Text;
                        _service.EditOrg(_org);
                    }
                    break;

                case 3:
                    if (_org != null)
                    {
                        _service.DelOrg(_org);
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
