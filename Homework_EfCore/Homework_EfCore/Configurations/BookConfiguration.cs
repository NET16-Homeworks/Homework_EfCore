using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Homework_EfCore.Entities;

namespace Homework_EfCore.Configurations
{
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.HasKey(book => book.BookId);
            builder.Property(book => book.Name).HasMaxLength(100).IsRequired();
            builder.Property(book => book.Year).IsRequired();
            builder.HasOne(book => book.Author)
                .WithMany(author => author.Books)
                .HasForeignKey(book => book.AuthorId);
        }
    }
}
