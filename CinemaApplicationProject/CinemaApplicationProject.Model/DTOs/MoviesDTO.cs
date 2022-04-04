using CinemaApplicationProject.Model.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApplicationProject.Model.DTOs
{
    public class MoviesDTO : RespondDTO
    {

        public String Title { get; set; }

        public int Length { get; set; }

        public String Description { get; set; }

        public String Director { get; set; }

        public String Trailer { get; set; }

        public byte[] Image { get; set; }

        public int TicketsCount { get; set; }

        public double Average { get; set; }

        public List<CategoriesDTO> Categories { get; set; }

        public List<ShowsDTO> Shows { get; set; }

        public List<ActorsDTO> Actors { get; set; }

        public List<OpinionsDTO> Opinions { get; set; }

        private static List<ShowsDTO> ConvertShowsToDTO(ICollection<Shows> m) => new(m.ToList().Select(x => new ShowsDTO
        {
            Id = x.Id,
            Date = x.Date,
            RoomId = x.RoomId,
            MovieId = x.MovieId,
        }));

        private static List<ActorsDTO> ConvertActorsToDTO(ICollection<Actors> m) => new(m.ToList().Select(x => (ActorsDTO)x));

        private static List<CategoriesDTO> ConvertCategoriesToDTO(ICollection<Categories> m) => new(m.ToList().Select(x => (CategoriesDTO)x));

        private static List<OpinionsDTO> ConvertOpinionsToDTO(ICollection<Opinions> m) => new(m.ToList().Select(x => (OpinionsDTO)x));
        
        private static List<Actors> ConvertActorsDTOToClass(ICollection<ActorsDTO> m) => new(m.ToList().Select(x => (Actors)x));

        private static List<Categories> ConvertCategoriesDTOToClass(ICollection<CategoriesDTO> m) => new(m.ToList().Select(x => (Categories)x));

        public static explicit operator MoviesDTO(Movies m) => new MoviesDTO {
            Id = m.Id,
            Title = m.Title,
            Length = m.Length,
            Description = m.Description,
            Image = m.Image,
            Director = m.Director,
            Shows = m.Shows != null ? ConvertShowsToDTO(m.Shows) : null, 
            Actors = m.Actors != null ? ConvertActorsToDTO(m.Actors) : null,
            Categories = m.Categories != null ? ConvertCategoriesToDTO(m.Categories) : null,
            Opinions = m.Opinions != null ? ConvertOpinionsToDTO(m.Opinions) : null
        };

        public static explicit operator Movies(MoviesDTO m) => new Movies
        {
            Id = m.Id,
            Title = m.Title,
            Length = m.Length,
            Image = m.Image,
            Director = m.Director,
            Description = m.Description,
            Actors = ConvertActorsDTOToClass(m.Actors),
            Categories = ConvertCategoriesDTOToClass(m.Categories),
        };

    }
}
