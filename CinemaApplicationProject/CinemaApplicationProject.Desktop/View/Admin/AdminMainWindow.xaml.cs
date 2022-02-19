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
            //var rowGroup = tblDummyData.RowGroups.FirstOrDefault();

            //if (rowGroup != null)
            //{
            //    TableRow row = new TableRow();

            //    TableCell cell = new TableCell();

            //    MainViewModel asd = (MainViewModel)this.DataContext;
                
            //    cell.Blocks.Add(new Paragraph(new Run(asd.name)));
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
    }
}
