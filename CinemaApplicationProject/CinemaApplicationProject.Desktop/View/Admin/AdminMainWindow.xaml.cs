using CinemaApplicationProject.Desktop.Viewmodel.Models;
using CinemaApplicationProject.Desktop.Viewmodel.Models.ForView;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CinemaApplicationProject.Desktop.View.Admin
{
    /// <summary>
    /// Interaction logic for AdminMainWindow.xaml
    /// </summary>
    public partial class AdminMainWindow : Window
    {
        public AdminMainWindow()
        {
            InitializeComponent();
        }


        private void ScrollViewer_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

            var text = (TextBlock)e.OriginalSource;
            var ctx = text.DataContext;
            var fasz = (MainViewModel)this.DataContext;
            fasz.SelectedShow.CopyFrom((ShowViewModel)ctx);
            Debug.WriteLine(e.OriginalSource);
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
            dataContext.CreateFieldAsync();
            tsw.ShowDialog();

        }

        private void IsNumber(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }

        private static readonly Regex _regex = new Regex("[^0-9.-]+"); //regex that matches disallowed text
        private static bool IsTextAllowed(string text)
        {
            return !_regex.IsMatch(text);
        }
    }
}
