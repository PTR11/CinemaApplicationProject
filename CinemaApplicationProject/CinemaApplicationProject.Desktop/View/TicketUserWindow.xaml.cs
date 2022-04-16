using CinemaApplicationProject.Desktop.Viewmodel.Models;
using CinemaApplicationProject.Desktop.Viewmodel.Models.ForView;
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
    /// Interaction logic for TicketUserWindow.xaml
    /// </summary>
    public partial class TicketUserWindow : Window
    {
        public TicketUserWindow()
        {
            InitializeComponent();
        }

        private async void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var dataContext = (MainViewModel)this.DataContext;
            await dataContext.LogoutAsync();
            this.Close();
        }

        private void ScrollViewer_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

            var text = (TextBlock)e.OriginalSource;
            var ctx = text.DataContext;
            var mainctx = (MainViewModel)this.DataContext;
            mainctx.SelectedShow.CopyFrom((ShowViewModel)ctx);
            mainctx.InvokeShowDetails();
        }

        private void TicketSell(object sender, MouseButtonEventArgs e)
        {

            TicketSellingWindow tsw = new TicketSellingWindow()
            {
                DataContext = this.DataContext,
            };
            var dataContext = (MainViewModel)this.DataContext;
            var text = (TextBlock)e.OriginalSource;
            var ctx = text.DataContext;
            dataContext.SelectedTicketShow.CopyFrom((ShowViewModel)ctx);
            dataContext.CreateMoviesFieldAsync();
            tsw.ShowDialog();

        }
    }
}
