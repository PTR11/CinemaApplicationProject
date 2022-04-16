using CinemaApplicationProject.Desktop.Viewmodel.Base;
using CinemaApplicationProject.Model.DTOs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApplicationProject.Desktop.Viewmodel.Models.ForView
{
    public class ProductSellerViewModel : ViewModelBase
    {
        private String _employeeName;
        private int _count;

        public int Count
        {
            get { return _count; }
            set { _count = value; OnPropertyChanged(); }
        }

        public String EmployeeName
        {
            get { return _employeeName; }
            set { _employeeName = value; OnPropertyChanged(); }
        }

        public static explicit operator ProductSellerViewModel(ProductSeller dto) => new ProductSellerViewModel { 
            EmployeeName = dto.EmployeeName,
            Count = dto.Count
        };

    }

    public class ProductStatViewModel : ViewModelBase
    {
        private String _productName;
        private Double _averageSell;
        private int _allSent;
        public ObservableCollection<ProductSellerViewModel> _sellers;


        public String ProductName {
            get { return _productName; }
            set { _productName = value; OnPropertyChanged(); }
        }

        public Double AverageSell
        {
            get { return _averageSell; }
            set { _averageSell = value; OnPropertyChanged(); }
        }

        public int AllSent
        {
            get { return _allSent; }
            set { _allSent = value; OnPropertyChanged(); }
        }

        public ObservableCollection<ProductSellerViewModel> Sellers
        {
            get { return _sellers; }
            set { _sellers = value; OnPropertyChanged(); }
        }


        public static explicit operator ProductStatViewModel(ProductStatDTO dto) => new ProductStatViewModel
        {
            Id = dto.Id,
            ProductName = dto.ProductName,
            AllSent = dto.AllSent,
            AverageSell = dto.AverageSell,
            Sellers = new(dto.ProductSellerList.ToList().Select(x => (ProductSellerViewModel)x))
        };
    }
}
