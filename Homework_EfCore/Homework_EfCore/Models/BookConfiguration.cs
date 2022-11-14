using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Homework_EfCore.Models
{
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
