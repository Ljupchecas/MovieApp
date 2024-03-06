using MoviesApp.Domain.Models;
using MoviesApp.Models.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesApp.DTOs
{
    public class UpdateMovieDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Year { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public GenreEnum Genre { get; set; }
    }
}
