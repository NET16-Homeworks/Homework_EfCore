using Homework_EfCore.Configurations;
using Homework_EfCore.Models;
using Microsoft.EntityFrameworkCore;
namespace Homework_EfCore
{
    public class MyDBContext : DbContext
    {
        public MyDBContext()
        {

        }
        public DbSet<Books> Books { get; set; }
        public DbSet<Authors> Authors { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<UserBooks> UserBooks { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            if (optionsBuilder.IsConfigured == false)
            {
                const string connectionString = "Server=localhost;Database=EfHomework;Trusted_Connection=True;Encrypt=False;";
                optionsBuilder.UseSqlServer(connectionString);
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new AuthorsConfigurations());
            modelBuilder.ApplyConfiguration(new BooksConfigurations());
            modelBuilder.ApplyConfiguration(new UsersConfigurations());
            modelBuilder.Entity<UserBooks>().HasKey(q => q.UserBooksId);
        }
    }
}
