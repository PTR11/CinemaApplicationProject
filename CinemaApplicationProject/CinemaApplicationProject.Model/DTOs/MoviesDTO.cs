using CinemaApplicationProject.Model.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApplicationProject.Model.DTOs
{
    public class MoviesDTO
    {
        public int Id { get; set; }

        public String Title { get; set; }

        public int Length { get; set; }

        public String Description { get; set; }

        public String Trailer { get; set; }

        public List<CategoriesDTO> Categories { get; set; }

        public List<ShowsDTO> Shows { get; set; }

        public List<ActorsDTO> Actors { get; set; }

        private static List<ShowsDTO> ConvertShowsToDTO(ICollection<Shows> m) => new(m.ToList().Select(x => (ShowsDTO)x));

        private static List<ActorsDTO> ConvertActorsToDTO(ICollection<Actors> m) => new(m.ToList().Select(x => (ActorsDTO)x));

        private static List<CategoriesDTO> ConvertCategoriesToDTO(ICollection<Categories> m) => new(m.ToList().Select(x => (CategoriesDTO)x));

        public static explicit operator MoviesDTO(Movies m) => new MoviesDTO {
            Id = m.Id,
            Title = m.Title,
            Length = m.Length,
            Description = m.Description,
            Shows = ConvertShowsToDTO(m.Shows),
            Actors = ConvertActorsToDTO(m.Actors),
            Categories = ConvertCategoriesToDTO(m.Categories)
        };
      
    }
}
