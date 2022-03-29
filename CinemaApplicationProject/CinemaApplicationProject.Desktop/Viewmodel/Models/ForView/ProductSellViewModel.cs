using CinemaApplicationProject.Desktop.Viewmodel.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApplicationProject.Desktop.Viewmodel.Models.ForView
{
    public class ProductSellViewModel : ViewModelBase
    {
        private static int _price = 0;
        private ObservableCollection<Field> _field = new ObservableCollection<Field>();
        private ObservableCollection<ProductsCounterViewModel> _productsCounter = new ObservableCollection<ProductsCounterViewModel>();
        private ProductsCounterViewModel _selectedProduct;
        
        private int _rows = 0;

        public ProductSellViewModel()
        {
            IncreaseProductCount = new DelegateCommand(_ => Increase());
            DecreaseProductCount = new DelegateCommand(_ => Decrease());
        }

        public int Rows
        {
            get { return _rows; }
            set { _rows = value; OnPropertyChanged(); }
        }

        public DelegateCommand DecreaseProductCount { get; set; }
        public DelegateCommand IncreaseProductCount { get; set; }
        public ObservableCollection<ProductsCounterViewModel> ProductsCounter
        {
            get { return _productsCounter; }
            set { _productsCounter = value; OnPropertyChanged(); }
        }

        public ProductsCounterViewModel SelectedProduct
        {
            get { return _selectedProduct; }
            set { _selectedProduct = value; OnPropertyChanged(); }
        }

        public int Price
        {
            get { return _price; }
            set { _price = value; OnPropertyChanged(); }
        }


        public ObservableCollection<Field> Field
        {
            get { return _field; }
            set { _field = value; OnPropertyChanged(); }
        }

        public void CalculatePrice(int price)
        {
            this.Price += price;
        }
        public void Increase()
        {
            if(this.SelectedProduct == null)
            {
                ((MainViewModel)this.MainModel).MessageSender("No product selected");
                return;
            }
            this.SelectedProduct.Count++;
            CalculatePrice(this.SelectedProduct.Price);
        }

        public void Decrease()
        {
            if (this.SelectedProduct == null)
            {
                ((MainViewModel)this.MainModel).MessageSender("No product selected");
                return;
            }
            this.SelectedProduct.Count--;
            CalculatePrice(-this.SelectedProduct.Price);
            if(this.SelectedProduct.Count <= 0)
            {
                this.ProductsCounter.Remove(this.SelectedProduct);
            }
        }

        public void PlaceManagement(int number)
        {
            Field act = this.Field[number];
            int x = act.X;
            int y = act.Y;
            var exist = this.ProductsCounter.FirstOrDefault(p => p.Type == act.Text );
            if (exist != null)
            {
                exist.Count = exist.Count + 1;
                
                CalculatePrice(act.Price);
            }
            else
            {
                this.ProductsCounter.Add(new ProductsCounterViewModel { Type = act.Text, Count = 1, Price = act.Price});
                Price = Price;
                CalculatePrice(act.Price);
            }
        }

    }
}
