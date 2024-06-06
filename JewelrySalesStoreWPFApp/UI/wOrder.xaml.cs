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

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {

        }
        private async void grdOrder_ButtonDelete_Click(object sender, RoutedEventArgs e) { }
        private async void grdOrder_ButtonEdit_Click(object sender, RoutedEventArgs e) { }

        private async void grdOrder_MouseDouble_Click(object sender, RoutedEventArgs e) { }


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
