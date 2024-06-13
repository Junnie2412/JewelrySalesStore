using JewelrySalesStoreBusiness;
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
    /// Interaction logic for wOrderDetail.xaml
    /// </summary>
    public partial class wOrderDetail : Window
    {
        private readonly OrderDetailBusiness _business;

        public wOrderDetail()
        {
            InitializeComponent();
            _business = new OrderDetailBusiness();
            LoadGridOrderDetails();

            txtQuantity.TextChanged += CalculateTotalPrice;
            txtUnitPrice.TextChanged += CalculateTotalPrice;
        }

        private async void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string orderDetailId = string.Empty;
                if (!string.IsNullOrWhiteSpace(txtOrderDetailId.Text.Trim()))
                {
                    orderDetailId = txtOrderDetailId.Text.Trim();
                }

                //var item = await _business.GetById(Guid.Parse(txtCompanyId.Text.Trim()));
                var item = await _business.GetById(Guid.Parse(orderDetailId));

                if (item.Data == null)
                {
                    var orderDetail = new OrderDetail()
                    {
                        // OrderDetailId = Guid.NewGuid().ToString("N"),
                        OrderDetailId = Guid.NewGuid(),
                        OrderId = Guid.Parse(txtOrderId.Text.Trim()),
                        ProductId = Guid.Parse(txtProductId.Text.Trim()),
                        Quantity = int.Parse(txtQuantity.Text.Trim()),
                        UnitPrice = double.Parse(txtUnitPrice.Text.Trim()),
                        TotalPrice = double.Parse(txtTotalPrice.Text.Trim()),
                    };

                    var result = await _business.Save(orderDetail);
                    MessageBox.Show(result.Message, "Save");
                }
                else
                {
                    var orderDetail = item.Data as OrderDetail;

                    //company.CompanyId = Guid.Parse(txtCompanyId.Text.Trim());
                    orderDetail.OrderId = Guid.Parse(txtOrderId.Text.Trim());
                    orderDetail.ProductId = Guid.Parse(txtProductId.Text.Trim());
                    orderDetail.Quantity = int.Parse(txtQuantity.Text.Trim());
                    orderDetail.UnitPrice = double.Parse(txtUnitPrice.Text.Trim());
                    orderDetail.TotalPrice = double.Parse(txtTotalPrice.Text.Trim());

                    var result = await _business.Update(orderDetail);
                    MessageBox.Show(result.Message, "Update");
                }

                txtOrderDetailId.Text = string.Empty;
                txtOrderId.Text = string.Empty;
                txtProductId.Text = string.Empty;
                txtQuantity.Text = string.Empty;
                txtUnitPrice.Text = string.Empty;
                txtTotalPrice.Text = string.Empty;

                this.LoadGridOrderDetails();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error");
            }

        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e) { }

        private async void grdOrderDetail_ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;

            string orderDetailId = btn.CommandParameter.ToString();

            //MessageBox.Show(currencyCode);

            if (!string.IsNullOrEmpty(orderDetailId))
            {
                if (MessageBox.Show("Do you want to delete this item?", "Delete", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    var result = await _business.DeleteById(Guid.Parse(orderDetailId));
                    MessageBox.Show($"{result.Message}", "Delete");
                    this.LoadGridOrderDetails();
                }
            }
        }

        private async void grdOrderDetail_ButtonEdit_Click(object sender, RoutedEventArgs e) { }

        private async void grdOrderDetail_MouseDouble_Click(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("Double Click on Grid");
            DataGrid grd = sender as DataGrid;
            if (grd != null && grd.SelectedItems != null && grd.SelectedItems.Count == 1)
            {
                var row = grd.ItemContainerGenerator.ContainerFromItem(grd.SelectedItem) as DataGridRow;
                if (row != null)
                {
                    var item = row.Item as OrderDetail;
                    if (item != null)
                    {
                        var currencyResult = await _business.GetById(item.OrderDetailId);

                        if (currencyResult.Status > 0 && currencyResult.Data != null)
                        {
                            item = currencyResult.Data as OrderDetail;

                            txtOrderDetailId.Text = item.OrderDetailId.ToString();
                            txtOrderId.Text = item.OrderId.ToString();
                            txtProductId.Text = item.ProductId.ToString();
                            txtQuantity.Text = item.Quantity.ToString();
                            txtUnitPrice.Text = item.UnitPrice.ToString();
                            txtTotalPrice.Text = item.TotalPrice.ToString();
                        }
                    }
                }
            }
        }

        private async void LoadGridOrderDetails()
        {

            var result = await _business.GetAll();

            if (result.Status > 0 && result.Data != null)
            {
                gridOrderDetail.ItemsSource = result.Data as List<OrderDetail>;
            }
            else
            {
                gridOrderDetail.ItemsSource = new List<OrderDetail>();
            }

        }

        private void CalculateTotalPrice(object sender, TextChangedEventArgs e)
        {
            if (int.TryParse(txtQuantity.Text.Trim(), out int quantity) && double.TryParse(txtUnitPrice.Text.Trim(), out double unitPrice))
            {
                txtTotalPrice.Text = (quantity * unitPrice).ToString("F2");
            }
            else
            {
                txtTotalPrice.Text = "0.00";
            }
        }

        // Hàm để tạo chuỗi ngẫu nhiên với độ dài cho trước
        private string GenerateOrderDetailId(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }

}
