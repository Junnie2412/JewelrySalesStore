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
    /// Interaction logic for wProduct.xaml
    /// </summary>
    public partial class wProduct : Window
    {
        private readonly ProductBusiness _business;
        public wProduct()
        {
            InitializeComponent();
            _business = new ProductBusiness();
            LoadGrdProducts();
        }

        private async void LoadGrdProducts()
        {
            var result = await _business.GetAll();

            if (result.Status > 0 && result.Data != null)
            {
                grdProduct.ItemsSource = result.Data as List<Product>;
            }
            else
            {
                grdProduct.ItemsSource = new List<Product>();
            }
        }

        private void Open_wProduct_Click(object sender, RoutedEventArgs e)
        {
            var p = new wProduct();
            p.Owner = this;
            p.Show();
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        private async void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //int idTmp = -1;
                //int.TryParse(txtproductCode.Text, out idTmp);

                if (txtProductID.Text.Equals(""))
                {
                    var product = new Product()
                    {
                        ProductId = Guid.NewGuid(),
                        Color = txtProductColor.Text,
                        Name = txtProductName.Text,
                        Weight = Double.Parse(txtProductWeight.Text),
                        Image = null,
                        Price = Double.Parse(txtProductPrice.Text),
                        PromotionId = Guid.Parse(txtPromotionID.Text),
                        CategoryId = Guid.Parse(txtCategoryID.Text),
                        Status = chkIsActive.IsChecked
                    };

                    var result = await _business.Save(product);
                    MessageBox.Show(result.Message, "Save");
                }
                else
                {
                    var item = await _business.GetById(Guid.Parse(txtProductID.Text));
                    var product = item.Data as Product;
                    //product.productCode = txtproductCode.Text;
                    product.Name = txtProductName.Text;
                    product.Color = txtProductColor.Text;
                    product.Weight = Double.Parse(txtProductWeight.Text);
                    product.Image = null;
                    product.Price = Double.Parse(txtProductPrice.Text);
                    product.PromotionId = Guid.Parse(txtPromotionID.Text);
                    product.CategoryId = Guid.Parse(txtCategoryID.Text);
                    product.Status = chkIsActive.IsChecked;

                    var result = await _business.Update(product);
                    MessageBox.Show(result.Message, "Update");
                }

                txtProductID.Text = string.Empty;
                txtProductName.Text = string.Empty;
                txtProductColor.Text = string.Empty;
                txtProductWeight.Text = string.Empty;
                txtProductImage.Text = string.Empty;
                txtProductPrice.Text = string.Empty;
                txtPromotionID.Text = string.Empty;
                txtCategoryID.Text = string.Empty;
                chkIsActive.IsChecked = false;

                this.LoadGrdProducts();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error");
            }

        }

        private async void grdProduct_MouseDouble_Click(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("Double Click on Grid");
            DataGrid grd = sender as DataGrid;
            if (grd != null && grd.SelectedItems != null && grd.SelectedItems.Count == 1)
            {
                var row = grd.ItemContainerGenerator.ContainerFromItem(grd.SelectedItem) as DataGridRow;
                if (row != null)
                {
                    var item = row.Item as Product;
                    if (item != null)
                    {
                        var currencyResult = await _business.GetById(item.ProductId);

                        if (currencyResult.Status > 0 && currencyResult.Data != null)
                        {
                            item = currencyResult.Data as Product;
                            txtProductID.Text = item.ProductId.ToString();
                            txtProductName.Text = item.Name;
                            txtProductColor.Text = item.Color;
                            txtProductWeight.Text = item.Weight.ToString();
                            txtProductImage.Text = string.Empty;
                            txtProductPrice.Text = item.Price.ToString();
                            txtPromotionID.Text = item.ProductId.ToString();
                            txtCategoryID.Text = item.CategoryId.ToString();
                            chkIsActive.IsChecked = item.Status;
                        }
                    }
                }
            }
        }
        private async void grdProduct_ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;

            string productCode = btn.CommandParameter.ToString();

            //MessageBox.Show(currencyCode);

            if (!string.IsNullOrEmpty(productCode))
            {
                if (MessageBox.Show("Do you want to delete this item?", "Delete", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    var result = await _business.DeleteById(Guid.Parse(productCode));
                    MessageBox.Show($"{result.Message}", "Delete");
                    this.LoadGrdProducts();
                }
            }
        }
    }
}
