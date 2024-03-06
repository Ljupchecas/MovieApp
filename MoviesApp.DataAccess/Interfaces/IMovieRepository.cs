using MoviesApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesApp.DataAccess.Interfaces
{
    public interface IMovieRepository : IRepository<Movie>
    {
        List<Movie> FilterMovies(int? year, int? genre);
    }
}
