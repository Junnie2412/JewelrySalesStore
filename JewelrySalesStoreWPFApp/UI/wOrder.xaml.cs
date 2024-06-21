using JewelrySalesStoreBusiness;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace JewelrySalesStoreWPFApp.UI
{
    /// <summary>
    /// Interaction logic for wOrder.xaml
    /// </summary>
    public partial class wOrder : Window
    {
        private readonly OrderBusiness _business;
        private readonly ProductBusiness _product;
        private readonly OrderDetailBusiness _detail;
        public wOrder()
        {
            InitializeComponent();
            _business = new OrderBusiness();
            _product = new ProductBusiness();
            _detail = new OrderDetailBusiness();
            LoadGridOrders();
        }

        private async void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //int idTmp = -1;
                //int.TryParse(txtCurrencyCode.Text, out idTmp);

                var item = await _business.GetById(Guid.Parse(txtOrderId.Text));
                var product = await _product.GetById(Guid.Parse(txtProduct.Text));
                var product2 = product.Data as Product;
                var test = product2.Price;
                var test2 = product2.ProductId;
                if (item.Data == null && product != null)
                {

                    var currency = new Order()
                    {
                        OrderId = Guid.NewGuid(),
                        PaymentMethod = txtPaymentMethod.Text,
                        CompanyId = (Guid.Parse(txtComapanyId.Text)),
                        CustomerId = (Guid.Parse(txtCustomerId.Text)),
                        Date = DateTime.Now,
                        Status = bool.Parse(txtStatus.Text),
                        ShippingMethod = txtShippingMethod.Text,
                        CustomerBankAccount = txtCustomerBankAccount.Text,
                        CustomerAddress = txtCustomerAddress.Text,
                        Notes = txtNote.Text,
                    };


                    var orderDetail = new OrderDetail()
                    {
                        OrderDetailId = Guid.NewGuid(),
                        OrderId = currency.OrderId,
                        ProductId = Guid.Parse(txtProduct.Text),
                        Quantity = int.Parse(txtQuantity.Text),
                        UnitPrice = product2.Price,
                        TotalPrice = product2.Price * int.Parse(txtQuantity.Text),
                    };
                    var result1 = await _business.Save(currency);
                    var result = await _detail.Save(orderDetail);
                    currency.TotalPrice = orderDetail.TotalPrice;

                    await _business.Update(currency);

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
                    currency.ShippingMethod = txtShippingMethod.Text;
                    currency.CustomerBankAccount = txtCustomerBankAccount.Text;
                    currency.Notes = txtNote.Text;
                    currency.CustomerAddress = txtCustomerAddress.Text;
                    var result = await _business.Update(currency);
                    MessageBox.Show(result.Message, "Update");
                }

                txtComapanyId.Text = string.Empty;
                txtStatus.Text = string.Empty;
                txtTotalPrice.Text = string.Empty;
                txtCustomerId.Text = string.Empty;
                txtStatus.Text = string.Empty;
                txtOrderId.Text = string.Empty;
                txtShippingMethod.Text= string.Empty;
                txtCustomerBankAccount.Text = string.Empty;
                txtNote.Text = string.Empty;
                txtCustomerAddress.Text = string.Empty;

                this.LoadGridOrders();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error");
            }
        }
        //public async Task<OrderDetail> getOrderDetailByOrder(Guid orderId)
        //{
        //    var allOrderDetail = await _detail.GetAll();
        //    var listOrderDetail = allOrderDetail.Data as List<OrderDetail>;
        //    if (listOrderDetail != null)
        //    {
        //        foreach (var item in listOrderDetail)
        //        {
        //            if (item.OrderId == orderId)
        //            {
        //                return item;
        //            }
        //        }
        //        return null;
        //    }
        //    return null;
        //} 

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {

        }
        private async void grdOrder_ButtonDelete_Click(object sender, RoutedEventArgs e) {
            Button btn = (Button)sender;

            string currencyCode = btn.CommandParameter.ToString();
            //var result2 =getOrderDetailByOrder(Guid.Parse(currencyCode));
            //var resultDetail = result2.Result as OrderDetail;
            //var Order = await _business.GetById(Guid.Parse(currencyCode));
            //var test = Order.Data as Order;
            var OrderDetailResult = await _detail.GetAll();
            var OrderDetail = new OrderDetail();
            var OrderDetailList = OrderDetailResult.Data as List<OrderDetail>;
            if(OrderDetailList != null)
            {
                foreach (var item in OrderDetailList)
                {
                    if(item.OrderId == Guid.Parse(currencyCode))
                    {
                        OrderDetail.OrderDetailId = item.OrderDetailId;
                    }
                }
            }

            //MessageBox.Show(currencyCode);

            if (!string.IsNullOrEmpty(currencyCode))
            {
                if (MessageBox.Show("Do you want to delete this item?", "Delete", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    //await _detail.DeleteById(resultDetail.OrderDetailId);
                    await _detail.DeleteById(OrderDetail.OrderDetailId);
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
                        //var currencyOrderDetail = await _detail.GetById(currencyResult.)
                        if (currencyResult.Data != null)
                        {
                            item = currencyResult.Data as Order;

                            txtComapanyId.Text = item.CompanyId.ToString();
                            txtCustomerId.Text = item.CustomerId.ToString();
                            txtPaymentMethod.Text = item.PaymentMethod;
                            txtStatus.Text = item.Status.ToString();
                            txtTotalPrice.Text = item.TotalPrice.ToString();
                            txtOrderId.Text = item.OrderId.ToString();
                            txtCustomerBankAccount.Text = item.CustomerBankAccount;
                            txtShippingMethod.Text = item.ShippingMethod;
                            txtCustomerAddress.Text = item.CustomerAddress.ToString();
                            txtNote.Text = item.Notes;
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
        private string GenerateOrderDetailId(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
