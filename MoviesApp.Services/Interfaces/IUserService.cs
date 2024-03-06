using MoviesApp.DTOs.UserDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesApp.Services.Interfaces
{
    public interface IUserService
    {
        void RegisterUser(RegisterDto registerDto);
        string Login(LoginDto loginDto);
        UserDto Authenticate(LoginDto loginDto);
    }
}
