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
    /// Interaction logic for wCustomer.xaml
    /// </summary>
    public partial class wCustomer : Window
    {
        private readonly CustomerBusiness _business;

        public wCustomer()
        {
            InitializeComponent();
            _business = new CustomerBusiness();
            LoadGridCustomers();

        }
        private async void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Guid.TryParse(txtCustomerId.Text, out Guid customerid);

                var item = await _business.GetById(customerid);
                if (item.Data == null)
                {
                    var customer = new Customer()
                    {
                        CustomerFirstName = txtFirstName.Text,
                        CustomerLastName = txtLastName.Text,
                        CustomerBirthDate = dpBirthDate.SelectedDate ?? DateTime.Now,
                        CustomerGender = bool.Parse((string)((ComboBoxItem)cbGender.SelectedItem).Tag),
                        CustomerPhone = txtPhone.Text,
                        CustomerEmail = txtEmail.Text,
                        CustomerAddress = txtAddress.Text,
                        CustomerPoint = double.Parse(txtPoint.Text),
                        CustomerVipStatus = chkVipStatus.IsChecked ?? false,
                        Notes = txtNotes.Text,
                    };

                    var result = await _business.Save(customer);
                    MessageBox.Show(result.Message, "Save");
                }
                else
                {
                    var customer = item.Data as Customer;
                    customer.CustomerFirstName = txtFirstName.Text;
                    customer.CustomerLastName = txtLastName.Text;
                    customer.CustomerBirthDate = dpBirthDate.SelectedDate ?? DateTime.Now;
                    customer.CustomerGender = bool.Parse((string)((ComboBoxItem)cbGender.SelectedItem).Tag);
                    customer.CustomerPhone = txtPhone.Text;
                    customer.CustomerEmail = txtEmail.Text;
                    customer.CustomerAddress = txtAddress.Text;
                    customer.CustomerPoint = double.Parse(txtPoint.Text);
                    customer.CustomerVipStatus = chkVipStatus.IsChecked ?? false;
                    customer.Notes = txtNotes.Text;

                    var result = await _business.Update(customer);
                    MessageBox.Show(result.Message, "Update");
                }

                clearForm();
                this.LoadGridCustomers();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error");
            }

        }

        private async void grdCustomer_ButtonEdit_Click(object sender, RoutedEventArgs e) { }



        private async void grdCustomer_ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;

            if (btn.CommandParameter is Guid customerId)
            {
                if (MessageBox.Show("Do you want to delete this item?", "Delete", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    var result = await _business.DeleteById(customerId);
                    MessageBox.Show($"{result.Message}", "Delete");
                    this.LoadGridCustomers();
                }

            }

            //MessageBox.Show(customerId);


        }


        private async void grdCustomer_MouseDouble_Click(object sender, RoutedEventArgs e)
        {
            if (gridCustomer.SelectedItem != null)
            {
                var selectedCustomer = gridCustomer.SelectedItem as Customer;
                if (selectedCustomer != null)
                {
                    // Hiển thị dữ liệu của khách hàng được chọn lên các trường nhập liệu
                    txtCustomerId.Text = selectedCustomer.CustomerId.ToString();
                    txtFirstName.Text = selectedCustomer.CustomerFirstName;
                    txtLastName.Text = selectedCustomer.CustomerLastName;
                    dpBirthDate.SelectedDate = selectedCustomer.CustomerBirthDate;
                    cbGender.SelectedValue = selectedCustomer.CustomerGender.ToString().ToLower();
                    txtPhone.Text = selectedCustomer.CustomerPhone;
                    txtEmail.Text = selectedCustomer.CustomerEmail;
                    txtAddress.Text = selectedCustomer.CustomerAddress;
                    txtPoint.Text = selectedCustomer.CustomerPoint.ToString();
                    chkVipStatus.IsChecked = selectedCustomer.CustomerVipStatus;
                    txtNotes.Text = selectedCustomer.Notes;
                }
            }
        }

        private async void LoadGridCustomers()
        {

            var result = await _business.GetAll();

            if (result.Status > 0 && result.Data != null)
            {
                gridCustomer.ItemsSource = result.Data as List<Customer>;
            }
            else
            {
                gridCustomer.ItemsSource = new List<Customer>();
            }

        }
        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            clearForm();
        }
        private void clearForm()
        {
            if (!string.IsNullOrEmpty(txtCustomerId.Text))
            {
                // Xóa CustomerId để chuyển sang chế độ thêm mới
                txtCustomerId.Text = string.Empty;
            }

            txtFirstName.Text = string.Empty;
            txtLastName.Text = string.Empty;
            dpBirthDate.Text = string.Empty;
            cbGender.SelectedIndex = 0;
            txtPhone.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtAddress.Text = string.Empty;
            txtPoint.Text = string.Empty;
            chkVipStatus.IsChecked = false;
            txtNotes.Text = string.Empty;
        }
    }
}
