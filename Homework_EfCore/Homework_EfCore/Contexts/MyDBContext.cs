using Microsoft.EntityFrameworkCore;
using Homework_EfCore.Configurations;
using Homework_EfCore.Entities;

namespace Homework_EfCore.Contexts
{
    public class MyDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<UserBook> UserBooks { get; set; }

        public MyDbContext(){}
        public MyDbContext(DbContextOptions contextOptions) : base(contextOptions){}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = "Server=localhost;Database=Homework_EfCore;Trusted_Connection=True;Encrypt=False;";

            if (optionsBuilder.IsConfigured == false)
            {
                optionsBuilder.UseSqlServer(connectionString);
            }

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new BookConfiguration());
            modelBuilder.ApplyConfiguration(new AuthorConfiguration());
            modelBuilder.ApplyConfiguration(new UserBookConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
