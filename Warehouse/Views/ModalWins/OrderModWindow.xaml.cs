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
using System.Xml.Linq;
using WarehouseData.Models;
using WarehouseData.Services;

namespace Warehouse.Views.ModalWins
{
    /// <summary>
    /// Логика взаимодействия для OrderModWindow.xaml
    /// </summary>
    public partial class OrderModWindow : Window
    {
        private ServiceOrds _service;
        private Order? _order;
        private int _mode;

        public OrderModWindow(
            string title,
            Order? order,
            ServiceOrds service,
            int mode)
        {
            InitializeComponent();

            _service = service;
            _order = order;
            _mode = mode;

            Title = title;

            if (_order != null)
            {
                tbNumber.Text = _order.Number;

                if (_order.Type == OrderType.Incoming)
                    cbType.SelectedIndex = 0;
                else
                    cbType.SelectedIndex = 1;
            }

            if (_mode == 1)
            {
                tbNumber.Text = $"НАКЛ-{DateTime.Now.Ticks}";
            }

            if (_mode == 3)
            {
                tbNumber.IsReadOnly = true;
                cbType.IsEnabled = false;
            }
        }

        private void BtnOk_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidateForm())
                return;

            switch (_mode)
            {
                // создание
                case 1:
                    Order newOrder = new Order
                    {
                        Number = tbNumber.Text,
                        Type = GetSelectedType()
                    };

                    _service.Add(newOrder);
                    break;

                // удаление
                case 3:
                    if (_order != null)
                    {
                        _service.Delete(_order);
                    }
                    break;
            }

            DialogResult = true;
            Close();
        }

        private OrderType GetSelectedType()
        {
            ComboBoxItem item =
                (ComboBoxItem)cbType.SelectedItem;

            if (item.Content.ToString() == "Incoming")
                return OrderType.Incoming;

            return OrderType.Outgoing;
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private bool ValidateForm()
        {
            if (string.IsNullOrWhiteSpace(tbNumber.Text))
            {
                ShowError("Введите номер накладной", tbNumber);
                return false;
            }

            if (cbType.SelectedItem == null)
            {
                ShowError("Выберите тип накладной");
                cbType.Focus();
                return false;
            }

            return true;
        }

        private void ShowError(string message, Control? control = null)
        {
            MessageBox.Show(
                message,
                "Ошибка ввода",
                MessageBoxButton.OK,
                MessageBoxImage.Warning);

            control?.Focus();
        }
    }
}
