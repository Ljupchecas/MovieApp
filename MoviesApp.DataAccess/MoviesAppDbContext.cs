using Microsoft.EntityFrameworkCore;
using MoviesApp.Domain.Models;
using MoviesApp.Models;
using MoviesApp.Models.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XSystem.Security.Cryptography;

namespace MoviesApp.DataAccess
{
    public class MoviesAppDbContext : DbContext
    {
        public MoviesAppDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Movie>()
                .HasOne(x => x.User)
                .WithMany(x => x.MoviesList)
                .HasForeignKey(x => x.UserId);

            var md5 = new MD5CryptoServiceProvider();

            var passwordUserOne = "Ljupche123";
            var md5data = md5.ComputeHash(Encoding.ASCII.GetBytes(passwordUserOne));
            var hashedPassword = Encoding.ASCII.GetString(md5data);

            var passwordUserTwo = "Angela123";
            var md5dataTwo = md5.ComputeHash(Encoding.ASCII.GetBytes(passwordUserTwo));
            var hashedPasswordTwo = Encoding.ASCII.GetString(md5dataTwo);

            modelBuilder.Entity<User>()
                .HasData(
                new User()
                {
                    Id = 1,
                    FirstName = "Ljubomir",
                    LastName = "Joldashev",
                    Username = "Ljupche",
                    Password = hashedPassword,
                    FavouriteGenre = GenreEnum.Action
                },
                new User()
                {
                    Id = 2,
                    FirstName = "Angela",
                    LastName = "Gjorgjievska",
                    Username = "Ang",
                    Password = hashedPasswordTwo,
                    FavouriteGenre = GenreEnum.Comedy
                });

            modelBuilder.Entity<Movie>()
                .Property(x => x.Title)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<Movie>()
                .Property(x =>x.Description)
                .HasMaxLength(250);

            modelBuilder.Entity<Movie>()
                .Property(x => x.Year)
                .IsRequired();

            modelBuilder.Entity<Movie>()
                .Property(x => x.Genre)
                .IsRequired();

            modelBuilder.Entity<Movie>()
                .HasData
                (
                    new Movie()
                    {
                        Id = 1,
                        Title = "Breaking Bad",
                        Description = "Money is not everything in life",
                        Genre = GenreEnum.Action,
                        Year = 2008,
                        UserId = 1
                    },
                    new Movie()
                    {
                        Id = 2,
                        Title = "James Bond",
                        Description = "Action Movie",
                        Genre = GenreEnum.Action,
                        Year = 2000,
                        UserId = 2
                    },
                    new Movie()
                    {
                        Id = 3,
                        Title = "Peaky Blinders",
                        Description = "Action Movie",
                        Genre = GenreEnum.Action,
                        Year = 2010,
                        UserId = 1
                    }
                );
        }
    }
}
