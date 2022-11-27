using Homework_EfCore.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Homework_EfCore.Database.Configurations
{
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.HasKey(q => q.BookId);
            builder.Property(q => q.Name).HasMaxLength(50).IsRequired();
            builder.Property(q => q.Year).IsRequired();
            builder.ToTable("Books");
        }
    }
}
