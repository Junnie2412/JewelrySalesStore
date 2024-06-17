using System.Windows;
using System.Collections.ObjectModel;
using JewelrySalesStoreData.Models;
using System.Windows.Input;
using JewelrySalesStoreBusines;
using System.Windows.Controls; // Replace with the actual namespace of your Promotion class

namespace JewelrySalesStoreWPFApp
{
    public partial class wPromotion : Window
    {
        private readonly PromotionBusiness _business;

        public wPromotion()
        {
            InitializeComponent();
            _business = new PromotionBusiness();
            LoadGrdPromotions();
        }
        private async void LoadGrdPromotions()
        {
            var result = await _business.GetAll();

            if (result.Status > 0 && result.Data != null)
            {
                grdPromotion.ItemsSource = result.Data as List<Promotion>;
            }
            else
            {
                grdPromotion.ItemsSource = new List<Promotion>();
            }
        }

        private void Open_wPromotion_Click(object sender, RoutedEventArgs e)
        {
            var p = new wPromotion();
            p.Owner = this;
            p.Show();
        }
        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            txtPromotionId.Text = string.Empty;
            txtPromotionName.Text = string.Empty;
            txtPromotionCode.Text = string.Empty;
            txtDiscountPercentage.Text = string.Empty;
            txtPromotionStartDate.Text = string.Empty;
            txtPromotionEndDate.Text = string.Empty;
            txtPromotionCondition.Text = string.Empty;
            txtPromotionDescription.Text = string.Empty;
            txtIsActive.IsChecked = false;
            txtPromotionNotes.Text = string.Empty;
        }

        private async void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtPromotionId.Text == string.Empty)
                {
                    var promotion = new Promotion
                    {
                        PromotionId = new Guid(),
                        PromotionName = txtPromotionName.Text,
                        PromotionCode = txtPromotionCode.Text,
                        DiscountPercentage = double.Parse(txtDiscountPercentage.Text),
                        StartDate = Convert.ToDateTime(txtPromotionStartDate.Text),
                        EndDate = Convert.ToDateTime(txtPromotionEndDate.Text),
                        Description = txtPromotionDescription.Text,
                        Condition = txtPromotionCondition.Text,
                        IsActive = Convert.ToBoolean(txtIsActive.IsChecked),
                        Notes = txtPromotionNotes.Text
                    };

                    var result = _business.Save(promotion);

                    if (result.Status > 0)
                    {
                        LoadGrdPromotions();
                        MessageBox.Show("Promotion added successfully");
                    }
                    else
                    {
                        MessageBox.Show("Error adding promotion");
                    }
                }
                else
                {
                    var item = await _business.GetById(Guid.Parse(txtPromotionId.Text));
                    var promotion = item.Data as Promotion;

                    promotion.StartDate = Convert.ToDateTime(txtPromotionStartDate.Text);
                    promotion.EndDate = Convert.ToDateTime(txtPromotionEndDate.Text);
                    promotion.Description = txtPromotionDescription.Text;
                    promotion.Condition = txtPromotionCondition.Text;
                    promotion.IsActive = Convert.ToBoolean(txtIsActive.IsChecked);
                    promotion.Notes = txtPromotionNotes.Text;
                    promotion.DiscountPercentage = double.Parse(txtDiscountPercentage.Text);
                    promotion.PromotionName = txtPromotionName.Text;
                    promotion.PromotionCode = txtPromotionCode.Text;


                    var result = await _business.Update(promotion);

                    if (result.Status > 0)
                    {
                        LoadGrdPromotions();
                        MessageBox.Show("Promotion updated successfully");
                    }
                    else
                    {
                        MessageBox.Show("Error updating promotion");
                    }





                }
                txtPromotionId.Text = string.Empty;
                txtPromotionName.Text = string.Empty;
                txtPromotionCode.Text = string.Empty;
                txtDiscountPercentage.Text = string.Empty;
                txtPromotionStartDate.Text = string.Empty;
                txtPromotionEndDate.Text = string.Empty;
                txtPromotionCondition.Text = string.Empty;
                txtPromotionDescription.Text = string.Empty;
                txtIsActive.IsChecked = false;
                txtPromotionNotes.Text = string.Empty;
                this.LoadGrdPromotions();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


            
        }

        private async void grdPromotion_MouseDouble_Click(object sender, RoutedEventArgs e)
        {
            DataGrid grd = sender as DataGrid;
            if (grd != null && grd.SelectedItems != null && grd.SelectedItems.Count == 1)
            {
                var row = grd.ItemContainerGenerator.ContainerFromItem(grd.SelectedItem) as DataGridRow;
                if (row != null)
                {
                    var item = row.Item as Promotion;
                    if (item != null)
                    {
                        var Result = await _business.GetById(item.PromotionId);

                        if (Result.Status > 0 && Result.Data != null)
                        {
                            item = Result.Data as Promotion;
                            txtPromotionId.Text = item.PromotionId.ToString();
                            txtPromotionName.Text = item.PromotionName.ToString();
                            txtPromotionCode.Text = item.PromotionCode.ToString();
                            txtPromotionStartDate.Text = item.StartDate?.ToString();
                            txtPromotionEndDate.Text = item.EndDate?.ToString();
                            txtPromotionDescription.Text = item.Description.ToString();
                            txtPromotionCondition.Text = item.Condition.ToString();
                            txtDiscountPercentage.Text = item.DiscountPercentage.ToString();
                            txtIsActive.IsChecked = item.IsActive;
                            txtPromotionNotes.Text = item.Notes.ToString();
                        }
                    }
                }
            }
        }


        private async void grdPromotion_ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;

            string PromotionId = btn.CommandParameter.ToString();

            //MessageBox.Show(currencyCode);

            if (!string.IsNullOrEmpty(PromotionId))
            {
                if (MessageBox.Show("Do you want to delete this item?", "Delete", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    var result = await _business.DeleteById(Guid.Parse(PromotionId));
                    MessageBox.Show($"{result.Message}", "Delete");
                    this.LoadGrdPromotions();
                }
            }
        }


    }
}