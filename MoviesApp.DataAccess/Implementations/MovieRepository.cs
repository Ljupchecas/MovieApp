using MoviesApp.DataAccess.Interfaces;
using MoviesApp.Models;
using MoviesApp.Models.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesApp.DataAccess.Implementations
{
    public class MovieRepository : IMovieRepository
    {
        private readonly MoviesAppDbContext _moviesAppDbContext;
        public MovieRepository(MoviesAppDbContext moviesAppDbContext)
        {
            _moviesAppDbContext = moviesAppDbContext;
        }

        public void Create(Movie entity)
        {
            _moviesAppDbContext.Movies.Add(entity);
            _moviesAppDbContext.SaveChanges();
        }

        public void Delete(Movie entity)
        {
            _moviesAppDbContext.Movies.Remove(entity);
            _moviesAppDbContext.SaveChanges();
        }

        public List<Movie> FilterMovies(int? year, int? genre)
        {
            if(year == null && genre == null)
            {
                return _moviesAppDbContext.Movies.ToList();
            }

            if(year == null)
            {
                List<Movie> moviesDbGenre = _moviesAppDbContext.Movies.Where(x => x.Genre == (GenreEnum)genre).ToList();
                return moviesDbGenre;
            }

            if(genre == null)
            {
                List<Movie> moviesDbYear = _moviesAppDbContext.Movies.Where(x => x.Year == year).ToList();
                return moviesDbYear;
            }

            List<Movie> moviesDb = _moviesAppDbContext.Movies.Where(x=> x.Year == year && x.Genre == (GenreEnum)genre).ToList();
            return moviesDb;
        }

        public List<Movie> GetAll()
        {
            return _moviesAppDbContext
                    .Movies
                    .ToList();
        }

        public Movie GetById(int id)
        {
            return _moviesAppDbContext
                    .Movies
                    .FirstOrDefault(x => x.Id == id);
        }

        public void Update(Movie entity)
        {
            _moviesAppDbContext.Movies.Update(entity);
            _moviesAppDbContext.SaveChanges();
        }
    }
}
