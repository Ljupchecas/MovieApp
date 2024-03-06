using MoviesApp.Models.Enum;
using MoviesApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesApp.DTOs.UserDto
{
    public class UserDto
    {
        public UserDto()
        {
            MoviesList = new List<MovieDto>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => $"{FirstName} {LastName}";
        public string Username { get; set; }
        public string Token { get; set; }
        public GenreEnum FavouriteGenre { get; set; }
        public List<MovieDto> MoviesList { get; set; }
    }
}
