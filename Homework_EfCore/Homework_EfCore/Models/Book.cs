using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;

namespace Homework_EfCore.Models
{
    public class Book
    {
        public int BookId { get; set; }
        public string Name { get; set; }

        public int AuthorId { get; set; }
        public Author Author { get; set; }

        public int Year { get; set; }

        public ICollection<User> Users { get; set; }
        public ICollection<UserBooks> UserBooks { get; set; }

    }

    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.HasKey(x => x.BookId);
            builder.Property(q => q.BookId).UseIdentityColumn();
            builder.Property(q => q.Name).HasMaxLength(50).IsRequired();
            builder.HasIndex(q => q.Name).IsUnique();

            builder.Property(q => q.AuthorId).IsRequired();
            builder.HasOne(q => q.Author).WithMany(q => q.Books).HasForeignKey(q => q.AuthorId);

            builder.Property(q => q.Year).IsRequired();
        }
    }
}
