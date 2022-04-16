using CinemaApplicationProject.Desktop.Viewmodel.Base;
using CinemaApplicationProject.Model.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApplicationProject.Desktop.Viewmodel.Models.ForView
{
    public class ProductViewModel : ViewModelBase
    {

        private byte[] _image;
        private String _imageForeground;

        private String _name;

        private int _price;

        private int _quantity;



        public ProductViewModel()
        {
            this.Name = "";
            this.Price = 0;
            this.Quantity = 0;
            DeleteProduct = new DelegateCommand(async _ => await Delete());
        }

        public String ImageForeground
        {
            get { return _image != null ? "Transparent" : "Red"; }
            set { _imageForeground = value; OnPropertyChanged(); }
        }

        public Byte[] Image
        {
            get { return _image; }
            set { _image = value; OnPropertyChanged(); }
        }

        public async Task Delete()
        {
            var main = (MainViewModel)this.MainModel;
            if (main._addProduct)
            {
                main.Warehouse.Remove(this);
            }
            else
            {
                await main.DeleteEntity("api/BuffetWarehouses", main.SelectedProduct.Id, main.LoadInit);
                main.ProductVisibility(false);
            }
        }

        public DelegateCommand DeleteProduct { get; set; }

        public String Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged(); }
        }

        public int Price
        {
            get { return _price; }
            set { _price = value; OnPropertyChanged(); }
        }

        public int Quantity
        {
            get { return _quantity; }
            set { _quantity = value; OnPropertyChanged(); }
        }

        public void CopyFrom(ProductViewModel rhs)
        {
            Id = rhs.Id;
            Name = rhs.Name;
            Price = rhs.Price;
            Quantity = rhs.Quantity;
            Image = rhs.Image;
        }

        public static explicit operator ProductViewModel(ProductDTO dto) => new ProductViewModel
        {
            Id = dto.Id,
            Name = dto.Name,
            Price = dto.Price,
            Quantity = dto.Quantity,
            Image = dto.Image,
        };

        public static explicit operator ProductDTO(ProductViewModel dto) => new ProductDTO
        {
            Id = dto.Id,
            Name = dto.Name,
            Price = dto.Price,
            Quantity = dto.Quantity,
            Image= dto.Image,
        };
    }
}
