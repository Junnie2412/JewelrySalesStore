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
    /// Interaction logic for wCompany.xaml
    /// </summary>
    public partial class wCompany : Window
    {
        private readonly CompanyBusiness _business;

        public wCompany()
        {
            InitializeComponent();
            _business = new CompanyBusiness();
            LoadGrdCompanies();
        }

        private async void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Guid companyId = Guid.Empty;
                if (!string.IsNullOrWhiteSpace(txtCompanyId.Text.Trim()))
                {
                    companyId = Guid.Parse(txtCompanyId.Text.Trim());
                }

                //var item = await _business.GetById(Guid.Parse(txtCompanyId.Text.Trim()));
                var item = await _business.GetById(companyId);

                if (item.Data == null)
                {
                    var company = new Company()
                    {
                        CompanyId = Guid.NewGuid(),
                        CompanyName = txtCompanyName.Text.Trim(),
                        CompanyPhone = txtCompanyPhone.Text.Trim(),
                        CompanyAddress = txtCompanyAddress.Text.Trim(),
                        CompanyDescription = txtCompanyDescription.Text.Trim(),
                        Website = txtCompanyWebsite.Text.Trim(),
                        FoundationDate = DateTime.Now,
                        Email = txtCompanyEmail.Text.Trim(),
                        IsActive = chkIsActive.IsChecked,
                        Notes = txtCompanyNotes.Text.Trim(),
                    };

                    var result = await _business.Save(company);
                    MessageBox.Show(result.Message, "Save");
                }
                else
                {
                    var company = item.Data as Company;

                    //company.CompanyId = Guid.Parse(txtCompanyId.Text.Trim());
                    company.CompanyName = txtCompanyName.Text.Trim();
                    company.CompanyPhone = txtCompanyPhone.Text.Trim();
                    company.CompanyAddress = txtCompanyAddress.Text.Trim();
                    company.CompanyDescription = txtCompanyDescription.Text.Trim();
                    company.Website = txtCompanyWebsite.Text.Trim();
                    company.FoundationDate = DateTime.Now;
                    company.Email = txtCompanyEmail.Text.Trim();
                    company.IsActive = chkIsActive.IsChecked;
                    company.Notes = txtCompanyNotes.Text.Trim();

                    var result = await _business.Update(company);
                    MessageBox.Show(result.Message, "Update");
                }

                txtCompanyId.Text = string.Empty;
                txtCompanyName.Text = string.Empty;
                txtCompanyPhone.Text = string.Empty;
                txtCompanyAddress.Text = string.Empty;
                txtCompanyDescription.Text = string.Empty;
                txtCompanyWebsite.Text = string.Empty;
                txtCompanyFoundationDate.Text = string.Empty;
                txtCompanyEmail.Text = string.Empty;
                chkIsActive.IsChecked = false;
                txtCompanyNotes.Text = string.Empty;

                this.LoadGrdCompanies();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error");
            }

        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e) { }

        private async void grdCompany_ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;

            string companyId = btn.CommandParameter.ToString();

            //MessageBox.Show(currencyCode);

            if (!string.IsNullOrEmpty(companyId))
            {
                if (MessageBox.Show("Do you want to delete this item?", "Delete", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    var result = await _business.DeleteById(Guid.Parse(companyId));
                    MessageBox.Show($"{result.Message}", "Delete");
                    this.LoadGrdCompanies();
                }
            }
        }

        private async void grdCompany_ButtonEdit_Click(object sender, RoutedEventArgs e) { }

        private async void grdCompany_MouseDouble_Click(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("Double Click on Grid");
            DataGrid grd = sender as DataGrid;
            if (grd != null && grd.SelectedItems != null && grd.SelectedItems.Count == 1)
            {
                var row = grd.ItemContainerGenerator.ContainerFromItem(grd.SelectedItem) as DataGridRow;
                if (row != null)
                {
                    var item = row.Item as Company;
                    if (item != null)
                    {
                        var currencyResult = await _business.GetById(item.CompanyId);

                        if (currencyResult.Status > 0 && currencyResult.Data != null)
                        {
                            item = currencyResult.Data as Company;

                            txtCompanyId.Text = item.CompanyId.ToString();
                            txtCompanyName.Text = item.CompanyName;
                            txtCompanyPhone.Text = item.CompanyPhone;
                            txtCompanyAddress.Text = item.CompanyAddress;
                            txtCompanyDescription.Text = item.CompanyDescription;
                            txtCompanyWebsite.Text = item.Website;
                            txtCompanyFoundationDate.Text = item.FoundationDate?.ToString("yyyy-MM-dd"); ;
                            txtCompanyEmail.Text = item.Email;
                            chkIsActive.IsChecked = item.IsActive;
                            txtCompanyNotes.Text = item.Notes;
                        }
                    }
                }
            }
        }

        private async void LoadGrdCompanies()
        {

            var result = await _business.GetAll();

            if (result.Status > 0 && result.Data != null)
            {
                gridCompany.ItemsSource = result.Data as List<Company>;
            }
            else
            {
                gridCompany.ItemsSource = new List<Company>();
            }

        }

    }
}
