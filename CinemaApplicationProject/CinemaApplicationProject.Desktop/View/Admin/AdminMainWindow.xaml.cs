using CinemaApplicationProject.Desktop.Viewmodel.Models;
using CinemaApplicationProject.Desktop.Viewmodel.Models.ForView;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            Debug.WriteLine("faszom");
        }

        public void Add()
        {
            //var rowGroup = tblDummyData.RowGroups.FirstOrDefault();

            //if (rowGroup != null)
            //{
            //    TableRow row = new TableRow();

            //    TableCell cell = new TableCell();

            //    MainViewModel asd = (MainViewModel)this.DataContext;

            //    //cell.Blocks.Add(new Paragraph(new Run(asd._list[0].MovieTitle)));
            //    row.Cells.Add(cell);

            //    cell = new TableCell();
            //    cell.Blocks.Add(new Paragraph(new Run("New cell 2")));
            //    row.Cells.Add(cell);

            //    cell = new TableCell();
            //    cell.Blocks.Add(new Paragraph(new Run("New cell 3")));
            //    row.Cells.Add(cell);

            //    rowGroup.Rows.Add(row);
            //}
        }

        private void ItemsControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Debug.WriteLine("faszomat");
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("faszomat22");
        }

        private void listBox_GotFocus(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("faszomat22");
        }

        private void ScrollViewer_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

            var asd = (TextBlock)e.OriginalSource;
            var ctx = asd.DataContext;
            var fasz = (MainViewModel)this.DataContext;
            fasz.SelectedShow.CopyFrom((ShowViewModel)ctx);
            Debug.WriteLine(e.OriginalSource);
        }
    }
}
