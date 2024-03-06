using Microsoft.EntityFrameworkCore.Migrations;
using MoviesApp.DataAccess.Interfaces;
using MoviesApp.DTOs;
using MoviesApp.Mappers;
using MoviesApp.Models;
using MoviesApp.Models.Enum;
using MoviesApp.Services.Interfaces;
using MoviesApp.Shared.CustomExceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesApp.Services.Implementations
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;
        public MovieService(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        public void CreateMovie(CreateMovieDto createMovieDto, int userId)
        {
            if(string.IsNullOrEmpty(createMovieDto.Title))
            {
                throw new MovieDataException("The movie must have title!");
            }

            if(!string.IsNullOrEmpty(createMovieDto.Description) && createMovieDto.Description.Length > 250)
            {
                throw new MovieDataException("The description must be less than 250 characters!");
            }

            if(createMovieDto.Year == null)
            {
                throw new MovieDataException("The movie must have a year!");
            }

            if(createMovieDto.Genre == null)
            {
                throw new MovieDataException("The movie must have a genre!");
            }

            Movie newMovie = createMovieDto.ToMovie();
            newMovie.UserId = userId;
            _movieRepository.Create(newMovie);
        }

        public void DeleteMovie(int id, int userId)
        {
            var movieDb = _movieRepository.GetById(id);

            if(movieDb == null)
            {
                throw new MovieNotFoundException("Movie not found");
            }

            if(movieDb.UserId != userId)
            {
                throw new MovieNotFoundException($"Movie with id {id} not found");
            }

            _movieRepository.Delete(movieDb);
        }

        public List<MovieDto> Filter(int? year, int? genre)
        {
            if (genre.HasValue)
            {
                var enumValues = Enum.GetValues(typeof(GenreEnum))
                                .Cast<GenreEnum>()
                                .ToList();

                if (!enumValues.Contains((GenreEnum)genre.Value))
                {
                    throw new Exception("Invalid genre");
                }
            }

            return _movieRepository.FilterMovies(year, genre).Select(x => x.ToMovieDto()).ToList();
        }

        public List<MovieDto> GetAllMovies(int userId)
        {
            return _movieRepository.GetAll().Where(x => x.UserId == userId).Select(x => x.ToMovieDto()).ToList();
        }

        public MovieDto GetMovieById(int id)
        {
            var movieDb = _movieRepository.GetById(id);

            if (movieDb == null)
            {
                throw new MovieNotFoundException($"Movie was not found");
            }

            return movieDb.ToMovieDto();
        }

        public void UpdateMovie(UpdateMovieDto updateMovieDto)
        {
            Movie movieDb = _movieRepository.GetById(updateMovieDto.Id);

            if (movieDb == null)
            {
                throw new MovieNotFoundException("Movie was not found!");
            }

            if (string.IsNullOrEmpty(updateMovieDto.Title))
            {
                throw new MovieDataException("The movie must have title!");
            }

            if (!string.IsNullOrEmpty(updateMovieDto.Description) && updateMovieDto.Description.Length > 250)
            {
                throw new MovieDataException("The description must be less than 250 characters!");
            }

            if (updateMovieDto.Year == null)
            {
                throw new MovieDataException("The movie must have a year!");
            }

            if (updateMovieDto.Genre == null)
            {
                throw new MovieDataException("The movie must have a genre!");
            }

            movieDb.Title = updateMovieDto.Title;
            movieDb.Description = updateMovieDto.Description;
            movieDb.Year = updateMovieDto.Year;
            movieDb.Genre = updateMovieDto.Genre;
            movieDb.UserId = updateMovieDto.UserId;

            _movieRepository.Update(movieDb);
        }
    }
}
