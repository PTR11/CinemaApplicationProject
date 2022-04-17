using CinemaApplicationProject.Desktop.Viewmodel.Models;
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

namespace CinemaApplicationProject.Desktop.View
{
    /// <summary>
    /// Interaction logic for ProductSuperVisiorWindow.xaml
    /// </summary>
    public partial class ProductSuperVisiorWindow : Window
    {
        public ProductSuperVisiorWindow()
        {
            InitializeComponent();
        }

        private async void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var dataContext = (MainViewModel)this.DataContext;
            await dataContext.LogoutAsync();
            this.Close();
        }

        private void ProductSell(object sender, RoutedEventArgs e)
        {

            ProductSellingWindow tsw = new ProductSellingWindow()
            {
                DataContext = this.DataContext,
            };
            var dataContext = (MainViewModel)this.DataContext;

            dataContext.CreateProductsFieldAsync();
            tsw.ShowDialog();

        }
    }
}
