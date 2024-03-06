using MoviesApp.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesApp.Services.Interfaces
{
    public interface IMovieService
    {
        List<MovieDto> GetAllMovies(int userId);

        MovieDto GetMovieById(int id);

        void CreateMovie(CreateMovieDto createMovieDto, int userId);

        void UpdateMovie(UpdateMovieDto updateMovieDto);

        void DeleteMovie(int id, int userId);

        List<MovieDto> Filter(int? year, int? genre);
    }
}
