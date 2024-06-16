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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace JewelrySalesStoreWPFApp.UI
{
    /// <summary>
    /// Interaction logic for wOrderDetail.xaml
    /// </summary>
    public partial class wOrderDetail : Window
    {
        private readonly OrderDetailBusiness _business;
        private readonly ProductBusiness _product;

        public wOrderDetail()
        {
            InitializeComponent();
            _business = new OrderDetailBusiness();
            _product = new ProductBusiness();
            LoadGridOrderDetails();

            txtQuantity.TextChanged += (sender, e) => { CalculateTotalPrice(); CalculateFinalPrice(); };
            txtUnitPrice.TextChanged += (sender, e) => { CalculateTotalPrice(); CalculateFinalPrice(); };
            txtDiscountPrice.TextChanged += (sender, e) => { CalculateFinalPrice(); };
        }


        private async void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Guid orderDetailId = Guid.Empty;
                if (!string.IsNullOrWhiteSpace(txtOrderDetailId.Text.Trim()))
                {
                    orderDetailId = Guid.Parse(txtOrderDetailId.Text.Trim());
                }

                //var item = await _business.GetById(Guid.Parse(txtCompanyId.Text.Trim()));
                var item = await _business.GetById(orderDetailId);

                if (item.Data == null)
                {
                    var orderDetail = new OrderDetail()
                    {
                        OrderDetailId = Guid.NewGuid(),
                        OrderId = Guid.Parse(txtOrderId.Text.Trim()),
                        ProductId = Guid.Parse(txtProductId.Text.Trim()),
                        Quantity = int.Parse(txtQuantity.Text.Trim()),
                        UnitPrice = double.Parse(txtUnitPrice.Text.Trim()),
                        TotalPrice = double.Parse(txtTotalPrice.Text.Trim()),
                        DiscountPrice = double.Parse(txtDiscountPrice.Text.Trim()),
                        FinalPrice = double.Parse(txtFinalPrice.Text.Trim()),
                        IsActive = chkIsActive.IsChecked ?? false,
                        Notes = txtNotes.Text.Trim(),
                    };

                    var result = await _business.Save(orderDetail);
                    MessageBox.Show(result.Message, "Save");
                }
                else
                {
                    var orderDetail = item.Data as OrderDetail;

                    orderDetail.OrderId = Guid.Parse(txtOrderId.Text.Trim());
                    orderDetail.ProductId = Guid.Parse(txtProductId.Text.Trim());
                    orderDetail.Quantity = int.Parse(txtQuantity.Text.Trim());
                    orderDetail.UnitPrice = double.Parse(txtUnitPrice.Text.Trim());
                    orderDetail.TotalPrice = double.Parse(txtTotalPrice.Text.Trim());
                    orderDetail.DiscountPrice = double.Parse(txtDiscountPrice.Text.Trim());
                    orderDetail.FinalPrice = double.Parse(txtFinalPrice.Text.Trim());
                    orderDetail.IsActive = chkIsActive.IsChecked ?? false;
                    orderDetail.Notes = txtNotes.Text.Trim();

                    var result = await _business.Update(orderDetail);
                    MessageBox.Show(result.Message, "Update");
                }

                txtOrderDetailId.Text = string.Empty;
                txtOrderId.Text = string.Empty;
                txtProductId.Text = string.Empty;
                txtQuantity.Text = string.Empty;
                txtUnitPrice.Text = string.Empty;
                txtTotalPrice.Text = string.Empty;
                txtDiscountPrice.Text = string.Empty;
                txtFinalPrice.Text = string.Empty;
                chkIsActive.IsChecked = false;
                txtNotes.Text = string.Empty;

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
                            txtDiscountPrice.Text = item.DiscountPrice.ToString();
                            txtFinalPrice.Text = item.FinalPrice.ToString();
                            chkIsActive.IsChecked = item.IsActive;
                            txtNotes.Text = item.Notes;
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

        private void CalculateTotalPrice()
        {
            if (int.TryParse(txtQuantity.Text.Trim(), out int quantity) && double.TryParse(txtUnitPrice.Text.Trim(), out double unitPrice))
            {
                double totalPrice = quantity * unitPrice;
                txtTotalPrice.Text = totalPrice.ToString("F2");
            }
            else
            {
                txtTotalPrice.Text = "0.00";
            }
        }

        private void CalculateFinalPrice()
        {
            if (double.TryParse(txtTotalPrice.Text.Trim(), out double totalPrice) && double.TryParse(txtDiscountPrice.Text.Trim(), out double discountPrice))
            {
                double finalPrice = totalPrice - discountPrice;
                txtFinalPrice.Text = finalPrice.ToString("F2");
            }
            else
            {
                txtFinalPrice.Text = "0.00";
            }
        }

        //private void CalculateDiscountPrice()
        //{
        //    if (double.TryParse(txtTotalPrice.Text.Trim(), out double totalPrice) && double.TryParse(txtDiscountPrice.Text.Trim(), out double discountPrice))
        //    {
        //        //double discountAmount = totalPrice * (discountPrice / 100);
        //        //txtTotalPrice.Text = (totalPrice - discountAmount).ToString("F2");
        //        txtTotalPrice.Text = (totalPrice - discountPrice).ToString("F2");
        //    }
        //    else
        //    {
        //        txtDiscountPrice.Text = "0.00";
        //    }
        //}
    }

}
