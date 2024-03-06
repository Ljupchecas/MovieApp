using MoviesApp.Models.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesApp.DTOs
{
    public class CreateMovieDto
    {
        public string Title { get; set; }

        public string? Description { get; set; }

        public int Year { get; set; }

        public GenreEnum Genre { get; set; }
    }
}
