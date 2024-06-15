using JewelrySalesStoreBusiness;
using JewelrySalesStoreData.Models;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
            txtProductID.Text = string.Empty;
            txtProductName.Text = string.Empty;
            txtProductColor.Text = string.Empty;
            txtProductWeight.Text = string.Empty;
            imageProduct.Source = null;
            txtProductPrice.Text = string.Empty;
            txtPromotionID.Text = string.Empty;
            txtCategoryID.Text = string.Empty;
            chkIsActive.IsChecked = false;
            txtProductNotes.Text = string.Empty;
        }

        private void ButtonUpload_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.Filter = "Image files|*.bmp;*.jpg;*.png";
            openDialog.FilterIndex = 1;
            if(openDialog.ShowDialog() == true)
            {
                imageProduct.Source = new BitmapImage(new Uri(openDialog.FileName));
            }
        }

        private async void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //int idTmp = -1;
                //int.TryParse(txtproductCode.Text, out idTmp);

                byte[] imageData = null;

                // Nếu người dùng đã chọn hình ảnh
                if (imageProduct.Source != null && imageProduct.Source is BitmapImage)
                {
                    BitmapImage bitmapImage = (BitmapImage)imageProduct.Source;
                    JpegBitmapEncoder encoder = new JpegBitmapEncoder();
                    encoder.Frames.Add(BitmapFrame.Create(bitmapImage));
                    using (MemoryStream stream = new MemoryStream())
                    {
                        encoder.Save(stream);
                        imageData = stream.ToArray();
                    }
                }

                if (txtProductID.Text.Equals(""))
                {
                    var product = new Product()
                    {
                        ProductId = Guid.NewGuid(),
                        Color = txtProductColor.Text,
                        Name = txtProductName.Text,
                        Weight = Double.Parse(txtProductWeight.Text),
                        Image = imageData,
                        Price = Double.Parse(txtProductPrice.Text),
                        PromotionId = Guid.Parse(txtPromotionID.Text),
                        CategoryId = Guid.Parse(txtCategoryID.Text),
                        IsActive = chkIsActive.IsChecked,
                        Notes = txtProductNotes.Text,
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
                    product.Image = imageData;
                    product.Price = Double.Parse(txtProductPrice.Text);
                    product.PromotionId = Guid.Parse(txtPromotionID.Text);
                    product.CategoryId = Guid.Parse(txtCategoryID.Text);
                    product.IsActive = chkIsActive.IsChecked;
                    product.Notes = txtProductNotes.Text;

                    var result = await _business.Update(product);
                    MessageBox.Show(result.Message, "Update");
                }

                txtProductID.Text = string.Empty;
                txtProductName.Text = string.Empty;
                txtProductColor.Text = string.Empty;
                txtProductWeight.Text = string.Empty;
                imageProduct.Source = null;
                txtProductPrice.Text = string.Empty;
                txtPromotionID.Text = string.Empty;
                txtCategoryID.Text = string.Empty;
                chkIsActive.IsChecked = false;
                txtProductNotes.Text = string.Empty;

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
                            if (item.Image != null && item.Image.Length > 0)
                            {
                                using (MemoryStream memoryStream = new MemoryStream(item.Image))
                                {
                                    BitmapImage bitmapImage = new BitmapImage();
                                    bitmapImage.BeginInit();
                                    bitmapImage.StreamSource = memoryStream;
                                    bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                                    bitmapImage.EndInit();
                                    imageProduct.Source = bitmapImage;
                                }
                            }
                            else
                            {
                                // Xóa hình ảnh nếu không có dữ liệu hình ảnh
                                imageProduct.Source = null;
                            }
                            txtProductPrice.Text = item.Price.ToString();
                            txtPromotionID.Text = item.PromotionId.ToString();
                            txtCategoryID.Text = item.CategoryId.ToString();
                            chkIsActive.IsChecked = item.IsActive;
                            txtProductNotes.Text = item.Notes;
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
