using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Db
{
    public class ChessContext:DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public ChessContext()
        {
            //Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=ChessDb;Trusted_Connection=True;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().Property(u => u.Name).HasMaxLength(100);
            modelBuilder.Entity<User>().Property(u => u.Email).HasMaxLength(300);
            modelBuilder.Entity<User>().Property(u => u.Password).HasMaxLength(200);
            modelBuilder.Entity<User>().Property(u => u.RegistrationTime);

            modelBuilder.Entity<Rating>().Property(r => r.Points).HasDefaultValue(20);
            modelBuilder.Entity<Rating>().Property(r => r.MaxPoints).HasDefaultValue(20);
            modelBuilder.Entity<Rating>().Property(r => r.MinPoints).HasDefaultValue(20);
            modelBuilder.Entity<Rating>().Property(r => r.Rang).IsRequired(false);
            modelBuilder.Entity<Rating>().Property(r => r.WinCount).HasDefaultValue(0);
            modelBuilder.Entity<Rating>().Property(r => r.MaxWinCount).HasDefaultValue(0);

            modelBuilder.Entity<User>().HasOne(u => u.Rating)
                                       .WithOne(r => r.User)
                                       .HasForeignKey<Rating>(r => r.UserId);
        }

        public User? AddUser(string name, string email, string password)
        {
            try
            {
                if (Users.Where(u => u.Name == name).ToList().Count() <= 0)
                {
                    User newUser = new User
                    {
                        Name = name,
                        Email = email,
                        Password = password,
                        RegistrationTime = DateTime.Now
                    };
                    Rating rating = new Rating();
                    newUser.Rating = rating;
                    rating.User = newUser;

                    Users.Add(newUser);
                    Ratings.Add(rating);

                    SaveChanges();

                    return newUser;
                }
                else return null;
            }
            catch
            {
                return null;
            }
        }

        public User? AddRating(User user)
        {
            var rating = Ratings.Include((r) => r.User).Where((u) => u.User.Equals(user)).FirstOrDefault();

            rating.WinCount += 1;
            rating.Points += 1;
            if (rating.WinCount % 10 == 0)
            {
                rating.Points += (rating.WinCount / 10) * 90 - ((rating.WinCount / 10) - 1) * 90;
            }
            if(rating.WinCount>rating.MaxWinCount)
            {
                rating.MaxWinCount = rating.WinCount;
            }
            if (rating.Points > rating.MaxPoints)
            {
                rating.MaxPoints = rating.Points;
            }
            SaveChanges();

            return Users.Include(u => u.Rating).Where(u => u.Equals(user)).FirstOrDefault();
        }

        public User? RemoveRating(User user)
        {
            var rating = Ratings.Include((r) => r.User).Where((u) => u.User.Equals(user)).FirstOrDefault();
            rating.Points -= 1;
            rating.WinCount = 0;
            if(rating.Points<rating.MinPoints)
            {
                rating.MinPoints = rating.Points;
            }

            SaveChanges();

            return Users.Include(u => u.Rating).Where(u => u.Equals(user)).FirstOrDefault();
        }

        public User? LogIn(string name,string password)
        {
            return Users.Where(u => u.Name.Equals(name) && u.Password.Equals(password)).Include(u=>u.Rating).FirstOrDefault();
        }
    }
}
