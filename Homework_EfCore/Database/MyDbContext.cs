using Homework_EfCore.Database.Configurations;
using Homework_EfCore.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace Homework_EfCore.Database
{
    public class MyDbContext : DbContext
    {
        const string connectionString = @"Server = DESKTOP-AIDE8C6\MSSQLSERVER01; Database = EfCoreHomeTask; Trusted_Connection = True";

        public MyDbContext()
        {
           
        }
        public DbSet<Author> Authors {get; set;}
        public DbSet<Book> Books { get; set; }
        public DbSet<User> Users { get; set;}
        public DbSet<UserBook> UserBooks { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            if (optionsBuilder.IsConfigured == false)
            {
                optionsBuilder.UseSqlServer(connectionString);
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new AuthorConfiguration());
            modelBuilder.ApplyConfiguration(new BookConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new UserBookConfiguration());
        }
    }
}
