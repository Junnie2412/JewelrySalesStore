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
    /// Interaction logic for wCategory.xaml
    /// </summary>
    public partial class wCategory : Window
    {
        private readonly CategoryBusiness _business;
        public wCategory()
        {
            InitializeComponent();
            _business = new CategoryBusiness();
            LoadGrdCategories();
        }

        private async void LoadGrdCategories()
        {
            var result = await _business.GetAll();

            if (result.Status > 0 && result.Data != null)
            {
                grdCategory.ItemsSource = result.Data as List<Category>;
            }
            else
            {
                grdCategory.ItemsSource = new List<Category>();
            }
        }

        private void Open_wCategory_Click(object sender, RoutedEventArgs e)
        {
            var p = new wCategory();
            p.Owner = this;
            p.Show();
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e) {
            txtCategoryCode.Text = string.Empty;
            txtCategoryCode.Text = string.Empty;
            txtCategoryMaterial.Text = string.Empty;
            txtCategoryName.Text = string.Empty;
            txtCategoryProductLine.Text = string.Empty;
            txtCategoryCollection.Text = string.Empty;
            txtCategoryFengShui.Text = string.Empty;
            txtCategoryPurpose.Text = string.Empty;
            chkGender.IsChecked = false;
            chkIsActive.IsChecked = false;
            txtCategoryNotes.Text = string.Empty;
        }

        private async void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //int idTmp = -1;
                //int.TryParse(txtcategoryCode.Text, out idTmp);

                if (txtCategoryCode.Text.Equals(""))
                {
                    var category = new Category()
                    {
                        CategoryId = Guid.NewGuid(),
                        Name = txtCategoryName.Text,
                        Material = txtCategoryMaterial.Text,
                        ProductLine = txtCategoryProductLine.Text,
                        Collection = txtCategoryCollection.Text,
                        FengShui = txtCategoryFengShui.Text,
                        Purpose = txtCategoryPurpose.Text,
                        Gender = chkGender.IsChecked,
                        IsActive = chkIsActive.IsChecked,
                        Notes = txtCategoryNotes.Text
                    };

                    var result = await _business.Save(category);
                    MessageBox.Show(result.Message, "Save");
                }
                else
                {
                    var item = await _business.GetById(Guid.Parse(txtCategoryCode.Text));
                    var category = item.Data as Category;
                    //category.categoryCode = txtcategoryCode.Text;
                    category.Name = txtCategoryName.Text;
                    category.Material = txtCategoryMaterial.Text;
                    category.ProductLine = txtCategoryProductLine.Text;
                    category.Collection = txtCategoryCollection.Text;
                    category.Purpose = txtCategoryPurpose.Text;
                    category.FengShui = txtCategoryFengShui.Text;
                    category.Gender = chkGender.IsChecked;
                    category.IsActive = chkIsActive.IsChecked;
                    category.Notes = txtCategoryNotes.Text;

                    var result = await _business.Update(category);
                    MessageBox.Show(result.Message, "Update");
                }

                txtCategoryCode.Text = string.Empty;
                txtCategoryName.Text = string.Empty;
                txtCategoryMaterial.Text = string.Empty;
                txtCategoryProductLine.Text = string.Empty;
                txtCategoryCollection.Text = string.Empty;
                txtCategoryFengShui.Text = string.Empty;
                txtCategoryPurpose.Text = string.Empty;
                chkGender.IsChecked = false;
                chkIsActive.IsChecked = false;
                txtCategoryNotes.Text = string.Empty;

                this.LoadGrdCategories();

              
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error");
            }

        }

        private async void grdCategory_MouseDouble_Click(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("Double Click on Grid");
            DataGrid grd = sender as DataGrid;
            if (grd != null && grd.SelectedItems != null && grd.SelectedItems.Count == 1)
            {
                var row = grd.ItemContainerGenerator.ContainerFromItem(grd.SelectedItem) as DataGridRow;
                if (row != null)
                {
                    var item = row.Item as Category;
                    if (item != null)
                    {
                        var currencyResult = await _business.GetById(item.CategoryId);

                        if (currencyResult.Status > 0 && currencyResult.Data != null)
                        {
                            item = currencyResult.Data as Category;
                            txtCategoryCode.Text = item.CategoryId.ToString();
                            txtCategoryName.Text = item.Name;
                            txtCategoryMaterial.Text = item.Material;
                            txtCategoryProductLine.Text = item.ProductLine;
                            txtCategoryCollection.Text = item.Collection;
                            txtCategoryFengShui.Text = item.FengShui;
                            txtCategoryPurpose.Text = item.Purpose;
                            chkGender.IsChecked = item.Gender;
                            chkIsActive.IsChecked = item.IsActive;
                            txtCategoryNotes.Text = item.Notes;
                        }
                    }
                }
            }
        }
        private async void grdCategory_ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;

            string categoryCode = btn.CommandParameter.ToString();

            //MessageBox.Show(currencyCode);

            if (!string.IsNullOrEmpty(categoryCode))
            {
                if (MessageBox.Show("Do you want to delete this item?", "Delete", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    var result = await _business.DeleteById(Guid.Parse(categoryCode));
                    MessageBox.Show($"{result.Message}", "Delete");
                    this.LoadGrdCategories();
                }
            }
        }
    }
}
