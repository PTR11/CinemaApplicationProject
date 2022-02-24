using CinemaApplicationProject.Desktop.Viewmodel.Base;
using CinemaApplicationProject.Model.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApplicationProject.Desktop.Viewmodel.Models.ForView
{
    public class CategoryViewModel : ViewModelBase
    {
        private string _category;

        public string Category
        {
            get { return _category; }
            set { _category = value; OnPropertyChanged(); }
        }

        public void CopyFrom(CategoryViewModel rhs)
        {
            Id = rhs.Id;
            Category = rhs.Category;
        }

        public static explicit operator CategoryViewModel(CategoriesDTO dto) => new CategoryViewModel
        {
            Id = dto.Id,
            Category = dto.Category
        };

        public static explicit operator CategoriesDTO(CategoryViewModel dto) => new CategoriesDTO
        {
            Id = dto.Id,
            Category = dto.Category
        };
    }
}
