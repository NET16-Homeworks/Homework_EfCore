using Homework_EfCore.Database.Configurations;
using Homework_EfCore.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace Homework_EfCore.Database
{
    public class MyDbContext : DbContext
    {
        const string connectionString = @"Server = (localdb)\MSSQLLocalDB; Database = EfCoreUBHW; Trusted_Connection = True";

        public MyDbContext()
        {
            //Database.EnsureDeleted();
            //Database.EnsureCreated();
        }

        public DbSet<Author> Authors {get; set;}
        public DbSet<Book> Books { get; set; }
        public DbSet<User> Users { get; set;}
        public DbSet<UserBook> UserBooks { get; set; }

        //userbooks не добавлять, тк она создастся сама? - создаем т.к создали отд сущность  и связываем букс и юзерс с новосозданной таблицей 

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
