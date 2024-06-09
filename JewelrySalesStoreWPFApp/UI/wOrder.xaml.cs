using JewelrySalesStoreBusiness.BusinessOrder;
using JewelrySalesStoreData.Models;
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

namespace JewelrySalesStoreWPFApp.UI
{
    /// <summary>
    /// Interaction logic for wOrder.xaml
    /// </summary>
    public partial class wOrder : Window
    {
        private readonly OrderBusiness _business;
        public wOrder()
        {
            InitializeComponent();
            _business = new OrderBusiness();
            LoadGridOrders();
        }

        private async void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //int idTmp = -1;
                //int.TryParse(txtCurrencyCode.Text, out idTmp);

                var item = await _business.GetById(Guid.Parse(txtOrderId.Text));

                if (item.Data == null)
                {
                    var currency = new Order()
                    {
                        OrderId = Guid.NewGuid(),
                        PaymentMethod = txtPaymentMethod.Text,
                        CompanyId = (Guid.Parse(txtComapanyId.Text)),
                        CustomerId = (Guid.Parse(txtCustomerId.Text)),
                        Date = DateTime.Now,
                        Status = bool.Parse(txtStatus.Text),
                        TotalPrice = double.Parse(txtTotalPrice.Text),
                    };

                    var result = await _business.Save(currency);
                    MessageBox.Show(result.Message, "Save");
                }
                else
                {
                    var currency = item.Data as Order;
                    //currency.CurrencyCode = txtCurrencyCode.Text;
                    currency.CompanyId = Guid.Parse(txtComapanyId.Text);
                    currency.Status = bool.Parse(txtStatus.Text);
                    currency.TotalPrice = double.Parse(txtTotalPrice.Text);
                    currency.CompanyId = Guid.Parse(txtComapanyId.Text);
                    currency.PaymentMethod = txtPaymentMethod.Text;
                    currency.Date = DateTime.Now;
                    var result = await _business.Update(currency);
                    MessageBox.Show(result.Message, "Update");
                }

                txtComapanyId.Text = string.Empty;
                txtStatus.Text = string.Empty;
                txtTotalPrice.Text = string.Empty;
                txtCustomerId.Text = string.Empty;
                txtStatus.Text = string.Empty;
                txtOrderId.Text = string.Empty;

                this.LoadGridOrders();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error");
            }
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {

        }
        private async void grdOrder_ButtonDelete_Click(object sender, RoutedEventArgs e) {
            Button btn = (Button)sender;

            string currencyCode = btn.CommandParameter.ToString();

            //MessageBox.Show(currencyCode);

            if (!string.IsNullOrEmpty(currencyCode))
            {
                if (MessageBox.Show("Do you want to delete this item?", "Delete", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    var result = await _business.DeleteById(Guid.Parse(currencyCode));
                    MessageBox.Show($"{result.Message}", "Delete");
                    this.LoadGridOrders();
                }
            }
        }
        private async void grdOrder_ButtonEdit_Click(object sender, RoutedEventArgs e) { }

        private async void grdOrder_MouseDouble_Click(object sender, RoutedEventArgs e) {
            DataGrid grd = sender as DataGrid;
            if (grd != null && grd.SelectedItems != null && grd.SelectedItems.Count == 1)
            {
                var row = grd.ItemContainerGenerator.ContainerFromItem(grd.SelectedItem) as DataGridRow;
                if (row != null)
                {
                    var item = row.Item as Order;
                    if (item != null)
                    {
                        var currencyResult = await _business.GetById(item.OrderId);

                        if (currencyResult.Data != null)
                        {
                            item = currencyResult.Data as Order;

                            txtComapanyId.Text = item.CompanyId.ToString();
                            txtCustomerId.Text = item.CustomerId.ToString();
                            txtPaymentMethod.Text = item.PaymentMethod;
                            txtStatus.Text = item.Status.ToString();
                            txtTotalPrice.Text = item.TotalPrice.ToString();
                            txtOrderId.Text = item.OrderId.ToString();
                        }
                    }
                }
            }
        }


        private async void LoadGridOrders()
        {

            var result = await _business.GetAll();

            if (result.Status > 0 && result.Data != null)
            {
                gridOrder.ItemsSource = result.Data as List<Order>;
            }
            else
            {
                gridOrder.ItemsSource = new List<Order>();
            }

        }
    }
}
