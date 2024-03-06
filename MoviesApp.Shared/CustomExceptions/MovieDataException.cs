using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesApp.Shared.CustomExceptions
{
    public class MovieDataException : Exception
    {
        public MovieDataException(string message) : base(message) { }

    }
}
