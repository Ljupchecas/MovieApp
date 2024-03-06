using Microsoft.EntityFrameworkCore;
using MoviesApp.DataAccess.Interfaces;
using MoviesApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesApp.DataAccess.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly MoviesAppDbContext _context;
        public UserRepository(MoviesAppDbContext context)
        {
            _context = context;
        }
        public void Create(User entity)
        {
            _context.Users.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(User entity)
        {
            _context.Users.Remove(entity);
            _context.SaveChanges();
        }

        public List<User> GetAll()
        {
            return _context.Users.Include(x => x.MoviesList).ToList();
        }

        public User GetById(int id)
        {
            return _context.Users.FirstOrDefault(x => x.Id == id);
        }

        public User GetUserByUserName(string username)
        {
            return _context.Users.FirstOrDefault(x => x.Username == username);
        }

        public User LoginUser(string username, string password)
        {
            return _context.Users.FirstOrDefault(
                x => x.Username.ToLower() == username.ToLower() && 
                x.Password == password);

        }

        public void Update(User entity)
        {
            _context.Users.Update(entity);
            _context.SaveChanges();
        }
    }
}
