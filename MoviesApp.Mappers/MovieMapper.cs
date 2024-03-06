using MoviesApp.DTOs;
using MoviesApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesApp.Mappers
{
    public static class MovieMapper
    {
        public static MovieDto ToMovieDto(this Movie movie)
        {
            return new MovieDto
            {
                Title = movie.Title,
                Description = movie.Description,
                Year = movie.Year,
                Genre = movie.Genre
            };
        }

        public static Movie ToMovie(this CreateMovieDto createMovieDto)
        {
            return new Movie
            {
                Title = createMovieDto.Title,
                Description = createMovieDto.Description,
                Year = createMovieDto.Year,
                Genre = createMovieDto.Genre
            };
        }
    }
}
